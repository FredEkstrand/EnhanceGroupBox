namespace GroupBoxDemo
{
    partial class Form2
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
            this.groupBox1 = new Ekstrand.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BorderItems.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.BorderItems.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.groupBox1.BorderItems.BorderCorners = Ekstrand.Windows.Forms.BorderCorners.None;
            this.groupBox1.BorderItems.DashPattern = null;
            this.groupBox1.DisabledTextColor = System.Drawing.SystemColors.GrayText;
            this.groupBox1.GroupBoxStyles = Ekstrand.Windows.Forms.GroupBoxStyles.Enhance;
            this.groupBox1.Header.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Header.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.groupBox1.Header.BorderCorners = Ekstrand.Windows.Forms.BorderCorners.None;
            this.groupBox1.Header.GradientEndColor = System.Drawing.Color.Empty;
            this.groupBox1.Header.GradientStartColor = System.Drawing.Color.Empty;
            this.groupBox1.Header.TextAlignment = Ekstrand.Windows.Forms.BorderTextAlignment.TopCenter;
            this.groupBox1.Header.Width = 0;
            this.groupBox1.InsideBorder.GradientEndColor = System.Drawing.Color.Empty;
            this.groupBox1.InsideBorder.GradientStartColor = System.Drawing.Color.Empty;
            this.groupBox1.Location = new System.Drawing.Point(174, 128);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(181, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBoxB";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);

        }

        #endregion

        private Ekstrand.Windows.Forms.GroupBox groupBox1;
    }
}