using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MySql.Data.MySqlClient;

namespace 컴퓨터_비전_홍길동_.Volume1
{
    public partial class Blob : Form
    {
        String connStr;
        MySqlConnection conn;
        MySqlCommand cmd;

        public Blob()
        {
            InitializeComponent();
        }

        private void Blob_Load(object sender, EventArgs e)
        {
            connStr = "Server=127.0.0.1; Uid=root; Pwd=1234;Database=blockDB;CHARSET=UTF8";
            conn = new MySqlConnection(connStr);
            conn.Open();

            cmd = new MySqlCommand("", conn);
            //콤보 박스 초기화 
            Array.Resize(ref _file_info, 0);
            //클리어
            comboDBarray.Items.Clear();
            string sql = "SELECT f_id, file_name, ext_name, fileSize FROM blob_table";
            cmd.CommandText = sql;
            MySqlDataReader reader; //콤보박스 읽기
            reader = cmd.ExecuteReader();//읽어서 가져오기 

            while (reader.Read())
            {
                string f_id = reader["f_id"].ToString();
                string file_name = reader["file_name"].ToString();
                string ext_name = reader["ext_name"].ToString();
                string size = reader["fileSize"].ToString();

                ListViewItem lvi = new ListViewItem(new string[] { f_id,file_name,ext_name, size});
                this.lvData.Items.Add(lvi);
            }
            //리더를 여기서 끝내준다.
            reader.Close();
            //콤보박스에 넣기
            conn.Close();
        }

        private void Blob_FormClosed(object sender, FormClosedEventArgs e)
        {
            conn.Close();
        }


        //파일 오픈
        private void btnSelect_Click(object sender, EventArgs e)
        {
            tbFullName.Text = "";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            string full_name = ofd.FileName;
            tbFullName.Text = full_name;
            tbFullName.Tag = full_name;
        }

        int fsopen;    // 폴더채로 열었을때 구분변수
        string[] fbd_name;  // 폴더안에있는 파일들

        private void btnFolders_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("파일 열기를 실행하시겠습니까?", "열기",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (dr != DialogResult.OK)
            {
                return;
            }

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                fsopen = 1;
                fbd_name = Directory.GetFiles(fbd.SelectedPath);
                tbFullName.Text = fbd.SelectedPath;
            }

            foreach (string filename in fbd_name)
            {
                combolist.Items.AddRange(fbd_name);
            }

            btnFolders.DialogResult = DialogResult.OK;
        }
        //파일 오픈


        //업로드
        private void btnUpload_Click(object sender, EventArgs e)
        {
            conn.Open();
            cmd = new MySqlCommand("", conn);
            string full_name = tbFullName.Text.ToString();
            string[] tmpArray = full_name.Split('\\');
            string tmp1 = tmpArray[tmpArray.Length - 1];
            string[] tmp2 = tmp1.Split('.');
            string file_name = tmp2[0];
            string ext_name = tmp2[1];
            long fsize = new FileInfo(full_name).Length;
            Random rnd = new Random();
            int f_id = rnd.Next(0, 2147483647);

            // 부모 테이블(파일 테이블)에 입력

            string sql = "INSERT INTO blob_table(f_id,file_name,ext_name,fileSize,fileData)";
            sql += "VALUES(" + f_id + ",'" + file_name + "', '";
            sql += ext_name + "', " + fsize + ", @FileData)";//@FileData는 변수입니다.

            //파일을 통째로 @FileData에 넣기

            FileStream fs = new FileStream(full_name, FileMode.Open, FileAccess.Read);
            byte[] fileData = new byte[fsize]; //선언
            fs.Read(fileData, 0, (int)fsize);//하나씩 처리하는 것이 아닌 통째로 쓰는 것입니다. 
            fs.Close();

            cmd.Parameters.AddWithValue("@FileData", fileData);
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            MessageBox.Show("업로드");
            conn.Close();
            tbFullName.Text = "";
        }

