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
using System.Drawing.Drawing2D;
using OpenCvSharp;


namespace 컴퓨터_비전_홍길동_.Volume1
{
    public partial class MainForm : Form
    {
        //File upload(Input) -> inimage -> outimage -> Output (flow)
        // _(underbar)로 Global variable Checking


        private bool rawColorSelect; // raw 파일과 color(png,jpg) 선택하면 각 이미지에 맞게 변환

        //raw                    

        private byte[,] _inImage, _outImage; // input output 설정
        private int _inH, _inW, _outH, _outW; // IO Video Size
        private string _filename = "";
        private Bitmap _paper; // Output   
        bool convert_image2 = false;
       

        Mat inCvImage, outCvimage;

        //color

        byte[,,] _inImageColor, _outImageColor; //color image 출력할 위치
        byte[,] _inImageraw, _outImageraw; //color image 출력할 위치
        int _inHC, _inWC, _outHC, _outWC; ////color IO Video Size


        //좌표 부분 변환을 하기 위한 변수 선언 

        int sx, sy, ex, ey;
        bool mouseYn = false;
        int callfunction; // 마우스 클릭으로 변화주기 

        private int value_setFlag;//종류 flag
        // value_setFlag Number 
        // 1 : Micro Range Exposue
        // 2 : Exposure
        // 3 : Move(select)

        public MainForm()
        {
            InitializeComponent();         
        }

        //Menu-bar Start    
        // File Menu      

