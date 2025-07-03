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
    public partial class siparisform : Form
    {
        private int masaNo;
        public siparisform(int masa)
        {
            InitializeComponent();
            
            masaNo = masa;
            lblMasanumarası.Text = $"Masa {masaNo}";
            cmbMenu.Items.AddRange(new string[] { "Çay", "Kahve", "Su", "Sandviç" });
        }

        private void siparisform_Load(object sender, EventArgs e)
        {

        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            string urun = cmbMenu.Text;
            int adet =(int)nudAdet.Value;
            if(!string.IsNullOrEmpty(urun) && adet > 0)
            {
                lstSiparişler.Items.Add($"{urun} x {adet}");
                MessageBox.Show("Ürünler eklendi");
                cmbMenu.SelectedIndex = -1;
                nudAdet.Value = 0;
            }
    
        }
    }
}