        private void btnFoldersSave_Click(object sender, EventArgs e)
        {
            if (btnFolders.DialogResult != DialogResult.OK)
            {
                MessageBox.Show("폴더를 선택해주세요", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int fileCount = 1;
            if (fsopen == 1)
            {
                foreach (string filename in fbd_name)
                {
                    MysqlInputImageall(filename, fileCount);
                    fileCount++;
                }
                return;
            }
        }

        private void MysqlInputImageall(string FileName, int FileCount)
        {
            conn.Open();
            cmd = new MySqlCommand("", conn);
            string full_name = FileName;
            string[] tmpArray = full_name.Split('\\');
            string tmp1 = tmpArray[tmpArray.Length - 1];
            string[] tmp2 = tmp1.Split('.');
            string file_name = tmp2[0];
            string ext_name = tmp2[1];
            long fsize = new FileInfo(full_name).Length;
            int size = (int)Math.Sqrt(fsize);
            double size_check = Math.Sqrt(fsize);
            double fLength = new FileInfo(full_name).Length;//정사각형 분별

            MySqlDataReader reader; //콤보박스 읽기

            //f_id 를 DB에서 검색하고 배열에 넣어서 그 최대값을 찾아 f_id에 +1한 값을 넣어줌
            int[] check_arr = { };
            Array.Resize(ref check_arr, 0);
            int f_id;
            string sql = "SELECT f_id FROM blob_table";
            cmd.CommandText = sql;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string f_id_buf = reader["f_id"].ToString();
                if (f_id_buf == "") return;
                f_id = int.Parse(f_id_buf);
                Array.Resize(ref check_arr, check_arr.Length + 1);
                check_arr[check_arr.Length - 1] = f_id;
            }
            f_id = check_arr.Max() + 1;
            reader.Close();

            // 부모 테이블(파일 테이블)에 입력
            sql = "INSERT INTO blob_table(f_id, file_name, ext_name, fileSize,fileData)";
            sql += "VALUES(" + f_id + ",'" + file_name + "', '";
            sql += ext_name + "', " + fsize + ", @FileData)";//@FileData는 변수입니다.

            //파일을 통째로 @FileData에 넣기
            FileStream fs = new FileStream(full_name, FileMode.Open, FileAccess.Read);
            byte[] fileData = new byte[fsize]; //선언
            fs.Read(fileData, 0, (int)fsize);//하나씩 처리하는 것이 아닌 통째로 쓰는 것입니다. 
            fs.Close();

            cmd.Parameters.AddWithValue("@FileData", fileData);
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("( " + FileCount + " )" + "--- UPLOAD SUCCESS!");
        }//sql insert 사용(파일 전부)
         //업로드

        //다운로드
        private void btnDownload_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("이 파일을 저장시키겠습니까?", "저장",
                               MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (dr != DialogResult.OK)
            {
                return;
            }

            int indxnum = lvData.FocusedItem.Index;
            int f_id = int.Parse(lvData.Items[indxnum].SubItems[0].Text.ToString());

            conn.Open();
            cmd = new MySqlCommand("", conn);
            MySqlDataReader reader;

            String sql = "SELECT f_id, file_name, ext_name, fileSize, fileData FROM blob_table WHERE f_id =" + f_id;

            cmd.CommandText = sql;
            reader = cmd.ExecuteReader();
            reader.Read(); // 첫번째 하나만 읽기.

            string file_name = reader["file_name"].ToString();
            string ext_name = reader["ext_name"].ToString();
            int file_size = int.Parse(reader["fileSize"].ToString());

            byte[] file_data = new byte[file_size];
            reader.GetBytes(reader.GetOrdinal("fileData"), 0, file_data, 0, file_size);

            string full_name = "C:\\images\\" + file_name + "." + ext_name;

            FileStream fs = new FileStream(full_name, FileMode.OpenOrCreate, FileAccess.Write);
            fs.Write(file_data, 0, (int)file_size);
            fs.Close();

            conn.Close();
            MessageBox.Show("다운로드 OK");
   
        }
        private void btnallsave_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("이 파일을 저장시키겠습니까?", "저장",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

            if (dr != DialogResult.OK)
            {
                return;
            }

            conn.Open();
            cmd = new MySqlCommand("", conn);
            MySqlDataReader reader;

            String sql = "SELECT f_id, file_name, ext_name, fileSize, fileData FROM blob_table";
            cmd.CommandText = sql;
            reader = cmd.ExecuteReader();

            while (reader.Read())  // 첫번째 하나만 읽기.
            {
                string file_name = reader["file_name"].ToString();
                string ext_name = reader["ext_name"].ToString();
                int file_size = int.Parse(reader["fileSize"].ToString());

                byte[] file_data = new byte[file_size];
                reader.GetBytes(reader.GetOrdinal("fileData"), 0, file_data, 0, file_size);

                string full_name = "C:\\images\\" + file_name + "." + ext_name;

                FileStream fs = new FileStream(full_name, FileMode.OpenOrCreate, FileAccess.Write);
                fs.Write(file_data, 0, (int)file_size);
                fs.Close();
            }

            conn.Close();
            MessageBox.Show("다운로드 OK");
            
        }
        private void comboDBarray_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectstr = comboDBarray.SelectedItem.ToString();

            int f_id = int.Parse(selectstr.Split('/')[0]);
            int size = int.Parse(selectstr.Split('/')[2]);

            conn.Open();
            cmd = new MySqlCommand("", conn);
            MySqlDataReader reader;

            String sql = "SELECT f_id, file_name, ext_name, fileSize, fileData FROM blob_table WHERE f_id =" + f_id;

            cmd.CommandText = sql;
            reader = cmd.ExecuteReader();
            reader.Read(); // 첫번째 하나만 읽기.
            string file_name = reader["file_name"].ToString();
            string ext_name = reader["ext_name"].ToString();
            int file_size = int.Parse(reader["fileSize"].ToString());

            byte[] file_data = new byte[file_size];
            reader.GetBytes(reader.GetOrdinal("fileData"), 0, file_data, 0, file_size);

            string full_name = "C:\\images\\" + file_name + "." + ext_name;

            FileStream fs = new FileStream(full_name, FileMode.OpenOrCreate, FileAccess.Write);
            fs.Write(file_data, 0, (int)file_size);
            fs.Close();

            conn.Close();
            MessageBox.Show("다운로드 OK");
        }
        private void btnDB_Click(object sender, EventArgs e)
        {
            combDisplay();
        }
        //함수

