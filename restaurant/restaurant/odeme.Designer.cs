namespace restaurant
{
    partial class odeme
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
            this.label = new System.Windows.Forms.Label();
            this.lstSiparisler = new System.Windows.Forms.ListBox();
            this.lblSiparisler = new System.Windows.Forms.Label();
            this.lblMasano = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblToplamtutar = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbMasanumaraları = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGeriDon = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label.Location = new System.Drawing.Point(195, 182);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(83, 23);
            this.label.TabIndex = 0;
            this.label.Text = "Masa No";
            // 
            // lstSiparisler
            // 
            this.lstSiparisler.FormattingEnabled = true;
            this.lstSiparisler.ItemHeight = 16;
            this.lstSiparisler.Location = new System.Drawing.Point(249, 350);
            this.lstSiparisler.Name = "lstSiparisler";
            this.lstSiparisler.Size = new System.Drawing.Size(119, 84);
            this.lstSiparisler.TabIndex = 1;
            // 
            // lblSiparisler
            // 
            this.lblSiparisler.AutoSize = true;
            this.lblSiparisler.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSiparisler.Location = new System.Drawing.Point(323, 304);
            this.lblSiparisler.Name = "lblSiparisler";
            this.lblSiparisler.Size = new System.Drawing.Size(85, 23);
            this.lblSiparisler.TabIndex = 2;
            this.lblSiparisler.Text = "Siparişler";
            // 
            // lblMasano
            // 
            this.lblMasano.AutoSize = true;
            this.lblMasano.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblMasano.Location = new System.Drawing.Point(323, 182);
            this.lblMasano.Name = "lblMasano";
            this.lblMasano.Size = new System.Drawing.Size(60, 23);
            this.lblMasano.TabIndex = 3;
            this.lblMasano.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(195, 254);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "Toplam Tutar";
            // 
            // lblToplamtutar
            // 
            this.lblToplamtutar.AutoSize = true;
            this.lblToplamtutar.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblToplamtutar.Location = new System.Drawing.Point(323, 254);
            this.lblToplamtutar.Name = "lblToplamtutar";
            this.lblToplamtutar.Size = new System.Drawing.Size(60, 23);
            this.lblToplamtutar.TabIndex = 5;
            this.lblToplamtutar.Text = "label2";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.button1.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(88, 342);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 92);
            this.button1.TabIndex = 6;
            this.button1.Text = "ÖDEMEYİ TAMAMLA";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbMasanumaraları
            // 
            this.cmbMasanumaraları.FormattingEnabled = true;
            this.cmbMasanumaraları.Location = new System.Drawing.Point(317, 117);
            this.cmbMasanumaraları.Name = "cmbMasanumaraları";
            this.cmbMasanumaraları.Size = new System.Drawing.Size(121, 24);
            this.cmbMasanumaraları.TabIndex = 7;
            this.cmbMasanumaraları.SelectedIndexChanged += new System.EventHandler(this.cmbMasanumaraları_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(196, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 23);
            this.label2.TabIndex = 8;
            this.label2.Text = "Masalar";
            // 
            // btnGeriDon
            // 
            this.btnGeriDon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnGeriDon.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnGeriDon.ForeColor = System.Drawing.Color.White;
            this.btnGeriDon.Location = new System.Drawing.Point(432, 342);
            this.btnGeriDon.Name = "btnGeriDon";
            this.btnGeriDon.Size = new System.Drawing.Size(110, 84);
            this.btnGeriDon.TabIndex = 9;
            this.btnGeriDon.Text = "GERİ DÖN";
            this.btnGeriDon.UseVisualStyleBackColor = false;
            this.btnGeriDon.Click += new System.EventHandler(this.btnGeriDon_Click);
            // 
            // odeme
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 562);
            this.Controls.Add(this.btnGeriDon);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbMasanumaraları);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblToplamtutar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMasano);
            this.Controls.Add(this.lblSiparisler);
            this.Controls.Add(this.lstSiparisler);
            this.Controls.Add(this.label);
            this.Name = "odeme";
            this.Text = "odeme";
            this.Load += new System.EventHandler(this.odeme_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label;
        private System.Windows.Forms.ListBox lstSiparisler;
        private System.Windows.Forms.Label lblSiparisler;
        private System.Windows.Forms.Label lblMasano;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblToplamtutar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmbMasanumaraları;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGeriDon;
    }
}