using OpenCvSharp;
using rect7.Marks;
using rect7.Shapes;
using smy.cs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WIA;
using WindowsFormsApplication1;
using Point = System.Drawing.Point;

namespace smy
{
    public partial class MainForm : Form
    {
        
        public MainForm()
        {
            InitializeComponent();
        }
        #region 退出按钮
        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();

        }
        private void mainFormCloseing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("确认退出程序吗？", "温馨提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                e.Cancel = false;
                System.Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }
        #endregion
        #region 最小化按钮
        private void MinButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        #endregion
        #region 无边框拖动效果
        [DllImport("user32.dll")]//拖动无窗体的控件
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

        private void Start_MouseDown(object sender, MouseEventArgs e)
        {
            //拖动窗体
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        private void Scan_Click(object sender, EventArgs e)
        {
           /* ImageFile imageFile = null;
            CommonDialogClass cdc = new CommonDialogClass();

            try
            {
                imageFile = cdc.ShowAcquireImage(WIA.WiaDeviceType.ScannerDeviceType,
                                                 WIA.WiaImageIntent.TextIntent,
                                                 WIA.WiaImageBias.MaximizeQuality,
                                                 "{00000000-0000-0000-0000-000000000000}",
                                                 true,
                                                 true,
                                                 false);
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                imageFile = null;
            }
            if (imageFile != null)
            {

                imageFile.SaveFile(@"c:\1.bmp");
                using (FileStream stream = new FileStream(@"c:\1.bmp", FileMode.Open,
                    FileAccess.Read, FileShare.Read))
                {
                    pictureBox1.Image = Image.FromStream(stream);
                }
                System.IO.File.Delete(@"c:\1.bmp");
            }*/
            /*DeviceManager manager = new DeviceManagerClass();
            Device device = null;
            foreach (DeviceInfo info in manager.DeviceInfos)
            {
                if (info.Type != WiaDeviceType.ScannerDeviceType) continue;
                device = info.Connect();
                break;
            }

            CommonDialogClass cdc = new WIA.CommonDialogClass();
            Items items = cdc.ShowSelectItems(device, WiaImageIntent.UnspecifiedIntent, WiaImageBias.MaximizeQuality, false, true, false);
            MessageBox.Show(items.Count.ToString());

            foreach (Item item in items)
            {

                CommonDialogClass cdcTemp = new WIA.CommonDialogClass();
                ImageFile imageFile = cdcTemp.ShowTransfer(item,
                "{B96B3CAB-0728-11D3-9D7B-0000F81EF32E}",
                true) as ImageFile;
                if (imageFile != null)
                {
                    var buffer = imageFile.FileData.get_BinaryData() as byte[];
                    using (MemoryStream ms = new MemoryStream())
                    {
                        ms.Write(buffer, 0, buffer.Length);
                        pictureBox1.Image = Image.FromStream(ms);
                        pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                    }
                }

            }*/
            pictureBox1.Image = Image.FromFile("C:\\Users\\EDZ\\Desktop\\2.png");
            canvas.Shapes.Clear();
            canvas.Shapes.Add(new Box(false) { Rectangle = new Rectangle(35, 319, 515, 68), });
            //canvas.Shapes.Add(new Box(false) { Rectangle = new Rectangle(35, 394, 515, 65), });
            canvas.Shapes.Add(new Box(true) { Rectangle = new Rectangle(37, 459, 512, 94), });
            //canvas.Shapes.Add(new Box(true) { Rectangle = new Rectangle(39, 544, 509, 239), });



        }
        /*private Point pStart, pEnd;//定义两个点（启点，终点）  
        private static bool drawing = false;//设置一个启动标志  
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            {
                if (e.Button == MouseButtons.Left)
                {
                    pStart = new Point(e.X, e.Y);
                }
                drawing = true;
            }
            

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (drawing)
            {
                Graphics g = pictureBox1.CreateGraphics();
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;//消除锯齿  
                if (pEnd.X > pStart.X)
                {
                    if (pEnd.Y > pStart.Y)
                    {
                        g.DrawRectangle(new Pen(Color.Red, 2), new Rectangle(pStart, new Size(pEnd.X - pStart.X, pEnd.Y - pStart.Y)));
                    }
                    else
                    {
                        g.DrawRectangle(new Pen(Color.Red, 2), new Rectangle(new Point(pStart.X,pEnd.Y), new Size(pEnd.X - pStart.X, pStart.Y - pEnd.Y)));
                    }
                }
                else
                {
                    if (pEnd.Y > pStart.Y)
                    {
                        g.DrawRectangle(new Pen(Color.Red, 2), new Rectangle(new Point(pEnd.X,pStart.Y), new Size(pStart.X - pEnd.X, pEnd.Y - pStart.Y)));
                    }
                    else
                    {
                        g.DrawRectangle(new Pen(Color.Red, 2), new Rectangle(pEnd, new Size(pStart.X - pEnd.X, pStart.Y - pEnd.Y)));
                    }
                }
                
                *//*g.DrawLine(new Pen(Color.Red, 2), zs, ys);
                g.DrawLine(new Pen(Color.Red, 2), ys, yx);
                g.DrawLine(new Pen(Color.Red, 2), yx, zx);
                g.DrawLine(new Pen(Color.Red, 2), zx, zs);*//*
            }
            drawing = false;
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)

        {

            if (drawing)
            {
                pEnd = new Point(e.X, e.Y);
            }

        }*/
        Canvas canvas = new Canvas();
        public void Draw(Graphics g)
        {
            canvas.Draw(g);
        }
        Mark markOnMouseDown = null;
        Shape shapeOnMouseDown = null;
        Point pointOnMouseDown;
        Point pointInShapeOnMouseDown;

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Draw(e.Graphics);
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (markOnMouseDown != null)
                {
                    int pointX = e.Location.X;
                    if (e.Location.X > pictureBox1.Width)
                    {
                        pointX = pictureBox1.Width;
                    }
                    else if (e.Location.X < 0)
                    {
                        pointX = 0;
                    }
                    int pointY = e.Location.Y;
                    if (e.Location.Y > pictureBox1.Height)
                    {
                        pointY = pictureBox1.Height;
                    }
                    else if (e.Location.Y < 0)
                    {
                        pointY = 0;
                    }
                    markOnMouseDown.MoveTo(new Point(pointX, pointY));
                    this.Refresh();
                    return;
                }
                else if (shapeOnMouseDown != null)
                {
                    int pointX = e.X - pointInShapeOnMouseDown.X;
                    if ((e.X - pointInShapeOnMouseDown.X + shapeOnMouseDown.Rectangle.Width) > pictureBox1.Width)
                    {
                        pointX = pictureBox1.Width - shapeOnMouseDown.Rectangle.Width;
                    }
                    else if (e.Location.X - pointInShapeOnMouseDown.X < 0)
                    {
                        pointX = 0;
                    }
                    int pointY = e.Y - pointInShapeOnMouseDown.Y;
                    if (e.Y - pointInShapeOnMouseDown.Y + shapeOnMouseDown.Rectangle.Height > pictureBox1.Height)
                    {
                        pointY = pictureBox1.Height - shapeOnMouseDown.Rectangle.Height;
                    }
                    else if (e.Y - pointInShapeOnMouseDown.Y < 0)
                    {
                        pointY = 0;
                    }
                    shapeOnMouseDown.MoveTo(new Point(pointX, pointY));
                    this.Refresh();
                    this.Cursor = Cursors.SizeAll;
                    return;
                }
            }

