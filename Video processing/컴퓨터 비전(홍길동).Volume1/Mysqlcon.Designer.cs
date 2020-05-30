namespace 컴퓨터_비전_홍길동_.Volume1
{
    partial class Mysqlcon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mysqlcon));
            this.btnSaveDb = new System.Windows.Forms.Button();
            this.btn_sh = new System.Windows.Forms.Button();
            this.btn_db_list = new System.Windows.Forms.Button();
            this.cmBOX = new System.Windows.Forms.ComboBox();
            this.btn_insert = new System.Windows.Forms.Button();
            this.btn_select = new System.Windows.Forms.Button();
            this.tbFullname = new System.Windows.Forms.TextBox();
            this.picBox = new System.Windows.Forms.PictureBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.ofdFile = new System.Windows.Forms.OpenFileDialog();
            this.sfdFile = new System.Windows.Forms.SaveFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnimageBack = new System.Windows.Forms.Button();
            this.btnGoforward = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnFolder = new System.Windows.Forms.Button();
            this.btnall = new System.Windows.Forms.Button();
            this.gbImage = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbImage.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSaveDb
            // 
            this.btnSaveDb.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnSaveDb.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSaveDb.Font = new System.Drawing.Font("휴먼모음T", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSaveDb.Location = new System.Drawing.Point(19, 24);
            this.btnSaveDb.Name = "btnSaveDb";
            this.btnSaveDb.Size = new System.Drawing.Size(168, 42);
            this.btnSaveDb.TabIndex = 22;
            this.btnSaveDb.Text = "변경 후 저장하기";
            this.btnSaveDb.UseVisualStyleBackColor = false;
            this.btnSaveDb.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_sh
            // 
            this.btn_sh.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btn_sh.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_sh.Font = new System.Drawing.Font("휴먼모음T", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_sh.Location = new System.Drawing.Point(19, 72);
            this.btn_sh.Name = "btn_sh";
            this.btn_sh.Size = new System.Drawing.Size(168, 42);
            this.btn_sh.TabIndex = 21;
            this.btn_sh.Text = "Micro Range Exposue";
            this.btn_sh.UseVisualStyleBackColor = false;
            this.btn_sh.Click += new System.EventHandler(this.btn_sh_Click);
            // 
            // btn_db_list
            // 
            this.btn_db_list.Location = new System.Drawing.Point(17, 53);
            this.btn_db_list.Name = "btn_db_list";
            this.btn_db_list.Size = new System.Drawing.Size(119, 28);
            this.btn_db_list.TabIndex = 18;
            this.btn_db_list.Text = "DB 목록 조회";
            this.btn_db_list.UseVisualStyleBackColor = true;
            this.btn_db_list.Click += new System.EventHandler(this.btn_db_list_Click);
            // 
            // cmBOX
            // 
            this.cmBOX.FormattingEnabled = true;
            this.cmBOX.Location = new System.Drawing.Point(152, 53);
            this.cmBOX.Name = "cmBOX";
            this.cmBOX.Size = new System.Drawing.Size(411, 25);
            this.cmBOX.TabIndex = 17;
            this.cmBOX.SelectedIndexChanged += new System.EventHandler(this.cmBOX_SelectedIndexChanged);
            // 
            // btn_insert
            // 
            this.btn_insert.BackColor = System.Drawing.SystemColors.Control;
            this.btn_insert.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_insert.Font = new System.Drawing.Font("휴먼모음T", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_insert.Location = new System.Drawing.Point(663, 53);
            this.btn_insert.Name = "btn_insert";
            this.btn_insert.Size = new System.Drawing.Size(82, 42);
            this.btn_insert.TabIndex = 16;
            this.btn_insert.Text = "DB Save";
            this.btn_insert.UseVisualStyleBackColor = false;
            this.btn_insert.Click += new System.EventHandler(this.btn_insert_Click);
            // 
            // btn_select
            // 
            this.btn_select.BackColor = System.Drawing.SystemColors.Control;
            this.btn_select.Location = new System.Drawing.Point(694, 15);
            this.btn_select.Name = "btn_select";
            this.btn_select.Size = new System.Drawing.Size(64, 26);
            this.btn_select.TabIndex = 15;
            this.btn_select.Text = "OPEN";
            this.btn_select.UseVisualStyleBackColor = false;
            this.btn_select.Click += new System.EventHandler(this.btn_select_Click);
            // 
            // tbFullname
            // 
            this.tbFullname.Location = new System.Drawing.Point(17, 15);
            this.tbFullname.Name = "tbFullname";
            this.tbFullname.ReadOnly = true;
            this.tbFullname.Size = new System.Drawing.Size(659, 25);
            this.tbFullname.TabIndex = 14;
            // 
            // picBox
            // 
            this.picBox.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.picBox.Location = new System.Drawing.Point(6, 29);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(46, 35);
            this.picBox.TabIndex = 23;
            this.picBox.TabStop = false;
            this.picBox.Visible = false;
            this.picBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picBox_MouseDown);
            this.picBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picBox_MouseUp);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBack.Font = new System.Drawing.Font("휴먼모음T", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnBack.Location = new System.Drawing.Point(21, 72);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(168, 42);
            this.btnBack.TabIndex = 24;
            this.btnBack.Text = "뒤로가기(그리기)";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // ofdFile
            // 
            this.ofdFile.FileName = "openFileDialog1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 505);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(840, 25);
            this.statusStrip1.TabIndex = 25;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(152, 20);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSearch.Font = new System.Drawing.Font("휴먼모음T", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSearch.Location = new System.Drawing.Point(19, 172);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(168, 42);
            this.btnSearch.TabIndex = 27;
            this.btnSearch.Text = "End-In Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("휴먼모음T", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.button1.Location = new System.Drawing.Point(19, 221);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(168, 42);
            this.button1.TabIndex = 28;
            this.button1.Text = "확대";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnimageBack);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.btnSearch);
            this.groupBox2.Controls.Add(this.btnSaveDb);
            this.groupBox2.Controls.Add(this.btn_sh);
            this.groupBox2.Font = new System.Drawing.Font("휴먼엑스포", 9F);
            this.groupBox2.Location = new System.Drawing.Point(606, 109);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(210, 280);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "값 입력";
            // 
            // btnimageBack
            // 
            this.btnimageBack.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnimageBack.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnimageBack.Font = new System.Drawing.Font("휴먼모음T", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnimageBack.Location = new System.Drawing.Point(18, 121);
            this.btnimageBack.Name = "btnimageBack";
            this.btnimageBack.Size = new System.Drawing.Size(168, 42);
            this.btnimageBack.TabIndex = 29;
            this.btnimageBack.Text = "되돌리기";
            this.btnimageBack.UseVisualStyleBackColor = false;
            this.btnimageBack.Click += new System.EventHandler(this.btnimageBack_Click);
            // 
            // btnGoforward
            // 
            this.btnGoforward.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnGoforward.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGoforward.Font = new System.Drawing.Font("휴먼모음T", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnGoforward.Location = new System.Drawing.Point(21, 24);
            this.btnGoforward.Name = "btnGoforward";
            this.btnGoforward.Size = new System.Drawing.Size(168, 42);
            this.btnGoforward.TabIndex = 29;
            this.btnGoforward.Text = "앞으로가기(그리기)";
            this.btnGoforward.UseVisualStyleBackColor = false;
            this.btnGoforward.Click += new System.EventHandler(this.btnGoforward_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnGoforward);
            this.groupBox1.Controls.Add(this.btnBack);
            this.groupBox1.Location = new System.Drawing.Point(389, 109);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(211, 128);
            this.groupBox1.TabIndex = 32;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "그림판";
            // 
            // btnFolder
            // 
            this.btnFolder.BackColor = System.Drawing.SystemColors.Control;
            this.btnFolder.Location = new System.Drawing.Point(764, 15);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(64, 26);
            this.btnFolder.TabIndex = 33;
            this.btnFolder.Text = "폴더";
            this.btnFolder.UseVisualStyleBackColor = false;
            this.btnFolder.Click += new System.EventHandler(this.btnFolder_Click);
            // 
            // btnall
            // 
            this.btnall.BackColor = System.Drawing.SystemColors.Control;
            this.btnall.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnall.Font = new System.Drawing.Font("휴먼모음T", 7F);
            this.btnall.Location = new System.Drawing.Point(751, 53);
            this.btnall.Name = "btnall";
            this.btnall.Size = new System.Drawing.Size(77, 42);
            this.btnall.TabIndex = 30;
            this.btnall.Text = "DB Folder save";
            this.btnall.UseVisualStyleBackColor = false;
            this.btnall.Click += new System.EventHandler(this.btnall_Click);
            // 
            // gbImage
            // 
            this.gbImage.Controls.Add(this.picBox);
            this.gbImage.Location = new System.Drawing.Point(17, 107);
            this.gbImage.Name = "gbImage";
            this.gbImage.Size = new System.Drawing.Size(287, 222);
            this.gbImage.TabIndex = 30;
            this.gbImage.TabStop = false;
            this.gbImage.Text = "Image in DB(MYsql)";
            // 
            // Mysqlcon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(840, 530);
            this.Controls.Add(this.btnall);
            this.Controls.Add(this.btnFolder);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btn_insert);
            this.Controls.Add(this.gbImage);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btn_db_list);
            this.Controls.Add(this.cmBOX);
            this.Controls.Add(this.btn_select);
            this.Controls.Add(this.tbFullname);
            this.Font = new System.Drawing.Font("휴먼모음T", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "Mysqlcon";
            this.Text = "Mysqlcon";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Mysqlcon_FormClosed);
            this.Load += new System.EventHandler(this.Mysqlcon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.gbImage.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSaveDb;
        private System.Windows.Forms.Button btn_sh;
        private System.Windows.Forms.Button btn_db_list;
        private System.Windows.Forms.ComboBox cmBOX;
        private System.Windows.Forms.Button btn_insert;
        private System.Windows.Forms.Button btn_select;
        private System.Windows.Forms.TextBox tbFullname;
        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.OpenFileDialog ofdFile;
        private System.Windows.Forms.SaveFileDialog sfdFile;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnGoforward;
        private System.Windows.Forms.Button btnimageBack;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnFolder;
        private System.Windows.Forms.Button btnall;
        private System.Windows.Forms.GroupBox gbImage;
    }
}