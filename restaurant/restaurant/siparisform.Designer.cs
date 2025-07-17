namespace restaurant
{
    partial class siparisform
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbMenu = new System.Windows.Forms.ComboBox();
            this.lblMasanumarası = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudAdet = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.btnekle = new System.Windows.Forms.Button();
            this.lstSiparişler = new System.Windows.Forms.ListBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.btngeri = new System.Windows.Forms.Button();
            this.btnOdemeyegec = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdet)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(137, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Seçilen Masa:";
            // 
            // cmbMenu
            // 
            this.cmbMenu.FormattingEnabled = true;
            this.cmbMenu.Location = new System.Drawing.Point(291, 141);
            this.cmbMenu.Name = "cmbMenu";
            this.cmbMenu.Size = new System.Drawing.Size(121, 24);
            this.cmbMenu.TabIndex = 2;
            this.cmbMenu.SelectedIndexChanged += new System.EventHandler(this.cmbMenu_SelectedIndexChanged);
            // 
            // lblMasanumarası
            // 
            this.lblMasanumarası.AutoSize = true;
            this.lblMasanumarası.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblMasanumarası.Location = new System.Drawing.Point(287, 95);
            this.lblMasanumarası.Name = "lblMasanumarası";
            this.lblMasanumarası.Size = new System.Drawing.Size(60, 23);
            this.lblMasanumarası.TabIndex = 3;
            this.lblMasanumarası.Text = "label2";
            this.lblMasanumarası.Click += new System.EventHandler(this.lblMasanumarası_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(137, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "Ürünler";
            // 
            // nudAdet
            // 
            this.nudAdet.Location = new System.Drawing.Point(291, 189);
            this.nudAdet.Name = "nudAdet";
            this.nudAdet.Size = new System.Drawing.Size(120, 22);
            this.nudAdet.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(137, 186);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 23);
            this.label3.TabIndex = 6;
            this.label3.Text = "Ürün Adedi";
            // 
            // btnekle
            // 
            this.btnekle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnekle.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnekle.ForeColor = System.Drawing.Color.White;
            this.btnekle.Location = new System.Drawing.Point(43, 324);
            this.btnekle.Name = "btnekle";
            this.btnekle.Size = new System.Drawing.Size(107, 93);
            this.btnekle.TabIndex = 7;
            this.btnekle.Text = "EKLE";
            this.btnekle.UseVisualStyleBackColor = false;
            this.btnekle.Click += new System.EventHandler(this.btnekle_Click);
            // 
            // lstSiparişler
            // 
            this.lstSiparişler.FormattingEnabled = true;
            this.lstSiparişler.ItemHeight = 16;
            this.lstSiparişler.Location = new System.Drawing.Point(169, 333);
            this.lstSiparişler.Name = "lstSiparişler";
            this.lstSiparişler.Size = new System.Drawing.Size(105, 84);
            this.lstSiparişler.TabIndex = 8;
            // 
            // btngeri
            // 
            this.btngeri.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btngeri.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btngeri.ForeColor = System.Drawing.Color.White;
            this.btngeri.Location = new System.Drawing.Point(308, 324);
            this.btngeri.Name = "btngeri";
            this.btngeri.Size = new System.Drawing.Size(104, 93);
            this.btngeri.TabIndex = 9;
            this.btngeri.Text = "GERİ DÖN";
            this.btngeri.UseVisualStyleBackColor = false;
            this.btngeri.Click += new System.EventHandler(this.btngeri_Click);
            // 
            // btnOdemeyegec
            // 
            this.btnOdemeyegec.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnOdemeyegec.Font = new System.Drawing.Font("Microsoft YaHei", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnOdemeyegec.ForeColor = System.Drawing.Color.White;
            this.btnOdemeyegec.Location = new System.Drawing.Point(459, 329);
            this.btnOdemeyegec.Name = "btnOdemeyegec";
            this.btnOdemeyegec.Size = new System.Drawing.Size(103, 88);
            this.btnOdemeyegec.TabIndex = 10;
            this.btnOdemeyegec.Text = "ÖDEMEYE GEÇ";
            this.btnOdemeyegec.UseVisualStyleBackColor = false;
            this.btnOdemeyegec.Click += new System.EventHandler(this.btnOdemeyegec_Click);
            // 
            // siparisform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(644, 563);
            this.Controls.Add(this.btnOdemeyegec);
            this.Controls.Add(this.btngeri);
            this.Controls.Add(this.lstSiparişler);
            this.Controls.Add(this.btnekle);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.nudAdet);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblMasanumarası);
            this.Controls.Add(this.cmbMenu);
            this.Controls.Add(this.label1);
            this.Name = "siparisform";
            this.Text = "siparisform";
            this.Load += new System.EventHandler(this.siparisform_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudAdet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbMenu;
        private System.Windows.Forms.Label lblMasanumarası;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudAdet;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnekle;
        private System.Windows.Forms.ListBox lstSiparişler;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btngeri;
        private System.Windows.Forms.Button btnOdemeyegec;
    }
}