        private void 열기ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            File_Open();
        }//Open(O)
        private void 새로만들기ToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }//New page..
        private void 종료ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("저정되지 않는 파일이 있습니다. 저장하시겠습니까","저장",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
            Close();
        }//Exit(E)
        private void BtnFileOpen_Click(object sender, EventArgs e)
        {
            File_Open();
        }//btn_Open File
        private void Button2_Click(object sender, EventArgs e)
        {
            Equal();
            GraphEnd();
        }//btn_Same
        private void Chbright_Click(object sender, EventArgs e)
        {
            chbright.Visible = false;
        }

        // File Menu End

        //Pixel Processing Menu 

        private void 동일영상ToolStripMenuItem_Click(object sender, EventArgs e)
        {          
            Equal();
        } //Equal
        private void MicroRangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            value_setFlag = 1; 
            Mrange_Ex();
        }//Micro Range Exposue
        private void StrongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            value_setFlag = 2;
            Strongweak_Bt();
        }//Strong Exposure
        private void BKBasicToolStripMenuItem_Click(object sender, EventArgs e)
        {           
            MonoChromeBasic();
        }//MonoChrome Conversion - Basic
        private void AverageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MonoChromeavr();
        }//MonoChrome Conversion - average
        private void MedianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MonoChromemdi();
        }//MonoChrome Conversion - median
        private void CapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PalabollaCap();
        }//Palabolla_cap
        private void CupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PalabollaCup();
        }//Palabolla_Cup
        private void PostrizingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Postrizing();
        }//postrizing
        private void NegativeTransFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Negative_TransForm();
        }// Negative_TransForm   

        //Pixel Processing Menu End

        // Geometry Processing 

        private void LeftAndRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LfMirroring();
        }//Mirroring -> left And right
        private void UpAndDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpDoMirroring();
        }//Mirroring -> Up And Down
        private void DegreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RightAngle();
        }// 90 degree
        private void CenterRotateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CtRotation();
        }//Center Roation
        private void BasicToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Rotation();
        }
        private void MoveselectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            value_setFlag = 3;
            ImgMove();
        }//image move
        private void EnlargerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Enlag_img();
        }//Enlargement
        private void DownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Reducimg();
        }// reduction 

        // Geometry Processing End

        // Convolution

        private void BasicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BlurBasic();
        }//blur - Basic
        private void SelectBlurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Blurselect();
        }//blur - Select
        private void EmbossingToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Embossing();
        }
        private void DOGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DOG();
        }// DOG
        private void LOGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LOG();
        }//LOG
        private void VerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VerEdge();
        }//vertical Edge
        private void HorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HoriEdge();
        }//horizontal Edge
        private void DifferentialRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DifferRow();
        }// Differential Row 
        private void DifferentialColToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Differcol();
        }//Differential col
        private void OnHomogenOperatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnOpr();
        }//onHomogenOperator
        private void Mask1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShMsk();
        }//Sharpening Mask(1)
        private void Mask2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShMskTwo();
        }//Sharpening Mask(2)

        // Convolution End

        // Histogram 

        private void BrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Drawchart();
        }//Brightness chart 
        private void BtnGraph_Click(object sender, EventArgs e)
        {
            Drawchart();
        }//Brightness chart 
        private void HistogramStretchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HistoStr();
        }// Histogram Stretch
        private void EndInSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EndInSearch();
        }//End In Search
        private void 평활화ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HistoStrequal();
        }

        // Histogram End 

        // Database 

        private void BtnHistogrm_Click(object sender, EventArgs e)
        {
            btnMysql.Visible = true;
            btnsqlserver.Visible = true;
            btnMouseSelect.Location = new System.Drawing.Point(-2, 295+(btnMysql.Height/3)-9);
        }
        private void BtnMysql_Click(object sender, EventArgs e)
        {
            Mysqlcon msl = new Mysqlcon();
            msl.ShowDialog();//sql 창띄우기
        }//mysql 창 띄우기
        private void Btnsqlserver_Click(object sender, EventArgs e)
        {
            SQLserver sqls = new SQLserver();
            sqls.ShowDialog();
        }//sqlserver 창 띄우기
        private void button1_Click_1(object sender, EventArgs e)
        {
            Blob b = new Blob();
            b.ShowDialog();
        }//blob 설계

        // Database End

        //select 

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {

            if (!mouseYn)
                return;
            ex = e.X;
            ey = e.Y;

            //클릭의 위치가 start --> end 위치를 동일하게 하기 위한 swap
            if (sx > ex)
            {
                int tmp = sx;
                sx = ex;
                ex = tmp;
            }
            if (sy > ey)
            {
                int tmp = sy;
                sy = ey;
                ey = tmp;
            }

            //각 변환 tool
            switch (callfunction)
            {
                case 1: Bright_mouse(); break;
                case 2: MonoChromemdi_mouse(); break;
                case 3: Negative_TransForm_mouse(); break;
                case 4: PalabollaCup_mouse(); break;
                case 5: //GAMMA_mouse(); break;
                case 6: VerEdge_mouse(); break;
                case 7: HoriEdge_mouse(); break;
                case 8: Differcol_mouse(); break;
                case 9: DifferRow_mouse(); break;
                case 10: HistoStr_mouse(); break;
                case 11: EndInSearch_mouse(); break;
                case 12: DOG_mouse(); break;
                case 13: LOG_mouse(); break;
            }
            
            mouseYn = false; // picturebox 선택 툴을 끄기 위한 작업
        }
        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!mouseYn)
                return;
            sx = e.X;
            sy = e.Y;
        }
        private void BtnMouseSelect_Click(object sender, EventArgs e)
        {
            mouseYn = true;
            SelectColorChange scc = new SelectColorChange();
            scc.ShowDialog();
            callfunction = scc.value;
        }

        //OpenCv 

        private void 색상추출ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getColorCv();
        }
        private void 코너추출ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            conerCV();
        }
        private void 회전ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rotateCV();
        }
        private void 이진화ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            binaryCode();
        }
        private void 이진화적응형ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            binaryCode2();
        }
        private void 밝게ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            shine();
        }
        private void 모폴로지ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mopolo();
        }
        private void 경계추출ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sideLine();
        }
        private void 원검출ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CircleS();
        }

        //Menu-bar End 

        //Common Function start

        private void File_Open()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            _filename = ofd.FileName; // 파일 크기 알아내기
            string full_name = _filename;
            string[] tmpArray = full_name.Split('\\');
            string tmp1 = tmpArray[tmpArray.Length - 1];
            string[] tmp2 = tmp1.Split('.');            
            string ext_name = tmp2[1];

            if (ext_name == "raw") // raw인지 color 파일인지 구분
                rawColorSelect = true;
            else
                rawColorSelect = false;

            if(rawColorSelect)
            {
                long fSize = new FileInfo(_filename).Length; // 입력 영상 크기 결정
                _inH = _inW = (int)Math.Sqrt((double)fSize); //입력 영상 메모리 할당            
                _inImage = new byte[_inH, _inW];// 파일 --> 메모리(배열)
                BinaryReader br = new BinaryReader(File.Open(_filename, FileMode.Open));// 파일 --> 입력 메모리     

                //input --> output 할당
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        _inImage[i, k] = br.ReadByte();//업로드된 파일 _inImage에 저장

                    }

                br.Close();//BinaryReader 종료
                Equal();
            }
            else
            {
                inCvImage = Cv2.ImRead(_filename); 
                Cv2.Transpose(inCvImage, inCvImage);
                _inHC = inCvImage.Height;
                _inWC = inCvImage.Width;


                _inImageColor = new byte[3, _inHC, _inWC];

                // opencv matrix에서 추출한 우리 배열 쓰기
                //파일 --> 비트맵(image) --> 배열로 값을 로딩하기 
                for (int i = 0; i < _inHC; i++)
                    for (int k = 0; k < _inWC; k++)
                    {
                        var c = inCvImage.At<Vec3b>(i, k); //여기서 한점에 들어갈 rgb 필요
                        _inImageColor[0, i, k] = c.Item2;
                        _inImageColor[1, i, k] = c.Item1;
                        _inImageColor[2, i, k] = c.Item0;
                    }
          
                Equal();

                //bitmap으로 출력할 경우 사용

                //_image = new Bitmap(_filename);  //파일 --> 비트맵(image)
                //                                // **중요 * *--> 영상 크기 할당
                //_inWC = _image.Height;
                //_inHC = _image.Width;
                ////높이 크기가 달라질 수 있으니깐 여기서 바꾸자

                //_inImageColor = new byte[3, _inHC, _inWC];

                ////파일 --> 비트맵(image) --> 배열로 값을 로딩하기 
                //for (int i = 0; i < _inHC; i++)
                //    for (int k = 0; k < _inWC; k++)
                //    {
                //        Color c = _image.GetPixel(i, k); //여기서 한점에 들어갈 rgb 필요
                //        _inImageColor[0, i, k] = c.R;
                //        _inImageColor[1, i, k] = c.G;
                //        _inImageColor[2, i, k] = c.B;
                //    }

            }
        }//Open File
        private void 저장ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            fileCheck();////파일확인 후 구현할 것
            SaveFileDialog sfd = new SaveFileDialog
            {
                DefaultExt = "raw",
                Filter = "raw file(*.raw) | *.raw"
            };

            if (sfd.ShowDialog() != DialogResult.OK)
                return;
            string saveFname = sfd.FileName;
            BinaryWriter bw = new BinaryWriter(File.Open(saveFname, FileMode.Create));

            for (int i = 0; i < _outH; i++)
                for (int k = 0; k < _outW; k++)
                    bw.Write(_outImage[i, k]);

            bw.Close();
            toolStripStatusLabel1.Text = saveFname + "으로 저장됨";
        }//Save File                                저장 수정
        private void fileCheck()
        {
            if (_filename == "" || _filename == null)
                return;
        }//File Checking
        public void DisplayImage()
        {
            int outpicture_H;
            int outpicture_W;
            int hop = 1;
            Color pen; // 펜 준비
            int cntreverse = 0;

            if (rawColorSelect)
            {
                _paper = new Bitmap(_outH, _outW); //Bitmap size 설정
                outpicture_H = _outH;
                outpicture_W = _outW;
            }           
            else
            {
                outpicture_W = _outWC;
                outpicture_H = _outHC;

                if (_outHC > 512 || _outWC > 512)
                {
                    if (_outWC > _outHC)
                        hop = (int)(_outWC / 512);
                    else
                        hop = (int)(_outHC / 512);

                    outpicture_W = _outWC / hop; outpicture_H = _outHC / hop;
                }
                _paper = new Bitmap(outpicture_H, outpicture_W); //Bitmap size 설정        
            }
            
            if (rawColorSelect)
            {
                pictureRervers(outpicture_H, outpicture_W);     
            }
            else
            {
                cntreverse++;
                pictureBox1.Size = new System.Drawing.Size(outpicture_H, outpicture_W); //form size 설정   
                gbPicture1.Size = new System.Drawing.Size(outpicture_H + 30, outpicture_W + 30); //group size 설정    
                
                if(pictureBox1.Visible && !mouseYn && cntreverse >= 2)
                {
                    cntreverse = 0;
                    pictureBox2.Size = new System.Drawing.Size(outpicture_H, outpicture_W); //form size 설정   
                    gbPicture2.Size = new System.Drawing.Size(outpicture_H + 30, outpicture_W + 30); //group size 설정    
                    gbPicture2.Visible = true;
                    pictureBox2.Visible = true;
                }                  
            }

            if(rawColorSelect)
            {
                for (int i = 0; i < _outH; i++)
                    for (int k = 0; k < _outW; k++)
                    {
                        byte data = _outImage[i, k];
                        pen = Color.FromArgb(data, data, data);
                        _paper.SetPixel(k, i, pen);
                    }

                if (chbright.Visible == true)
                {
                    Drawchart();
                }      

                if(gbPicture1.Visible)
                {
                    this.Size = new System.Drawing.Size(outpicture_H + outpicture_H + 270, outpicture_W + 130);//(form size) 
                    gbPicture2.Visible = true;
                    pictureBox2.Visible = true;
                    pictureBox2.Image = _paper;  // form에 Display imageResult
                }
                else
                {
                    this.Size = new System.Drawing.Size(outpicture_H + 270, outpicture_W + 130);//(form size) 
                    gbPicture1.Visible = true;
                    pictureBox1.Visible = true;
                    pictureBox1.Image = _paper;  // form에 Display imageResult
                    pictureBox2.Image = _paper;  //
                }
                           
                _outH = outpicture_H;
                _outW = outpicture_W;
            }
            else
            {
                for (int i = 0; i < outpicture_H; i++)
                    for (int k = 0; k < outpicture_W; k++)
                    {
                        if (i >= outpicture_H - 1 || k >= outpicture_W - 1)
                            continue;

                        byte dataR = _outImageColor[0, i * hop, k * hop];  // 색깔 (잉크)
                        byte dataG = _outImageColor[1, i * hop, k * hop];  // 색깔 (잉크)
                        byte dataB = _outImageColor[2, i * hop, k * hop];  // 색깔 (잉크)
                        pen = Color.FromArgb(dataR, dataG, dataB); // 펜에 잉크 묻힘
                        _paper.SetPixel(i, k, pen); // 종이에 콕 찍음.
                    }
                
                this.Size = new System.Drawing.Size(outpicture_H + 270, outpicture_W + 130);//(form size) 
                gbPicture1.Visible = true;
                pictureBox1.Visible = true;
                pictureBox1.Image = _paper;
                
                _outH = outpicture_H;
                _outW = outpicture_W;
            }
            
            toolStripStatusLabel1.Text = "영상크기 : " + _outH + "x" + _outW;
        }//Result Display     
        private double GetValue()
        {          
            double retvalue = 0.0;
            if(rawColorSelect)
            {
                Valuescontrol valueNum = new Valuescontrol(_inImage, _inH, _inW, value_setFlag);

                if (valueNum.ShowDialog() == DialogResult.OK)
                {
                    retvalue = (double)valueNum.numUpDown.Value;
                }
            }           
            else
            {
                ColorValueControl cvc = new ColorValueControl(_inImageColor, _inHC, _inWC, value_setFlag);

                if (cvc.ShowDialog() == DialogResult.OK)
                {
                    retvalue = (double)cvc.numUpDown.Value;
                }
            }

            return retvalue;
        } // receive a value of Valuescontrol     
        private void InOutChange()
        {
            if(rawColorSelect)
            {
                //Image Size 결정 
                _outH = _inH;
                _outW = _inW;

                //출력 영상 메모리 할당
                _outImage = new byte[_outH, _outW];
            }
            else
            {
                _outHC = _inHC;
                _outWC = _inWC;

                _outImageColor = new byte[3, _outHC, _outWC];
            }
         
        } // inimage --> outimage Memory allocation(raw)    
        private int CheckRange(int i)
        {
            if (i < 0) return 0;
            if (i > 255) return 255;
            return i;
        }//overflow & underflow checking
        private void pictureRervers(int outpicture_H,int outpicture_W)
        {
            if(gbPicture1.Visible)
            {
                pictureBox2.Size = new System.Drawing.Size(outpicture_H, outpicture_W);//form size 설정     
                gbPicture2.Size = new System.Drawing.Size(outpicture_H + 50, outpicture_W + 50);

                this.Size = new System.Drawing.Size(outpicture_H + outpicture_H + 220, outpicture_W + 130);//(form size) +160 +100

                //if (pictureBox1.Size.Height > 1000)
                if (pictureBox1.Size.Width >= 512)
                {
                    gbPicture2.Location = new System.Drawing.Point(720, 40);
                }
                else if (pictureBox1.Size.Width >= 256 && outpicture_H < 512)
                {
                    gbPicture2.Location = new System.Drawing.Point(470, 40);
                }
                else if (pictureBox1.Size.Width >= 128 && outpicture_H < 256)
                {
                    gbPicture2.Location = new System.Drawing.Point(350, 40);
                }
                else if (pictureBox1.Size.Width <= 64)
                {
                    gbPicture2.Font = new Font("Gulim", 7);
                    gbPicture2.Location = new System.Drawing.Point(280, 40);
                }
            }
            else
            {
                pictureBox1.Size = new System.Drawing.Size(outpicture_H, outpicture_W); //form size 설정   
                gbPicture1.Size = new System.Drawing.Size(outpicture_H + 30, outpicture_W + 30); //group size 설정  

 
                if (pictureBox1.Size.Width >= 512)
                {
                    gbPicture2.Location = new System.Drawing.Point(720, 40);
                }
                else if (pictureBox1.Size.Width >= 256 && outpicture_H < 512)
                {
                    gbPicture2.Location = new System.Drawing.Point(470, 40);
                }
                else if (pictureBox1.Size.Width >= 128 && outpicture_H < 256)
                {
                    gbPicture2.Location = new System.Drawing.Point(350, 40);
                }
                else if (pictureBox1.Size.Width <= 64)
                {
                    gbPicture2.Font = new Font("Gulim", 7);
                    gbPicture2.Location = new System.Drawing.Point(280, 40);
                }
            }
        }//pictureBox1과 pictureBox2의 갈림길 
        private void button1_Click(object sender, EventArgs e)
        {
            rawColorSelect ^= true;

            if(rawColorSelect)
            {
                btnRawColor.Text = "RAW";
            }
            else
            {
                btnRawColor.Text = "COLOR";
            }
        } // raw color select 후 변환 작업 실시

        //Common Function End

        //HSV

        private void SaturationControlToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Saturation();
        }
        private void BrightnessControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BirghtnessHSV();
        }
        private void HsvToRgb(double h, double S, double V, out int r, out int g, out int b)
        {
            double H = h;
            while (H < 0) { H += 360; };
            while (H >= 360) { H -= 360; };
            double R, G, B;
            if (V <= 0)
            { R = G = B = 0; }
            else if (S <= 0)
            {
                R = G = B = V;
            }
            else
            {
                double hf = H / 60.0;
                int i = (int)Math.Floor(hf);
                double f = hf - i;
                double pv = V * (1 - S);
                double qv = V * (1 - S * f);
                double tv = V * (1 - S * (1 - f));
                switch (i)
                {

                    // Red is the dominant color

                    case 0:
                        R = V;
                        G = tv;
                        B = pv;
                        break;

                    // Green is the dominant color

                    case 1:
                        R = qv;
                        G = V;
                        B = pv;
                        break;
                    case 2:
                        R = pv;
                        G = V;
                        B = tv;
                        break;

                    // Blue is the dominant color

                    case 3:
                        R = pv;
                        G = qv;
                        B = V;
                        break;
                    case 4:
                        R = tv;
                        G = pv;
                        B = V;
                        break;

                    // Red is the dominant color

                    case 5:
                        R = V;
                        G = pv;
                        B = qv;
                        break;

                    case 6:
                        R = V;
                        G = tv;
                        B = pv;
                        break;

                    case -1:
                        R = V;
                        G = pv;
                        B = qv;
                        break;
                    // The color is not defined, we should throw an error.

                    default:
                        //LFATAL("i Value error in Pixel conversion, Value is %d", i);
                        R = G = B = V; // Just pretend its black/white
                        break;
                }
            }
            r = CheckRange((int)(R * 255.0));
            g = CheckRange((int)(G * 255.0));
            b = CheckRange((int)(B * 255.0));
        } //HSV

        /// <summary>
        /// OpenCV 함수 모음
        /// </summary>
        /// 

        private void Cv2ToOutImage()
        {
            // 원래 출력 배열 메모리 확보
            _outHC = outCvimage.Height;
            _outWC = outCvimage.Width;
            _outImageColor = new byte[3, _outHC, _outWC];

            // outCvImage --> outImage
            for (int i = 0; i < _outHC; i++)
                for (int k = 0; k < _outWC; k++)
                {
                    var c = outCvimage.At<Vec3b>(i, k);
                    _outImageColor[0, i, k] = c.Item2;
                    _outImageColor[1, i, k] = c.Item1;
                    _outImageColor[2, i, k] = c.Item0;
                }

            DisplayImage();
        }

        //Common Function End

        //image processing Function start

        //Pixel Processing Menu 

        private void Equal()
        {
            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성

            if (rawColorSelect)
            {
                //영상 처리 
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        _outImage[i, k] = (byte)_inImage[i, k];//화면으로 출력되는 배열에 저장
                    }
            }
            else
            {
                //영상 처리
                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            _outImageColor[rgb, i, k] = (byte)_inImageColor[rgb, i, k];//화면으로 출력되는 배열에 저장
                        }
            }
           
            DisplayImage();// 화면 출력
        }//equal image(raw)     
        private void Mrange_Ex()
        {
            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성
            
            int value = (int)GetValue();
            int UnderOverFlow_num;

            if (value < -101 || value > 101)
            {
                MessageBox.Show("100 혹은 -100 이하로 지정해주세요", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
  
            if (rawColorSelect)
            {
                //input --> output 할당
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        UnderOverFlow_num = _inImage[i, k] + (value);                           
                        _outImage[i, k] = (byte)CheckRange(UnderOverFlow_num);//화면으로 출력되는 배열에 저장
                    }
            }
            else
            {
                //input --> output 할당
                for (int rgb = 0; rgb < 3; rgb++)
                {
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            UnderOverFlow_num = _inImageColor[rgb, i, k] + (value);
                            _outImageColor[rgb, i, k] = (byte)CheckRange(UnderOverFlow_num);//화면으로 출력되는 배열에 저장
                        }
                }

            }

             DisplayImage(); // 화면 출력
    
        }//Brightness Control(+,-)
        private void Strongweak_Bt()
        {
            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성
            bool DivMulflag = false;
            int UnderOverFlow_num;

            int value = (int)GetValue();

            if (value > 0)
                DivMulflag = true;

            if (value < -4 || value > 4)
            {
                MessageBox.Show("2 이하로 지정해주세요", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
          
            if (rawColorSelect)
            {
                //input --> output 할당
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        if(DivMulflag)
                            UnderOverFlow_num = _inImage[i, k] * value;
                        else
                            UnderOverFlow_num = _inImage[i, k] / value;

                        _outImage[i, k] = (byte)CheckRange(UnderOverFlow_num);//화면으로 출력되는 배열에 저장
                    }
            }
            else
            {
                //input --> output 할당
                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            if (DivMulflag)
                                UnderOverFlow_num = CheckRange(_inImageColor[rgb,i, k] * value);
                            else
                                UnderOverFlow_num = CheckRange(_inImageColor[rgb, i, k] / Math.Abs(value));

                            _outImageColor[rgb,i, k] = (byte)UnderOverFlow_num;//화면으로 출력되는 배열에 저장
                        }
            } //영상 처리 

            DisplayImage();// 화면 출력
 
        }//Brightness (Mul/Div)                                    수정할 것
        private void MonoChromeBasic()
        {
            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성

    
            if (rawColorSelect)
            {
                int value = (int)GetValue();

                if (value < 0 && value > 255)
                {
                    MessageBox.Show("100 이하로 지정해주세요", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //input --> output 할당
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        if (value < _inImage[i, k])
                        {
                            _outImage[i, k] = 235;
                        }
                        else if (value > _inImage[i, k])
                        {
                            _outImage[i, k] = 25;
                        }
                    }
                             DisplayImage(); // 화면 출력
            }
            else
            {
                int oW, oH; // OpenCV 매트릭스의 출력Mat 크기
                            // outCvImage 크기를 결정(중요!) --> 알고리즘
                oH = inCvImage.Height;
                oW = inCvImage.Width;
                outCvimage = new Mat();

                /// OpenCV용 알고리즘 활용 ////
                Cv2.CvtColor(inCvImage, outCvimage, ColorConversionCodes.BGR2GRAY);
                ////////////////////////////

                Cv2ToOutImage();
            }      
        }//Brightness(Basic)
        private void MonoChromeavr()
        {
            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성

            if(rawColorSelect)
            {
                long Img_avr = 0; //average 합산 후 계산 

                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        Img_avr += _inImage[i, k];//sum
                    }

                Img_avr /= (_inH * _inW); //평균값으로 초기화

                //input --> output 할당
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        if (Img_avr < _inImage[i, k])
                            _outImage[i, k] = 240;
                        else if (Img_avr == _inImage[i, k])
                            _outImage[i, k] = 120;
                        else if (Img_avr > _inImage[i, k] && Img_avr - 50 <= _inImage[i, k])
                            _outImage[i, k] = 60;
                        else
                            _outImage[i, k] = 0;
                    }
            }
            else
            {
                long Img_avrRGB = 0; //average 합산 후 계산 
                long Img_avrR = 0; //average 합산 후 계산 
                long Img_avrG = 0;
                long Img_avrB = 0;

                for (int i = 0; i < _inHC; i++)
                    for (int k = 0; k < _inWC; k++)
                    {
                        Img_avrR += _inImageColor[0, i, k];//sum
                        Img_avrG += _inImageColor[1, i, k];//sum
                        Img_avrB += _inImageColor[2, i, k];//sum
                    }
                Img_avrRGB = (Img_avrR + Img_avrG + Img_avrB) / 3;
                Img_avrRGB /= (_inHC * _inWC); //평균값으로 초기화

                //input --> output 할당
                for (int rgb = 0; rgb < 3; rgb++)
                {
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            if (Img_avrRGB < _inImageColor[0, i, k] || Img_avrRGB < _inImageColor[1, i, k] || Img_avrRGB < _inImageColor[2, i, k])
                            {
                                for (int m = 0; m < 3; m++)
                                    _outImageColor[m, i, k] = 255;
                            }
                            else
                            {
                                for (int m = 0; m < 3; m++)
                                    _outImageColor[m, i, k] = 0;
                            }
                        }
                }
            }
           

            DisplayImage(); // 화면 출력

        }//Brightness(average)
        private void MonoChromemdi()
        {
            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성

            if(rawColorSelect)
            {
                int[] num = new int[(_inH * _inW)];

                int idx = 0;//1차원 배열로 변경할 index 0으로 초기화

                for (int i = 0; i < _inH; i++)
                {
                    for (int k = 0; k < _inW; k++)
                    {
                        num[idx] = _inImage[i, k];
                        idx++;
                    }
                }

                idx--;

                Array.Sort(num);

                //input --> output 할당
                for (int i = 0; i < _inH; i++)
                {
                    for (int k = 0; k < _inW; k++)
                    {
                        if (num[(idx / 2)] < _inImage[i, k])
                        {
                            _outImage[i, k] = 255;
                        }
                        else
                        {
                            _outImage[i, k] = 0;
                        }
                    }
                }
            }
            else
            {
                long[] num = new long[(_inHC * _inWC * 3)];

                long idx = 0;//1차원 배열로 변경할 index 0으로 초기화

                for (int rgb = 0; rgb < 3; rgb++)
                {
                    for (int i = 0; i < _inHC; i++)
                    {
                        for (int k = 0; k < _inWC; k++)
                        {
                            num[idx] = _inImageColor[rgb, i, k];
                            idx++;
                        }
                    }
                }

                idx--;

                Array.Sort(num);

                //input --> output 할당
                for (int rgb = 0; rgb < 3; rgb++)
                {                  
                    for (int i = 0; i < _inHC; i++)
                    {
                        for (int k = 0; k < _inWC; k++)
                        {
                            if (num[(idx / 2)] < _inImageColor[0, i, k] || num[(idx / 2)] < _inImageColor[1, i, k] || num[(idx / 2)] < _inImageColor[2, i, k])
                            {
                                for (int m = 0; m < 3; m++)
                                    _outImageColor[m, i, k] = 255;
                            }
                            else
                            {
                                for (int m = 0; m < 3; m++)
                                    _outImageColor[m, i, k] = 0;
                            }
                        }
                    }
                }

            }

            //출력 메모리  ---> 화면
            DisplayImage();
        }//Brightness(median)
        private void PalabollaCap()
        {
            fileCheck();//File 확인
            InOutChange();//화면 크기 구성
          
            if(rawColorSelect)
            {
                //input --> output 할당
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        _outImage[i, k] = (byte)(255.0 - 255.0 * (_inImage[i, k] / 128.0 - 1) * (_inImage[i, k] / 128.0 - 1.0));
                    }
            }
            else
            {
                //input --> output 할당
                for (int rgb = 0; rgb < 3; rgb++)
                {
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                            _outImageColor[rgb, i, k] = (byte)(255.0 - 255.0 * (_inImageColor[rgb, i, k] / 128.0 - 1) * (_inImageColor[rgb, i, k] / 128.0 - 1.0));//화면으로 출력되는 배열에 저장                   
                }
            }
            //출력 메모리  ---> 화면
            DisplayImage();
        } //Palabolla(Cap)
        private void PalabollaCup()
        {
            fileCheck();//File 확인
            InOutChange();//화면 크기 구성           

            //영상 처리
            if(rawColorSelect)
            {
                //input --> output 할당
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        _outImage[i, k] = (byte)(255.0 * ((_inImage[i, k] / 128.0 - 1) * (_inImage[i, k] / 128.0 - 1.0)));
                    }
            }
            else
            {
                //input --> output 할당
                for (int rgb = 0; rgb < 3; rgb++)
                {
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                            _outImageColor[rgb, i, k] = (byte)(255.0 * (_inImageColor[rgb, i, k] / 128.0 - 1) * (_inImageColor[rgb, i, k] / 128.0 - 1.0));//화면으로 출력되는 배열에 저장                   
                }
            }
            //출력 메모리  ---> 화면
            DisplayImage();

        }//Palabolla(Cup)
        private void Postrizing()
        {
            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성

            int value;//_inImage의 배열값 사용 


            if (rawColorSelect)
            {
                //input --> output 할당
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        value = _inImage[i, k];
                        //256 / 8  = 32   , -32 씩 값지정
                        if (value > 224)
                            _outImage[i, k] = 255;
                        else if (value > 192 && value <= 224)
                            _outImage[i, k] = 224;
                        else if (value > 160 && value <= 192)
                            _outImage[i, k] = 192;
                        else if (value > 128 && value <= 160)
                            _outImage[i, k] = 160;
                        else if (value > 96 && value <= 128)
                            _outImage[i, k] = 128;
                        else if (value > 64 && value <= 96)
                            _outImage[i, k] = 96;
                        else if (value > 32 && value <= 64)
                            _outImage[i, k] = 64;
                        else if (value > 0 && value <= 32)
                            _outImage[i, k] = 32;
                    }
            }
            else
            {
                //input --> output 할당
                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            value = _inImageColor[rgb, i, k];
                            //256 / 8  = 32   , -32 씩 값지정
                            if (value > 224)
                                _outImageColor[rgb, i, k] = 255;
                            else if (value > 192 && value <= 224)
                                _outImageColor[rgb, i, k] = 224;
                            else if (value > 160 && value <= 192)
                                _outImageColor[rgb, i, k] = 192;
                            else if (value > 128 && value <= 160)
                                _outImageColor[rgb, i, k] = 160;
                            else if (value > 96 && value <= 128)
                                _outImageColor[rgb, i, k] = 128;
                            else if (value > 64 && value <= 96)
                                _outImageColor[rgb, i, k] = 96;
                            else if (value > 32 && value <= 64)
                                _outImageColor[rgb, i, k] = 64;
                            else if (value > 0 && value <= 32)
                                _outImageColor[rgb, i, k] = 32;
                        }
            }

            DisplayImage();// 화면 출력
        }//Postrizing
        private void Negative_TransForm()
        {
            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성

            if(rawColorSelect)
            {
                //input --> output 할당
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        _outImage[i, k] = (byte)(255 - _inImage[i, k]);//화면으로 출력되는 배열에 저장
                    }
            }
            else
            {
                //input --> output 할당
                for (int rgb = 0; rgb < 3; rgb++)
                {
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            _outImageColor[rgb, i, k] = (byte)(255 - _inImageColor[rgb, i, k]);//화면으로 출력되는 배열에 저장
                        }

                }
            }
    
            DisplayImage();// 화면 출력
        }//binary reverse

        // Geometry Processing 

        private void LfMirroring()
        {
            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성

            if (rawColorSelect)
            {
                int w = _inW - 1;

                //input --> output 할당
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        _outImage[i, w - k] = _inImage[i, k];
                    }
            }
            else
            {
                int w = _inHC - 1;

                //input --> output 할당
                for (int rgb = 0; rgb < 3; rgb++)
                {
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            _outImageColor[rgb, w - i,  k] = (byte)_inImageColor[rgb, i, k];//화면으로 출력되는 배열에 저장
                        }
                }
            }

            //출력 메모리  ---> 화면
            DisplayImage();
        }//Mirroring (left And right)
        private void UpDoMirroring()
        {
            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성

            //진짜 영상 처리 알고리즘 사용
         
            if (rawColorSelect)
            {
                int w = _inH - 1;
                //input --> output 할당
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        _outImage[w - i, k] = _inImage[i, k];
                    }
            }
            else
            {
                int w = _inWC - 1;

                //input --> output 할당
                for (int rgb = 0; rgb < 3; rgb++)
                {
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            _outImageColor[rgb,i, w - k] = (byte)_inImageColor[rgb, i, k];//화면으로 출력되는 배열에 저장
                        }
                }
            }
            //출력 메모리  ---> 화면
            DisplayImage();
        }//Mirroring (Up And Down)
        private void RightAngle()
        {
            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성

            //input --> output 할당
            for (int i = 0; i < _inH; i++)
                for (int k = 0; k < _inW; k++)
                {
                    _outImage[i, k] = _inImage[k, i];
                }
            //출력 메모리  ---> 화면
            DisplayImage();
        } //90 degree
        private void CtRotation()
        {
            fileCheck(); //파일확인 후 구현할 것
            int angle = (int)GetValue();//각도 입력

            if(rawColorSelect)
            {
                // 영상 알고리즘 구현
                double radion = angle * Math.PI / 180.0;
                _outH = (int)(_inW * Math.Cos(radion) + _inW * Math.Sin(radion));
                _outW = (int)(_inH * Math.Sin(radion) + _inH * Math.Cos(radion));// 출력 영상 크기 결정 
                int outline_row = _outH - _inH;
                int outline_col = _outW - _inW;
                byte[,] tempimage = new byte[_outH, _outW];//평행이동 영상 메모리 할당
                _outImage = new byte[_outH, _outW];//출력 영상 메모리 할당

                int x, y;
                int newx, newy;

                for (int i = 0; i < _outH; i++)
                    for (int j = 0; j < _outW; j++)
                    {
                        _outImage[i, j] = 240;
                        tempimage[i, j] = 150;
                    }

                for (int i = 0; i < _inH; i++)
                    for (int j = 0; j < _inW; j++)
                    {
                        tempimage[i + outline_row / 2, j + outline_col / 2] = _inImage[i, j];
                    }// 입력 평행이동

                //input --> output 할당
                for (int i = 0; i < _outH; i++)
                    for (int j = 0; j < _outW; j++)
                    {
                        x = i;
                        y = j;

                        newx = (int)(Math.Cos(radion) * (x - _outH / 2) - Math.Sin(radion) * (y - _outW / 2) + _outH / 2);
                        newy = (int)(Math.Sin(radion) * (x - _outH / 2) + Math.Cos(radion) * (y - _outW / 2) + _outW / 2);

                        if ((0 <= newx && newx < _outH) && (0 <= newy && newy < _outW))
                            _outImage[i, j] = tempimage[newx, newy];
                    }//회전이동
            }
            else
            {
                // 영상 알고리즘 구현
                double radion = angle * Math.PI / 180.0;
                _outHC = (int)(_inWC * Math.Cos(radion) + _inWC * Math.Sin(radion));
                _outWC = (int)(_inHC * Math.Sin(radion) + _inHC * Math.Cos(radion));// 출력 영상 크기 결정 
                int outline_row = _outHC - _inHC;
                int outline_col = _outWC - _inWC;
                byte[,,] tempimage = new byte[3, _outHC, _outWC];//평행이동 영상 메모리 할당
                _outImageColor = new byte[3, _outHC, _outWC];//출력 영상 메모리 할당

                int x, y;
                int newx, newy;

                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _outHC; i++)
                        for (int j = 0; j < _outWC; j++)
                        {
                            _outImageColor[rgb, i, j] = _inImageColor[rgb, i / 8, j / 8];
                            tempimage[rgb, i, j] = _inImageColor[rgb, i / 8, j / 8];
                        }

                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int j = 0; j < _inWC; j++)
                        {
                            tempimage[rgb, i + outline_row / 2, j + outline_col / 2] = _inImageColor[rgb, i, j];
                        }// 입력 평행이동

                //input --> output 할당
                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _outHC; i++)
                        for (int j = 0; j < _outWC; j++)
                        {
                            x = i;
                            y = j;

                            newx = (int)(Math.Cos(radion) * (x - _outHC / 2) - Math.Sin(radion) * (y - _outWC / 2) + _outHC / 2);
                            newy = (int)(Math.Sin(radion) * (x - _outHC / 2) + Math.Cos(radion) * (y - _outWC / 2) + _outWC / 2);

                            if ((0 <= newx && newx < _outHC) && (0 <= newy && newy < _outWC))
                                _outImageColor[rgb, i, j] = tempimage[rgb, newx, newy];
                        }//회전이동
            }
           
            DisplayImage();
        } //Center Roation
        private void Rotation()
        {
            int oW, oH; // OpenCV 매트릭스의 출력Mat 크기
                        // outCvImage 크기를 결정(중요!) --> 알고리즘
            oH = inCvImage.Height;
            oW = inCvImage.Width;
            outCvimage = new Mat();

            /// OpenCV용 알고리즘 활용 ////
            Mat matrix = Cv2.GetRotationMatrix2D(
              new Point2f(inCvImage.Width / 2, inCvImage.Height / 2), 45.0, 1.0);
            ////////////////////////////

            Cv2ToOutImage();
        }//rotation
        private void ImgMove()
        {
            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성

            int val = (int)GetValue();


            if(rawColorSelect)
            {
                for (int i = 0; i < val; i++)
                {
                    for (int k = 0; k < _inW; k++)
                        _outImage[i, k] = 60;
                }

                for (int i = 0; i < _inH; i++)
                {
                    for (int k = 0; k < val; k++)
                        _outImage[i, k] = 60;
                }

                //input --> output 할당
                for (int i = 0; i < _inH - val; i++)
                {
                    for (int k = 0; k < _inW - val; k++)
                        _outImage[i + val, k + val] = _inImage[i, k];
                }
            }
            else
            {
                for (int i = 0; i < val; i++)
                    for (int k = 0; k < _inWC; k++)
                    {
                        _outImageColor[0, i, k] = 60;
                        _outImageColor[1, i, k] = 60;
                        _outImageColor[2, i, k] = 60;
                    }

                for (int i = 0; i < _inHC; i++)
                    for (int k = 0; k < val; k++)
                    {
                        _outImageColor[0, i, k] = 60;
                        _outImageColor[1, i, k] = 60;
                        _outImageColor[2, i, k] = 60;
                    }

                //input --> output 할당
                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC - val; i++)
                        for (int k = 0; k < _inWC - val; k++)
                            _outImageColor[rgb, i + val, k + val] = _inImageColor[rgb, i, k];
            }

            //출력 메모리  ---> 화면
            DisplayImage();
        }//Img Move
        private void Enlag_img()
        {
            fileCheck(); //파일확인 후 구현할 것         
            if(rawColorSelect)
            {
                int scale = (int)GetValue(); //축소할 값 입력

                if (scale < 1 && scale > 4)
                {
                    MessageBox.Show("2 이하로 지정해주세요", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _outH = _inH * scale; //출력영상의 크기를 결정
                _outW = _inW * scale;
                // int value = 대화상자에서 입력받은 숫자 

                //출력 영상 메모리 할당
                _outImage = new byte[_outH, _outW];

                //input --> output 할당
                for (int i = 0; i < _outH; i++)
                    for (int k = 0; k < _outW; k++)
                    {
                        //백워딩 
                        _outImage[i, k] = _inImage[i / scale, k / scale];
                    }
                //출력 메모리  ---> 화면
                DisplayImage();
            }
            else
            {
                //_outHC = _inHC * scale; //출력영상의 크기를 결정
                //_outWC = _inWC * scale;
                //// int value = 대화상자에서 입력받은 숫자 

                ////출력 영상 메모리 할당
                //_outImageColor = new byte[3, _outHC, _outWC];
                ////진짜 영상 처리 알고리즘 사용

                //for (int rgb = 0; rgb < 3; rgb++)
                //    for (int i = 0; i < _outH; i++)
                //        for (int k = 0; k < _outW; k++)
                //        {
                //            //백워딩 
                //            _outImageColor[rgb, i, k] = _inImageColor[rgb, i / scale, k / scale];
                //        }
                ////출력 메모리  ---> 화면
                ///

                Cv2.Transpose(inCvImage, inCvImage);
                outCvimage = new Mat();
                _outHC = _inHC;
                _outWC = _inWC;
                Cv2.PyrUp(inCvImage, outCvimage, new OpenCvSharp.Size(inCvImage.Width * 2, inCvImage.Height * 2));
                Cv2.ImShow("출력영상", outCvimage);
                Cv2.WaitKey(0);
            }
        }//Enlagment
        private void Reducimg()
        {
            fileCheck(); //파일확인 후 구현할 것
            

            if(rawColorSelect)
            {
                int scale = (int)GetValue();

                if (scale < 1 && scale > 4)
                {
                    MessageBox.Show("2 이하로 지정해주세요", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _outH = _inH / scale; //출력영상의 크기를 결정
                _outW = _inW / scale;
                // int value = 대화상자에서 입력받은 숫자 

                //출력 영상 메모리 할당
                _outImage = new byte[_outH, _outW];

                //input --> output 할당
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        //백워딩 
                        _outImage[i / scale, k / scale] = _inImage[i, k];
                    }
                //출력 메모리  ---> 화면
                DisplayImage();
            }
            else
            {
                Cv2.Transpose(inCvImage, inCvImage);
                outCvimage = new Mat();
                _outHC = _inHC;
                _outWC = _inWC;
                Cv2.PyrDown(inCvImage, outCvimage, new OpenCvSharp.Size(_outHC / 2, _outWC / 2));
                Cv2.ImShow("출력영상", outCvimage);
                Cv2.WaitKey(0);
            }
        }//reducing

        //Area Processing

        private void BlurBasic()
        {
            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성

            const int msize = 3;//사이즈는 변수로 지정해서 사용하는 것이 좋습니다.

            //마스크 사이즈 홀수만 필요하다
            // 왜 중간값이 필요하기 때문입니다.
            // 5*5 7*7
            // 마스크 준비 및 정의
            double[,] mask = new double[msize, msize]
            {
                { 1.0/9 , 1.0/9 , 1.0/9},
                { 1.0/9 , 1.0/9 , 1.0/9},
                { 1.0/9 , 1.0/9 , 1.0/9}
            };
            //512 * 512 ===> 514 * 514
            //코드를 명확하게 하면 확장할 때 편하다.
            //회선 영상 실수를 정수로 만드는 것 
            if(rawColorSelect)
            {
                //임시 버퍼 
                double[,] tmpInImage = new double[_inH + 2, _inW + 2];
                double[,] tmpoutImage = new double[_outH, _outW];

                //임시 입력을 127로 초기화
                for (int i = 0; i < _inH + 2; i++)
                    for (int k = 0; k < _inW + 2; k++)
                    {
                        tmpInImage[i, k] = 127.0;
                        //지금 127 중간값을 지정 
                    }

                //입력 영상 --> 임시 입력 영상
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        tmpInImage[i + 1, k + 1] = _inImage[i, k];
                    } //왜냐하면 2만큼 더해졌기 때문에 원래 자리에 있던 자리에 쓴다.

                //회선 연산 처리 
                double s = 0.0;

                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        //한점에 대해서 마스크와 연산 (3x3)
                        for (int m = 0; m < msize; m++)
                            for (int n = 0; n < msize; n++)
                            {
                                s += tmpInImage[m + i, n + k] * mask[m, n];
                            }
                        tmpoutImage[i, k] = s;
                        s = 0;
                    }

                //input --> output 할당
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {                
                       _outImage[i, k] = (byte)CheckRange((int)tmpoutImage[i, k]);
                    }
                //출력 메모리  ---> 화면
                DisplayImage();
            }
            else
            {          
                int oW, oH; // OpenCV 매트릭스의 출력Mat 크기
                            // outCvImage 크기를 결정(중요!) --> 알고리즘
                oH = inCvImage.Height;
                oW = inCvImage.Width;
                outCvimage = new Mat();

                /// OpenCV용 알고리즘 활용 ////
                Cv2.Blur(inCvImage, outCvimage, new OpenCvSharp.Size(15, 15));
                ////////////////////////////

                Cv2ToOutImage();        
            }         
        
        }//blurBasic
        private void Blurselect()
        {
            int msize;
            int div_msize;

            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성

            int value = (int)GetValue();
            
            if (value % 2 == 0)
            {
                MessageBox.Show("홀수 지정해주세요", "에러", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if(rawColorSelect)
            {
                msize = value;
                div_msize = msize / 2;
                double[,] mask = new double[msize, msize];

                for (int i = 0; i < msize; i++)
                    for (int k = 0; k < msize; k++)
                    {
                        mask[i, k] = (1.0 / (msize * msize));
                    }

                int new_inH = _inH + (div_msize * 2);
                int new_inW = _inW + (div_msize * 2);

                //임시 버퍼 
                double[,] tmpInImage = new double[new_inH, new_inW];
                double[,] tmpoutImage = new double[_outH, _outW];

                //임시 입력을 127로 초기화
                for (int i = 0; i < new_inH; i++)
                    for (int k = 0; k < new_inW; k++)
                    {
                        tmpInImage[i, k] = 50.0;
                        //지금 127 중간값을 지정 
                    }

                //입력 영상 --> 임시 입력 영상
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        tmpInImage[i + div_msize, k + div_msize] = _inImage[i, k];
                    } //왜냐하면 2만큼 더해졌기 때문에 원래 자리에 있던 자리에 쓴다.

                //회선 연산 처리 
                double s = 0.0;

                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        //한점에 대해서 마스크와 연산 (3x3)
                        for (int m = 0; m < msize; m++)
                            for (int n = 0; n < msize; n++)
                            {
                                s += tmpInImage[m + i, n + k] * mask[m, n];
                            }
                        tmpoutImage[i, k] = s;
                        s = 0;
                    }

                //input --> output 할당
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        if (tmpoutImage[i, k] > 255)
                        {
                            _outImage[i, k] = 255;
                        }
                        else if (tmpoutImage[i, k] < 0)
                        {
                            _outImage[i, k] = 0;
                        }
                        else
                            _outImage[i, k] = (byte)tmpoutImage[i, k];
                    }
            }
            else
            {
                msize = value;
                div_msize = msize / 2;
                double[,] mask = new double[msize, msize];

                for (int i = 0; i < msize; i++)
                    for (int k = 0; k < msize; k++)
                    {
                        mask[i, k] = (1.0 / (msize * msize));
                    }

                int new_inH = _inHC + (div_msize * 2);
                int new_inW = _inWC + (div_msize * 2);

                //임시 버퍼 
                double[,,] tmpInImage = new double[3,new_inH, new_inW];
                double[,,] tmpoutImage = new double[3,_outH, _outW];

                //임시 입력을 127로 초기화
                for (int i = 0; i < new_inH; i++)
                    for (int k = 0; k < new_inW; k++)
                    {
                        tmpInImage[0,i, k] = 127.0;
                        tmpInImage[1, i, k] = 127.0;
                        tmpInImage[2, i, k] = 127.0;
                        //지금 127 중간값을 지정 
                    }

                //입력 영상 --> 임시 입력 영상
                for(int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            tmpInImage[rgb,i + div_msize, k + div_msize] = _inImageColor[rgb,i, k];
                        } //왜냐하면 2만큼 더해졌기 때문에 원래 자리에 있던 자리에 쓴다.

                //회선 연산 처리 
                double s = 0.0;

                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            //한점에 대해서 마스크와 연산 (3x3)
                            for (int m = 0; m < msize; m++)
                                for (int n = 0; n < msize; n++)
                                {
                                    s += tmpInImage[rgb,m + i, n + k] * mask[m, n];
                                }
                            tmpoutImage[rgb,i, k] = s;
                            s = 0.0;
                        }

                //input --> output 할당
                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {                            
                            _outImageColor[rgb,i, k] = (byte)CheckRange((int)tmpoutImage[rgb, i, k]);                                                        
                        }
            }                          
                //출력 메모리  ---> 화면
                DisplayImage();
            
        }//blurselect   
        private void Embossing()  // 엠보싱 알고리즘
        {

            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성

            // ** 진짜 영상처리 알고리즘 구현
            const int mSize = 3;// 마스크 사이즈
                                // 마스크 준비 및 정의
            double[,] mask = new double[mSize, mSize]
            {
                { -1.0 , 0.0 , 0.0 },
                {  0.0 , 0.0 , 0.0 },
                {  0.0 , 0.0 , 1.0 }
            };

            if (rawColorSelect)
            {         
                // 임시 입력, 임시 출력 메모리 할당
                double[,] tmpInImage = new double[_inH + 2, _inW + 2];
                double[,] tmpOutImage = new double[_outH, _outW];
                // 임시 입력을 127로 초기화.
                
                    for (int i = 0; i < _inH + 2; i++)
                        for (int k = 0; k < _inW + 2; k++)
                            tmpInImage[i, k] = 127.0;
                // 입력 영상 --> 임시 입력 영상
                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inH; i++)
                        for (int k = 0; k < _inW; k++)
                            tmpInImage[i + 1, k + 1] = _inImage[i, k];

                // 회선 연산 처리
                double S = 0.0;
                
                    for (int i = 0; i < _inH; i++)
                        for (int k = 0; k < _inW; k++)
                        {
                            // 한점에 대해서 마스크와 연산 (3x3)
                            for (int m = 0; m < mSize; m++)
                                for (int n = 0; n < mSize; n++)
                                    S += tmpInImage[m + i, n + k] * mask[m, n];
                            tmpOutImage[i, k] = S;
                            S = 0.0;
                        }
                // 마스크의 합계에 따라서 127 더할지 결정
                
                    for (int i = 0; i < _outH; i++)
                        for (int k = 0; k < _outW; k++)
                            tmpOutImage[i, k] += 127.0;

                    //input --> output 할당
                    for (int i = 0; i < _outH; i++)
                        for (int k = 0; k < _outW; k++)
                        {
                            double v = tmpOutImage[ i, k];
                            if (v > 255)
                                _outImage[i, k] = 255;
                            else if (v < 0)
                                _outImage[i, k] = 0;
                            else
                                _outImage[i, k] = (byte)v;
                        }
            }
            else
            {
         
                // 임시 입력, 임시 출력 메모리 할당
                double[,,] tmpInImage = new double[3, _inHC + 2, _inWC + 2];
                double[,,] tmpOutImage = new double[3, _outHC, _outWC];
                // 임시 입력을 127로 초기화.
                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC + 2; i++)
                        for (int k = 0; k < _inWC + 2; k++)
                            tmpInImage[rgb, i, k] = 127.0;
                // 입력 영상 --> 임시 입력 영상
                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                            tmpInImage[rgb, i + 1, k + 1] = _inImageColor[rgb, i, k];

                // 회선 연산 처리
                double S = 0.0;
                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            // 한점에 대해서 마스크와 연산 (3x3)
                            for (int m = 0; m < mSize; m++)
                                for (int n = 0; n < mSize; n++)
                                    S += tmpInImage[rgb, m + i, n + k] * mask[m, n];
                            tmpOutImage[rgb, i, k] = S;
                            S = 0.0;
                        }
                // 마스크의 합계에 따라서 127 더할지 결정
                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _outHC; i++)
                        for (int k = 0; k < _outWC; k++)
                            tmpOutImage[rgb, i, k] += 127.0;

                //input --> output 할당
                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _outHC; i++)
                        for (int k = 0; k < _outWC; k++)
                        {
                            double v = tmpOutImage[rgb, i, k];
                            _outImageColor[rgb, i, k] = (byte)CheckRange((int)v); 
                        }
            }

            DisplayImage();  // 출력메모리 --> 화면
        }
        private void DOG()
        {
            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성

            const int msize = 7;//사이즈는 변수로 지정해서 사용하는 것이 좋습니다.
          
            // 마스크 준비 및 정의
            double[,] mask = new double[msize, msize]
            {
                { 0.0 , 0.0 , -1.0 , -1.0,-1.0,0.0,0.0},
                { 0.0 , -2.0 , -3.0 , -3.0,-3.0,-2.0,0.0},
                { -1.0 , -3.0 , 5.0 , 5.0,5.0,-3.0,-1.0},
                { -1.0 , -3.0 , 5.0 , 16.0,5.0,-3.0,-1.0},
                { -1.0 , -3.0 , 5.0 , 5.0,5.0,-3.0,-1.0},
                { 0.0 , -2.0 , -3.0 , -3.0,-3.0,-2.0,0.0},
                { 0.0 , 0.0 , -1.0 , -1.0,-1.0,0.0,0.0}

            };
             
            if(rawColorSelect)
            {
                //임시 버퍼 
                double[,] tmpInImage = new double[_inH + 6, _inW + 6];
                double[,] tmpoutImage = new double[_outH, _outW];

                //임시 입력을 127로 초기화
                for (int i = 0; i < _inH + 6; i++)
                    for (int k = 0; k < _inW + 6; k++)
                    {
                        tmpInImage[i, k] = 127.0;
                        //지금 127 중간값을 지정 
                    }

                //입력 영상 --> 임시 입력 영상
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        tmpInImage[i + 3, k + 3] = _inImage[i, k];//matrix x y가 [i+1,k+1]하게 되면 
                    } //왜냐하면 2만큼 더해졌기 때문에 원래 자리에 있던 자리에 쓴다.

                //회선 연산 처리 
                double s = 0.0;

                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        //한점에 대해서 마스크와 연산 (3x3)
                        for (int m = 0; m < msize; m++)
                            for (int n = 0; n < msize; n++)
                            {
                                s += tmpInImage[m + i, n + k] * mask[m, n];
                            }
                        tmpoutImage[i, k] = s;
                        s = 0;
                    }

                //input --> output 할당
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                         _outImage[i, k] = (byte)CheckRange((int)tmpoutImage[i, k]);
                    }

            }
            else
            {
                //임시 버퍼 
                double[,,] tmpInImage = new double[3,_inHC + 6, _inWC + 6];
                double[,,] tmpoutImage = new double[3,_outHC, _outWC];

                //임시 입력을 127로 초기화
                for (int i = 0; i < _inHC + 6; i++)
                    for (int k = 0; k < _inWC + 6; k++)
                    {
                        tmpInImage[0,i, k] = 127.0;
                        tmpInImage[1, i, k] = 127.0;
                        tmpInImage[2, i, k] = 127.0;
                        //지금 127 중간값을 지정 
                    }

                //입력 영상 --> 임시 입력 영상
                for(int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            tmpInImage[rgb,i + 3, k + 3] = _inImageColor[rgb,i,k];//matrix x y가 [i+1,k+1]하게 되면 
                        } //왜냐하면 2만큼 더해졌기 때문에 원래 자리에 있던 자리에 쓴다.

                //회선 연산 처리 
                double s = 0.0;
                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            //한점에 대해서 마스크와 연산 (3x3)
                            for (int m = 0; m < msize; m++)
                                for (int n = 0; n < msize; n++)
                                {
                                    s += tmpInImage[rgb,m + i, n + k] * mask[m, n];
                                }
                            tmpoutImage[rgb,i, k] = s;
                            s = 0.0;
                        }

                //input --> output 할당
                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {                
                            _outImageColor[rgb,i, k] = (byte)CheckRange((int)tmpoutImage[rgb,i, k]);
                        }
            }
            //출력 메모리  ---> 화면
            DisplayImage();
        } //DOG
        private void LOG()
        {
            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성

            const int msize = 5;//사이즈는 변수로 지정해서 사용하는 것이 좋습니다.

            double[,] mask = new double[msize, msize]
            {
                {0.0,0.0,-1.0,0.0,0.0},
                {0.0,-1.0,-2.0,-1.0,0.0},
                {-1.0,-2.0,16.0,-2.0,-1.0},
                {0.0,-1.0,-2.0,-1.0,0.0},
                {0.0,0.0,-1.0,0.0,0.0}
            };

            if(rawColorSelect)
            {
                //임시 버퍼 
                double[,] tmpInImage = new double[_inH + 4, _inW + 4];
                double[,] tmpoutImage = new double[_outH, _outW];
                //임시 입력을 127로 초기화
                for (int i = 0; i < _inH + 4; i++)
                    for (int k = 0; k < _inW + 4; k++)
                    {
                        tmpInImage[i, k] = 127.0;
                        //지금 127 중간값을 지정 
                    }

                //입력 영상 --> 임시 입력 영상
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        tmpInImage[i + 2, k + 2] = _inImage[i, k];
                    } //왜냐하면 2만큼 더해졌기 때문에 원래 자리에 있던 자리에 쓴다.

                //회선 연산 처리 
                double s = 0.0;

                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        //한점에 대해서 마스크와 연산 (3x3)
                        for (int m = 0; m < msize; m++)
                            for (int n = 0; n < msize; n++)
                            {
                                s += tmpInImage[m + i, n + k] * mask[m, n];
                            }
                        tmpoutImage[i, k] = s;
                        s = 0;
                    }

                //input --> output 할당
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {                        
                        _outImage[i, k] = (byte)CheckRange((int)tmpoutImage[i, k]);
                    }
            }
            else
            {
                //임시 버퍼 
                double[,,] tmpInImage = new double[3, _inHC + 4, _inWC + 4];
                double[,,] tmpoutImage = new double[3, _outHC, _outWC];

                //임시 입력을 127로 초기화
                for (int i = 0; i < _inHC + 4; i++)
                    for (int k = 0; k < _inWC + 4; k++)
                    {
                        tmpInImage[0, i, k] = 127.0;
                        tmpInImage[1, i, k] = 127.0;
                        tmpInImage[2, i, k] = 127.0;
                        //지금 127 중간값을 지정 
                    }

                //입력 영상 --> 임시 입력 영상
                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            tmpInImage[rgb, i + 3, k + 3] = _inImageColor[rgb, i, k];//matrix x y가 [i+1,k+1]하게 되면 
                        } //왜냐하면 2만큼 더해졌기 때문에 원래 자리에 있던 자리에 쓴다.

                //회선 연산 처리 
                double s = 0.0;
                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            //한점에 대해서 마스크와 연산 (3x3)
                            for (int m = 0; m < msize; m++)
                                for (int n = 0; n < msize; n++)
                                {
                                    s += tmpInImage[rgb, m + i, n + k] * mask[m, n];
                                }
                            tmpoutImage[rgb, i, k] = s;
                            s = 0.0;
                        }

                //input --> output 할당
                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            _outImageColor[rgb, i, k] = (byte)CheckRange((int)tmpoutImage[rgb, i, k]);
                        }
            }
            
            //출력 메모리  ---> 화면
            DisplayImage();
        } //LOG
        private void VerEdge()
        {
            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성

            const int msize = 3;

            // 마스크 준비 및 정의
            double[,] mask = new double[msize, msize]
            {
                { 0.0 , -1.0 , 0.0},
                { 0.0 , 0.0 , 0.0},
                { 0.0 , 0.0 , 1.0}
            };
            //512 * 512 ===> 514 * 514
            //코드를 명확하게 하면 확장할 때 편하다.
            //회선 영상 실수를 정수로 만드는 것 

            if(rawColorSelect)
            {
                //임시 버퍼 
                double[,] tmpInImage = new double[_inH + 2, _inW + 2];
                double[,] tmpoutImage = new double[_outH, _outW];

                //임시 입력을 127로 초기화
                for (int i = 0; i < _inH + 2; i++)
                    for (int k = 0; k < _inW + 2; k++)
                    {
                        tmpInImage[i, k] = 127.0;
                        //지금 127 중간값을 지정 
                    }

                //입력 영상 --> 임시 입력 영상
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        tmpInImage[i + 1, k + 1] = _inImage[i, k];//matrix x y가 [i+1,k+1]하게 되면 
                    } //왜냐하면 2만큼 더해졌기 때문에 원래 자리에 있던 자리에 쓴다.

                //회선 연산 처리 
                double s = 0.0;

                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        //한점에 대해서 마스크와 연산 (3x3)
                        for (int m = 0; m < msize; m++)
                            for (int n = 0; n < msize; n++)
                            {
                                s += tmpInImage[m + i, n + k] * mask[m, n];
                            }
                        tmpoutImage[i, k] = s;
                        s = 0;
                    }

                //마스크의 합계에 따라서 127 더할지 결정
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                        tmpoutImage[i, k] += 127.0;

                //input --> output 할당
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {                        
                       _outImage[i, k] = (byte)CheckRange((int)tmpoutImage[i, k]);
                    }
            }
            else
            {
                //임시 버퍼 
                double[,,] tmpInImage = new double[3,_inHC + 2, _inWC + 2];
                double[,,] tmpoutImage = new double[3,_outHC, _outWC];

                //임시 입력을 127로 초기화
                for (int i = 0; i < _inHC + 2; i++)
                    for (int k = 0; k < _inWC + 2; k++)
                    {
                        tmpInImage[0,i, k] = 127.0;
                        tmpInImage[1, i, k] = 127.0;
                        tmpInImage[2, i, k] = 127.0;
                        //지금 127 중간값을 지정 
                    }

                //입력 영상 --> 임시 입력 영상
                for(int rgb = 0; rgb < 3;rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            tmpInImage[rgb,i + 1, k + 1] = _inImageColor[rgb,i, k];//matrix x y가 [i+1,k+1]하게 되면 
                        } //왜냐하면 2만큼 더해졌기 때문에 원래 자리에 있던 자리에 쓴다.

                //회선 연산 처리 
                double s = 0.0;
                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            //한점에 대해서 마스크와 연산 (3x3)
                            for (int m = 0; m < msize; m++)
                                for (int n = 0; n < msize; n++)
                                {
                                    s += tmpInImage[rgb,m + i, n + k] * mask[m, n];
                                }
                            tmpoutImage[rgb,i, k] = s;
                            s = 0.0;
                        }

                //마스크의 합계에 따라서 127 더할지 결정
                for (int i = 0; i < _inHC; i++)
                    for (int k = 0; k < _inWC; k++)
                    {
                        tmpoutImage[0,i, k] += 127.0;
                        tmpoutImage[1, i, k] += 127.0;
                        tmpoutImage[2, i, k] += 127.0;
                    }

                //input --> output 할당
                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            _outImageColor[rgb,i, k] = (byte)CheckRange((int)tmpoutImage[rgb,i, k]);
                        }
            }
            
            //출력 메모리  ---> 화면
            DisplayImage();
        }//vertical Edge
        private void HoriEdge()
        {
            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성

            const int msize = 3;//사이즈는 변수로 지정해서 사용하는 것이 좋습니다.

            // 마스크 준비 및 정의
            double[,] mask = new double[msize, msize]
            {
                { 0.0 , -1.0 , 0.0},
                { 0.0 , 1.0 , 0.0},
                { 0.0 , 0.0 , 0.0}
            };   

            if(rawColorSelect)
            {
                //임시 버퍼 
                double[,] tmpInImage = new double[_inH + 2, _inW + 2];
                double[,] tmpoutImage = new double[_outH, _outW];

                //임시 입력을 127로 초기화
                for (int i = 0; i < _inH + 2; i++)
                    for (int k = 0; k < _inW + 2; k++)
                    {
                        tmpInImage[i, k] = 127.0;
                        //지금 127 중간값을 지정 
                    }

                //입력 영상 --> 임시 입력 영상
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        tmpInImage[i + 1, k + 1] = _inImage[i, k];//matrix x y가 [i+1,k+1]하게 되면 
                    } //왜냐하면 2만큼 더해졌기 때문에 원래 자리에 있던 자리에 쓴다.

                //회선 연산 처리 
                double s = 0.0;

                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        //한점에 대해서 마스크와 연산 (3x3)
                        for (int m = 0; m < msize; m++)
                            for (int n = 0; n < msize; n++)
                            {
                                s += tmpInImage[m + i, n + k] * mask[m, n];
                            }
                        tmpoutImage[i, k] = s;
                        s = 0;
                    }

                //마스크의 합계에 따라서 127 더할지 결정
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                        tmpoutImage[i, k] += 127.0;

                //input --> output 할당
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {                       
                       _outImage[i, k] = (byte)CheckRange((int)tmpoutImage[i, k]);
                    }

            }
            else
            {
                //임시 버퍼 
                double[,,] tmpInImage = new double[3, _inHC + 2, _inWC + 2];
                double[,,] tmpoutImage = new double[3, _outHC, _outWC];

                //임시 입력을 127로 초기화
                for (int i = 0; i < _inHC + 2; i++)
                    for (int k = 0; k < _inWC + 2; k++)
                    {
                        tmpInImage[0, i, k] = 127.0;
                        tmpInImage[1, i, k] = 127.0;
                        tmpInImage[2, i, k] = 127.0;
                        //지금 127 중간값을 지정 
                    }

                //입력 영상 --> 임시 입력 영상
                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            tmpInImage[rgb, i + 1, k + 1] = _inImageColor[rgb,i, k];//matrix x y가 [i+1,k+1]하게 되면 
                        } //왜냐하면 2만큼 더해졌기 때문에 원래 자리에 있던 자리에 쓴다.

                //회선 연산 처리 
                double s = 0.0;
                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            //한점에 대해서 마스크와 연산 (3x3)
                            for (int m = 0; m < msize; m++)
                                for (int n = 0; n < msize; n++)
                                {
                                    s += tmpInImage[rgb, m + i, n + k] * mask[m, n];
                                }
                            tmpoutImage[rgb, i, k] = s;
                            s = 0.0;
                        }

                //마스크의 합계에 따라서 127 더할지 결정
                for (int i = 0; i < _inHC; i++)
                    for (int k = 0; k < _inWC; k++)
                    {
                        tmpoutImage[0, i, k] += 127.0;
                        tmpoutImage[1, i, k] += 127.0;
                        tmpoutImage[2, i, k] += 127.0;
                    }

                //input --> output 할당
                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            _outImageColor[rgb, i, k] = (byte)CheckRange((int)tmpoutImage[rgb, i, k]);
                        }
            }
            
            //출력 메모리  ---> 화면
            DisplayImage();

        }//horizontal Edge
        private void Differcol()
        {
            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성

            const int msize = 3;//사이즈는 변수로 지정해서 사용하는 것이 좋습니다.

            // 마스크 준비 및 정의
            double[,] mask = new double[msize, msize]
            {
                { 1.0 , 1.0 , 1.0},
                { 0.0 , 0.0, 0.0},
                { -1.0 , -1.0 , -1.0}
            };

            if(rawColorSelect)
            {
                //임시 버퍼 
                double[,] tmpInImage = new double[_inH + 2, _inW + 2];
                double[,] tmpoutImage = new double[_outH, _outW];
                //임시 입력을 127로 초기화
                for (int i = 0; i < _inH + 2; i++)
                    for (int k = 0; k < _inW + 2; k++)
                    {
                        tmpInImage[i, k] = 127.0;
                        //지금 127 중간값을 지정 
                    }

                //입력 영상 --> 임시 입력 영상
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        tmpInImage[i + 1, k + 1] = _inImage[i, k];
                    } //왜냐하면 2만큼 더해졌기 때문에 원래 자리에 있던 자리에 쓴다.

                //회선 연산 처리 
                double s = 0.0;

                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        //한점에 대해서 마스크와 연산 (3x3)
                        for (int m = 0; m < msize; m++)
                            for (int n = 0; n < msize; n++)
                            {
                                s += tmpInImage[m + i, n + k] * mask[m, n];
                            }
                        tmpoutImage[i, k] = s;
                        s = 0;
                    }

                //input --> output 할당
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        _outImage[i, k] = (byte)CheckRange((int)tmpoutImage[i, k]);
                    }
            }
            else
            {
                //임시 버퍼 
                double[,,] tmpInImage = new double[3, _inHC + 2, _inWC + 2];
                double[,,] tmpoutImage = new double[3, _outHC, _outWC];

                //임시 입력을 127로 초기화
                for (int i = 0; i < _inHC + 2; i++)
                    for (int k = 0; k < _inWC + 2; k++)
                    {
                        tmpInImage[0, i, k] = 127.0;
                        tmpInImage[1, i, k] = 127.0;
                        tmpInImage[2, i, k] = 127.0;
                        //지금 127 중간값을 지정 
                    }

                //입력 영상 --> 임시 입력 영상
                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            tmpInImage[rgb, i + 1, k + 1] = _inImageColor[rgb, i, k];//matrix x y가 [i+1,k+1]하게 되면 
                        } //왜냐하면 2만큼 더해졌기 때문에 원래 자리에 있던 자리에 쓴다.

                //회선 연산 처리 
                double s = 0.0;
                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            //한점에 대해서 마스크와 연산 (3x3)
                            for (int m = 0; m < msize; m++)
                                for (int n = 0; n < msize; n++)
                                {
                                    s += tmpInImage[rgb, m + i, n + k] * mask[m, n];
                                }
                            tmpoutImage[rgb, i, k] = s;
                            s = 0.0;
                        }

                //마스크의 합계에 따라서 127 더할지 결정
                for (int i = 0; i < _inHC; i++)
                    for (int k = 0; k < _inWC; k++)
                    {
                        tmpoutImage[0, i, k] += 127.0;
                        tmpoutImage[1, i, k] += 127.0;
                        tmpoutImage[2, i, k] += 127.0;
                    }

                //input --> output 할당
                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            _outImageColor[rgb, i, k] = (byte)CheckRange((int)tmpoutImage[rgb, i, k]);
                        }
            }
            
            //출력 메모리  ---> 화면
            DisplayImage();
        } //Differential Row 
        private void DifferRow()
        {
            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성

            const int msize = 3;//사이즈는 변수로 지정해서 사용하는 것이 좋습니다.

            // 마스크 준비 및 정의
            double[,] mask = new double[msize, msize]
            {
                { 1.0 , 0.0 , -1.0},
                { 1.0 , 0.0, -1.0},
                { 1.0 , 0.0 , -1.0}       
            };
            //512 * 512 ===> 514 * 514
            //코드를 명확하게 하면 확장할 때 편하다.
            //회선 영상 실수를 정수로 만드는 것 

            if(rawColorSelect)
            {
                //임시 버퍼 
                double[,] tmpInImage = new double[_inH + 2, _inW + 2];
                double[,] tmpoutImage = new double[_outH, _outW];
                //임시 입력을 127로 초기화
                for (int i = 0; i < _inH + 2; i++)
                    for (int k = 0; k < _inW + 2; k++)
                    {
                        tmpInImage[i, k] = 127.0;
                        //지금 127 중간값을 지정 
                    }

                //입력 영상 --> 임시 입력 영상
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        tmpInImage[i + 1, k + 1] = _inImage[i, k];
                    } //왜냐하면 2만큼 더해졌기 때문에 원래 자리에 있던 자리에 쓴다.

                //회선 연산 처리 
                double s = 0.0;

                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        //한점에 대해서 마스크와 연산 (3x3)
                        for (int m = 0; m < msize; m++)
                            for (int n = 0; n < msize; n++)
                            {
                                s += tmpInImage[m + i, n + k] * mask[m, n];
                            }
                        tmpoutImage[i, k] = s;
                        s = 0;
                    }

                //input --> output 할당
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {                     
                         _outImage[i, k] = (byte)CheckRange((int)tmpoutImage[i, k]);
                    }
            }
            else
            {
                //임시 버퍼 
                double[,,] tmpInImage = new double[3, _inHC + 2, _inWC + 2];
                double[,,] tmpoutImage = new double[3, _outHC, _outWC];

                //임시 입력을 127로 초기화
                for (int i = 0; i < _inHC + 2; i++)
                    for (int k = 0; k < _inWC + 2; k++)
                    {
                        tmpInImage[0, i, k] = 127.0;
                        tmpInImage[1, i, k] = 127.0;
                        tmpInImage[2, i, k] = 127.0;
                        //지금 127 중간값을 지정 
                    }

                //입력 영상 --> 임시 입력 영상
                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            tmpInImage[rgb, i + 1, k + 1] = _inImageColor[rgb,i,k];//matrix x y가 [i+1,k+1]하게 되면 
                        } //왜냐하면 2만큼 더해졌기 때문에 원래 자리에 있던 자리에 쓴다.

                //회선 연산 처리 
                double s = 0.0;
                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            //한점에 대해서 마스크와 연산 (3x3)
                            for (int m = 0; m < msize; m++)
                                for (int n = 0; n < msize; n++)
                                {
                                    s += tmpInImage[rgb, m + i, n + k] * mask[m, n];
                                }
                            tmpoutImage[rgb, i, k] = s;
                            s = 0.0;
                        }

                //마스크의 합계에 따라서 127 더할지 결정
                for (int i = 0; i < _inHC; i++)
                    for (int k = 0; k < _inWC; k++)
                    {
                        tmpoutImage[0, i, k] += 127.0;
                        tmpoutImage[1, i, k] += 127.0;
                        tmpoutImage[2, i, k] += 127.0;
                    }

                //input --> output 할당
                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            _outImageColor[rgb, i, k] = (byte)CheckRange((int)tmpoutImage[rgb, i, k]);
                        }
            }

            //출력 메모리  ---> 화면
            DisplayImage();
        } //Differential col
        private void OnOpr()
        {
            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성

            if(rawColorSelect)
            {
                //임시 버퍼 
                double[,] tmpInImage = new double[_inH + 2, _inW + 2];
                double[,] tmpoutImage = new double[_outH, _outW];

                //임시 입력을 127로 초기화
                for (int i = 0; i < _inH + 2; i++)
                    for (int k = 0; k < _inW + 2; k++)
                    {
                        tmpInImage[i, k] = 127.0;
                        //지금 127 중간값을 지정 
                    }

                //입력 영상 --> 임시 입력 영상
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        tmpInImage[i + 1, k + 1] = _inImage[i, k];//matrix x y가 [i+1,k+1]하게 되면 
                    } //왜냐하면 2만큼 더해졌기 때문에 원래 자리에 있던 자리에 쓴다.

                //회선 연산 처리 
                double s = 0.0;

                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        //한점에 대해서 마스크와 연산 (3x3)
                        for (int m = 0; m < 3; m++)
                            for (int n = 0; n < 3; n++)
                            {
                                if (Math.Abs((tmpInImage[i + 1, k + 1] - tmpInImage[m + i, n + k])) >= s)
                                    s = Math.Abs(tmpInImage[i + 1, k + 1] - tmpInImage[m + i, n + k]);
                            }
                        tmpoutImage[i, k] = s;
                        s = 0;
                    }

                //input --> output 할당
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        if (tmpoutImage[i, k] > 255)
                        {
                            _outImage[i, k] = 255;
                        }
                        else if (tmpoutImage[i, k] < 0)
                        {
                            _outImage[i, k] = 0;
                        }
                        else
                            _outImage[i, k] = (byte)tmpoutImage[i, k];
                    }
            }
            else
            {
                //임시 버퍼 
                double[,,] tmpInImage = new double[3,_inHC + 2, _inWC + 2];
                double[,,] tmpoutImage = new double[3,_outHC, _outWC];

                //임시 입력을 127로 초기화
                for (int i = 0; i < _inHC + 2; i++)
                    for (int k = 0; k < _inWC + 2; k++)
                    {
                        tmpInImage[0,i, k] = 127.0;
                        tmpInImage[1, i, k] = 127.0;
                        tmpInImage[2, i, k] = 127.0;
                        //지금 127 중간값을 지정 
                    }

                //입력 영상 --> 임시 입력 영상
                for(int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            tmpInImage[rgb,i + 1, k + 1] = _inImageColor[rgb,i, k];//matrix x y가 [i+1,k+1]하게 되면 
                        } //왜냐하면 2만큼 더해졌기 때문에 원래 자리에 있던 자리에 쓴다.

                //회선 연산 처리 
                double s = 0.0;
                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            //한점에 대해서 마스크와 연산 (3x3)
                            for (int m = 0; m < 3; m++)
                                for (int n = 0; n < 3; n++)
                                {
                                    if (Math.Abs((tmpInImage[rgb,i + 1, k + 1] - tmpInImage[rgb,m + i, n + k])) >= s)
                                        s = Math.Abs(tmpInImage[rgb,i + 1, k + 1] - tmpInImage[rgb,m + i, n + k]);
                                }
                            tmpoutImage[rgb,i, k] = s;
                            s = 0;
                        }

                //input --> output 할당
                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {                        
                           _outImageColor[rgb,i, k] = (byte)CheckRange((int)(tmpoutImage[rgb,i, k]));
                        }
            }
            
            //출력 메모리  ---> 화면
            DisplayImage();
        } //onHomogenOperator

        //Histogram

        private void HistoStr()
        {
            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성

            // out = (in - min) / (max - min) * 255
            double max_value = _inImage[0, 0];
            double min_value = _inImage[0, 0];

            for (int i = 0; i < _inH; i++)
                for (int k = 0; k < _inW; k++)
                {
                    if (max_value < _inImage[i, k])
                        max_value = _inImage[i, k];
                    if (min_value > _inImage[i, k])
                        min_value = _inImage[i, k];
                }

            //input --> output 할당
            for (int i = 0; i < _inH; i++)
                for (int k = 0; k < _inW; k++)
                {
                    _outImage[i, k] = (byte)(((_inImage[i, k] - min_value) / (max_value - min_value)) * 255.0);
                }

            //출력 메모리  ---> 화면
            DisplayImage();
        }//Histogram Stretch
        private void EndInSearch()
        {
            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성

            if(rawColorSelect)
            {
                // out = (in - min) / (max - min) * 255
                double max_value = _inImage[0, 0];
                double min_value = _inImage[0, 0];

                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        if (max_value < _inImage[i, k])
                            max_value = _inImage[i, k];
                        if (min_value > _inImage[i, k])
                            min_value = _inImage[i, k];
                    }

                min_value += 70; //min_value에는 +30 왜 너무 차이나지 않게  
                max_value -= 40; //max_value에는 -30

                //input --> output 할당
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        int outvalue = (int)(((_inImage[i, k] - min_value) / (max_value - min_value)) * 255.0);
                        _outImage[i, k] = (byte)CheckRange(outvalue);
                    }
                //출력 메모리  ---> 화면
            }
            else
            {
                byte[,] tmp = new byte[_inHC * 3, _inWC * 3];
                // out = (in - min) / (max - min) * 255         

                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            tmp[i, k] = _inImageColor[rgb, i, k];
                        }

                // out = (in - min) / (max - min) * 255
                double max_value = tmp[0, 0];
                double min_value = tmp[0, 0];

                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            if (max_value < tmp[i, k])
                                max_value = tmp[i, k];
                            if (min_value > tmp[i, k])
                                min_value = tmp[i, k];
                        }

                min_value += 70; //min_value에는 +30 왜 너무 차이나지 않게  
                max_value -= 40; //max_value에는 -30

                //input --> output 할당
                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            int outvalue = (int)(((tmp[i, k] - min_value) / (max_value - min_value)) * 255.0);
                            if (outvalue > 255)
                                for (int m = 0; m < 3; m++)
                                    _outImageColor[m, i, k] = 255;                            
                            else if (outvalue < 0)
                                for (int m = 0; m < 3; m++)
                                    _outImageColor[m, i, k] = 0;
                            else
                                for (int m = 0; m < 3; m++)
                                    _outImageColor[m, i, k] = (byte)outvalue;
                        }
            }

            DisplayImage();
        } //end In Search
        private void HistoStrequal()
        {
            fileCheck();
            //(중요!) 출력영상의 크기를 결정 ---> 알고리즘에 따라서
            _outH = _inH;
            _outW = _inW;
            // 출력 영상 메모리 할당
            _outImage = new byte[_outH, _outW];
            // ** 진짜 영상처리 알고리즘 구현

            // 1단계 : 히스토그램 생성
            int[] histo = new int[256]; // 색상별 개수 카운트
            for (int i = 0; i < _inH; i++)
                for (int k = 0; k < _inW; k++)
                    histo[_inImage[i, k]]++;

            // 2단계: 누적 히스토그램 생성
            int[] sumHisto = new int[256];
            sumHisto[0] = histo[0];
            for (int i = 1; i < histo.Length; i++)
                sumHisto[i] = sumHisto[i - 1] + histo[i];

            // 3단계 : 정규화된 누적 히스토그램 생성
            double[] normalHisto = new double[256];

            // 공식 : 누적합 / 픽셀개수 * 최대화소값
            for (int i = 0; i < sumHisto.Length; i++)
                normalHisto[i] = (double)sumHisto[i] / (_inH * _inW) * 255;

            // 4단계 : 정규화된 값으로 영상 출력(input --> output 할당)
            for (int i = 0; i < _inH; i++)
                for (int k = 0; k < _inW; k++)
                {
                    _outImage[i, k] = (byte)normalHisto[_inImage[i, k]];
                }

            DisplayImage();  // 출력메모리 --> 화면
        }
        private void ShMsk()
        {
            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성

            const int msize = 3;//사이즈는 변수로 지정해서 사용하는 것이 좋습니다.

            // 마스크 준비 및 정의
            double[,] mask = new double[msize, msize]
            {
                { -1.0 , -1.0 , -1.0},
                { -1.0 , 9.0 , -1.0},
                { -1.0 , -1.0 , -1.0}
            };
     

            if(rawColorSelect)
            {
                //임시 버퍼 
                double[,] tmpInImage = new double[_inH + 2, _inW + 2];
                double[,] tmpoutImage = new double[_outH, _outW];

                //임시 입력을 127로 초기화
                for (int i = 0; i < _inH + 2; i++)
                    for (int k = 0; k < _inW + 2; k++)
                    {
                        tmpInImage[i, k] = 127.0;
                        //지금 127 중간값을 지정 
                    }

                //입력 영상 --> 임시 입력 영상
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        tmpInImage[i + 1, k + 1] = _inImage[i, k];//matrix x y가 [i+1,k+1]하게 되면 
                    } //왜냐하면 2만큼 더해졌기 때문에 원래 자리에 있던 자리에 쓴다.

                //회선 연산 처리 
                double s = 0.0;

                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        //한점에 대해서 마스크와 연산 (3x3)
                        for (int m = 0; m < msize; m++)
                            for (int n = 0; n < msize; n++)
                            {
                                s += tmpInImage[m + i, n + k] * mask[m, n];
                            }
                        tmpoutImage[i, k] = s;
                        s = 0;
                    }

                //input --> output 할당
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        if (tmpoutImage[i, k] > 255)
                        {
                            _outImage[i, k] = 255;
                        }
                        else if (tmpoutImage[i, k] < 0)
                        {
                            _outImage[i, k] = 0;
                        }
                        else
                            _outImage[i, k] = (byte)tmpoutImage[i, k];
                    }
            }
            else
            {
                //임시 버퍼 
                double[,,] tmpInImage = new double[3,_inHC + 2, _inWC + 2];
                double[,,] tmpoutImage = new double[3,_outHC, _outWC];

                //임시 입력을 127로 초기화
                for (int i = 0; i < _inH + 2; i++)
                    for (int k = 0; k < _inW + 2; k++)
                    {
                        tmpInImage[0,i, k] = 127.0;
                        tmpInImage[1, i, k] = 127.0;
                        tmpInImage[2, i, k] = 127.0;
                        //지금 127 중간값을 지정 
                    }

                //입력 영상 --> 임시 입력 영상
                for(int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            tmpInImage[rgb,i + 1, k + 1] = _inImageColor[rgb,i, k];//matrix x y가 [i+1,k+1]하게 되면 
                        } //왜냐하면 2만큼 더해졌기 때문에 원래 자리에 있던 자리에 쓴다.

                //회선 연산 처리 
                double s = 0.0;
                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            //한점에 대해서 마스크와 연산 (3x3)
                            for (int m = 0; m < msize; m++)
                                for (int n = 0; n < msize; n++)
                                {
                                    s += tmpInImage[rgb,m + i, n + k] * mask[m, n];
                                }
                            tmpoutImage[rgb,i, k] = s;
                            s = 0.0;
                        }

                //input --> output 할당
                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {                       
                           _outImageColor[rgb,i, k] = (byte)CheckRange((int)tmpoutImage[rgb,i, k]);
                        }
            }
            
            //출력 메모리  ---> 화면
            DisplayImage();
        }//Sharpening Mask(1)
        private void ShMskTwo()
        {
            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성

            const int msize = 3;//사이즈는 변수로 지정해서 사용하는 것이 좋습니다.

            // 마스크 준비 및 정의
            double[,] mask = new double[msize, msize]
            {
                { 0.0 , -1.0 , 0.0},
                { -1.0 , 5.0 , -1.0},
                { 0.0 , -1.0 , 1.0}
            };  

            //임시 버퍼
            if(rawColorSelect)
            {
                double[,] tmpInImage = new double[_inH + 2, _inW + 2];
                double[,] tmpoutImage = new double[_outH, _outW];

                //임시 입력을 127로 초기화
                for (int i = 0; i < _inH + 2; i++)
                    for (int k = 0; k < _inW + 2; k++)
                    {
                        tmpInImage[i, k] = 127.0;
                        //지금 127 중간값을 지정 
                    }

                //입력 영상 --> 임시 입력 영상
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        tmpInImage[i + 1, k + 1] = _inImage[i, k];//matrix x y가 [i+1,k+1]하게 되면 
                    } //왜냐하면 2만큼 더해졌기 때문에 원래 자리에 있던 자리에 쓴다.

                //회선 연산 처리 
                double s = 0.0;

                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {
                        //한점에 대해서 마스크와 연산 (3x3)
                        for (int m = 0; m < msize; m++)
                            for (int n = 0; n < msize; n++)
                            {
                                s += tmpInImage[m + i, n + k] * mask[m, n];
                            }
                        tmpoutImage[i, k] = s;
                        s = 0;
                    }

                //input --> output 할당
                for (int i = 0; i < _inH; i++)
                    for (int k = 0; k < _inW; k++)
                    {                     
                       _outImage[i, k] = (byte)CheckRange((int)tmpoutImage[i, k]);
                    }
            }
            else
            {    
                //임시 버퍼 
                double[,,] tmpInImage = new double[3, _inHC + 2, _inWC + 2];
                double[,,] tmpoutImage = new double[3, _outHC, _outWC];

                //임시 입력을 127로 초기화
                for (int i = 0; i < _inH + 2; i++)
                    for (int k = 0; k < _inW + 2; k++)
                    {
                        tmpInImage[0, i, k] = 127.0;
                        tmpInImage[1, i, k] = 127.0;
                        tmpInImage[2, i, k] = 127.0;
                        //지금 127 중간값을 지정 
                    }

                //입력 영상 --> 임시 입력 영상
                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            tmpInImage[rgb, i + 1, k + 1] = _inImageColor[rgb, i, k];//matrix x y가 [i+1,k+1]하게 되면 
                        } //왜냐하면 2만큼 더해졌기 때문에 원래 자리에 있던 자리에 쓴다.

                //회선 연산 처리 
                double s = 0.0;
                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            //한점에 대해서 마스크와 연산 (3x3)
                            for (int m = 0; m < msize; m++)
                                for (int n = 0; n < msize; n++)
                                {
                                    s += tmpInImage[rgb, m + i, n + k] * mask[m, n];
                                }
                            tmpoutImage[rgb, i, k] = s;
                            s = 0.0;
                        }

                //input --> output 할당
                for (int rgb = 0; rgb < 3; rgb++)
                    for (int i = 0; i < _inHC; i++)
                        for (int k = 0; k < _inWC; k++)
                        {
                            _outImageColor[rgb, i, k] = (byte)CheckRange((int)tmpoutImage[rgb, i, k]);
                        }

            }

            //출력 메모리  ---> 화면
            DisplayImage();
        }//Sharpening Mask(2)
        private void Drawchart()
        {
            fileCheck(); //파일확인 후 구현할 것

            // 빈도수 카운트
            int[] count = new int[256];

            for (int i = 0; i < _outH; i++)
                for (int k = 0; k < _outW; k++)
                    count[_outImage[i, k]]++;

            // 차트 그리기
            chbright.Visible = true;
            chbright.Size = new System.Drawing.Size(_outH,_outW);
            
            chbright.Series[0].ChartType =
                System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chbright.Series[0].Points.Clear();

            for (int i = 0; i < 256; i++)
                chbright.Series[0].Points.AddXY(i, count[i]);

        }//Brightness chart 
        private void GraphEnd()
        {
            chbright.Visible = false;
        }//graph End
 
        //openCv

        private void getColorCv()
        {
            int oW, oH; // OpenCV 매트릭스의 출력Mat 크기
            // outCvImage 크기를 결정(중요!) --> 알고리즘
            oH = inCvImage.Height;
            oW = inCvImage.Width;
            outCvimage = new Mat();

            /// OpenCV용 알고리즘 활용 ////
            Mat hsv = new Mat(new OpenCvSharp.Size(oW, oH), MatType.CV_8UC3);
            Cv2.CvtColor(inCvImage, hsv, ColorConversionCodes.BGR2HSV);
            Mat[] HSV = Cv2.Split(hsv);
            Mat H = new Mat(inCvImage.Size(), MatType.CV_8UC1);
            Cv2.InRange(HSV[0], new Scalar(8), new Scalar(20), H);
            Cv2.BitwiseAnd(hsv, hsv, outCvimage, H);
            Cv2.CvtColor(outCvimage, outCvimage, ColorConversionCodes.HSV2BGR);
            ////////////////////////////

            Cv2ToOutImage();
        }
        private void conerCV()
        {
            // 알고리즘에 의해 outCvImage 크기 결정
            int oH, oW;
            oH = inCvImage.Height;
            oW = inCvImage.Width;
            // CV2용 메모리 확보
            outCvimage = Mat.Ones(new OpenCvSharp.Size(oW, oH), MatType.CV_8UC1);

            // 알고리즘 적용
            Mat gray = new Mat();
            outCvimage = inCvImage.Clone();

            Cv2.CvtColor(inCvImage, gray, ColorConversionCodes.BGR2GRAY);
            Point2f[] corners = Cv2.GoodFeaturesToTrack(gray, 1000, 0.03, 5, null, 3, false, 0);

            for (int i = 0; i < corners.Length; i++)
            {
                OpenCvSharp.Point pt = new OpenCvSharp.Point((int)corners[i].X, (int)corners[i].Y);
                Cv2.Circle(outCvimage, pt, 5, Scalar.Yellow, Cv2.FILLED);
            }
 
            // Cv2 --> outImage
            Cv2ToOutImage();
        }
        private void rotateCV()
        {
            // 알고리즘에 의해 outCvImage 크기 결정
            int oH, oW;
            oH = inCvImage.Height;
            oW = inCvImage.Width;

            // CV2용 메모리 확보
            outCvimage = Mat.Ones(new OpenCvSharp.Size(oH, oW), MatType.CV_8UC3);

            // 알고리즘 적용
            Mat matrix = Cv2.GetRotationMatrix2D(new Point2f(oW / 2, oH / 2), 45.0, 1.0);
            Cv2.WarpAffine(outCvimage, outCvimage, matrix, new OpenCvSharp.Size(oH, oW));

            // Cv2 --> outImage
            Cv2ToOutImage();
        }
        private void binaryCode()
        {
            // 알고리즘에 의해 outCvImage 크기 결정
            int oH, oW;
            oH = inCvImage.Height;
            oW = inCvImage.Width;

            // CV2용 메모리 확보
            outCvimage = Mat.Ones(new OpenCvSharp.Size(oW, oH), MatType.CV_8UC1);

            // 알고리즘 적용
            Cv2.CvtColor(inCvImage, outCvimage, ColorConversionCodes.BGR2GRAY);
            Cv2.Threshold(outCvimage, outCvimage, 127, 255, ThresholdTypes.Otsu);

            // Cv2 --> outImage
            Cv2ToOutImage();
        }
        private void binaryCode2()
        {
            // 알고리즘에 의해 outCvImage 크기 결정
            int oH, oW;
            oH = inCvImage.Height;
            oW = inCvImage.Width;

            // CV2용 메모리 확보
            outCvimage = Mat.Ones(new OpenCvSharp.Size(oW, oH), MatType.CV_8UC1);

            // 알고리즘 적용
            Cv2.CvtColor(inCvImage, outCvimage, ColorConversionCodes.BGR2GRAY);
            Cv2.AdaptiveThreshold(outCvimage, outCvimage, 255,
                AdaptiveThresholdTypes.GaussianC, ThresholdTypes.Binary, 25, 5);

            // Cv2 --> outImage
            Cv2ToOutImage();
        }
        private void shine()
        {
            // 알고리즘에 의해 outCvImage 크기 결정
            int oH, oW;
            oH = inCvImage.Height;
            oW = inCvImage.Width;

            // CV2용 메모리 확보
            outCvimage = Mat.Ones(new OpenCvSharp.Size(oW, oH), MatType.CV_8UC1);

            // 알고리즘 적용
            Cv2.Blur(inCvImage, outCvimage, new OpenCvSharp.Size(9, 9));

            // Cv2 --> outImage
            Cv2ToOutImage();
        }
        private void mopolo()
        {
            // 알고리즘에 의해 outCvImage 크기 결정
            int oH, oW;
            oH = inCvImage.Height;
            oW = inCvImage.Width;

            // CV2용 메모리 확보
            outCvimage = Mat.Ones(new OpenCvSharp.Size(oW, oH), MatType.CV_8UC1);

            // 알고리즘 적용
            Mat kernel = Cv2.GetStructuringElement(MorphShapes.Cross,
                new OpenCvSharp.Size(3, 3));
            Cv2.Dilate(inCvImage, outCvimage, kernel, new OpenCvSharp.Point(-1, -1),
                3, BorderTypes.Reflect101, new Scalar(0));

            // Cv2 --> outImage
            Cv2ToOutImage();
        }
        private void sideLine()
        {
            // 알고리즘에 의해 outCvImage 크기 결정
            int oH, oW;
            oH = inCvImage.Height;
            oW = inCvImage.Width;

            // CV2용 메모리 확보
            outCvimage = Mat.Ones(new OpenCvSharp.Size(oW, oH), MatType.CV_8UC1);

            // 알고리즘 적용
            Cv2.Sobel(inCvImage, outCvimage, MatType.CV_8UC1,
                1, 0, 3, 1, 0, BorderTypes.Reflect101);

            // Cv2 --> outImage
            Cv2ToOutImage();
        }
        private void CircleS()
        {
            // 알고리즘에 의해 outCvImage 크기 결정
            int oH, oW;
            oH = inCvImage.Height;
            oW = inCvImage.Width;

            // CV2용 메모리 확보
            outCvimage = Mat.Ones(new OpenCvSharp.Size(oW, oH), MatType.CV_8UC1);


            // 알고리즘 적용
            Mat image = new Mat();
            outCvimage = inCvImage.Clone();

            Mat kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(3, 3));

            Cv2.CvtColor(inCvImage, image, ColorConversionCodes.BGR2GRAY);
            Cv2.Dilate(image, image, kernel, new OpenCvSharp.Point(-1, -1), 3);
            Cv2.GaussianBlur(image, image, new OpenCvSharp.Size(13, 13), 3, 3, BorderTypes.Reflect101);
            Cv2.Erode(image, image, kernel, new OpenCvSharp.Point(-1, -1), 3);

            CircleSegment[] circles = Cv2.HoughCircles(image, HoughMethods.Gradient, 1, 100, 100, 35, 0, 0);

            for (int i = 0; i < circles.Length; i++)
            {
                OpenCvSharp.Point center = new OpenCvSharp.Point(circles[i].Center.X, circles[i].Center.Y);

                Cv2.Circle(outCvimage, center, (int)circles[i].Radius, Scalar.White, 3);
                Cv2.Circle(outCvimage, center, 5, Scalar.AntiqueWhite, Cv2.FILLED);
            }

            // Cv2 --> outImage
            Cv2ToOutImage();
        }
        
        //HSV

        //HSV 변환

        private void Saturation()
        {
            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성

            Color c;
            double hh, ss = 0, vv;
            int rr, gg, bb;

            //영상 처리

            double value = GetValue();

            for (int i = 0; i < _inHC; i++)
                for (int k = 0; k < _inWC; k++)
                {
                    rr = _inImageColor[0, i, k];
                    gg = _inImageColor[1, i, k];
                    bb = _inImageColor[2, i, k];
                    //rgb  --> hsv(원뿔모양)
                    c = Color.FromArgb(rr, gg, bb); //색상 픽셀
                    hh = c.GetHue();
                    ss = c.GetSaturation();
                    vv = c.GetBrightness();

                    ss += value;

                    //채도 조절

                    HsvToRgb(hh, ss, vv, out rr, out gg, out bb);

                    _outImageColor[0, i, k] = (byte)CheckRange(rr);
                    _outImageColor[1, i, k] = (byte)CheckRange(gg);
                    _outImageColor[2, i, k] = (byte)CheckRange(bb);
                }

            DisplayImage();// 화면 출력
        } //채도 조절
        private void BirghtnessHSV()
        {
            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성

            Color c;
            double hh, ss, vv = 0;
            int rr, gg, bb;

            //영상 처리
            //채도 조절
            double value = GetValue();

            //input --> output 할당
            for (int i = 0; i < _inHC; i++)
                for (int k = 0; k < _inWC; k++)
                {
                    rr = _inImageColor[0, i, k];
                    gg = _inImageColor[1, i, k];
                    bb = _inImageColor[2, i, k];
                    //rgb  --> hsv(원뿔모양)


                    c = Color.FromArgb(rr, gg, bb); //색상 픽셀
                    hh = c.GetHue();
                    ss = c.GetSaturation();
                    vv = c.GetBrightness();

                    vv += value;

                    HsvToRgb(hh, ss, vv, out rr, out gg, out bb);

                    _outImageColor[0, i, k] = (byte)CheckRange(rr);
                    _outImageColor[1, i, k] = (byte)CheckRange(gg);
                    _outImageColor[2, i, k] = (byte)CheckRange(bb);
                }

            DisplayImage();// 화면 출력
        } //명도 조절

        // mouse select

        private void Bright_mouse()
        {
            // ** 진짜 영상처리 알고리즘 구현
            int value = 100;

            //input --> output 할당
            for (int rgb = 0; rgb < 3; rgb++)
                for (int i = 0; i < _inHC; i++)
                    for (int k = 0; k < _inWC; k++)
                    {
                        if ((sx <= i && i <= ex) && (sy <= k && k <= ey))
                        {                     
                            _outImageColor[rgb, i, k] = (byte)CheckRange(_inImageColor[rgb, i, k] + value);
                        }
                        else
                        {
                            _outImageColor[rgb, i, k] = _inImageColor[rgb, i, k];
                        }
                    }

            DisplayImage();  // 출력메모리 --> 화면
        }//brightness
        private void EndInSearch_mouse()
        {
            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성

            byte[,] tmp = new byte[_inHC * 3, _inWC * 3];
            // out = (in - min) / (max - min) * 255         

            for (int rgb = 0; rgb < 3; rgb++)
                for (int i = 0; i < _inHC; i++)
                    for (int k = 0; k < _inWC; k++)
                    {
                        tmp[i, k] = _inImageColor[rgb, i, k];
                    }

            // out = (in - min) / (max - min) * 255
            double max_value = tmp[0, 0];
            double min_value = tmp[0, 0];

            for (int rgb = 0; rgb < 3; rgb++)
                for (int i = 0; i < _inHC; i++)
                    for (int k = 0; k < _inWC; k++)
                    {
                        if (max_value < tmp[i, k])
                            max_value = tmp[i, k];
                        if (min_value > tmp[i, k])
                            min_value = tmp[i, k];
                    }

            min_value += 70; //min_value에는 +30 왜 너무 차이나지 않게  
            max_value -= 40; //max_value에는 -30

            //input --> output 할당
            for (int rgb = 0; rgb < 3; rgb++)
                for (int i = 0; i < _inHC; i++)
                    for (int k = 0; k < _inWC; k++)
                    {
                        if ((sx <= i && i <= ex) && (sy <= k && k <= ey))
                        {
                            int outvalue = (int)(((tmp[i, k] - min_value) / (max_value - min_value)) * 255.0);                       
                                 _outImageColor[rgb, i, k] = (byte)CheckRange(outvalue);
                        }
                        else
                        {
                            _outImageColor[rgb, i, k] = _inImageColor[rgb, i, k];
                        }
                    }

            //출력 메모리  ---> 화면
            DisplayImage();
        }//End-in Search    
        private void MonoChromemdi_mouse()
        {
            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성
            long[] num = new long[(_inHC * _inWC * 3)];

            long idx = 0;//1차원 배열로 변경할 index 0으로 초기화

            for (int rgb = 0; rgb < 3; rgb++)
            {
                for (int i = 0; i < _inHC; i++)
                {
                    for (int k = 0; k < _inWC; k++)
                    {
                        num[idx] = _inImageColor[rgb, i, k];
                        idx++;
                    }
                }
            }

            idx--;

            Array.Sort(num);

            //input --> output 할당
            for (int rgb = 0; rgb < 3; rgb++)
            {
                //영상 처리
                for (int i = 0; i < _inHC; i++)
                {
                    for (int k = 0; k < _inWC; k++)
                    {
                        if ((sx <= i && i <= ex) && (sy <= k && k <= ey))
                        {
                            if (num[(idx / 2)] < _inImageColor[0, i, k] || num[(idx / 2)] < _inImageColor[1, i, k] || num[(idx / 2)] < _inImageColor[2, i, k])
                            {
                                for (int m = 0; m < 3; m++)
                                    _outImageColor[m, i, k] = 255;
                            }
                            else
                            {
                                for (int m = 0; m < 3; m++)
                                    _outImageColor[m, i, k] = 0;
                            }
                        }
                        else
                        {
                            _outImageColor[rgb, i, k] = _inImageColor[rgb, i, k];
                        }
                    }
                }
            }
            DisplayImage();
        }//흑백처리
        private void Negative_TransForm_mouse()
        {
            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성

            //input --> output 할당
            for (int rgb = 0; rgb < 3; rgb++)
            {
                for (int i = 0; i < _inHC; i++)
                    for (int k = 0; k < _inWC; k++)
                    {
                        if ((sx <= i && i <= ex) && (sy <= k && k <= ey))
                        {
                            _outImageColor[rgb, i, k] = (byte)(255 - _inImageColor[rgb, i, k]);//화면으로 출력되는 배열에 저장
                        }
                        else
                        {
                            _outImageColor[rgb, i, k] = _inImageColor[rgb,i,k];
                        }
                    }

            }

            DisplayImage();
        }//색상반전
        private void PalabollaCup_mouse()
        {
            fileCheck();//File 확인
            InOutChange();//화면 크기 구성 

            //input --> output 할당
            for (int rgb = 0; rgb < 3; rgb++)
            {
                for (int i = 0; i < _inHC; i++)
                    for (int k = 0; k < _inWC; k++)
                    {
                        if ((sx <= i && i <= ex) && (sy <= k && k <= ey))
                        {
                            _outImageColor[rgb, i, k] = (byte)(255.0 * (_inImageColor[rgb, i, k] / 128.0 - 1) * (_inImageColor[rgb, i, k] / 128.0 - 1.0));//화면으로 출력되는 배열에 저장
                        }
                        else
                        {
                            _outImageColor[rgb, i, k] = _inImageColor[rgb, i, k];
                        }
                              
                    }                                        
            }
            DisplayImage();
        }       
        private void VerEdge_mouse()
        {
            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성

            const int msize = 3;//사이즈는 변수로 지정해서 사용하는 것이 좋습니다.

            // 마스크 준비 및 정의
            double[,] mask = new double[msize, msize]
            {
                { 0.0 , -1.0 , 0.0},
                { 0.0 , 0.0 , 0.0},
                { 0.0 , 0.0 , 1.0}
            };
            //512 * 512 ===> 514 * 514
            //코드를 명확하게 하면 확장할 때 편하다.
            //회선 영상 실수를 정수로 만드는 것 
            //임시 버퍼 
            double[,,] tmpInImage = new double[3, _inHC + 2, _inWC + 2];
            double[,,] tmpoutImage = new double[3, _outHC, _outWC];

            //임시 입력을 127로 초기화
            for (int i = 0; i < _inHC + 2; i++)
                for (int k = 0; k < _inWC + 2; k++)
                {
                    tmpInImage[0, i, k] = 127.0;
                    tmpInImage[1, i, k] = 127.0;
                    tmpInImage[2, i, k] = 127.0;
                    //지금 127 중간값을 지정 
                }

            //입력 영상 --> 임시 입력 영상
            for (int rgb = 0; rgb < 3; rgb++)
                for (int i = 0; i < _inHC; i++)
                    for (int k = 0; k < _inWC; k++)
                    {
                        tmpInImage[rgb, i + 1, k + 1] = _inImageColor[rgb, i, k];//matrix x y가 [i+1,k+1]하게 되면 
                    } //왜냐하면 2만큼 더해졌기 때문에 원래 자리에 있던 자리에 쓴다.

            //회선 연산 처리 
            double s = 0.0;
            for (int rgb = 0; rgb < 3; rgb++)
                for (int i = 0; i < _inHC; i++)
                    for (int k = 0; k < _inWC; k++)
                    {
                        //한점에 대해서 마스크와 연산 (3x3)
                        for (int m = 0; m < msize; m++)
                            for (int n = 0; n < msize; n++)
                            {
                                s += tmpInImage[rgb, m + i, n + k] * mask[m, n];
                            }
                        tmpoutImage[rgb, i, k] = s;
                        s = 0.0;
                    }

            //마스크의 합계에 따라서 127 더할지 결정
            for (int i = 0; i < _inHC; i++)
                for (int k = 0; k < _inWC; k++)
                {
                    tmpoutImage[0, i, k] += 127.0;
                    tmpoutImage[1, i, k] += 127.0;
                    tmpoutImage[2, i, k] += 127.0;
                }

            //input --> output 할당
            for (int rgb = 0; rgb < 3; rgb++)
                for (int i = 0; i < _inHC; i++)
                    for (int k = 0; k < _inWC; k++)
                    {
                        if ((sx <= i && i <= ex) && (sy <= k && k <= ey))
                        {
                            _outImageColor[rgb, i, k] = (byte)CheckRange((int)tmpoutImage[rgb, i, k]);
                        }
                        else
                        {
                            _outImageColor[rgb, i, k] = _inImageColor[rgb, i, k];
                        }
                    }
            DisplayImage();
        }
        private void HoriEdge_mouse()
        {
            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성

            const int msize = 3;//사이즈는 변수로 지정해서 사용하는 것이 좋습니다.

            // 마스크 준비 및 정의
            double[,] mask = new double[msize, msize]
            {
                { 0.0 , -1.0 , 0.0},
                { 0.0 , 1.0 , 0.0},
                { 0.0 , 0.0 , 0.0}
            };

            //임시 버퍼 
            double[,,] tmpInImage = new double[3, _inHC + 2, _inWC + 2];
            double[,,] tmpoutImage = new double[3, _outHC, _outWC];

            //임시 입력을 127로 초기화
            for (int i = 0; i < _inHC + 2; i++)
                for (int k = 0; k < _inWC + 2; k++)
                {
                    tmpInImage[0, i, k] = 127.0;
                    tmpInImage[1, i, k] = 127.0;
                    tmpInImage[2, i, k] = 127.0;
                    //지금 127 중간값을 지정 
                }

            //입력 영상 --> 임시 입력 영상
            for (int rgb = 0; rgb < 3; rgb++)
                for (int i = 0; i < _inHC; i++)
                    for (int k = 0; k < _inWC; k++)
                    {
                        tmpInImage[rgb, i + 1, k + 1] = _inImageColor[rgb, i, k];//matrix x y가 [i+1,k+1]하게 되면 
                    } //왜냐하면 2만큼 더해졌기 때문에 원래 자리에 있던 자리에 쓴다.

            //회선 연산 처리 
            double s = 0.0;
            for (int rgb = 0; rgb < 3; rgb++)
                for (int i = 0; i < _inHC; i++)
                    for (int k = 0; k < _inWC; k++)
                    {
                        //한점에 대해서 마스크와 연산 (3x3)
                        for (int m = 0; m < msize; m++)
                            for (int n = 0; n < msize; n++)
                            {
                                s += tmpInImage[rgb, m + i, n + k] * mask[m, n];
                            }
                        tmpoutImage[rgb, i, k] = s;
                        s = 0.0;
                    }

            //마스크의 합계에 따라서 127 더할지 결정
            for (int i = 0; i < _inHC; i++)
                for (int k = 0; k < _inWC; k++)
                {
                    tmpoutImage[0, i, k] += 127.0;
                    tmpoutImage[1, i, k] += 127.0;
                    tmpoutImage[2, i, k] += 127.0;
                }

            //input --> output 할당
            for (int rgb = 0; rgb < 3; rgb++)
                for (int i = 0; i < _inHC; i++)
                    for (int k = 0; k < _inWC; k++)
                    {
                        if ((sx <= i && i <= ex) && (sy <= k && k <= ey))
                        {
                            _outImageColor[rgb, i, k] = (byte)CheckRange((int)tmpoutImage[rgb, i, k]);
                        }
                        else
                        {
                            _outImageColor[rgb, i, k] = _inImageColor[rgb, i, k];
                        }
                    }
            DisplayImage();
        }
        private void Differcol_mouse()
        {
            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성

            const int msize = 3;//사이즈는 변수로 지정해서 사용하는 것이 좋습니다.

            // 마스크 준비 및 정의
            double[,] mask = new double[msize, msize]
            {
                { 1.0 , 1.0 , 1.0},
                { 0.0 , 0.0, 0.0},
                { -1.0 , -1.0 , -1.0}
            };

            //임시 버퍼 
            double[,,] tmpInImage = new double[3, _inHC + 2, _inWC + 2];
            double[,,] tmpoutImage = new double[3, _outHC, _outWC];

            //임시 입력을 127로 초기화
            for (int i = 0; i < _inHC + 2; i++)
                for (int k = 0; k < _inWC + 2; k++)
                {
                    tmpInImage[0, i, k] = 127.0;
                    tmpInImage[1, i, k] = 127.0;
                    tmpInImage[2, i, k] = 127.0;
                    //지금 127 중간값을 지정 
                }

            //입력 영상 --> 임시 입력 영상
            for (int rgb = 0; rgb < 3; rgb++)
                for (int i = 0; i < _inHC; i++)
                    for (int k = 0; k < _inWC; k++)
                    {
                        tmpInImage[rgb, i + 1, k + 1] = _inImageColor[rgb, i, k];//matrix x y가 [i+1,k+1]하게 되면 
                    } //왜냐하면 2만큼 더해졌기 때문에 원래 자리에 있던 자리에 쓴다.

            //회선 연산 처리 
            double s = 0.0;
            for (int rgb = 0; rgb < 3; rgb++)
                for (int i = 0; i < _inHC; i++)
                    for (int k = 0; k < _inWC; k++)
                    {
                        //한점에 대해서 마스크와 연산 (3x3)
                        for (int m = 0; m < msize; m++)
                            for (int n = 0; n < msize; n++)
                            {
                                s += tmpInImage[rgb, m + i, n + k] * mask[m, n];
                            }
                        tmpoutImage[rgb, i, k] = s;
                        s = 0.0;
                    }

            //마스크의 합계에 따라서 127 더할지 결정
            for (int i = 0; i < _inHC; i++)
                for (int k = 0; k < _inWC; k++)
                {
                    tmpoutImage[0, i, k] += 127.0;
                    tmpoutImage[1, i, k] += 127.0;
                    tmpoutImage[2, i, k] += 127.0;
                }

            //input --> output 할당
            for (int rgb = 0; rgb < 3; rgb++)
                for (int i = 0; i < _inHC; i++)
                    for (int k = 0; k < _inWC; k++)
                    {
                        if ((sx <= i && i <= ex) && (sy <= k && k <= ey))
                        {
                            _outImageColor[rgb, i, k] = (byte)CheckRange((int)tmpoutImage[rgb, i, k]);
                        }
                        else
                        {
                            _outImageColor[rgb, i, k] = _inImageColor[rgb, i, k];
                        }
                    }


            DisplayImage();
        }
        private void DifferRow_mouse()
        {
            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성

            const int msize = 3;//사이즈는 변수로 지정해서 사용하는 것이 좋습니다.

            // 마스크 준비 및 정의
            double[,] mask = new double[msize, msize]
            {
                { 1.0 , 0.0 , -1.0},
                { 1.0 , 0.0, -1.0},
                { 1.0 , 0.0 , -1.0}
            };

            //임시 버퍼 
            double[,,] tmpInImage = new double[3, _inHC + 2, _inWC + 2];
            double[,,] tmpoutImage = new double[3, _outHC, _outWC];

            //임시 입력을 127로 초기화
            for (int i = 0; i < _inHC + 2; i++)
                for (int k = 0; k < _inWC + 2; k++)
                {
                    tmpInImage[0, i, k] = 127.0;
                    tmpInImage[1, i, k] = 127.0;
                    tmpInImage[2, i, k] = 127.0;
                    //지금 127 중간값을 지정 
                }

            //입력 영상 --> 임시 입력 영상
            for (int rgb = 0; rgb < 3; rgb++)
                for (int i = 0; i < _inHC; i++)
                    for (int k = 0; k < _inWC; k++)
                    {
                        tmpInImage[rgb, i + 1, k + 1] = _inImageColor[rgb, i, k];//matrix x y가 [i+1,k+1]하게 되면 
                    } //왜냐하면 2만큼 더해졌기 때문에 원래 자리에 있던 자리에 쓴다.

            //회선 연산 처리 
            double s = 0.0;
            for (int rgb = 0; rgb < 3; rgb++)
                for (int i = 0; i < _inHC; i++)
                    for (int k = 0; k < _inWC; k++)
                    {
                        //한점에 대해서 마스크와 연산 (3x3)
                        for (int m = 0; m < msize; m++)
                            for (int n = 0; n < msize; n++)
                            {
                                s += tmpInImage[rgb, m + i, n + k] * mask[m, n];
                            }
                        tmpoutImage[rgb, i, k] = s;
                        s = 0.0;
                    }

            //마스크의 합계에 따라서 127 더할지 결정
            for (int i = 0; i < _inHC; i++)
                for (int k = 0; k < _inWC; k++)
                {
                    tmpoutImage[0, i, k] += 127.0;
                    tmpoutImage[1, i, k] += 127.0;
                    tmpoutImage[2, i, k] += 127.0;
                }

            //input --> output 할당
            for (int rgb = 0; rgb < 3; rgb++)
                for (int i = 0; i < _inHC; i++)
                    for (int k = 0; k < _inWC; k++)
                    {
                        if ((sx <= i && i <= ex) && (sy <= k && k <= ey))
                        {
                            _outImageColor[rgb, i, k] = (byte)CheckRange((int)tmpoutImage[rgb, i, k]);
                        }
                        else
                        {
                            _outImageColor[rgb, i, k] = _inImageColor[rgb, i, k];
                        }
                    }
            DisplayImage();
        }
        private void HistoStr_mouse()
        {
            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성

            // out = (in - min) / (max - min) * 255
            double max_value = _inImageColor[0,0,0];
            double min_value = _inImageColor[0,0, 0];

            for (int rgb = 0; rgb < 3; rgb++)
                for (int i = 0; i < _inHC; i++)
                    for (int k = 0; k < _inWC; k++)
                    {
                        if (max_value < _inImageColor[rgb,i, k])
                            max_value = _inImageColor[rgb,i, k];
                        if (min_value > _inImageColor[rgb,i, k])
                            min_value = _inImageColor[rgb,i, k];
                    }

            //input --> output 할당
            for (int rgb = 0; rgb < 3; rgb++)
                for (int i = 0; i < _inHC; i++)
                    for (int k = 0; k < _inWC; k++)
                    {
                        if ((sx <= i && i <= ex) && (sy <= k && k <= ey))
                        {
                            _outImageColor[rgb,i, k] = (byte)(((_inImageColor[rgb,i, k] - min_value) / (max_value - min_value)) * 255.0);
                        }
                        else
                        {
                            _outImageColor[rgb, i, k] = _inImageColor[rgb,i,k];
                        }
                    }

            //출력 메모리  ---> 화면
            DisplayImage();
        }
        private void DOG_mouse()
        {
            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성

            const int msize = 7;//사이즈는 변수로 지정해서 사용하는 것이 좋습니다.

            // 마스크 준비 및 정의
            double[,] mask = new double[msize, msize]
            {
                { 0.0 , 0.0 , -1.0 , -1.0,-1.0,0.0,0.0},
                { 0.0 , -2.0 , -3.0 , -3.0,-3.0,-2.0,0.0},
                { -1.0 , -3.0 , 5.0 , 5.0,5.0,-3.0,-1.0},
                { -1.0 , -3.0 , 5.0 , 16.0,5.0,-3.0,-1.0},
                { -1.0 , -3.0 , 5.0 , 5.0,5.0,-3.0,-1.0},
                { 0.0 , -2.0 , -3.0 , -3.0,-3.0,-2.0,0.0},
                { 0.0 , 0.0 , -1.0 , -1.0,-1.0,0.0,0.0}

            };

            //임시 버퍼 
            double[,,] tmpInImage = new double[3, _inHC + 6, _inWC + 6];
            double[,,] tmpoutImage = new double[3, _outHC, _outWC];

            //임시 입력을 127로 초기화
            for (int i = 0; i < _inHC + 6; i++)
                for (int k = 0; k < _inWC + 6; k++)
                {
                    tmpInImage[0, i, k] = 127.0;
                    tmpInImage[1, i, k] = 127.0;
                    tmpInImage[2, i, k] = 127.0;
                    //지금 127 중간값을 지정 
                }

            //입력 영상 --> 임시 입력 영상
            for (int rgb = 0; rgb < 3; rgb++)
                for (int i = 0; i < _inHC; i++)
                    for (int k = 0; k < _inWC; k++)
                    {
                        tmpInImage[rgb, i + 3, k + 3] = _inImageColor[rgb, i, k];//matrix x y가 [i+1,k+1]하게 되면 
                    } //왜냐하면 2만큼 더해졌기 때문에 원래 자리에 있던 자리에 쓴다.

            //회선 연산 처리 
            double s = 0.0;
            for (int rgb = 0; rgb < 3; rgb++)
                for (int i = 0; i < _inHC; i++)
                    for (int k = 0; k < _inWC; k++)
                    {
                        //한점에 대해서 마스크와 연산 (3x3)
                        for (int m = 0; m < msize; m++)
                            for (int n = 0; n < msize; n++)
                            {
                                s += tmpInImage[rgb, m + i, n + k] * mask[m, n];
                            }
                        tmpoutImage[rgb, i, k] = s;
                        s = 0.0;
                    }

            //input --> output 할당
            for (int rgb = 0; rgb < 3; rgb++)
                for (int i = 0; i < _inHC; i++)
                    for (int k = 0; k < _inWC; k++)
                    {
                        if ((sx <= i && i <= ex) && (sy <= k && k <= ey))
                        {
                            _outImageColor[rgb, i, k] = (byte)CheckRange((int)tmpoutImage[rgb, i, k]);
                        }
                        else
                        {
                            _outImageColor[rgb, i, k] = _inImageColor[rgb, i, k];
                        }

                    }
            //출력 메모리  ---> 화면
            DisplayImage();
        }
        private void LOG_mouse()
        {
            fileCheck(); //파일확인 후 구현할 것
            InOutChange();//화면 크기 구성

            const int msize = 5;//사이즈는 변수로 지정해서 사용하는 것이 좋습니다.

            double[,] mask = new double[msize, msize]
            {
                {0.0,0.0,-1.0,0.0,0.0},
                {0.0,-1.0,-2.0,-1.0,0.0},
                {-1.0,-2.0,16.0,-2.0,-1.0},
                {0.0,-1.0,-2.0,-1.0,0.0},
                {0.0,0.0,-1.0,0.0,0.0}
            };

            //임시 버퍼 
            double[,,] tmpInImage = new double[3, _inHC + 4, _inWC + 4];
            double[,,] tmpoutImage = new double[3, _outHC, _outWC];

            //임시 입력을 127로 초기화
            for (int i = 0; i < _inHC + 4; i++)
                for (int k = 0; k < _inWC + 4; k++)
                {
                    tmpInImage[0, i, k] = 127.0;
                    tmpInImage[1, i, k] = 127.0;
                    tmpInImage[2, i, k] = 127.0;
                    //지금 127 중간값을 지정 
                }

            //입력 영상 --> 임시 입력 영상
            for (int rgb = 0; rgb < 3; rgb++)
                for (int i = 0; i < _inHC; i++)
                    for (int k = 0; k < _inWC; k++)
                    {
                        tmpInImage[rgb, i + 3, k + 3] = _inImageColor[rgb, i, k];//matrix x y가 [i+1,k+1]하게 되면 
                    } //왜냐하면 2만큼 더해졌기 때문에 원래 자리에 있던 자리에 쓴다.

            //회선 연산 처리 
            double s = 0.0;
            for (int rgb = 0; rgb < 3; rgb++)
                for (int i = 0; i < _inHC; i++)
                    for (int k = 0; k < _inWC; k++)
                    {
                        //한점에 대해서 마스크와 연산 (3x3)
                        for (int m = 0; m < msize; m++)
                            for (int n = 0; n < msize; n++)
                            {
                                s += tmpInImage[rgb, m + i, n + k] * mask[m, n];
                            }
                        tmpoutImage[rgb, i, k] = s;
                        s = 0.0;
                    }

            //input --> output 할당
            for (int rgb = 0; rgb < 3; rgb++)
                for (int i = 0; i < _inHC; i++)
                    for (int k = 0; k < _inWC; k++)
                    {
                        if ((sx <= i && i <= ex) && (sy <= k && k <= ey))
                        {
                            _outImageColor[rgb, i, k] = (byte)CheckRange((int)tmpoutImage[rgb, i, k]);
                        }
                        else
                        {
                            _outImageColor[rgb, i, k] = _inImageColor[rgb, i, k];
                        }
                    }
            DisplayImage();
        }

        //image processing Function End

    }
}