        string[] _file_info = { };//콤보박스

        private void combDisplay()
        {
            conn.Open();
            cmd = new MySqlCommand("", conn);
            //콤보 박스 초기화 
            Array.Resize(ref _file_info, 0);
            //클리어
            comboDBarray.Items.Clear();
            string sql = "SELECT f_id, file_name, ext_name, fileSize, fileData FROM blob_table WHERE ext_name ='raw'";
            cmd.CommandText = sql;
            MySqlDataReader reader; //콤보박스 읽기
            reader = cmd.ExecuteReader();//읽어서 가져오기 

            while (reader.Read())
            {
                string f_id = reader["f_id"].ToString();
                string file_name = reader["file_name"].ToString();
                string size = reader["fileSize"].ToString();

                string totalstr = f_id + "/" + file_name + "/" + size;
                Array.Resize(ref _file_info, _file_info.Length + 1);
                _file_info[_file_info.Length - 1] = totalstr;
            }
            //리더를 여기서 끝내준다.
            reader.Close();
            //콤보박스에 넣기
            comboDBarray.Items.AddRange(_file_info);
            conn.Close();
        }//
        int _inWC, _inHC;

        private void lvData_SelectedIndexChanged(object sender, EventArgs e)
        {         
            conn.Open();
            cmd = new MySqlCommand("", conn);
            MySqlDataReader reader;
            Bitmap images;
            string size = "";
            int rsize;

            int indxnum = lvData.FocusedItem.Index;
            int f_id = int.Parse(lvData.Items[indxnum].SubItems[0].Text.ToString());

            string sql = "SELECT fileData,fileSize FROM blob_table WHERE f_id = " + f_id + ";";
            cmd.CommandText = sql;
            reader = cmd.ExecuteReader();
            byte[] bImage = null;

            while (reader.Read())
            {
                bImage = (byte[])reader[0];
                size = reader["fileSize"].ToString();
            }
            if (bImage != null)
            {
                
                System.Threading.Thread.Sleep(100);
                rsize = (int)Math.Sqrt(int.Parse(size)); //입력 영상 메모리 할당 
                pictureBox1.Size = new System.Drawing.Size(rsize, rsize);
                pictureBox1.Image = new Bitmap(new MemoryStream(bImage));
            }
            reader.Close();
            conn.Close();

        }

        private void lvData_ItemMouseHover(object sender, ListViewItemMouseHoverEventArgs e)
        {
            Delay(300);
            conn.Open();
            cmd = new MySqlCommand("", conn);
            MySqlDataReader reader;
            Bitmap images;
            string size = "";
            int rsize;

            int indxnum = lvData.FocusedItem.Index;
            int f_id = int.Parse(lvData.Items[indxnum].SubItems[0].Text.ToString());

            string sql = "SELECT fileData,fileSize FROM blob_table WHERE f_id = " + f_id + ";";
            cmd.CommandText = sql;
            reader = cmd.ExecuteReader();
            byte[] bImage = null;

            while (reader.Read())
            {
                bImage = (byte[])reader[0];
                size = reader["fileSize"].ToString();
            }
            if (bImage != null)
            {
                Delay(1000);
                rsize = (int)Math.Sqrt(int.Parse(size)); //입력 영상 메모리 할당 
                pictureBox1.Size = new System.Drawing.Size(rsize, rsize);
                pictureBox1.Image = new Bitmap(new MemoryStream(bImage));
            }
            reader.Close();
            conn.Close();

        }

        private void btnOut_Click(object sender, EventArgs e)
        {          
            conn.Open();
            cmd = new MySqlCommand("", conn);
            MySqlDataReader reader;
            Bitmap images;
            string size="";
            int rsize;

            int indxnum = lvData.FocusedItem.Index;
            int f_id = int.Parse(lvData.Items[indxnum].SubItems[0].Text.ToString());

            string sql = "SELECT fileData,fileSize FROM blob_table WHERE f_id = " + f_id + ";";
            cmd.CommandText = sql;
            reader = cmd.ExecuteReader();
            byte[] bImage = null;
            while (reader.Read())
            {
                bImage = (byte[])reader[0];
                size = reader["fileSize"].ToString();           
            }
            if (bImage != null)
            {
                rsize = (int)Math.Sqrt(int.Parse(size)); //입력 영상 메모리 할당 
                pictureBox1.Size = new System.Drawing.Size(rsize , rsize);
                pictureBox1.Image = new Bitmap(new MemoryStream(bImage));                
            }
            reader.Close();
            conn.Close();
        }
        //다운로드

        private static DateTime Delay(int MS)
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)
            {
                System.Windows.Forms.Application.DoEvents();
                ThisMoment = DateTime.Now;
            }

            return DateTime.Now;
        }
    }
}
