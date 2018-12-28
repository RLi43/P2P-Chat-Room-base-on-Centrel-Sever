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
        static Socket SocketToSever = null;
        public string userID;                   //用户ID
        List<ChatGroup> Groups = new List<ChatGroup>();
        List<Chat> Chats = new List<Chat>();
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
            friend[1] = "嗯";                  //状态
            friend[2] = userID;                 //ID
            friend[3] = friendsQuery(userID);   //IP
            ListViewItem newfrd = new ListViewItem(friend);
            listView_Frds.Items.Add(newfrd);

        }
       
        //发送字符信息到指定socket 
        public static void SendMsg2(string sendMsg,Socket send2)
        {
            //将输入的内容字符串转换为机器可以识别的字节数组     
            byte[] arrClientSendMsg = Encoding.UTF8.GetBytes(sendMsg);
            //调用客户端套接字发送字节数组     
            send2.Send(arrClientSendMsg);
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
            if (textBox_find.Text == "" )
            {
                find_text_empty = true;
                textBox_find.ForeColor = Color.Gray;
                textBox_find.Text = "学号/群号";
            }else if(textBox_find.Text == "学号/群号") find_text_empty = true;
            else find_text_empty = false;
        }

        private void button_send_Click(object sender, EventArgs e)
        {
            SendMsg2(richTextBox_Input.Text, SocketToSever);
        }

        //退出&下线
        private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("确认退出？", "操作提示",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                SendMsg2("logout" + userID, SocketToSever);
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

        //--查询好友--
        string friendsQuery(string IDnum)
        {
            SendMsg2('q' + IDnum, SocketToSever);
            return receiveFromSever();    
        }
        /*
         * 好友List
         * 0:   昵称
         * 1:   状态
         * 2：   ID
         * 3：   ip
         */
         bool isIP(string str)
        {
            try
            {
                IPAddress.Parse(str);
                return true;
            }
            catch 
            {
                return false;
            }
        }
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
            if ("n" != result&&!isIP(result) )
            {
                MessageBox.Show("您拨打的电话是空号", "出错啦",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(result);
            }
            else
            {
                string state = "";
                if ("n" == result)
                {
                    MessageBox.Show("您拨打的电话不在服务器，请稍后再试", "信息提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //好友在线
                    state = "嗯";
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
        //--end of 查询好友 ---

        //--发起聊天--
        //单独
        private void listView_Frds_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var selected = listView_Frds.SelectedItems;
            //Debug
            foreach (ListViewItem item in selected)
            {
                Console.WriteLine(item.SubItems[2].Text);
            }
            //end of Debug

            if (selected.Count != 1)
            {
                return;
            }
            string frds = userID; ;     //通知自己的ID
            foreach (ListViewItem item in selected)
            {
                //通知所有人群组名单（ID）
                frds += "," + item.SubItems[2].Text;
            }
            foreach (ListViewItem item in selected)
            {
                try
                {
                    string frdsIP = item.SubItems[3].Text;
                    string frdsID = item.SubItems[2].Text;
                    string frdsName = item.SubItems[0].Text;
                    Socket p2ps = connect2other(item.SubItems[2].Text, frds);
                    Chat newChat = new Chat(frdsIP, frdsID, frdsName,p2ps);
                    Chats.Add(newChat);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                    MessageBox.Show("连接失败😔", "发生错误",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            /*
            string friends = "";
            foreach (ListViewItem item in this.listView_Frds.SelectedItems)
            {
                friends += item.SubItems[0].Text + ",";
            }
            friends = friends.Substring(0, friends.Length - 1);

            Thread Thread_Chat = new Thread(() =>
                        Application.Run(new ChatDialog(this.Text, friends, Chatters, contedNum)));
            Thread_Chat.SetApartmentState(System.Threading.ApartmentState.STA);
            Thread_Chat.Start();*/
        }
        //群聊       
        private void button_initGrp_Click(object sender, EventArgs e)
        {
            var selected = listView_Frds.SelectedItems;
            //Debug
            foreach (ListViewItem item in selected)
            {
                Console.WriteLine(item.SubItems[2].Text);
            }
            //end of Debug
            if (selected.Count < 1)
            {
                MessageBox.Show("你要找谁聊天呀嘿？在列表里点击哦", "信息提示",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string[] frdsIP = new string[selected.Count];
            string[] frdsID = new string[selected.Count];
            string[] frdsName = new string[selected.Count];
            int contedNum = 0;
            string frds = userID; ;     //广播发起连接
            Socket[] p2ps = new Socket[selected.Count];
            foreach (ListViewItem item in selected)
            {
                //通知所有人群组名单（ID）
                frds += "," + item.SubItems[2].Text;
            }
            foreach (ListViewItem item in selected)
            {
                try
                {
                    frdsIP[contedNum] = item.SubItems[3].Text;
                    frdsID[contedNum] = item.SubItems[2].Text;
                    frdsID[contedNum] = item.SubItems[0].Text;
                    p2ps[contedNum] = connect2other(item.SubItems[2].Text, frds);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    MessageBox.Show("连接失败，是不是有人下线了？", "发生错误",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                contedNum++;
            }
            ChatGroup newGroup = new ChatGroup(frdsIP, frdsID, frdsName,p2ps);
            Groups.Add(newGroup);
        }

        // 对某个ID对应的学号发起连接，并传递信息
        public Socket connect2other(string ID, string Msg)
        {
            string IPstr = friendsQuery(ID);
            if (!isIP(IPstr))
            {
                Console.WriteLine("ID: "+ID+"  IPstr: "+IPstr);
                throw new Exception("IP地址异常");
            }
            IPEndPoint serverIp = new IPEndPoint(IPAddress.Parse(IPstr), 9876);
            Socket tcpClient = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            tcpClient.Connect(serverIp);
            SendMsg2(Msg, tcpClient);
            return tcpClient;
        }
        
        //--end of 查询好友--

        //修改昵称
        private void button_chgName_Click(object sender, EventArgs e)
        {
            var selected = listView_Frds.SelectedItems;
            if (selected.Count != 1)
            {
                MessageBox.Show("小孩子就选一个哦", "请选择",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            text inp = new text("新的昵称:");
            DialogResult dr = inp.ShowDialog();
            if (dr == DialogResult.OK && inp.Value.Length > 0)
            {
                foreach (ListViewItem item in selected)
                {
                    item.SubItems[0].Text = inp.Value;
                }
            }
            inp.Dispose();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            var selected = listView_Frds.SelectedItems;
            if (selected.Count < 1)return;
            DialogResult result = MessageBox.Show("确定删除?", "真的嘛?",
                                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (result == DialogResult.OK)
            {
                foreach (ListViewItem item in selected)
                {
                    item.Remove();
                }
            }
        }

        //进度条
        //https://www.w3cschool.cn/csharp/csharp-6z9g2pls.html


    }
    class ChatGroup
    {
        public string groupID = "0";
        public string[] frdsIP;
        public string[] frdsID;
        public string[] frdsName;
        public Socket[] p2ps;
        ChatGroup() { }
        public ChatGroup(string[] _frdsIP, string[] _frdsID, string[] _frdsName,Socket[] _p2ps)
        {
            frdsID = _frdsID;
            frdsIP = _frdsIP;
            frdsName = _frdsName;
            p2ps = _p2ps;
            // 唯一标识
            groupID = "";
            List<string> sortedID = new List<string>(frdsID);
            sortedID.Sort();
            for (int i = 0; i < frdsID.Length; i++)
            {
                groupID = groupID + sortedID[i].ToString();
            }
        }
    }
    class Chat
    {
        public string groupID = "0";
        public string frdsIP;
        public string frdsID;
        public string frdsName;
        public Socket p2ps;
        Chat() { }
        public Chat(string _frdsIP, string _frdsID, string _frdsName, Socket _p2ps)
        {
            frdsID = _frdsID;
            frdsIP = _frdsIP;
            frdsName = _frdsName;
            p2ps = _p2ps;
            groupID = frdsID;
        }
    }

}
