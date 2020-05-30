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
using System.Data.SqlClient;

namespace 컴퓨터_비전_홍길동_.Volume1
{
    public partial class SQLserver : Form
    {
        // sql 과 c# 연결다리
        String _connStr;//connet할 수 는 단어를 저장해놓습니다.
        SqlConnection _conn;//MySqlConnection - 다리 역할을 할 부분입니다.
        SqlCommand _cmd;//mysql에게 명령을 보내는 부분
                        // sql 과 c# 연결다리
        string[] _file_info = { };//콤보박스
        SqlDataReader _reader; //콤보박스 읽기
        private byte[,] _upDateImage, _InsertImage;//100밝게 이미지 출력하는 부분
        private int _size_send;//이미지 출력하는 부분을 보내는 역할
        private int _f_id_save;// 파일 id를 저장하는 공간 
        private int _MsqOutH, _MsqOutW;//크기 
        private int value_setFlag;

        public SQLserver()
        {
            InitializeComponent();
        }
        private void SQLserver_Load(object sender, EventArgs e)
        {
            //server = 127.0.0.1
            _connStr = "server=127.0.0.1; Uid=sa; pwd=1234; Database=gray_db;";
            try
            {
                //connet string 변수를 받아서 
                _conn = new SqlConnection(_connStr);
                // open를 합니다.
                _conn.Open();
                //cmd는 sql에게 명령을 보내는 것을 초기화해줍니다. 
                _cmd = new SqlCommand("", _conn);

                MessageBox.Show("GOOD 프로젝트 다하면 지우기");
            }
            catch
            {
                MessageBox.Show("연결실패 - 아이디 비번 확인 요망");

            }
        }
        private void SQLserver_FormClosed(object sender, FormClosedEventArgs e)
        {
            _conn.Close();
        }
        private void btn_select_Click(object sender, EventArgs e)
        {
            File_Open();
        }
        private void btn_insert_Click(object sender, EventArgs e)
        {
            sqlInputImage();
        }
        private void btn_sh_Click(object sender, EventArgs e)
        {
            value_setFlag = 1;
            Mrange_Ex();
        }
        private void btnSaveDb_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "raw";
            sfd.Filter = "raw file(*.raw) | *.raw";

            if (sfd.ShowDialog() != DialogResult.OK)
                return;
            string saveFname = sfd.FileName;
            BinaryWriter bw = new BinaryWriter(File.Open(saveFname, FileMode.Create));

            for (int i = 0; i < _MsqOutH; i++)
                for (int k = 0; k < _MsqOutW; k++)
                    bw.Write(_upDateImage[i, k]);

            bw.Close();
            toolStripStatusLabel1.Text = saveFname + "으로 저장됨";

            btnSaveDbs(saveFname, _upDateImage, _MsqOutH, _MsqOutW);
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            endInSearch();           
        }
        private void button1_Click(object sender, EventArgs e)
        {
            brienlarge();
        }

        // Event End

        /// <summary>
        /// 함수 내용 설정
        /// </summary>

        // 공통 함수