            Mark m = canvas.GetMark(e.X, e.Y);
            if (m != null)
                this.Cursor = m.Cursor;
            else
                this.Cursor = Cursors.Default;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pointOnMouseDown = e.Location;

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                markOnMouseDown = canvas.GetMark(e.X, e.Y);
            if (markOnMouseDown != null)
                return;

            shapeOnMouseDown = canvas.GetShape(e.X, e.Y);
            if (shapeOnMouseDown != null)
            {
                pointInShapeOnMouseDown = new Point(e.Location.X - shapeOnMouseDown.Rectangle.Location.X, e.Location.Y - shapeOnMouseDown.Rectangle.Location.Y);
                shapeOnMouseDown.Selected = true;
            }
            else
                canvas.ClearSelection();
            this.Refresh();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ShapeCollection shapes = canvas.Shapes;
            MessageBox.Show(checkCollection(shapes).ToString());
            /*if (checkCollection(shapes))
            {
                *//*for (int i = 0; i < shapes.Count; i++)
                {
                    Shape s = shapes[i];
                    Console.WriteLine(s.Rectangle.X);
                    Console.WriteLine(s.Rectangle.Y);
                    Console.WriteLine(s.Rectangle.Width);
                    Console.WriteLine(s.Rectangle.Height);
                }*//*
            }*/
        }
        private Boolean checkCollection(ShapeCollection sc)
        {
            for (int i=0;i<sc.LongCount();i++)
            {
                Shape s = sc[i];
                for (int j=0;j < sc.LongCount();j++)
                {
                    if (j == i)
                    {
                        continue;
                    }
                    Shape c = sc[j];
                    Console.WriteLine("-------------------");
                    Console.WriteLine(s.Rectangle.X);
                    Console.WriteLine(s.Rectangle.Y);
                    Console.WriteLine(s.Rectangle.X + s.Rectangle.Width);
                    Console.WriteLine(s.Rectangle.Y + s.Rectangle.Height);
                    Console.WriteLine(c.Rectangle.X);
                    Console.WriteLine(c.Rectangle.Y);
                    Console.WriteLine(c.Rectangle.X + c.Rectangle.Width);
                    Console.WriteLine(c.Rectangle.Y + c.Rectangle.Height);
                    Console.WriteLine("-------------------");
                    if (s.Rectangle.X >= c.Rectangle.X 
                        && s.Rectangle.Y >= c.Rectangle.Y
                        && (s.Rectangle.X+s.Rectangle.Width) <= (c.Rectangle.X+c.Rectangle.Width)
                        && (s.Rectangle.Y+s.Rectangle.Height) <= (c.Rectangle.Y+c.Rectangle.Height))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {

            
            AnswerHandler handler = new AnswerHandler();
            string imgFilePath = "C:\\Users\\EDZ\\Desktop\\7.png";
            Mat srcImage = handler.imageHandle(imgFilePath);
            Mat dst = new Mat();
            AnswerHandler.rotate(srcImage, -90f, out dst);
            Cv2.ImShow("Demo", dst);
            //Console.WriteLine(srcImage.Width);
            //Console.WriteLine(srcImage.Height);
            //Mat imag_ch1 = new Mat(srcImage, new Rect(307, 229, 436, 180));
            //Cv2.ImShow("Demo", imag_ch1);
            //学号
            string stuNo = handler.stuNoHandle(srcImage, 15);
            //单选
            int chooseY = 448;
            int chooseCount = 55;
            int chooseNum = 4;
            string[] listenAnswer = handler.simpleChooseHandle(srcImage, chooseY, chooseCount, chooseNum);
            for (int i = 0; i < listenAnswer.Length; i++)
            {
                string s = listenAnswer[i];
                Console.WriteLine("num:" + (i + 1) + ",answer:" + s);
            }

            Console.WriteLine("xh:" + stuNo);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            
        }
    }

}
