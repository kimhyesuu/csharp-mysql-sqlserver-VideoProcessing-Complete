using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 컴퓨터_비전_홍길동_.Volume1
{
    public partial class Valuescontrol : Form
    {
        //중요 전역변수 선언 start

        private byte[,] _tempimage;//임시 입출력 공간 
        private byte[,] _suboutimage, _subinimage;//입출력 
        private int newSub_H, newSub_W;// 원본 배열 ->  New size
        private int oldSub_H, oldSub_W;// 원본 배열 크기 저장
        private int flagTrackbar;// TrackBar 재설정
        private int flagControl;
        //private int flagControl;// 각 종류마다 재설정
        Bitmap paper;
        private Double modalOpa = 0.0;

        public string SetText
        {
            set { this.Text = value; }
        }

        public Valuescontrol(byte[,] ary, int H, int W, int flag)
        {
            InitializeComponent();

            _tempimage = ary;
            oldSub_H = H;
            oldSub_W = W;
            flagTrackbar = flag;
        }//Form1 데이터가지고 옴 
        private void Valuescontrol_Load(object sender, EventArgs e)
        {
            this.timer.Enabled = true;
            Size_compare();
            switch (flagTrackbar)
            {
                case 1:
                    {
                        //mainform에서 -100 < x < 100으로 지정
                        // trackbar, numUpDown도 한계 지정
                        tkBar.Maximum = 100;
                        tkBar.Minimum = -100;
                        tkBar.TickFrequency = 10;
                        numUpDown.Maximum = 100;
                        numUpDown.Minimum = -100;
                        lblMini.Text = (-100).ToString();
                        lblMax.Text = (100).ToString();
                        flagControl = 1;
                    }
                    break;
                case 2:
                    {
                        //mainform에서 -100 < x < 100으로 지정
                        // trackbar, numUpDown도 한계 지정
                        tkBar.Maximum = 3;
                        tkBar.Minimum = -3;
                        tkBar.TickFrequency = 1;
                        numUpDown.Maximum = 3;
                        numUpDown.Minimum = -3;
                        lblMini.Text = (-3).ToString();
                        lblMax.Text = (3).ToString();
                        flagControl = 2;
                    }
                    break;
                case 3:
                    {
                        //mainform에서 -100 < x < 100으로 지정
                        // trackbar, numUpDown도 한계 지정
                        tkBar.Maximum = 100;
                        tkBar.Minimum = 0;
                        tkBar.TickFrequency = 10;
                        numUpDown.Maximum = 100;
                        numUpDown.Minimum = 0;
                        lblMini.Text = (0).ToString();
                        lblMax.Text = (100).ToString();
                        flagControl = 3;
                    }
                    break;
            }
        }//Form1 데이터를 Form2 데이터로 변환

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Close();
        } //값 확정(Form2 -> Form1)
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }//값 취소
        private void btn_return_Click(object sender, EventArgs e)
        {
            tkBar.Value = 0;
            numUpDown.Value = 0;
            Size_compare();
        }//동일 영상
        private void numUpDown_ValueChanged(object sender, EventArgs e)
        {
            int value = (int)numUpDown.Value;
            tkBar.Value = (int)numUpDown.Value;

            switch (flagControl)
            {
                case 1: Mrange_Ex(value); break;
                case 2: strongweak_Bt(value); break;
                case 3: imgMove(value); break;
            }
        }// numUpDown 값 변환
        private void tkBar_ValueChanged(object sender, EventArgs e)
        {
            int value = (int)tkBar.Value;
            numUpDown.Value = tkBar.Value;

            switch (flagControl)
            {
                case 1: Mrange_Ex(value); break;
                case 2: strongweak_Bt(value); break;
                case 3: imgMove(value); break;
            }
        }// tkBar 값 변환

        //Common Function

        private int CheckRange(int i)
        {
            if (i < 0) return 0;
            if (i > 255) return 255;
            return i;
        }//overflow & underflow checking
        private void Size_compare()
        {
            if (oldSub_H == 512)
                subreducimg();
            else if (oldSub_H == 256)
            {
                _suboutimage = _tempimage;
                newSub_H = oldSub_H;
                newSub_W = oldSub_W;
                SubDisplayImage();
            }
            else if (oldSub_H == 128)
                subEnlag_img();
        }  // Size 선택, bar & numupdown 설정
        private void timer_Tick(object sender, EventArgs e)
        {
            if (modalOpa < 100.0)
            {
                modalOpa += 3.6;
                float modalc = Convert.ToSingle(modalOpa);
                float modalf = modalc / 100;
                this.Opacity = modalf;
            }
            else
            {
                this.Opacity = Convert.ToSingle(100 / 100);
                modalOpa = 0.0;
                this.timer.Enabled = false;
            }
        }
        private void SubDisplayImage()
        {
            // 종이,게시판,form(벽)크기 결정
            paper = new Bitmap(newSub_H, newSub_W);
            picBoxImg2.Size = new Size(newSub_H, newSub_W);
            panel2.Size = new Size(newSub_H + 30, newSub_W + 30);
            this.Size = new Size(newSub_H + 400, newSub_W + 200);//tip. +20 +90
            Color pen; // 펜 준비

            for (int i = 0; i < newSub_H; i++)
                for (int k = 0; k < newSub_W; k++)
                {
                    byte data = _suboutimage[i, k];
                    pen = Color.FromArgb(data, data, data); // 펜에 잉크 묻히기
                    paper.SetPixel(k, i, pen); // 종이에 콕 찍기.
                }
            picBoxImg2.Image = paper;  // 벽에 종이 걸기.
        } //image 출력
        private void subreducimg()
        {
            int scale = 2;

            newSub_H = oldSub_H / scale; //출력영상의 크기를 결정
            newSub_W = oldSub_W / scale;
            // int value = 대화상자에서 입력받은 숫자 

            //출력 영상 메모리 할당
            _subinimage = new byte[newSub_H, newSub_H];
            _suboutimage = new byte[newSub_H, newSub_H];
            //진짜 영상 처리 알고리즘 사용

            for (int i = 0; i < oldSub_H; i++)
                for (int k = 0; k < oldSub_W; k++)
                {
                    //백워딩 
                    _suboutimage[i / scale, k / scale] = _subinimage[i / scale, k / scale] = _tempimage[i, k];
                }

            //출력 메모리  ---> 화면
            SubDisplayImage();
        }// reducing
        private void subEnlag_img()
        {
            int scale = 2;

            newSub_H = oldSub_H * scale; //출력영상의 크기를 결정
            newSub_W = oldSub_W * scale;
            // int value = 대화상자에서 입력받은 숫자 

            //출력 영상 메모리 할당
            _subinimage = new byte[newSub_H, newSub_W];
            _suboutimage = new byte[newSub_H, newSub_W];

            //진짜 영상 처리 알고리즘 사용
            for (int i = 0; i < newSub_H; i++)
                for (int k = 0; k < newSub_W; k++)
                {
                    //백워딩 
                    _suboutimage[i, k] = _subinimage[i, k] = _tempimage[i / scale, k / scale];

                }

            //출력 메모리  ---> 화면
            SubDisplayImage();

        }//Enlagmen   

        //Common Function End


        private void Mrange_Ex(int value)
        {
            for (int i = 0; i < newSub_H; i++)
                for (int k = 0; k < newSub_W; k++)
                {
                    _suboutimage[i,k] = _subinimage[i, k];
                }

             int valueplsmis = value;

            //영상 처리 
            for (int i = 0; i < newSub_H; i++)
                for (int k = 0; k < newSub_W; k++)
                {
                    int UnderOverFlow_num = _suboutimage[i, k] + valueplsmis;              
                    _suboutimage[i, k] = (byte)CheckRange(UnderOverFlow_num);               
                }

            SubDisplayImage(); // 화면 출력
        }  //Brightness Control(+,-)
        private void strongweak_Bt(int value)
        {
            for (int i = 0; i < newSub_H; i++)
                for (int k = 0; k < newSub_W; k++)
                {
                    _suboutimage[i, k] = _tempimage[ i, k];
                }

            bool DivMulflag = false;
            int UnderOverFlow_num;

            if (value > 0)
                DivMulflag = true;

            for (int i = 0; i < newSub_H; i++)
                for (int k = 0; k < newSub_W; k++)
                {
                    if (DivMulflag)
                        UnderOverFlow_num = _tempimage[ i, k] * value;
                    else
                        UnderOverFlow_num = _tempimage[i, k] / value;

                    _suboutimage[ i, k] = (byte)CheckRange(UnderOverFlow_num);//화면으로 출력되는 배열에 저장
                }
            SubDisplayImage();// 화면 출력
        }
        private void imgMove(int value)
        {
            for (int i = 0; i < newSub_H; i++)
                for (int k = 0; k < newSub_W; k++)
                {
                    _suboutimage[i, k] = _tempimage[ i, k];
                }

            for (int i = 0; i < value; i++)
                for (int k = 0; k < newSub_W; k++)
                {
                    _suboutimage[i, k] = 60;          
                }

            for (int i = 0; i < newSub_H; i++)
                for (int k = 0; k < value; k++)
                {
                    _suboutimage[i, k] = 60; 
                }

                for (int i = 0; i < newSub_H - value; i++)
                    for (int k = 0; k < newSub_W - value; k++)
                        _suboutimage[i + value, k + value] = _tempimage[i, k];

            //출력 메모리  ---> 화면
            SubDisplayImage();
        }





    }
}
