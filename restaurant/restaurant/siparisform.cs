using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;
using Newtonsoft.Json;
using restaurant.Models;

namespace restaurant
{
    public partial class siparisform : Form
    {
        private int _masaNo;
        
        private List<SiparisKalemi> _currentTableOrders = new List<SiparisKalemi>();

        string BaseApiUrl = "https://localhost:44363/"; 

        
        public siparisform(int masaNo)
        {
            InitializeComponent();
            _masaNo = masaNo;
            lblMasanumarası.Text = $" {_masaNo}"; 

            LoadUrunlerToComboBox();
            LoadExistingOrdersForTable(_masaNo);

            
             this.btngeri.Click += new System.EventHandler(this.btngeri_Click); 
        }

        private async void LoadUrunlerToComboBox()
        {
            var client = new RestClient(BaseApiUrl);
            var request = new RestRequest("api/resarvation/geturunler", Method.Get);

            // JWT Token'ı Authorization header'ına ekle
            if (!string.IsNullOrEmpty(TokenStorage.JwtToken))
            {
                request.AddHeader("Authorization", $"Bearer {TokenStorage.JwtToken}");
            }
            else
            {
                MessageBox.Show("Oturum süresi dolmuş veya token yok. Lütfen tekrar giriş yapın.", "Yetkilendirme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close(); 
                return;
            }

            try
            {
                var response = await client.ExecuteAsync<List<Urunler>>(request);

                if (response.IsSuccessful && response.Data != null)
                {
                  
                    cmbMenu.DisplayMember = "UrunAdi";
                    cmbMenu.ValueMember = "UrunID";
                    cmbMenu.DataSource = response.Data;
                }
                else
                {
                    MessageBox.Show($"Ürünler yüklenirken hata oluştu: {response.StatusCode} - {response.ErrorMessage ?? response.Content}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ürünleri çekerken bir hata oluştu: {ex.Message}", "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


       

        private void UpdateListBox()
        {
            
            lstSiparişler.Items.Clear();
            decimal totalOrderPrice = 0;
            foreach (var item in _currentTableOrders)
            {
                lstSiparişler.Items.Add($"{item.UrunAdi} x {item.Adet} = {item.ToplamFiyat:C2}");
                totalOrderPrice += item.ToplamFiyat;
            }
            lstSiparişler.Items.Add("--------------------");
            lstSiparişler.Items.Add($"Toplam Tutar: {totalOrderPrice:C2}");
        }

        // API'ye sipariş detayını gönderme
        private async void SendOrderToApi(int urunId, int adet, decimal urunFiyat, string urunAdi)
        {
            var client = new RestClient(BaseApiUrl);
            var request = new RestRequest("api/resarvation/postsiparisdetay", Method.Post);

            if (!string.IsNullOrEmpty(TokenStorage.JwtToken))
            {
                request.AddHeader("Authorization", $"Bearer {TokenStorage.JwtToken}");
            }
            else
            {
                MessageBox.Show("Oturum süresi dolmuş veya token yok. Lütfen tekrar giriş yapın.", "Yetkilendirme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            request.AddJsonBody(new
            {
                MasaNo = _masaNo,
                UrunID = urunId,
                Adet = adet,
                UrunAdi = urunAdi, 
                Fiyat = urunFiyat
            });

            try
            {
                var response = await client.ExecuteAsync(request);

                if (response.IsSuccessful)
                {
                    MessageBox.Show("Sipariş başarılı API' ye gönderildi");
                    // Sipariş sonrası masa durumunu "dolu" yap
                    var durumRequest = new RestRequest("api/account/updatemasadurumu", Method.Put);
                    durumRequest.AddHeader("Authorization", $"Bearer {TokenStorage.JwtToken}");
                    durumRequest.AddJsonBody(new
                    {
                        MasaID = _masaNo,
                        Durum = "dolu"
                    });


                    var durumResponse = await client.ExecuteAsync(durumRequest);

                    if (!durumResponse.IsSuccessful)
                    {
                        MessageBox.Show("Masa durumu güncellenemedi!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }

                else
                {
                    MessageBox.Show($"Sipariş eklenirken hata oluştu: {response.StatusCode} - {response.ErrorMessage ?? response.Content}", "Sipariş Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                LoadExistingOrdersForTable(_masaNo);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Sipariş gönderilirken bir hata oluştu: {ex.Message}", "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void LoadExistingOrdersForTable(int masaNo)
        {
            var client = new RestClient(BaseApiUrl);
            
            
            var request = new RestRequest($"api/resarvation/getsiparislerbymasa/{masaNo}", Method.Get);

            if (!string.IsNullOrEmpty(TokenStorage.JwtToken))
            {
                request.AddHeader("Authorization", $"Bearer {TokenStorage.JwtToken}");
            }
            else
            {
                MessageBox.Show("Oturum süresi dolmuş veya token yok. Lütfen tekrar giriş yapın.", "Yetkilendirme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            try
            {
                
                var response = await client.ExecuteAsync<List<SiparisKalemi>>(request);

                if (response.IsSuccessful && response.Data != null)
                {
                    _currentTableOrders.Clear();
                    _currentTableOrders.AddRange(response.Data); 
                    UpdateListBox(); 
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound || response.Data == null || response.Data.Count == 0)
                {
                    // Masanın henüz siparişi yoksa boş liste gelebilir
                    _currentTableOrders.Clear();
                    UpdateListBox(); 
                }
                else
                {
                    MessageBox.Show($"Mevcut siparişler yüklenirken hata oluştu: {response.StatusCode} - {response.ErrorMessage ?? response.Content}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Mevcut siparişleri çekerken bir hata oluştu: {ex.Message}", "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void siparisform_Load(object sender, EventArgs e)
        {

        }
        private void btnekle_Click(object sender, EventArgs e)
        {
            if (cmbMenu.SelectedItem == null)
            {
                MessageBox.Show("Lütfen bir ürün seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nudAdet.Value <= 0)
            {
                MessageBox.Show("Ürün adedi 0'dan büyük olmalıdır.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Urunler selectedUrun = cmbMenu.SelectedItem as Urunler;
            int adet = (int)nudAdet.Value;

            if (selectedUrun != null)
            {
                // API'ye sipariş kalemini gönder - urunAdi'yi de gönderiyoruz
                SendOrderToApi(selectedUrun.UrunID, adet, selectedUrun.UrunFiyat ?? 0m, selectedUrun.UrunAdi);

                SiparisKalemi existingItem = _currentTableOrders.FirstOrDefault(item => item.UrunID == selectedUrun.UrunID);

                if (existingItem != null)
                {
                    existingItem.Adet += adet;
                }
                else
                {
                    SiparisKalemi newItem = new SiparisKalemi
                    {
                        UrunID = selectedUrun.UrunID,
                        UrunAdi = selectedUrun.UrunAdi,
                        Adet = adet,
                        Fiyat = selectedUrun.UrunFiyat ?? 0m,
                        MasaNo = _masaNo
                    };
                    _currentTableOrders.Add(newItem);
                }

                UpdateListBox();
            }
        }

        private void btngeri_Click(object sender, EventArgs e)
        {
            masalar masalarForm = new masalar();
            masalarForm.Show(); 
            this.Close();
        }

        private void cmbMenu_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnOdemeyegec_Click(object sender, EventArgs e)
        {
            odeme odemeForm = new odeme(); 
            odemeForm.Show();
            this.Hide();
        }

        private void lblMasanumarası_Click(object sender, EventArgs e)
        {

        }
    }
}
