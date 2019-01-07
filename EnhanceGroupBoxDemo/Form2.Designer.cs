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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new Ekstrand.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(427, 156);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 87);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
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
            this.groupBox1.Header.BorderColor = System.Drawing.Color.Red;
            this.groupBox1.Header.BorderCorners = Ekstrand.Windows.Forms.BorderCorners.None;
            this.groupBox1.Header.GradientEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.groupBox1.Header.GradientStartColor = System.Drawing.Color.White;
            this.groupBox1.Header.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            this.groupBox1.Header.ImageSide = Ekstrand.Windows.Forms.ImageSide.Right;
            this.groupBox1.Header.Width = 2;
            this.groupBox1.InsideBorder.GradientEndColor = System.Drawing.Color.Empty;
            this.groupBox1.InsideBorder.GradientStartColor = System.Drawing.Color.Empty;
            this.groupBox1.Location = new System.Drawing.Point(221, 156);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBoxB";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox2;
        private Ekstrand.Windows.Forms.GroupBox groupBox1;
    }
}