namespace Ekstrand.Windows.Forms
{
    partial class CornerSelector
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnTL = new System.Windows.Forms.Button();
            this.btnBL = new System.Windows.Forms.Button();
            this.btnAll = new System.Windows.Forms.Button();
            this.btnTR = new System.Windows.Forms.Button();
            this.btnBR = new System.Windows.Forms.Button();
            this.btnNone = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Controls.Add(this.btnTL, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnBL, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnAll, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnTR, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnBR, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnNone, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 28F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(156, 108);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btnTL
            // 
            this.btnTL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTL.FlatAppearance.BorderSize = 0;
            this.btnTL.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnTL.Location = new System.Drawing.Point(0, 0);
            this.btnTL.Margin = new System.Windows.Forms.Padding(0);
            this.btnTL.Name = "btnTL";
            this.btnTL.Size = new System.Drawing.Size(40, 40);
            this.btnTL.TabIndex = 0;
            this.btnTL.Text = "TL";
            this.btnTL.UseVisualStyleBackColor = true;
            this.btnTL.Click += new System.EventHandler(this.Button_Click);
            // 
            // btnBL
            // 
            this.btnBL.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBL.FlatAppearance.BorderSize = 0;
            this.btnBL.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnBL.Location = new System.Drawing.Point(0, 40);
            this.btnBL.Margin = new System.Windows.Forms.Padding(0);
            this.btnBL.Name = "btnBL";
            this.btnBL.Size = new System.Drawing.Size(40, 40);
            this.btnBL.TabIndex = 1;
            this.btnBL.Text = "BL";
            this.btnBL.UseVisualStyleBackColor = true;
            this.btnBL.Click += new System.EventHandler(this.Button_Click);
            // 
            // btnAll
            // 
            this.btnAll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnAll.FlatAppearance.BorderSize = 0;
            this.btnAll.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnAll.Location = new System.Drawing.Point(40, 0);
            this.btnAll.Margin = new System.Windows.Forms.Padding(0);
            this.btnAll.Name = "btnAll";
            this.tableLayoutPanel1.SetRowSpan(this.btnAll, 2);
            this.btnAll.Size = new System.Drawing.Size(76, 80);
            this.btnAll.TabIndex = 2;
            this.btnAll.Text = "ALL";
            this.btnAll.UseVisualStyleBackColor = true;
            this.btnAll.Click += new System.EventHandler(this.Button_Click);
            // 
            // btnTR
            // 
            this.btnTR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTR.FlatAppearance.BorderSize = 0;
            this.btnTR.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnTR.Location = new System.Drawing.Point(116, 0);
            this.btnTR.Margin = new System.Windows.Forms.Padding(0);
            this.btnTR.Name = "btnTR";
            this.btnTR.Size = new System.Drawing.Size(40, 40);
            this.btnTR.TabIndex = 3;
            this.btnTR.Text = "TR";
            this.btnTR.UseVisualStyleBackColor = true;
            this.btnTR.MouseCaptureChanged += new System.EventHandler(this.Button_Click);
            // 
            // btnBR
            // 
            this.btnBR.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnBR.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBR.FlatAppearance.BorderSize = 0;
            this.btnBR.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnBR.Location = new System.Drawing.Point(116, 40);
            this.btnBR.Margin = new System.Windows.Forms.Padding(0);
            this.btnBR.Name = "btnBR";
            this.btnBR.Size = new System.Drawing.Size(40, 40);
            this.btnBR.TabIndex = 4;
            this.btnBR.Text = "BR";
            this.btnBR.UseVisualStyleBackColor = false;
            this.btnBR.Click += new System.EventHandler(this.Button_Click);
            // 
            // btnNone
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.btnNone, 3);
            this.btnNone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnNone.FlatAppearance.BorderSize = 0;
            this.btnNone.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnNone.Location = new System.Drawing.Point(0, 80);
            this.btnNone.Margin = new System.Windows.Forms.Padding(0);
            this.btnNone.Name = "btnNone";
            this.btnNone.Size = new System.Drawing.Size(156, 28);
            this.btnNone.TabIndex = 5;
            this.btnNone.Text = "None";
            this.btnNone.UseVisualStyleBackColor = true;
            this.btnNone.Click += new System.EventHandler(this.Button_Click);
            // 
            // CornerSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MaximumSize = new System.Drawing.Size(158, 110);
            this.MinimumSize = new System.Drawing.Size(158, 110);
            this.Name = "CornerSelector";
            this.Size = new System.Drawing.Size(156, 108);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnTL;
        private System.Windows.Forms.Button btnBL;
        private System.Windows.Forms.Button btnAll;
        private System.Windows.Forms.Button btnTR;
        private System.Windows.Forms.Button btnBR;
        private System.Windows.Forms.Button btnNone;
    }
}
