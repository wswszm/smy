using Magnum.FileSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace smy
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, EventArgs e)
        {
            Console.WriteLine(userName.Text);
            Console.WriteLine(password.Text);
            Console.WriteLine(rememberMe.Checked);
            if (userName.Text == "admin" && password.Text == "admin")
            {
                this.Hide();
                MainForm mainForm = new MainForm();
                mainForm.Show();
            }
        }

        
         #region 关闭按钮
         
        private void CloseButton_Click(object sender, EventArgs e)
        {

            this.Close();

        }
        private void loginFormClosing(object sender, FormClosingEventArgs e)
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
        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
