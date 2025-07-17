namespace restaurant
{
    partial class adminpanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlIcerik = new System.Windows.Forms.Panel();
            this.btnFiyatguncelle = new System.Windows.Forms.Button();
            this.btnEkle = new System.Windows.Forms.Button();
            this.btnSil = new System.Windows.Forms.Button();
            this.btnCıkıs = new System.Windows.Forms.Button();
            this.btnRaporlama = new System.Windows.Forms.Button();
            this.btnMenuyonetimi = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txturunAdi = new System.Windows.Forms.TextBox();
            this.txturunFiyati = new System.Windows.Forms.TextBox();
            this.txtkategoriID = new System.Windows.Forms.TextBox();
            this.dgvUrunler = new System.Windows.Forms.DataGridView();
            this.pnlIcerik.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUrunler)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlIcerik
            // 
            this.pnlIcerik.Controls.Add(this.btnFiyatguncelle);
            this.pnlIcerik.Controls.Add(this.btnEkle);
            this.pnlIcerik.Controls.Add(this.btnSil);
            this.pnlIcerik.Controls.Add(this.btnCıkıs);
            this.pnlIcerik.Controls.Add(this.btnRaporlama);
            this.pnlIcerik.Controls.Add(this.btnMenuyonetimi);
            this.pnlIcerik.Location = new System.Drawing.Point(535, 2);
            this.pnlIcerik.Name = "pnlIcerik";
            this.pnlIcerik.Size = new System.Drawing.Size(212, 559);
            this.pnlIcerik.TabIndex = 0;
            // 
            // btnFiyatguncelle
            // 
            this.btnFiyatguncelle.Location = new System.Drawing.Point(41, 84);
            this.btnFiyatguncelle.Name = "btnFiyatguncelle";
            this.btnFiyatguncelle.Size = new System.Drawing.Size(168, 32);
            this.btnFiyatguncelle.TabIndex = 3;
            this.btnFiyatguncelle.Text = "İsim/Fiyat Güncelle";
            this.btnFiyatguncelle.UseVisualStyleBackColor = true;
            this.btnFiyatguncelle.Visible = false;
            this.btnFiyatguncelle.Click += new System.EventHandler(this.btnFiyatguncelle_Click);
            // 
            // btnEkle
            // 
            this.btnEkle.Location = new System.Drawing.Point(41, 44);
            this.btnEkle.Name = "btnEkle";
            this.btnEkle.Size = new System.Drawing.Size(168, 23);
            this.btnEkle.TabIndex = 1;
            this.btnEkle.Text = "Ürün Ekle";
            this.btnEkle.UseVisualStyleBackColor = true;
            this.btnEkle.Visible = false;
            this.btnEkle.Click += new System.EventHandler(this.btnEkle_Click);
            // 
            // btnSil
            // 
            this.btnSil.Location = new System.Drawing.Point(41, 64);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(168, 23);
            this.btnSil.TabIndex = 2;
            this.btnSil.Text = "Ürün Sil";
            this.btnSil.UseVisualStyleBackColor = true;
            this.btnSil.Visible = false;
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
            // 
            // btnCıkıs
            // 
            this.btnCıkıs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnCıkıs.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnCıkıs.ForeColor = System.Drawing.Color.White;
            this.btnCıkıs.Location = new System.Drawing.Point(3, 247);
            this.btnCıkıs.Name = "btnCıkıs";
            this.btnCıkıs.Size = new System.Drawing.Size(209, 42);
            this.btnCıkıs.TabIndex = 1;
            this.btnCıkıs.Text = "ÇIKIŞ";
            this.btnCıkıs.UseVisualStyleBackColor = false;
            this.btnCıkıs.Click += new System.EventHandler(this.btnCıkıs_Click);
            // 
            // btnRaporlama
            // 
            this.btnRaporlama.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnRaporlama.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnRaporlama.ForeColor = System.Drawing.Color.White;
            this.btnRaporlama.Location = new System.Drawing.Point(0, 122);
            this.btnRaporlama.Name = "btnRaporlama";
            this.btnRaporlama.Size = new System.Drawing.Size(209, 39);
            this.btnRaporlama.TabIndex = 1;
            this.btnRaporlama.Text = "RAPORLAMA";
            this.btnRaporlama.UseVisualStyleBackColor = false;
            // 
            // btnMenuyonetimi
            // 
            this.btnMenuyonetimi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnMenuyonetimi.ForeColor = System.Drawing.Color.White;
            this.btnMenuyonetimi.Location = new System.Drawing.Point(3, 10);
            this.btnMenuyonetimi.Name = "btnMenuyonetimi";
            this.btnMenuyonetimi.Size = new System.Drawing.Size(206, 35);
            this.btnMenuyonetimi.TabIndex = 1;
            this.btnMenuyonetimi.Text = "MENÜ YÖNETİMİ";
            this.btnMenuyonetimi.UseVisualStyleBackColor = false;
            this.btnMenuyonetimi.Click += new System.EventHandler(this.btnMenuyonetimi_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(99, 334);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ürün Adı";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(99, 459);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Kategori ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(99, 397);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 23);
            this.label3.TabIndex = 3;
            this.label3.Text = "Ürün Fiyatı";
            // 
            // txturunAdi
            // 
            this.txturunAdi.Location = new System.Drawing.Point(230, 336);
            this.txturunAdi.Name = "txturunAdi";
            this.txturunAdi.Size = new System.Drawing.Size(122, 22);
            this.txturunAdi.TabIndex = 5;
            // 
            // txturunFiyati
            // 
            this.txturunFiyati.Location = new System.Drawing.Point(230, 399);
            this.txturunFiyati.Name = "txturunFiyati";
            this.txturunFiyati.Size = new System.Drawing.Size(122, 22);
            this.txturunFiyati.TabIndex = 6;
            // 
            // txtkategoriID
            // 
            this.txtkategoriID.Location = new System.Drawing.Point(230, 461);
            this.txtkategoriID.Name = "txtkategoriID";
            this.txtkategoriID.Size = new System.Drawing.Size(122, 22);
            this.txtkategoriID.TabIndex = 7;
            // 
            // dgvUrunler
            // 
            this.dgvUrunler.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUrunler.Location = new System.Drawing.Point(34, 46);
            this.dgvUrunler.Name = "dgvUrunler";
            this.dgvUrunler.RowHeadersWidth = 51;
            this.dgvUrunler.RowTemplate.Height = 24;
            this.dgvUrunler.Size = new System.Drawing.Size(450, 206);
            this.dgvUrunler.TabIndex = 9;
            this.dgvUrunler.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUrunler_CellClick);
            this.dgvUrunler.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUrunler_CellClick);
            // 
            // adminpanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 573);
            this.Controls.Add(this.dgvUrunler);
            this.Controls.Add(this.txtkategoriID);
            this.Controls.Add(this.txturunFiyati);
            this.Controls.Add(this.txturunAdi);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pnlIcerik);
            this.Name = "adminpanel";
            this.Text = "adminpanel";
            this.Load += new System.EventHandler(this.adminpanel_Load);
            this.pnlIcerik.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUrunler)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlIcerik;
        private System.Windows.Forms.Button btnMenuyonetimi;
        private System.Windows.Forms.Button btnRaporlama;
        private System.Windows.Forms.Button btnCıkıs;
        private System.Windows.Forms.Button btnEkle;
        private System.Windows.Forms.Button btnFiyatguncelle;
        private System.Windows.Forms.Button btnSil;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txturunAdi;
        private System.Windows.Forms.TextBox txturunFiyati;
        private System.Windows.Forms.TextBox txtkategoriID;
        private System.Windows.Forms.DataGridView dgvUrunler;
    }
}