        private void InOutChange()
        {
            //Image Size 결정 
            _MsqOutH = _size_send;
            _MsqOutW = _size_send;

            //출력 영상 메모리 할당
            _upDateImage = new byte[_MsqOutH, _MsqOutW];
        } // inimage --> outimage Memory allocation
        private void DisplayImage()
        {
            // 종이, 게시판, 벽 크기 조절
            Bitmap paper = new Bitmap(_MsqOutH, _MsqOutW);
            picBox.Size = new Size(_MsqOutH, _MsqOutW);
            this.Size = new Size(_MsqOutH + 200, _MsqOutW + 300);
            Color pen; // 펜 (콕콕 찍을 펜)

            for (int i = 0; i < _MsqOutH; i++)
                for (int k = 0; k < _MsqOutW; k++)
                {
                    byte data = _upDateImage[i, k];  // 색깔 (잉크)
                    pen = Color.FromArgb(data, data, data); // 펜에 잉크 묻힘
                    paper.SetPixel(k, i, pen); // 종이에 콕 찍음.
                }

            picBox.Image = paper; // 게시판에 종이 걸기
        }//ReDisplay
        private double GetValue()
        {
            double retvalue = 0.0;
            Valuescontrol valueNum = new Valuescontrol(_InsertImage, _size_send, _size_send, value_setFlag);
            valueNum.Owner = this;

            if (valueNum.ShowDialog() == DialogResult.OK)
            {
                retvalue = (double)valueNum.numUpDown.Value;
            }

            return retvalue;
        } // receive a value of Valuescontrol 
        private int ValueCheck()
        {
            int value = (int)GetValue();

            if (value != 0.0)
            {
                return value;
            }
            else
            {
                MessageBox.Show("값을 지정해주세요", "확인", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return 0;
        }//value Check
        private void File_Open()
        {
            //오픈 다이얼로그를 사용
            OpenFileDialog ofd = new OpenFileDialog();
            //저장할 위치 와 파일 확장자 
            ofd.DefaultExt = "raw";
            ofd.Filter = "raw file(*.raw) | *.raw";

            //파일창을 열어주는 곳
            if (ofd.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("파일 입력이 안되었습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //파일 이름을 string 변수로 받아주는 공간
            string full_name = ofd.FileName;
            //내 눈에 보이게 텍스트로 뿌려주는 역할
            tbFullname.Text = ofd.FileName;
            //다 했으면 btn_select을 DialogResult.OK으로 변경합니다.
            btn_select.DialogResult = DialogResult.OK;
        } //file open
        private void sqlInputImage()
        {
            //선택 파일이 저장되어있으면
            if (btn_select.DialogResult == DialogResult.OK)
            {
                //파일네임은 텍스트 박스에서 가져오는 역할 
                string full_name = tbFullname.Text.ToString();
                //파일이 적혀있지 않으면 파일 입력이 안되었습니다. 두번 하네 별로
                if (full_name == "" || full_name == null)
                {
                    MessageBox.Show("파일 입력이 안되었습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int row_num, col_num, pixel;// pixel(바이트)
                                            //C:\Users\NEXT_ENERGY\Desktop\사진\Pet_RAW(squre)\Pet_RAW(128x128)\cat02_128.raw
                                            //파일명 :  cat02_128  확장명 : raw
                string[] tmpArray = full_name.Split('\\');// \기준으로 split 한다
                //tmp1의 값은 마지막 / 다음부터 읽는 것입니다.
                string tmp1 = tmpArray[tmpArray.Length - 1];//\cat02_128.raw
                //이 값을 두개로 쪼개서 사용할 예정입니다.
                string[] tmp2 = tmp1.Split('.');// . 으로 나뉜다
                //파일 네임은 tmp[0]에 넣을 것이고
                string file_name = tmp2[0];//cat02_128
                //확장자는 tmp[1]에 넣을 것입니다.
                string ext_name = tmp2[1];// raw
                //확장자 raw면 return  먹여서 끝내게 합니다.

                if (ext_name != "raw")
                {
                    MessageBox.Show("확장자를  raw로 바꿔주세요");
                    return;
                }
                //파일 FileInfo의 길이? 
                long fsize = new FileInfo(full_name).Length;
                //제곱근으로 사이즈 지정합니다,
                int size = (int)Math.Sqrt(fsize);
                //랜덤함수 사용할 것입니다.
                Random rnd = new Random();
                int f_id = rnd.Next(2147483647) + 1;//중복 제거

                //부모 테이블에 입력하기
                //인설트 부모꺼 쓸거입니다.
                string sql = "INSERT INTO file_table(f_id, file_name, ext_name, size)";
                sql += "VALUES(" + f_id + ",'" + file_name + "', '";
                sql += ext_name + "', " + size + ")";
                _cmd.CommandText = sql;
                _cmd.ExecuteNonQuery();

                //써먹을수 있는가
                BinaryReader br = new BinaryReader(File.Open(full_name, FileMode.Open));
                _cmd = new SqlCommand("", _conn);

                for (int i = 0; i < size; i++)
                    for (int k = 0; k < size; k++)
                    {
                        row_num = i; // 행
                        col_num = k; // 열
                        pixel = (int)br.ReadByte(); // 픽셀               
                        sql = "INSERT INTO pixel_table(f_id, row_num,col_num, pixel) VALUES(";
                        sql += f_id + "," + row_num + "," + col_num + "," + pixel + ")";
                        _cmd.CommandText = sql;
                        _cmd.ExecuteNonQuery();
                    }
                br.Close();
                MessageBox.Show("end");
                tbFullname.Text = "";
            }
            else
            {
                MessageBox.Show("파일 경로를 설정해주세요", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }//sql insert 사용
        private void combDisplay()
        {
            //콤보 박스 초기화 
            Array.Resize(ref _file_info, 0);
            //클리어
            cmBOX.Items.Clear();
            string sql = "SELECT f_id,file_name,size FROM file_table";
            _cmd.CommandText = sql;
            _reader = _cmd.ExecuteReader();//읽어서 가져오기 

            while (_reader.Read())
            {
                string f_id = _reader["f_id"].ToString();
                string file_name = _reader["file_name"].ToString();
                string size = _reader["size"].ToString();

                string totalstr = f_id + "/" + file_name + "/" + size;
                Array.Resize(ref _file_info, _file_info.Length + 1);
                _file_info[_file_info.Length - 1] = totalstr;
            }
            //리더를 여기서 끝내준다.
            _reader.Close();
            //콤보박스에 넣기
            cmBOX.Items.AddRange(_file_info);
        }//
        private void btnSave()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = "raw";
            sfd.Filter = "raw file(*.raw) | *.raw";

            if (sfd.ShowDialog() != DialogResult.OK)
                return;
            string saveFname = sfd.FileName;
            BinaryWriter bw = new BinaryWriter(File.Open(saveFname, FileMode.Create));

            for (int i = 0; i < _MsqOutH; i++)
                for (int k = 0; k < _MsqOutW; k++)
                    bw.Write(_upDateImage[i, k]);

            bw.Close();
            toolStripStatusLabel1.Text = saveFname + "으로 저장됨";            
            btnSaveDbs(saveFname, _upDateImage, _MsqOutH, _MsqOutW);
            tbFullname.Text = "";

        }//저장하는 함수
        private void btnSaveDbs(string File, byte[,] UpdImage, int _MsqOutH, int _MsqOutW)
        {
            int row_num, col_num, pixel;// pixel(바이트)
                                        //C:\Users\NEXT_ENERGY\Desktop\사진\Pet_RAW(squre)\Pet_RAW(128x128)\cat02_128.raw
                                        //파일명 :  cat02_128  확장명 : raw                                                                      
            string[] tmpArray = File.Split('\\');// \기준으로 split 한다
                                                 //tmp1의 값은 마지막 / 다음부터 읽는 것입니다.
            string tmp1 = tmpArray[tmpArray.Length - 1];//\cat02_128.raw
                                                        //이 값을 두개로 쪼개서 사용할 예정입니다.
            string[] tmp2 = tmp1.Split('.');// . 으로 나뉜다
                                            //파일 네임은 tmp[0]에 넣을 것이고
            string file_name = tmp2[0];//cat02_128
                                       //확장자는 tmp[1]에 넣을 것입니다.
            string ext_name = tmp2[1];// raw
                                      //확장자는 tmp[1]에 넣을 것입니다.
                                      //확장자 raw면 return  먹여서 끝내게 합니다.
            if (ext_name != "raw")
            {
                MessageBox.Show("확장자를  raw로 바꿔주세요");
                return;
            }

            //랜덤함수 사용할 것입니다.
            Random rnd = new Random();
            int f_id = rnd.Next(2147483647) + 1;//중복 제거

            //부모 테이블에 입력하기
            //인설트 부모꺼 쓸거입니다.
            string sql = "INSERT INTO file_table(f_id, file_name, ext_name, size)";
            sql += "VALUES(" + f_id + ",'" + file_name + "', '";
            sql += ext_name + "', " + _MsqOutH + ")";
            _cmd.CommandText = sql;
            _cmd.ExecuteNonQuery();

            _cmd = new SqlCommand("", _conn);

            for (int i = 0; i < _MsqOutH; i++)
                for (int k = 0; k < _MsqOutW; k++)
                {
                    row_num = i; // 행
                    col_num = k; // 열
                    pixel = (int)UpdImage[i, k]; // 픽셀               
                    sql = "INSERT INTO pixel_table(f_id, row_num,col_num, pixel) VALUES(";
                    sql += f_id + "," + row_num + "," + col_num + "," + pixel + ")";
                    _cmd.CommandText = sql;
                    _cmd.ExecuteNonQuery();
                }
            MessageBox.Show("end");
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            endInSearch();
        }// End-In Search
        private void btn_db_list_Click(object sender, EventArgs e)
        {
            combDisplay();
        }
        private void cmBOX_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectstr = cmBOX.SelectedItem.ToString();
            int f_id = int.Parse(selectstr.Split('/')[0]);
            int size = int.Parse(selectstr.Split('/')[2]);

            _size_send = size; //밝게하기 사이즈
            string sql = "SELECT row_num, col_num, pixel FROM pixel_table WHERE f_id =";
            sql += f_id;
            _f_id_save = f_id; //변수 저장

            _cmd.CommandText = sql;
            _reader = _cmd.ExecuteReader();
            Bitmap paper = new Bitmap(size, size);
            picBox.Size = new Size(size, size);
            _InsertImage = new byte[size, size];
            Color pen;
            int r, g, b;

            while (_reader.Read())
            {
                int row = int.Parse(_reader["row_num"].ToString());
                int col = int.Parse(_reader["col_num"].ToString());
                r = g = b = int.Parse(_reader["pixel"].ToString());
                int data = int.Parse(_reader["pixel"].ToString());
                _InsertImage[row, col] = (byte)data;

                pen = Color.FromArgb(r, g, b);
                paper.SetPixel(col, row, pen);
            }

            _reader.Close();
            picBox.Image = paper;
        }//cmBox 내의 값을 넣어주는 넣어서 output 

        // 공통 함수 End


        //영상 처리 함수

        private void Mrange_Ex()
        {
            int value = ValueCheck();

            if (value == 0)
            {
                return;
            }
            else if (value > -100 && value < 100)
            {
                InOutChange();
                int UnderOverFlow_num;
                //영상 처리
                for (int i = 0; i < _size_send; i++)
                    for (int k = 0; k < _size_send; k++)
                    {
                        UnderOverFlow_num = _InsertImage[i, k] + (100);
                        if (UnderOverFlow_num > 255)
                        {
                            _upDateImage[i, k] = 255;
                        }
                        else if (UnderOverFlow_num < 0)
                        {
                            _upDateImage[i, k] = 0;
                        }
                        else
                            _upDateImage[i, k] = (byte)UnderOverFlow_num;//화면으로 출력되는 배열에 저장
                    }
                DisplayImage();
            }
        }//
        private void Equal()
        {
            InOutChange();
            //영상 처리 
            for (int i = 0; i < _MsqOutH; i++)
                for (int k = 0; k < _MsqOutW; k++)
                {
                    _upDateImage[i, k] = (byte)_InsertImage[i, k];//화면으로 출력되는 배열에 저장 
                }

            DisplayImage();// 화면 출력
        } //동일영상
        private void endInSearch()
        {
            InOutChange();//화면 크기 구성

            // out = (in - min) / (max - min) * 255
            double max_value = _InsertImage[0, 0];
            double min_value = _InsertImage[0, 0];

            for (int i = 0; i < _MsqOutH; i++)
                for (int k = 0; k < _MsqOutW; k++)
                {
                    if (max_value < _InsertImage[i, k])
                        max_value = _InsertImage[i, k];
                    if (min_value > _InsertImage[i, k])
                        min_value = _InsertImage[i, k];
                }

            min_value += 70; //min_value에는 +30 왜 너무 차이나지 않게  
            max_value -= 40; //max_value에는 -30

            //진짜 영상 처리 알고리즘 사용
            for (int i = 0; i < _MsqOutH; i++)
                for (int k = 0; k < _MsqOutW; k++)
                {
                    int outvalue = (int)(((_InsertImage[i, k] - min_value) / (max_value - min_value)) * 255.0);
                    if (outvalue > 255)
                        _upDateImage[i, k] = 255;
                    else if (outvalue < 0)
                        _upDateImage[i, k] = 0;
                    else
                        _upDateImage[i, k] = (byte)outvalue;
                }
            //출력 메모리  ---> 화면
            DisplayImage();
        } //end In Search
        private void brienlarge()
        {
            byte[,] tmpary = new byte[_size_send, _size_send];

            for (int i = 0; i < _size_send; i++)
                for (int k = 0; k < _size_send; k++)
                {
                    //백워딩 
                    tmpary[i, k] = _upDateImage[i, k];
                }

            _MsqOutH = _size_send * 2; //출력영상의 크기를 결정
            _MsqOutW = _size_send * 2;
            // int value = 대화상자에서 입력받은 숫자 

            //출력 영상 메모리 할당
            _upDateImage = new byte[_MsqOutH, _MsqOutW];
            //진짜 영상 처리 알고리즘 사용
            for (int i = 0; i < _MsqOutH; i++)
                for (int k = 0; k < _MsqOutW; k++)
                {
                    //백워딩 
                    _upDateImage[i, k] = tmpary[i / 2, k / 2];
                }

            //출력 메모리  ---> 화면
            DisplayImage();
        }

        //영상 처리 함수
    }
}
