namespace restaurant
{
    partial class masalar
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
            this.panelmasalar = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // panelmasalar
            // 
            this.panelmasalar.AutoScroll = true;
            this.panelmasalar.BackColor = System.Drawing.Color.White;
            this.panelmasalar.Location = new System.Drawing.Point(34, 28);
            this.panelmasalar.Name = "panelmasalar";
            this.panelmasalar.Size = new System.Drawing.Size(573, 515);
            this.panelmasalar.TabIndex = 0;
            this.panelmasalar.Paint += new System.Windows.Forms.PaintEventHandler(this.panelmasalar_Paint);
            // 
            // masalar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(645, 565);
            this.Controls.Add(this.panelmasalar);
            this.Name = "masalar";
            this.Text = "masalar";
            this.Load += new System.EventHandler(this.masalar_Load_1);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel panelmasalar;
    }
}