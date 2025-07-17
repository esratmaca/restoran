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
using rezervasyonAPI.Models;

namespace restaurant
{
    public partial class adminpanel : Form
    {
        private int _selectedProductId = -1; 
        private int _selectedCategoryId = -1;
        private const string BaseApiUrl = "https://localhost:44363/";
        public adminpanel()
        {
            InitializeComponent();
            dgvUrunler.CellClick += dgvUrunler_CellClick;
            LoadProductsToDataGridView();
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

            if (string.IsNullOrWhiteSpace(txturunAdi.Text) || string.IsNullOrWhiteSpace(txturunFiyati.Text))
            {
                MessageBox.Show("Lütfen ürün adı ve fiyatını doldurun.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txturunFiyati.Text, out decimal urunFiyat))
            {
                MessageBox.Show("Ürün fiyatı geçerli bir sayı olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(txtkategoriID.Text, out int kategoriId)) 
            {
                MessageBox.Show("Kategori ID geçerli bir tam sayı olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            var urunData = new
            {
                UrunAdi = txturunAdi.Text,
                UrunFiyat = urunFiyat,
                KategoriID = kategoriId
            };

            var client = new RestClient(BaseApiUrl);
            var request = new RestRequest("api/resarvation/posturun", Method.Post); 
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + TokenStorage.JwtToken);
            request.AddJsonBody(urunData);

            try
            {
                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    MessageBox.Show("Ürün başarıyla eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputFields(); // Girdi alanlarını temizle
                    LoadProductsToDataGridView(); // DataGridView'i yenile
                }
                else // Diğer hata durumları (400, 401, 403, 500 vb.)
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

            if (_selectedProductId == -1) // Hiçbir ürün seçilmemişse
            {
                MessageBox.Show("Lütfen silinecek ürünü listeden seçin.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dialogResult = MessageBox.Show($"'{txturunAdi.Text}' adlı ürünü silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.No)
            {
                return;
            }

            var client = new RestClient(BaseApiUrl);
            var request = new RestRequest($"api/resarvation/deleteurun/{_selectedProductId}", Method.Delete);
            request.AddHeader("Authorization", "Bearer " + TokenStorage.JwtToken);

            try
            {
                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    MessageBox.Show($"Ürün (ID: {_selectedProductId}) başarıyla silindi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputFields(); 
                    _selectedProductId = -1; 
                    _selectedCategoryId = -1; 
                    LoadProductsToDataGridView(); 
                }
                else // Diğer hata durumları
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

            if (_selectedProductId == -1) // Hiçbir ürün seçilmemişse
            {
                MessageBox.Show("Lütfen güncellenecek ürünü listeden seçin.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txturunAdi.Text) || string.IsNullOrWhiteSpace(txturunFiyati.Text))
            {
                MessageBox.Show("Lütfen ürün adı ve fiyatını doldurun.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txturunFiyati.Text, out decimal urunFiyat))
            {
                MessageBox.Show("Ürün fiyatı geçerli bir sayı olmalıdır.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
            // API'nizin PUT endpoint'inin beklentisine göre bu nesneyi ayarlayın.
            var urunData = new
            {
                UrunID = _selectedProductId, 
                UrunAdi = txturunAdi.Text,
                UrunFiyat = urunFiyat,
                KategoriID = _selectedCategoryId // KategoriID'yi de gönderiyoruz, eğer API bekliyorsa
            };

            var client = new RestClient(BaseApiUrl);
            var request = new RestRequest($"api/resarvation/puturun/{_selectedProductId}", Method.Put);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer " + TokenStorage.JwtToken);
            request.AddJsonBody(urunData);

            try
            {
                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    MessageBox.Show("Ürün başarıyla güncellendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearInputFields(); 
                    _selectedProductId = -1; 
                    _selectedCategoryId = -1; 
                    LoadProductsToDataGridView(); 
                }
                else // Diğer hata durumları
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

        private async void LoadProductsToDataGridView()
        {
            try
            {
                var client = new RestClient(BaseApiUrl);
                var request = new RestRequest("api/resarvation/geturunler", Method.Get);

                // JWT token'ı Authorization başlığına ekle (eğer API'de yetkilendirme varsa)
                if (!string.IsNullOrEmpty(TokenStorage.JwtToken))
                {
                    request.AddHeader("Authorization", "Bearer " + TokenStorage.JwtToken);
                }

                var response = await client.ExecuteAsync(request);

               
                if (response.IsSuccessful)
                {
                    var urunList = JsonConvert.DeserializeObject<List<UrunlerDto>>(response.Content);
                    dgvUrunler.DataSource = urunList;

                    // DataGridView sütun başlıklarını ayarlayın
                    if (dgvUrunler.Columns.Contains("UrunID")) dgvUrunler.Columns["UrunID"].HeaderText = "Ürün ID";
                    if (dgvUrunler.Columns.Contains("UrunAdi")) dgvUrunler.Columns["UrunAdi"].HeaderText = "Ürün Adı";
                    if (dgvUrunler.Columns.Contains("UrunFiyat")) dgvUrunler.Columns["UrunFiyat"].HeaderText = "Ürün Fiyatı";
                    if (dgvUrunler.Columns.Contains("KategoriID")) dgvUrunler.Columns["KategoriID"].HeaderText = "Kategori ID";
                    if (dgvUrunler.Columns.Contains("KategoriAdi")) dgvUrunler.Columns["KategoriAdi"].HeaderText = "Kategori Adı";
                }
                else
                {
                    MessageBox.Show($"Ürünler yüklenirken bir hata oluştu: {response.StatusCode} - {response.Content}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"API bağlantı hatası: {ex.Message}", "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ClearInputFields()
        {
            txturunAdi.Clear();
            txturunFiyati.Clear();
           txtkategoriID.Clear();
        }

        private void dgvUrunler_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //başlıkların seçilmememesi için
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvUrunler.Rows[e.RowIndex];

                txturunAdi.Text = selectedRow.Cells["UrunAdi"].Value?.ToString();
                txturunFiyati.Text = selectedRow.Cells["UrunFiyat"].Value?.ToString();

                // ID'leri global değişkenlere sakla, metin kutularına yazma
                _selectedProductId = selectedRow.Cells["UrunID"].Value != null ? Convert.ToInt32(selectedRow.Cells["UrunID"].Value) : -1;
                _selectedCategoryId = selectedRow.Cells["KategoriID"].Value != null ? Convert.ToInt32(selectedRow.Cells["KategoriID"].Value) : -1;
            }
        }

       
    }
}

