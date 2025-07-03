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
    public partial class adminpanel : Form
    {
        public adminpanel()
        {
            InitializeComponent();
        }

        bool altButonlar = false;
        private void btnMenuyonetimi_Click(object sender, EventArgs e)
        {
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
            Application.Exit();
        }
    }
}

