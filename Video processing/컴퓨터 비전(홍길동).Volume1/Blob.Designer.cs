namespace 컴퓨터_비전_홍길동_.Volume1
{
    partial class Blob
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Blob));
            this.btnallsave = new System.Windows.Forms.Button();
            this.combolist = new System.Windows.Forms.ComboBox();
            this.btnDB = new System.Windows.Forms.Button();
            this.comboDBarray = new System.Windows.Forms.ComboBox();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnFoldersSave = new System.Windows.Forms.Button();
            this.btnFolders = new System.Windows.Forms.Button();
            this.btnUpload = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.tbFullName = new System.Windows.Forms.TextBox();
            this.lvData = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnOut = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnallsave
            // 
            this.btnallsave.Location = new System.Drawing.Point(533, 544);
            this.btnallsave.Name = "btnallsave";
            this.btnallsave.Size = new System.Drawing.Size(167, 36);
            this.btnallsave.TabIndex = 47;
            this.btnallsave.Text = "전체파일 다운로드";
            this.btnallsave.UseVisualStyleBackColor = true;
            this.btnallsave.Click += new System.EventHandler(this.btnallsave_Click);
            // 
            // combolist
            // 
            this.combolist.FormattingEnabled = true;
            this.combolist.Location = new System.Drawing.Point(490, 50);
            this.combolist.Name = "combolist";
            this.combolist.Size = new System.Drawing.Size(210, 23);
            this.combolist.TabIndex = 46;
            // 
            // btnDB
            // 
            this.btnDB.Location = new System.Drawing.Point(375, 43);
            this.btnDB.Name = "btnDB";
            this.btnDB.Size = new System.Drawing.Size(93, 38);
            this.btnDB.TabIndex = 45;
            this.btnDB.Text = "DB 조회";
            this.btnDB.UseVisualStyleBackColor = true;
            this.btnDB.Click += new System.EventHandler(this.btnDB_Click);
            // 
            // comboDBarray
            // 
            this.comboDBarray.FormattingEnabled = true;
            this.comboDBarray.Location = new System.Drawing.Point(27, 52);
            this.comboDBarray.Name = "comboDBarray";
            this.comboDBarray.Size = new System.Drawing.Size(326, 23);
            this.comboDBarray.TabIndex = 44;
            this.comboDBarray.SelectedIndexChanged += new System.EventHandler(this.comboDBarray_SelectedIndexChanged);
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(360, 544);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(167, 36);
            this.btnDownload.TabIndex = 43;
            this.btnDownload.Text = "파일 다운로드";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnFoldersSave
            // 
            this.btnFoldersSave.Location = new System.Drawing.Point(187, 544);
            this.btnFoldersSave.Name = "btnFoldersSave";
            this.btnFoldersSave.Size = new System.Drawing.Size(167, 36);
            this.btnFoldersSave.TabIndex = 42;
            this.btnFoldersSave.Text = "DB Folder Save";
            this.btnFoldersSave.UseVisualStyleBackColor = true;
            this.btnFoldersSave.Click += new System.EventHandler(this.btnFoldersSave_Click);
            // 
            // btnFolders
            // 
            this.btnFolders.Location = new System.Drawing.Point(746, 6);
            this.btnFolders.Name = "btnFolders";
            this.btnFolders.Size = new System.Drawing.Size(93, 38);
            this.btnFolders.TabIndex = 41;
            this.btnFolders.Text = "Files";
            this.btnFolders.UseVisualStyleBackColor = true;
            this.btnFolders.Click += new System.EventHandler(this.btnFolders_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(12, 544);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(167, 36);
            this.btnUpload.TabIndex = 40;
            this.btnUpload.Text = "FileUPload";
            this.btnUpload.UseVisualStyleBackColor = true;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(647, 6);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(93, 38);
            this.btnSelect.TabIndex = 39;
            this.btnSelect.Text = "FileOpen";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // tbFullName
            // 
            this.tbFullName.Location = new System.Drawing.Point(24, 12);
            this.tbFullName.Name = "tbFullName";
            this.tbFullName.Size = new System.Drawing.Size(608, 25);
            this.tbFullName.TabIndex = 38;
            // 
            // lvData
            // 
            this.lvData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvData.FullRowSelect = true;
            this.lvData.GridLines = true;
            this.lvData.HideSelection = false;
            this.lvData.Location = new System.Drawing.Point(19, 97);
            this.lvData.Name = "lvData";
            this.lvData.Size = new System.Drawing.Size(820, 433);
            this.lvData.TabIndex = 48;
            this.lvData.UseCompatibleStateImageBehavior = false;
            this.lvData.View = System.Windows.Forms.View.Details;
            this.lvData.SelectedIndexChanged += new System.EventHandler(this.lvData_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            this.columnHeader1.Width = 106;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "File NAME";
            this.columnHeader2.Width = 295;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Ext NAME";
            this.columnHeader3.Width = 118;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "File Size";
            this.columnHeader4.Width = 327;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Location = new System.Drawing.Point(868, 67);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(313, 301);
            this.pictureBox1.TabIndex = 49;
            this.pictureBox1.TabStop = false;
            // 
            // btnOut
            // 
            this.btnOut.Location = new System.Drawing.Point(721, 544);
            this.btnOut.Name = "btnOut";
            this.btnOut.Size = new System.Drawing.Size(161, 30);
            this.btnOut.TabIndex = 50;
            this.btnOut.Text = "출력";
            this.btnOut.UseVisualStyleBackColor = true;
            this.btnOut.Click += new System.EventHandler(this.btnOut_Click);
            // 
            // Blob
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1213, 592);
            this.Controls.Add(this.btnOut);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lvData);
            this.Controls.Add(this.btnallsave);
            this.Controls.Add(this.combolist);
            this.Controls.Add(this.btnDB);
            this.Controls.Add(this.comboDBarray);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.btnFoldersSave);
            this.Controls.Add(this.btnFolders);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.tbFullName);
            this.Name = "Blob";
            this.Text = "Blob";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Blob_FormClosed);
            this.Load += new System.EventHandler(this.Blob_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnallsave;
        private System.Windows.Forms.ComboBox combolist;
        private System.Windows.Forms.Button btnDB;
        private System.Windows.Forms.ComboBox comboDBarray;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnFoldersSave;
        private System.Windows.Forms.Button btnFolders;
        private System.Windows.Forms.Button btnUpload;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.TextBox tbFullName;
        private System.Windows.Forms.ListView lvData;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnOut;
    }
}