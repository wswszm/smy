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
        public string path = "";
        public static int fileNum = 0;
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
            if ("" == path || null == path) 
            {
                MessageBox.Show("请先重置并切图");
                return;
            }
            //pictureBox1.Image = Image.FromFile("D:\\A4.jpg");
            AnswerHandler ah = new AnswerHandler();
            /*Mat mat1 = ah.Subimage("D:\\123.png", 78, 510, 183, 157);
            Mat mat2 = ah.Subimage("D:\\123.png", 261, 510, 183, 157);
            Mat mat3 = ah.Subimage("D:\\123.png", 444, 510, 218, 157);
            Mat mat4 = ah.Subimage("D:\\123.png", 661, 510, 218, 157);
            Mat mat5 = ah.Subimage("D:\\123.png", 78, 666, 183, 157);
            Mat mat6 = ah.Subimage("D:\\123.png", 261, 666, 218, 157);*/
            //ah.imageHandle("D:\\12345.jpg");
            
            DirectoryInfo root = new DirectoryInfo(path);
            FileInfo[] files = root.GetFiles();
            scan.Text = "下一张";
            if (fileNum >= files.Length) 
            {
                MessageBox.Show("已到达最后一张了");
                return;
            }
            FileInfo fileInfo = files[fileNum];
            Bitmap bt = new Bitmap(fileInfo.FullName);
            Graphics g = Graphics.FromImage(bt);
            g.DrawLine(new Pen(Color.Brown, 1), new Point(0, 14), new Point(bt.Width, 14));
            g.DrawLine(new Pen(Color.Brown, 1), new Point(0, 44), new Point(bt.Width, 44));
            g.DrawLine(new Pen(Color.Brown, 1), new Point(0, 74), new Point(bt.Width, 74));
            g.DrawLine(new Pen(Color.Brown, 1), new Point(0, 104), new Point(bt.Width, 104));
            g.DrawLine(new Pen(Color.Brown, 1), new Point(0, 134), new Point(bt.Width, 134));
            g.DrawLine(new Pen(Color.Brown, 1), new Point(0, 164), new Point(bt.Width, 164));
            /*      g.DrawLine(new Pen(Color.Brown, 1), new Point(47, 0), new Point(47, bt.Height));
                  g.DrawLine(new Pen(Color.Brown, 1), new Point(86, 0), new Point(86, bt.Height));
                  g.DrawLine(new Pen(Color.Brown, 1), new Point(125, 0), new Point(125, bt.Height));
                  g.DrawLine(new Pen(Color.Brown, 1), new Point(164, 0), new Point(164, bt.Height));*/
            g.DrawLine(new Pen(Color.Brown, 1), new Point(41, 0), new Point(41, bt.Height));
            g.DrawLine(new Pen(Color.Brown, 1), new Point(82, 0), new Point(82, bt.Height));
            g.DrawLine(new Pen(Color.Brown, 1), new Point(123, 0), new Point(123, bt.Height));
            g.DrawLine(new Pen(Color.Brown, 1), new Point(164, 0), new Point(164, bt.Height));
            g.DrawLine(new Pen(Color.Brown, 1), new Point(205, 0), new Point(205, bt.Height));
            pictureBox1.Image = bt;
            //bt.Dispose();

            /*Mat img = ah.imageHandle("D:\\outimage\\image_88_696_206_177.jpg");*/
            /*ah.test(img, 47, 44);
            ah.test(img, 47, 44);*/
            Mat img = ah.imageHandle(fileInfo.FullName);
            //Cv2.ImShow("test",img);
            ah.simpleChooseHandleAfter(img, 5, 4);
            fileNum++;
            /* ah.CutImage(bit, 88 + 10, 696, 206, 177);
             ah.CutImage(bit, 294 + 10, 696, 206, 177);
             ah.CutImage(bit, 500 + 10, 696, 246, 177);
             ah.CutImage(bit, 746 + 10, 696, 246, 177);
             ah.CutImage(bit, 88 + 10, 873, 206, 177);*/
            //ah.CutImage(bit, 294 + 10, 873, 246, 177);
            /*canvas.Shapes.Clear();
            canvas.Shapes.Add(new Box(false) { Rectangle = new Rectangle(35, 319, 515, 68), });
            //canvas.Shapes.Add(new Box(false) { Rectangle = new Rectangle(35, 394, 515, 65), });
            canvas.Shapes.Add(new Box(true) { Rectangle = new Rectangle(37, 459, 512, 94), });*/
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
            
            fileNum = 0;
            scan.Text = "扫描";
            try
            {
                pictureBox1.Image = null;
                pictureBox1.Image.Dispose();
            }
            catch (Exception ee) 
            {
                Console.WriteLine(ee.ToString());
            }
            
            if ("" != path && null != path)
            {
                try
                {
                    DirectoryInfo root = new DirectoryInfo(path);
                    FileInfo[] files = root.GetFiles();
                    foreach (FileInfo f in files)
                    {
                        f.Delete();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

            }
            path = "";
        }

        private void cutImage_Click(object sender, EventArgs e)
        {
            AnswerHandler ah = new AnswerHandler();
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            
            try
            {
                OpenFileDialog of = new OpenFileDialog();

                if (of.ShowDialog() == DialogResult.OK)
                {
                    Bitmap bit = new Bitmap(of.FileName);
                    if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string dictoryPath = this.folderBrowserDialog1.SelectedPath;
                        DirectoryInfo root = new DirectoryInfo(dictoryPath);
                        FileInfo[] files = root.GetFiles();
                        if (files.Length > 0) 
                        {
                            MessageBox.Show("请选择一个空文件夹");
                            return;
                        }

                        /*ah.CutImage(bit, 88, 696, 206, 177, dictoryPath);
                        ah.CutImage(bit, 294, 696, 206, 177, dictoryPath);
                        ah.CutImage(bit, 500, 696, 206, 177, dictoryPath);
                        ah.CutImage(bit, 706, 696, 206, 177, dictoryPath);
                        ah.CutImage(bit, 912, 696, 206, 177, dictoryPath);
                        ah.CutImage(bit, 88, 873, 206, 177, dictoryPath);*/

                        ah.CutImage(bit, 88, 696, 206, 177, dictoryPath);
                        ah.CutImage(bit, 294, 696, 206, 177, dictoryPath);
                        ah.CutImage(bit, 500, 696, 246, 177, dictoryPath);
                        ah.CutImage(bit, 746, 696, 246, 177, dictoryPath);
                        ah.CutImage(bit, 88, 873, 246, 177, dictoryPath);
                        ah.CutImage(bit, 334, 873, 206, 177, dictoryPath);
                        this.path = dictoryPath;
                        MessageBox.Show("切图完成");
                    }
                }
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
            
        }
    }

}
