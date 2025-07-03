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
        public masalar()
        {
            InitializeComponent();
            this.Load += masalar_Load;
        }

        

        private void masalar_Load(object sender, EventArgs e)
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
                btn.BackColor = Color.FromArgb(40, 180, 40); 
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
        }
        private void MasaButon_Click(object sender, EventArgs e)
        {
            Button tiklanan = sender as Button;
            int masaNo = (int)tiklanan.Tag;

            MessageBox.Show($"Masa {masaNo} seçildi.");
            siparisform sipariş = new siparisform(masaNo);
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
