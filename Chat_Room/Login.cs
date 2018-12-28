using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace Chat_Room
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        public string userID;
        public bool exit = false;
        static string psw = "_net2018";
        public static Socket SocketClient = null;
        private void button1_Click(object sender, EventArgs e)
        {
            userID = textBox_IDnum.Text;
            if (userID.Length != 10)
                MessageBox.Show("请输入正确的学号");
            else
            {
                //连接服务器
                IPAddress serverIP = IPAddress.Parse("166.111.140.14");
                IPEndPoint ipep = new IPEndPoint(serverIP, 8000);
                SocketClient = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.IP);
                try
                {
                    SocketClient.Connect(ipep);
                }
                catch (SocketException)
                {
                    MessageBox.Show(this, "连接失败，请检查", "无法连接",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //调用客户端套接字发送登陆信息  
                string onlineStr = userID + psw;
                byte[] arrClientSendMsg = Encoding.UTF8.GetBytes(onlineStr);
                SocketClient.Send(arrClientSendMsg);                
                
                byte[] arrRecvmsg = new byte[1024];  //内存缓冲区，临时性存储接收到的消息 
                //将客户端套接字接收到的数据存入内存缓冲区，并获取长度  
                int length = SocketClient.Receive(arrRecvmsg);
                string strRevMsg = Encoding.UTF8.GetString(arrRecvmsg, 0, length);
                
                if (strRevMsg == "lol")
                {
                    MessageBox.Show("登陆成功！", "Welcome",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("登陆失败："+strRevMsg, "登陆失败",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;    
                }
                DialogResult = DialogResult.OK;
                exit = false;
                Close();
            }
        }
        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            exit = true;
        }

        private void Login_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }
    }
}
