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
                btn.BackColor = Color.Gray; 
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
            var request = new RestRequest("api/resarvation/getmasadurumlari", Method.Get); 

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
                    foreach (var status in response.Data) // status artık string "Durum" property'sine sahip
                    {
                        foreach (Control control in panelmasalar.Controls)
                        {
                            if (control is Button btn && (int)btn.Tag == status.MasaID)
                            {
                                // status.Durum'u doğrudan kullanın
                                switch (status.Durum.ToLowerInvariant()) // ToLowerInvariant() veya ToLower()
                                {
                                    case "dolu":
                                        btn.BackColor = Color.FromArgb(180, 40, 40); 
                                        break;
                                    case "boş":
                                        btn.BackColor = Color.FromArgb(40, 180, 40); 
                                        break;
                                    case "rezerve":
                                        btn.BackColor = Color.FromArgb(255, 165, 0); 
                                        break;
                                    default:
                                        btn.BackColor = Color.Gray; // Bilinmeyen durumlar için
                                        break;
                                }
                                break;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show($"Masa durumları yüklenirken hata oluştu: {response.StatusCode} - {response.ErrorMessage ?? response.Content}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Masa durumlarını çekerken bağlantı hatası: {ex.Message}", "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MasaButon_Click(object sender, EventArgs e)
        {
            Button tiklanan = sender as Button;
            int masaNo = (int)tiklanan.Tag;

           
            MessageBox.Show($"Masa {masaNo} seçildi.");
            siparisform sipariş = new siparisform(masaNo);
            this.Hide();
            sipariş.ShowDialog();
        }

        private void panelmasalar_Paint(object sender, PaintEventArgs e)
        {

        }

        private void masalar_Load_1(object sender, EventArgs e)
        {

        }
    }
}
