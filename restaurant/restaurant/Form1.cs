using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace restaurant
{
    public partial class Form1 : Form
    {
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

        private void button1_Click(object sender, EventArgs e)
        {
            string kullaniciAdi = txtkullanıcıadı.Text.Trim();
            string sifre = txtsifre.Text.Trim();

            if(kullaniciAdi == "admin" ||  sifre == "admin123")
            {
                adminpanel admin = new adminpanel();
                admin.Show();
                this.Hide();
            }

            else if(kullaniciAdi =="garson" ||  sifre == "garson123")
            {
                masalar masaform = new masalar();
                masaform.Show();
                this.Hide();
            }
            else {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btncıkıs_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
