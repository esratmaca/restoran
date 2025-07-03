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
            this.btnMenuyonetimi = new System.Windows.Forms.Button();
            this.btnRaporlama = new System.Windows.Forms.Button();
            this.btnCıkıs = new System.Windows.Forms.Button();
            this.btnEkle = new System.Windows.Forms.Button();
            this.btnSil = new System.Windows.Forms.Button();
            this.btnFiyatguncelle = new System.Windows.Forms.Button();
            this.pnlIcerik.SuspendLayout();
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
            // btnRaporlama
            // 
            this.btnRaporlama.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnRaporlama.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnRaporlama.ForeColor = System.Drawing.Color.White;
            this.btnRaporlama.Location = new System.Drawing.Point(3, 103);
            this.btnRaporlama.Name = "btnRaporlama";
            this.btnRaporlama.Size = new System.Drawing.Size(209, 39);
            this.btnRaporlama.TabIndex = 1;
            this.btnRaporlama.Text = "RAPORLAMA";
            this.btnRaporlama.UseVisualStyleBackColor = false;
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
            // btnEkle
            // 
            this.btnEkle.Location = new System.Drawing.Point(41, 44);
            this.btnEkle.Name = "btnEkle";
            this.btnEkle.Size = new System.Drawing.Size(168, 23);
            this.btnEkle.TabIndex = 1;
            this.btnEkle.Text = "Ürün Ekle";
            this.btnEkle.UseVisualStyleBackColor = true;
            this.btnEkle.Visible = false;
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
            // 
            // btnFiyatguncelle
            // 
            this.btnFiyatguncelle.Location = new System.Drawing.Point(41, 83);
            this.btnFiyatguncelle.Name = "btnFiyatguncelle";
            this.btnFiyatguncelle.Size = new System.Drawing.Size(168, 23);
            this.btnFiyatguncelle.TabIndex = 3;
            this.btnFiyatguncelle.Text = "Fiyat Güncelle";
            this.btnFiyatguncelle.UseVisualStyleBackColor = true;
            this.btnFiyatguncelle.Visible = false;
            // 
            // adminpanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 573);
            this.Controls.Add(this.pnlIcerik);
            this.Name = "adminpanel";
            this.Text = "adminpanel";
            this.Load += new System.EventHandler(this.adminpanel_Load);
            this.pnlIcerik.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlIcerik;
        private System.Windows.Forms.Button btnMenuyonetimi;
        private System.Windows.Forms.Button btnRaporlama;
        private System.Windows.Forms.Button btnCıkıs;
        private System.Windows.Forms.Button btnEkle;
        private System.Windows.Forms.Button btnFiyatguncelle;
        private System.Windows.Forms.Button btnSil;
    }
}