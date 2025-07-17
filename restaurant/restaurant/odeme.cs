using Newtonsoft.Json;
using RestSharp;
using rezervasyonAPI.Models;
using rezervasyonAPI.Services.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace restaurant
{
    public partial class odeme : Form
    {
        private readonly RestClient _restClient = new RestClient("https://localhost:44363/");
        private int _secilenMasaID = -1; 
        private decimal _toplamTutar = 0;
        string BaseApiUrl = "https://localhost:44363/";

        public odeme()
        {
            InitializeComponent();
            this.Load += odeme_Load;


            lblMasano.Text = "Masa No:"; 
           // lblToplamtutar.Text = "Toplam Tutar: 0.00 TL";

            
             cmbMasanumaraları.SelectedIndexChanged += cmbMasanumaraları_SelectedIndexChanged; 
        }

        private async void odeme_Load(object sender, EventArgs e)
        {
            await LoadDoluMasalar();
        }

       
        private async Task LoadDoluMasalar()
        {
            var client = new RestClient(BaseApiUrl);
            var request = new RestRequest("api/account/getmasadurumlari", Method.Get);

            if (string.IsNullOrEmpty(TokenStorage.JwtToken))
            {
                MessageBox.Show("Oturum süresi dolmuş veya token yok. Lütfen tekrar giriş yapın.", "Yetkilendirme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }
            request.AddHeader("Authorization", $"Bearer {TokenStorage.JwtToken}");

            try
            {
                var response = await client.ExecuteAsync<List<MasaDurum>>(request);

                if (response.IsSuccessful && response.Data != null)
                {
                   
                    var doluMasalar = response.Data
                                              .Where(m => m.Durum.ToLowerInvariant() == "dolu" || m.Durum.ToLowerInvariant() == "bekliyor")
                                              .OrderBy(m => m.MasaID)
                                              .ToList();

                    // ComboBox'ı temizle
                    cmbMasanumaraları.Items.Clear();

                    if (doluMasalar.Any())
                    {
                        foreach (var masa in doluMasalar)
                        {
                            cmbMasanumaraları.Items.Add($"Masa {masa.MasaID}"); 
                        }
                        cmbMasanumaraları.SelectedIndex = 0; 
                    }
                    else
                    {
                        MessageBox.Show("Şu anda siparişi olan veya ödeme bekleyen masa bulunmamaktadır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                    }
                }
                else
                {
                    MessageBox.Show($"Masa durumları yüklenirken API'den hata alındı.\nHTTP Durum Kodu: {response.StatusCode}\nHata Mesajı: {response.ErrorMessage}\nAPI Yanıt İçeriği: {response.Content}", "API Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Dolu masaları çekerken bağlantı hatası: {ex.Message}", "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       

        private  async void button1_Click(object sender, EventArgs e)
        {
            if (_secilenMasaID == -1)
            {
                MessageBox.Show("Lütfen ödeme yapılacak bir masa seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show($"Masa {_secilenMasaID}'nin toplam {_toplamTutar:C} tutarındaki ödemesini tamamlamak istediğinizden emin misiniz?", "Ödeme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                var client = new RestClient(BaseApiUrl);
                
                var request = new RestRequest($"api/resarvation/odemetamamla/{_secilenMasaID}", Method.Post);
                request.AddHeader("Authorization", $"Bearer {TokenStorage.JwtToken}");

                try
                {
                    var response = await client.ExecuteAsync(request);

                    if (response.IsSuccessful)
                    {
                        MessageBox.Show($"Masa {_secilenMasaID} için ödeme başarıyla tamamlandı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        await LoadDoluMasalar();
                        lstSiparisler.Items.Clear();
                        lblToplamtutar.Text = " 0.00 TL";
                        lblMasano.Text = "Seçilen Masa:";
                        _secilenMasaID = -1; 
                    }
                    else
                    {
                        MessageBox.Show($"Ödeme tamamlanırken API'den hata alındı.\nHTTP Durum Kodu: {response.StatusCode}\nHata Mesajı: {response.ErrorMessage}\nAPI Yanıt İçeriği: {response.Content}", "API Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ödemeyi tamamlarken bağlantı hatası: {ex.Message}", "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
       

       

        private async  void cmbMasanumaraları_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMasanumaraları.SelectedItem == null) return;

            string selectedText = cmbMasanumaraları.SelectedItem.ToString();
            _secilenMasaID = int.Parse(selectedText.Replace("Masa ", "")); // "Masa " kısmını kaldırıp sadece ID'yi al

            lblMasano.Text = $" {_secilenMasaID}";
            await LoadMasaSiparisleri(_secilenMasaID);
        }

       
        private async Task LoadMasaSiparisleri(int masaID)
        {
            var client = new RestClient(BaseApiUrl);
           
            var request = new RestRequest($"api/resarvation/getsiparislerbymasa/{masaID}", Method.Get);

            request.AddHeader("Authorization", $"Bearer {TokenStorage.JwtToken}");

            try
            {
                var response = await client.ExecuteAsync<ServiceResult<IEnumerable<SiparisKalemi>>>(request);

                if (response.IsSuccessful && response.Data != null && response.Data.IsSuccess)
                {
                    lstSiparisler.Items.Clear();
                    _toplamTutar = 0;

                    if (response.Data.Data != null && response.Data.Data.Any())
                    {
                        foreach (var siparisKalemi in response.Data.Data)
                        {

                            string urunAdi = siparisKalemi.UrunAdi ?? "Bilinmeyen Ürün"; 
                            decimal birimFiyat = siparisKalemi.Fiyat; 
                            decimal araToplam = siparisKalemi.Adet * birimFiyat;


                            lstSiparisler.Items.Add($"{urunAdi} (Adet: {siparisKalemi.Adet}) - {araToplam:C}"); 
                            _toplamTutar += araToplam;
                        }
                        lblToplamtutar.Text = $"Toplam Tutar: {_toplamTutar:C}";
                    }
                    else
                    {
                        lstSiparisler.Items.Add("Bu masaya ait bekleyen sipariş bulunmamaktadır.");
                        lblToplamtutar.Text = "Toplam Tutar: 0.00 TL";
                    }
                }
                else
                {
                    MessageBox.Show($"Sipariş detayları yüklenirken API'den hata alındı.\nHTTP Durum Kodu: {response.StatusCode}\nHata Mesajı: {response.ErrorMessage}\nAPI Yanıt İçeriği: {response.Content}", "API Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Masa siparişlerini çekerken bağlantı hatası: {ex.Message}", "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGeriDon_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
