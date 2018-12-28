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
//https://blog.csdn.net/luming666/article/details/79125453
//166.111.140.14:8000
//2016011000_net2018    lol
//“q+好友学号”          ip/'n'
//”logout+本人学号”     'loo

namespace Chat_Room
{
    public partial class MainWin : Form
    {
        static Thread ThreadClient = null;
        static Socket SocketToSever = null;
        static string severip = "166.111.140.14";
        static int severport = 8000;
        public string userID;           //用户ID
        public MainWin()
        {
            InitializeComponent();
            Visible = false;
            Login log = new Login();
            log.ShowDialog();
            while (log.DialogResult!=DialogResult.OK)
            {
                if (log.exit) System.Environment.Exit(0);
            }
            Visible = true;
            userID = log.userID;
            SocketToSever = Login.SocketClient;

            //增加本人为好友
            string[] friend = new string[4];
            friend[0] = "我";                    //昵称
            friend[1] = "在线";                  //状态
            friend[2] = userID;                 //ID
            friend[3] = friendsQuery(userID);   //IP
            ListViewItem newfrd = new ListViewItem(friend);
            listView_Frds.Items.Add(newfrd);

        }
        
        //通信
        static void connect2sever(){
            try
            {    
                IPAddress ip = IPAddress.Parse(severip);
                IPEndPoint ipe = new IPEndPoint(ip, severport);    
                //定义一个套接字监听  
                SocketToSever = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);    
                try
                {
                    //客户端套接字连接到网络节点上，用的是Connect  
                    SocketToSever.Connect(ipe);
                }
                catch (Exception)
                {
                    Console.WriteLine("连接失败！\r\n");
                    Console.ReadLine();
                }    
                ThreadClient = new Thread(Recv);
                ThreadClient.IsBackground = true;
                ThreadClient.Start();    
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
        //接收服务端发来信息的方法    
        public static void Recv()
        {
                int x = 0;
            //持续监听服务端发来的消息 
            while (true)
            {
                try
                {
                    //定义一个1M的内存缓冲区，用于临时性存储接收到的消息  
                    byte[] arrRecvmsg = new byte[1024 * 1024];
 
                    //将客户端套接字接收到的数据存入内存缓冲区，并获取长度  
                    int length = SocketToSever.Receive(arrRecvmsg);
 
                    //将套接字获取到的字符数组转换为人可以看懂的字符串  
                    string strRevMsg = Encoding.UTF8.GetString(arrRecvmsg, 0, length);
                    if (x == 1)
                    {
                        Console.WriteLine("\r\n服务器：" 
                            + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") 
                            + "\r\n" + strRevMsg+"\r\n");                        
                    }
                    else
                    {
                        Console.WriteLine(strRevMsg + "\r\n");
                        x = 1;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("远程服务器已经中断连接！" + ex.Message + "\r\n");
                    break;
                }
            }
        }
 
        //发送字符信息到服务端的方法  
        public static void ClientSendMsg2Sever(string sendMsg)
        {
            //将输入的内容字符串转换为机器可以识别的字节数组     
            byte[] arrClientSendMsg = Encoding.UTF8.GetBytes(sendMsg);
            //调用客户端套接字发送字节数组     
            SocketToSever.Send(arrClientSendMsg);
        }
        string receiveFromSever(int size = 1024)
        {
            byte[] arrRecvmsg = new byte[size];  //内存缓冲区，临时性存储接收到的消息 
                                                 //将客户端套接字接收到的数据存入内存缓冲区，并获取长度  
            int length = SocketToSever.Receive(arrRecvmsg);
            string rev = Encoding.UTF8.GetString(arrRecvmsg, 0, length);
            Console.WriteLine(rev);
            return rev;
        }
        //UI相关
        bool find_text_empty = true;
        private void textBox_find_Click(object sender, EventArgs e)
        {
            if (find_text_empty)
            {
                textBox_find.ForeColor = Color.Black;
                textBox_find.Text = "";
            }
        }
        private void textBox_find_Leave(object sender, EventArgs e)
        {
            if (textBox_find.Text == "")
            {
                find_text_empty = true;
                textBox_find.ForeColor = Color.Gray;
                textBox_find.Text = "学号/群号";
            }
            else find_text_empty = false;
        }

        private void button_send_Click(object sender, EventArgs e)
        {
            ClientSendMsg2Sever(richTextBox_Input.Text);
        }
        //退出&下线
        private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("确认退出？", "操作提示",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                ClientSendMsg2Sever("logout" + userID);
                string strRevMsg = receiveFromSever();
                if (strRevMsg == "loo")
                {
                    MessageBox.Show("下线成功！", "Goodbye",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("下线失败：" + strRevMsg+"\r\n将强制退出", "下线失败",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }
        //查询好友
        string friendsQuery(string IDnum)
        {
            ClientSendMsg2Sever('q' + IDnum);
            return receiveFromSever();    
        }
        /*
         * 好友List
         * 0:   昵称
         * 1:   状态
         * 2：   ID
         * 3：   ip
         */
        private void button_find_Click(object sender, EventArgs e)
        {
            //TODO:群查询
            string idname = textBox_find.Text;
            if (idname == userID)
            {
                MessageBox.Show("嘟嘟嘟……\r\n打通了自己的电话", "自问自答",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string result = friendsQuery(idname);
            bool isIP = true;
            try { IPAddress.Parse(result); }
            catch (Exception ex)
            {
                isIP = false;
            }
            if (!isIP && "n" != result)
            {
                MessageBox.Show("您拨打的电话是空号", "出错啦",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(result);
            }
            else
            {
                string state = "不在线";
                if ("n" == result)
                {
                    MessageBox.Show("您拨打的电话不在服务器，请稍后再试", "信息提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //好友在线
                    state = "在线";
                    MessageBox.Show("好友电话是：" + result, "信息提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                bool isnew = true;
                //新的好友？
                foreach (ListViewItem item in listView_Frds.Items)
                {
                    if (idname == item.SubItems[2].Text)
                    {
                        MessageBox.Show("该好友可以通过通讯录找到哦", "信息提示",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        isnew = false;
                        item.SubItems[3].Text = result;
                        item.SubItems[1].Text = state;
                        break;
                    }
                }
                //新的好友 
                if (isnew)
                {
                    string[] friend = new string[4];
                    friend[0] = idname;
                    friend[1] = state;
                    friend[2] = idname;
                    friend[3] = result;
                    ListViewItem newfrd = new ListViewItem(friend);
                    listView_Frds.Items.Add(newfrd);
                }
            }
        }

    }
}
