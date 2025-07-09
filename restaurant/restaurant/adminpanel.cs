using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace restaurant
{
    public partial class adminpanel : Form
    {
        private const string BaseApiUrl = "https://localhost:44363/";
        public adminpanel()
        {
            InitializeComponent();
        }

        bool altButonlar = false;
        private void btnMenuyonetimi_Click(object sender, EventArgs e)
        {
            //altButonlar değişkeninin değerini her butona tıklandığında tersine çeviricek
            altButonlar = !altButonlar;

            btnEkle.Visible = altButonlar;
            btnSil.Visible = altButonlar;
            btnFiyatguncelle.Visible = altButonlar;

        }

        private void adminpanel_Load(object sender, EventArgs e)
        {

        }

        private void btnCıkıs_Click(object sender, EventArgs e)
        {
        //    TokenStorage.Clear(); 
            Form1 loginForm = new Form1(); 
            loginForm.Show();
            this.Close(); // Admin formunu kapat
            Application.Exit();
        }
        //await kelimesini kullanbilmek için async ekliyoruz butonun başına
        //ürün ekleme butonu
        private async void btnEkle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TokenStorage.JwtToken) || TokenStorage.Rol != "admin")
            {
                MessageBox.Show("Bu işlemi yapmak için yönetici olarak giriş yapmalısınız.", "Yetkisiz Erişim", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            if (string.IsNullOrWhiteSpace(txturunAdi.Text) || string.IsNullOrWhiteSpace(txturunFiyati.Text) || string.IsNullOrWhiteSpace(txtkategoriID.Text))
            {
                MessageBox.Show("Lütfen tüm ürün bilgilerini doldurun (Ad, Fiyat, Kategori ID).", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txturunFiyati.Text, out decimal urunFiyat))
            {
                MessageBox.Show("Ürün fiyatı geçerli bir sayı olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txtkategoriID.Text, out int kategoriID))
            {
                MessageBox.Show("Kategori ID geçerli bir sayı olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //API'ye göndericilicek  ürün bilgilerini içeren anonim bir nesne
            var urunData = new
            {
                UrunAdi = txturunAdi.Text,
                UrunFiyat = urunFiyat,
                KategoriID = kategoriID
            };
            //urunData adlı nesneyi JSON formatında bir string'e dönüştürür(serileştirmek)
            string jsonContent = JsonConvert.SerializeObject(urunData);

            var client = new RestClient(BaseApiUrl);
            var request = new RestRequest("api/resarvation/posturun", Method.Post);
            //HTTP isteğinin başlıklarına bir Content-Type başlığı ekler.
            request.AddHeader("Content-Type", "application/json");
            //Authorization:HTTP standartlarında kimlik doğrulama veya yetkilendirme bilgilerini göndermek için kullanılan standart bir başlıktır.
            request.AddHeader("Authorization", "Bearer " + TokenStorage.JwtToken); // JWT token'ı Authorization başlığına ekle
            //urunData nesnesini HTTP isteğinin gövdesine JSON formatında ekler.
            request.AddJsonBody(urunData); 

            try
            {
                //await:asenkron bir işlemin tamamlanmasını beklemek için kullanılır.
                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful) // HTTP 201 
                {
                    MessageBox.Show("Ürün başarıyla eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   
                    txturunAdi.Clear();
                    txturunFiyati.Clear();
                    txtkategoriID.Clear();
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized) // HTTP 401
                {
                    MessageBox.Show("Giriş oturumunuz sona ermiş veya yetkiniz yok. Lütfen tekrar giriş yapın.", "Yetkisiz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Giriş formuna yönlendirme yapabilirsiniz
                    // LoginForm loginForm = new LoginForm(); loginForm.Show(); this.Close();
                }
                else if (response.StatusCode == HttpStatusCode.Forbidden) // HTTP 403
                {
                    MessageBox.Show("Bu işlemi yapmaya yetkiniz yok. Sadece adminler ürün ekleyebilir.", "Yasak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else // Diğer hatalar (400 Bad Request, 500 Internal Server Error vb.)
                {
                    string errorContent = response.Content;
                    MessageBox.Show($"Ürün eklenirken bir hata oluştu: {response.StatusCode} - {errorContent}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)    
            {
                MessageBox.Show($"API bağlantı hatası: {ex.Message}", "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        //ürün silme
        private async void btnSil_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TokenStorage.JwtToken) || TokenStorage.Rol != "admin")
            {
                MessageBox.Show("Bu işlemi yapmak için yönetici olarak giriş yapmalısınız.", "Yetkisiz Erişim", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txturunID.Text))
            {
                MessageBox.Show("Lütfen silinecek ürünün ID'sini girin.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txturunID.Text, out int urunID))
            {
                MessageBox.Show("Ürün ID geçerli bir sayı olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var client = new RestClient(BaseApiUrl);
            
            var request = new RestRequest($"api/resarvation/deleteurun/{urunID}", Method.Delete);
            request.AddHeader("Authorization", "Bearer " + TokenStorage.JwtToken); // JWT token'ı ekle

            try
            {
                var response = await client.ExecuteAsync(request);
                MessageBox.Show($"Status: {response.StatusCode}\nContent: {response.Content}");


                if (response.IsSuccessful)
                {
                    MessageBox.Show($"Ürün (ID: {urunID}) başarıyla silindi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txturunID.Clear();
                }
                else if (response.StatusCode == HttpStatusCode.NotFound) 
                {
                    MessageBox.Show($"Belirtilen ID ({urunID}) ile ürün bulunamadı.", "Ürün Bulunamadı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized) 
                {
                    MessageBox.Show("Giriş oturumunuz sona ermiş veya yetkiniz yok. Lütfen tekrar giriş yapın.", "Yetkisiz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (response.StatusCode == HttpStatusCode.Forbidden) 
                {
                    MessageBox.Show("Bu işlemi yapmaya yetkiniz yok. Sadece adminler ürün silebilir.", "Yasak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string errorContent = response.Content;
                    MessageBox.Show($"Ürün silinirken bir hata oluştu: {response.StatusCode} - {errorContent}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"API bağlantı hatası: {ex.Message}", "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnFiyatguncelle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TokenStorage.JwtToken) || TokenStorage.Rol != "admin")
            {
                MessageBox.Show("Bu işlemi yapmak için yönetici olarak giriş yapmalısınız.", "Yetkisiz Erişim", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txturunID.Text) || string.IsNullOrWhiteSpace(txturunAdi.Text) || string.IsNullOrWhiteSpace(txturunFiyati.Text) || string.IsNullOrWhiteSpace(txtkategoriID.Text))
            {
                MessageBox.Show("Lütfen tüm ürün bilgilerini girin:", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //sonra tekraardan bak buraya
            if(!int.TryParse(txturunID.Text, out int urunID))
            {
                MessageBox.Show("Ürün ID geçerli bir sayı olmalıdır!", "Hata", MessageBoxButtons.OK);
                return;
            }

            var urunData = new
            {
                urunID = txturunID.Text,
                urunAdi = txturunAdi.Text,
                urunFİyat = txturunFiyati.Text,
                urunKategori = txtkategoriID.Text,

            };

            string jsonContent = JsonConvert.SerializeObject(urunData);
            var client = new RestClient(BaseApiUrl);
            var request = new RestRequest($"api/resarvation/puturun/{urunID}", Method.Put);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", jsonContent);
            request.AddHeader("Authorization" , "Bearer" + TokenStorage.JwtToken);

            try
            {
                var response = await client.ExecuteAsync(request);
                //messagebox kısmını kontrol et
                if (response.IsSuccessful)
                {
                    MessageBox.Show("Ürün başarıyla güncellendi");
                    txturunID.Clear();
                    txturunAdi.Clear();
                    txturunFiyati.Clear();
                    txtkategoriID.Clear();
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    MessageBox.Show("Giriş oturumunuz sona ermiş veya yetkiniz yok. Lütfen tekrar giriş yapın.", "Yetkisiz", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    MessageBox.Show("Bu işlemi yapmaya yetkiniz yok. Sadece adminler fiyat güncelleyebilir.", "Yasak", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string errorContent = response.Content;
                    MessageBox.Show($"Fiyat güncellenirken bir hata oluştu: {response.StatusCode} - {errorContent}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"API bağlantı hatası: {ex.Message}", "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}

