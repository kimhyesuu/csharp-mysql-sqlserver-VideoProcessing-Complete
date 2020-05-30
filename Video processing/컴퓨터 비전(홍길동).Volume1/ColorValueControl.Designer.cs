namespace 컴퓨터_비전_홍길동_.Volume1
{
    partial class ColorValueControl
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
            this.components = new System.ComponentModel.Container();
            this.panel2 = new System.Windows.Forms.Panel();
            this.picBoxImg2 = new System.Windows.Forms.PictureBox();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.numUpDown = new System.Windows.Forms.NumericUpDown();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.lblContollor = new System.Windows.Forms.Label();
            this.btn_return = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.lblMax = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblMini = new System.Windows.Forms.Label();
            this.tkBar = new System.Windows.Forms.TrackBar();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxImg2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tkBar)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(55)))));
            this.panel2.Controls.Add(this.picBoxImg2);
            this.panel2.Location = new System.Drawing.Point(227, 160);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(236, 149);
            this.panel2.TabIndex = 20;
            // 
            // picBoxImg2
            // 
            this.picBoxImg2.Location = new System.Drawing.Point(21, 15);
            this.picBoxImg2.Name = "picBoxImg2";
            this.picBoxImg2.Size = new System.Drawing.Size(100, 50);
            this.picBoxImg2.TabIndex = 9;
            this.picBoxImg2.TabStop = false;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(55)))));
            this.btn_Cancel.Font = new System.Drawing.Font("휴먼엑스포", 9F);
            this.btn_Cancel.ForeColor = System.Drawing.Color.LightGray;
            this.btn_Cancel.Location = new System.Drawing.Point(628, 69);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(90, 45);
            this.btn_Cancel.TabIndex = 16;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = false;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(55)))));
            this.btn_OK.Font = new System.Drawing.Font("휴먼엑스포", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_OK.ForeColor = System.Drawing.Color.LightGray;
            this.btn_OK.Location = new System.Drawing.Point(532, 69);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(90, 45);
            this.btn_OK.TabIndex = 15;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = false;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // numUpDown
            // 
            this.numUpDown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(55)))));
            this.numUpDown.DecimalPlaces = 1;
            this.numUpDown.ForeColor = System.Drawing.SystemColors.Info;
            this.numUpDown.Location = new System.Drawing.Point(328, 89);
            this.numUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numUpDown.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.numUpDown.Name = "numUpDown";
            this.numUpDown.Size = new System.Drawing.Size(167, 25);
            this.numUpDown.TabIndex = 14;
            this.numUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numUpDown.ValueChanged += new System.EventHandler(this.numUpDown_ValueChanged);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(55)))));
            this.panel3.Controls.Add(this.label4);
            this.panel3.Location = new System.Drawing.Point(227, 69);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(284, 59);
            this.panel3.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("휴먼엑스포", 9F);
            this.label4.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label4.Location = new System.Drawing.Point(9, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Number";
            // 
            // lblContollor
            // 
            this.lblContollor.AutoSize = true;
            this.lblContollor.Font = new System.Drawing.Font("휴먼엑스포", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblContollor.ForeColor = System.Drawing.Color.LightGray;
            this.lblContollor.Location = new System.Drawing.Point(23, 11);
            this.lblContollor.Name = "lblContollor";
            this.lblContollor.Size = new System.Drawing.Size(154, 63);
            this.lblContollor.TabIndex = 0;
            this.lblContollor.Text = "BRIGHTNESS\r\n\r\nCONTROLLOR";
            // 
            // btn_return
            // 
            this.btn_return.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(100)))), ((int)(((byte)(110)))));
            this.btn_return.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_return.Font = new System.Drawing.Font("휴먼엑스포", 11F);
            this.btn_return.ForeColor = System.Drawing.Color.Crimson;
            this.btn_return.Location = new System.Drawing.Point(0, 88);
            this.btn_return.Name = "btn_return";
            this.btn_return.Size = new System.Drawing.Size(200, 81);
            this.btn_return.TabIndex = 2;
            this.btn_return.Text = "Return";
            this.btn_return.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(55)))));
            this.panel1.Controls.Add(this.btn_return);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 336);
            this.panel1.TabIndex = 13;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(65)))));
            this.panel4.Controls.Add(this.lblContollor);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(200, 88);
            this.panel4.TabIndex = 1;
            // 
            // timer
            // 
            this.timer.Interval = 10;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // lblMax
            // 
            this.lblMax.AutoSize = true;
            this.lblMax.Font = new System.Drawing.Font("휴먼엑스포", 9F);
            this.lblMax.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblMax.Location = new System.Drawing.Point(682, 49);
            this.lblMax.Name = "lblMax";
            this.lblMax.Size = new System.Drawing.Size(36, 17);
            this.lblMax.TabIndex = 22;
            this.lblMax.Text = "255";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("휴먼엑스포", 9F);
            this.label3.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.label3.Location = new System.Drawing.Point(459, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 17);
            this.label3.TabIndex = 19;
            this.label3.Text = "0";
            // 
            // lblMini
            // 
            this.lblMini.AutoSize = true;
            this.lblMini.Font = new System.Drawing.Font("휴먼엑스포", 9F);
            this.lblMini.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblMini.Location = new System.Drawing.Point(214, 46);
            this.lblMini.Name = "lblMini";
            this.lblMini.Size = new System.Drawing.Size(44, 17);
            this.lblMini.TabIndex = 18;
            this.lblMini.Text = "-255";
            // 
            // tkBar
            // 
            this.tkBar.Location = new System.Drawing.Point(217, 7);
            this.tkBar.Maximum = 255;
            this.tkBar.Minimum = -255;
            this.tkBar.Name = "tkBar";
            this.tkBar.Size = new System.Drawing.Size(502, 56);
            this.tkBar.TabIndex = 17;
            this.tkBar.TickFrequency = 15;
            this.tkBar.ValueChanged += new System.EventHandler(this.tkBar_ValueChanged);
            // 
            // ColorValueControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(100)))), ((int)(((byte)(110)))));
            this.ClientSize = new System.Drawing.Size(765, 336);
            this.Controls.Add(this.lblMax);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblMini);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.numUpDown);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.tkBar);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ColorValueControl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ColorValueControl";
            this.Load += new System.EventHandler(this.ColorValueControl_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxImg2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tkBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox picBoxImg2;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_OK;
        public System.Windows.Forms.NumericUpDown numUpDown;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblContollor;
        private System.Windows.Forms.Button btn_return;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Label lblMax;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblMini;
        public System.Windows.Forms.TrackBar tkBar;
    }
}