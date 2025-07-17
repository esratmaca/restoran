using RestSharp;
using rezervasyonAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace restaurant
{
    public partial class masalar : Form
    {
        private string BaseApiUrl = "https://localhost:44363/";
        public masalar()
        {
            InitializeComponent();
            this.Load += masalar_Load;
        }

        

        private async void masalar_Load(object sender, EventArgs e)
        {
            int masaSayisi = 12;
            int butonGenislik = 100;
            int butonYukseklik = 80;
            int x = 10, y = 10;

            for (int i = 1; i <= masaSayisi; i++)
            {
                Button btn = new Button();
                btn.Text = $"Masa {i}";
                btn.Width = butonGenislik;
                btn.Height = butonYukseklik;
                btn.Tag = i;
                btn.BackColor = Color.Blue; 
                btn.Left = x;
                btn.Top = y;
                btn.Click += MasaButon_Click;

                panelmasalar.Controls.Add(btn);

                x += butonGenislik + 10;

                if (i % 4 == 0)
                {
                    x = 10;
                    y += butonYukseklik + 10;
                }
            }
            await LoadTableStatuses();
        }

        private async Task LoadTableStatuses()
        {
            var client = new RestClient(BaseApiUrl);
            var request = new RestRequest("api/account/getmasadurumlari", Method.Get);

            if (string.IsNullOrEmpty(TokenStorage.JwtToken))
            {
                MessageBox.Show("Oturum süresi dolmuş veya token yok. Lütfen tekrar giriş yapın.", "Yetkilendirme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Form1 loginForm = new Form1();
                loginForm.Show();
                this.Close();
                return;
            }
            request.AddHeader("Authorization", $"Bearer {TokenStorage.JwtToken}");

            try
            {
                var response = await client.ExecuteAsync<List<MasaDurum>>(request);

                if (response.IsSuccessful && response.Data != null)
                {
                    foreach (var status in response.Data)
                    {
                        foreach (Control control in panelmasalar.Controls)
                        {
                            
                            if (control is Button btn && btn.Tag is int masaIdFromTag && masaIdFromTag == status.MasaID)
                            {
                              
                                string durumLowerCase = status.Durum?.ToLowerInvariant() ?? "boş";

                                switch (durumLowerCase) 
                                {
                                    case "dolu": 
                                        btn.BackColor = Color.FromArgb(180, 40, 40); 
                                        break;
                                    case "boş": 
                                        btn.BackColor = Color.FromArgb(40, 180, 40); 
                                        break;
                                    case "ödeme bekleniyor": 
                                        btn.BackColor = Color.Orange; 
                                        break;
                                    default:
                                       
                                        System.Diagnostics.Debug.WriteLine($"Masa {status.MasaID} için beklenmeyen durum: {status.Durum}");
                                        btn.BackColor = Color.Gray; 
                                        break;
                                }
                                break; 
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show($"Masa durumları yüklenirken API'den hata alındı.\n" +
                                    $"HTTP Durum Kodu: {response.StatusCode}\n" +
                                    $"Hata Mesajı (RestSharp): {response.ErrorMessage}\n" +
                                    $"API Yanıt İçeriği: {response.Content}",
                                    "API Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Masa durumlarını çekerken bağlantı hatası: {ex.Message}", "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void MasaButon_Click(object sender, EventArgs e)
        {
            Button tiklanan = sender as Button;
            int masaNo = (int)tiklanan.Tag;

            //MessageBox.Show($"Masa {masaNo} seçildi."); // Bu mesaj kutusu akışı kesebilir, test için kaldırılabilir
            siparisform sipariş = new siparisform(masaNo);
            this.Hide(); 

            sipariş.ShowDialog(); 

           
            this.Show(); // masalar formunu tekrar göster
            await LoadTableStatuses();

        }

        private void panelmasalar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void masalar_Load_1(object sender, EventArgs e)
        {

        }
    }
}
