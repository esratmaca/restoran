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
using Newtonsoft.Json;
using rezervasyonAPI.Models;


namespace restaurant
{
    public partial class Form1 : Form
    {
        private const string BaseApiUrl = "https://localhost:44363/";
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
        //giriş butonu
        //await ifadesi sadece async olan metodlarda kullanılabilir o yüzden butona async eklememiz gerekiyor
        private async void button1_Click(object sender, EventArgs e)
        {
             var client = new RestClient(BaseApiUrl);
            var request = new RestRequest("api/account/login", Method.Post); // Method.Post olarak düzeltildi
            
            // JSON body eklemek için AddJsonBody kullanılır
            request.AddJsonBody(new
            {
                KullaniciAdi = txtkullanıcıadı.Text, // txtkullanıcıadı ve txtsifre sizin formunuzdaki TextBox'ların isimleri olmalı
                Sifre = txtsifre.Text
            });

            try
            {
                var response = await client.ExecuteAsync(request); 

                if (response.StatusCode == HttpStatusCode.OK) // HTTP 200 OK
                {
                    // Yanıt içeriğini LoginResponse nesnesine dönüştür
                    LoginResponse loginResponse = JsonConvert.DeserializeObject<LoginResponse>(response.Content);

                    // Token'ı ve diğer kullanıcı bilgilerini global olarak TokenStorage'da sakla
                    TokenStorage.JwtToken = loginResponse.Token;
                    TokenStorage.KullaniciID = loginResponse.KullaniciID;
                    TokenStorage.KullaniciAdi = loginResponse.KullaniciAdi;
                    TokenStorage.Rol = loginResponse.Rol; // Rol bilgisini de sakla

                    MessageBox.Show($"Giriş başarılı! Hoş geldiniz, {TokenStorage.KullaniciAdi} ({TokenStorage.Rol})", "Giriş Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Rolüne göre yeni formu aç veya yönlendir
                    if (TokenStorage.Rol == "admin")
                    {
                       
                        adminpanel adminForm = new adminpanel(); 
                        adminForm.Show(); 
                        this.Hide(); 
                        MessageBox.Show("Admin paneli açılacak."); 
                    }
                    else if (TokenStorage.Rol == "garson")
                    {
                      
                        masalar masalarForm = new masalar(); 
                        masalarForm.Show(); 
                        this.Hide(); 
                        MessageBox.Show("Garson paneli açılacak."); 
                    }
                    else
                    {
                        MessageBox.Show("Bilinmeyen rol. Lütfen yöneticinizle iletişime geçin.", "Rol Hatası", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else if (response.StatusCode == HttpStatusCode.Unauthorized) // HTTP 401 Unauthorized
                {
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı.", "Giriş Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else // Diğer hata durumları (400 Bad Request, 500 Internal Server Error vb.)
                {
                    string errorContent = response.Content; // Hata mesajını oku
                    MessageBox.Show($"Giriş sırasında bir hata oluştu: {response.StatusCode} - {errorContent}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex) // Genel hata yakalama
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btncıkıs_Click_1(object sender, EventArgs e)
        {
            //Application.Exit();
            this.Close();
        }
    }
}
