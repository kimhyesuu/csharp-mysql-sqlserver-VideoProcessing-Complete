namespace 컴퓨터_비전_홍길동_.Volume1
{
    partial class SQLserver
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
            this.button1 = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.picBox = new System.Windows.Forms.PictureBox();
            this.btnSaveDb = new System.Windows.Forms.Button();
            this.btn_sh = new System.Windows.Forms.Button();
            this.btn_db_list = new System.Windows.Forms.Button();
            this.cmBOX = new System.Windows.Forms.ComboBox();
            this.btn_insert = new System.Windows.Forms.Button();
            this.btn_select = new System.Windows.Forms.Button();
            this.tbFullname = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(620, 271);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(168, 37);
            this.button1.TabIndex = 39;
            this.button1.Text = "확대";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(620, 228);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(168, 37);
            this.btnSearch.TabIndex = 38;
            this.btnSearch.Text = "End-In Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(620, 185);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(168, 37);
            this.btnBack.TabIndex = 37;
            this.btnBack.Text = "되돌리기";
            this.btnBack.UseVisualStyleBackColor = true;
            // 
            // picBox
            // 
            this.picBox.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.picBox.Location = new System.Drawing.Point(40, 99);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(46, 31);
            this.picBox.TabIndex = 36;
            this.picBox.TabStop = false;
            // 
            // btnSaveDb
            // 
            this.btnSaveDb.Location = new System.Drawing.Point(620, 142);
            this.btnSaveDb.Name = "btnSaveDb";
            this.btnSaveDb.Size = new System.Drawing.Size(168, 37);
            this.btnSaveDb.TabIndex = 35;
            this.btnSaveDb.Text = "저장하기";
            this.btnSaveDb.UseVisualStyleBackColor = true;
            this.btnSaveDb.Click += new System.EventHandler(this.btnSaveDb_Click);
            // 
            // btn_sh
            // 
            this.btn_sh.Location = new System.Drawing.Point(620, 99);
            this.btn_sh.Name = "btn_sh";
            this.btn_sh.Size = new System.Drawing.Size(168, 37);
            this.btn_sh.TabIndex = 34;
            this.btn_sh.Text = "밝게하기";
            this.btn_sh.UseVisualStyleBackColor = true;
            this.btn_sh.Click += new System.EventHandler(this.btn_sh_Click);
            // 
            // btn_db_list
            // 
            this.btn_db_list.Location = new System.Drawing.Point(40, 46);
            this.btn_db_list.Name = "btn_db_list";
            this.btn_db_list.Size = new System.Drawing.Size(119, 25);
            this.btn_db_list.TabIndex = 33;
            this.btn_db_list.Text = "DB 목록 조회";
            this.btn_db_list.UseVisualStyleBackColor = true;
            this.btn_db_list.Click += new System.EventHandler(this.btn_db_list_Click);
            // 
            // cmBOX
            // 
            this.cmBOX.FormattingEnabled = true;
            this.cmBOX.Location = new System.Drawing.Point(175, 46);
            this.cmBOX.Name = "cmBOX";
            this.cmBOX.Size = new System.Drawing.Size(411, 23);
            this.cmBOX.TabIndex = 32;
            this.cmBOX.SelectedIndexChanged += new System.EventHandler(this.cmBOX_SelectedIndexChanged);
            // 
            // btn_insert
            // 
            this.btn_insert.Location = new System.Drawing.Point(620, 56);
            this.btn_insert.Name = "btn_insert";
            this.btn_insert.Size = new System.Drawing.Size(168, 37);
            this.btn_insert.TabIndex = 31;
            this.btn_insert.Text = "DB에 입력";
            this.btn_insert.UseVisualStyleBackColor = true;
            this.btn_insert.Click += new System.EventHandler(this.btn_insert_Click);
            // 
            // btn_select
            // 
            this.btn_select.Location = new System.Drawing.Point(620, 12);
            this.btn_select.Name = "btn_select";
            this.btn_select.Size = new System.Drawing.Size(168, 37);
            this.btn_select.TabIndex = 30;
            this.btn_select.Text = "파일선택";
            this.btn_select.UseVisualStyleBackColor = true;
            this.btn_select.Click += new System.EventHandler(this.btn_select_Click);
            // 
            // tbFullname
            // 
            this.tbFullname.Location = new System.Drawing.Point(40, 15);
            this.tbFullname.Name = "tbFullname";
            this.tbFullname.ReadOnly = true;
            this.tbFullname.Size = new System.Drawing.Size(505, 25);
            this.tbFullname.TabIndex = 29;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 424);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 26);
            this.statusStrip1.TabIndex = 40;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(152, 20);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // SQLserver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.picBox);
            this.Controls.Add(this.btnSaveDb);
            this.Controls.Add(this.btn_sh);
            this.Controls.Add(this.btn_db_list);
            this.Controls.Add(this.cmBOX);
            this.Controls.Add(this.btn_insert);
            this.Controls.Add(this.btn_select);
            this.Controls.Add(this.tbFullname);
            this.Name = "SQLserver";
            this.Text = "SQLserver";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SQLserver_FormClosed);
            this.Load += new System.EventHandler(this.SQLserver_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.Button btnSaveDb;
        private System.Windows.Forms.Button btn_sh;
        private System.Windows.Forms.Button btn_db_list;
        private System.Windows.Forms.ComboBox cmBOX;
        private System.Windows.Forms.Button btn_insert;
        private System.Windows.Forms.Button btn_select;
        private System.Windows.Forms.TextBox tbFullname;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}