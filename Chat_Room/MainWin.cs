using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
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
        Friend user;                                //用户
        List<Chat> Chats = new List<Chat>();        //会话
        List<Friend> Frds = new List<Friend>();     //通讯录
        List<string> BlackList = new List<string>();//黑名单ID
        List<Socket> ConnectedLinks = new List<Socket>();   //所有Socket//TODO 删掉
        int localPort;                              //本地大门端口
        public MainWin()
        {
            InitializeComponent();
            MainLoad();
        }
        public void MainLoad()
        {
            Frds.Clear();
            listView1.Items.Clear();
            Chats.Clear();

            //---登陆---
            Visible = false;
            Login log = new Login();
            log.ShowDialog();
            while (log.DialogResult != DialogResult.OK)
            {
                if (log.exit) System.Environment.Exit(0);
            }
            Visible = true;
            //---增加本人为好友---
            user = new Friend("", true, log.userID, "我", null);
            Frds.Add(user);
            
            SocketToSever = Login.SocketClient;
            localPort = int.Parse(user.ID.Substring(user.ID.Length - 5)) + 2000;

            StartListening();
            outputBoxWritting = false;
            //显示对话
            Chat newchat = new Chat(user);
            Chats.Add(newchat);
            UpdateChats();
        }
        #region 通信辅助函数
        bool IsIP(string str)
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
        public static IPAddress GetLocalIP()//获取本机地址      
        {
            //https://www.cnblogs.com/iack/p/3685680.html
            try
            {
                string HostName = Dns.GetHostName(); //得到主机名
                IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
                for (int i = 0; i < IpEntry.AddressList.Length; i++)
                {
                    //从IP地址列表中筛选出IPv4类型的IP地址
                    //AddressFamily.InterNetwork表示此IP为IPv4,
                    //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                    if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        return IpEntry.AddressList[i];
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取本机IP出错:" + ex.Message);
                return null;
            }
        }         
        public static bool SendMsg2(string sendMsg, Socket send2)//发送字符信息到指定socket
        {
            if (send2.Connected)
            {
                //将输入的内容字符串转换为机器可以识别的字节数组     
                byte[] arrClientSendMsg = Encoding.UTF8.GetBytes(sendMsg);
                //调用客户端套接字发送字节数组     
                send2.Send(arrClientSendMsg);
                if (sendMsg.Length < 80)
                    Console.WriteLine("Send to " + send2.RemoteEndPoint.ToString() + ": " + sendMsg);
                return true;
            }
            else return false;
        }        
        #endregion
        #region 本机服务 - 左侧界面
        //--UI相关
        //-搜索提示
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
                textBox_find.Text = "查找学号";
            } else if (textBox_find.Text == "查找学号") find_text_empty = true;
            else find_text_empty = false;
            AcceptButton = null;
        }        
        //-绘制会话表
        bool listviewUsing = false;
        private delegate void UpdateChatsList(List<Chat> cs);
        void DrawChatsList(List<Chat> cs)
        {
            listView1.Items.Clear();
            int onc = 0;
            foreach (Chat c in Chats)
            {
                listView1.Items.Add(c.setItem());
                if (c.state == Chat.CHATSTATE.ONCHAT) onc++;
            }
            if(onc==0)panel2.Visible=false;
            else panel2.Visible = true;
            if (onc > 1)
            {
                Console.WriteLine("Error! 两个当前对话！");
            }
        }
        void UpdateChats()
        {
            //更新会话列表
            while (listviewUsing) { };
            listviewUsing = true;   //占用之
            UpdateChatsList cl = new UpdateChatsList(DrawChatsList);
            this.Invoke(cl, new object[] { Chats });
            listviewUsing = false;  //恢复不被占用
        }

        //--退出&下线
        private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("确认退出？", "操作提示",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (result == DialogResult.OK)
            {
                foreach(Friend fd in Frds)
                {
                    if (fd.link != null)
                    {
                        fd.link.Close();
                        fd.link = null;
                        fd.online = false;
                    }
                }
                SendMsg2("logout" + user.ID, SocketToSever);
                string strRevMsg = ReceiveFromSever();
                if (strRevMsg == "loo")
                {
                    MessageBox.Show("下线成功！", "Goodbye",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("下线失败：" + strRevMsg + "\r\n将强制退出", "下线失败",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                e.Cancel = true;
            }
        }
        //--信息操作---
        //-修改昵称
        private void button_chgName_Click(object sender, EventArgs e)
        {
            var selected = listView1.SelectedItems;
            if (selected.Count != 1)
            {
                MessageBox.Show("小孩子就选一个哦", "请选择",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            text inp = new text("新的昵称/群名:");
            DialogResult dr = inp.ShowDialog();
            if (dr == DialogResult.OK && inp.Value.Length > 0)
            {
                ListViewItem item = selected[0];
                //更新界面
                item.SubItems[0].Text = inp.Value;
                string id = item.SubItems[2].Text;
                //更新对话列表
                Chat theChat = null;
                for (int i = 0; i < Chats.Count; i++)
                {
                    if (Chats[i].ID == id)
                    {
                        theChat = Chats[i];
                        break;
                    }
                }
                theChat.Name = inp.Value;
                if (!theChat.isGroup)
                {
                    theChat.friends[0].Name = inp.Value;
                }
            }
            inp.Dispose();
        }
        //-删除好友
        private void button_delete_Click(object sender, EventArgs e)
        {
            var selected = listView1.SelectedItems;
            if (selected.Count < 1) return;
            DialogResult result = MessageBox.Show("确定删除?", "真的嘛?",
                                MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            if (result == DialogResult.OK)
            {
                foreach (ListViewItem item in selected)
                {
                    string id = item.SubItems[2].Text;
                    //更新对话列表
                    for (int i = 0; i < Chats.Count; i++)
                    {
                        if (Chats[i].ID == id)
                        {
                            Chats.RemoveAt(i);
                        }
                    }
                    //更新通讯录
                    if (item.Tag.ToString() == "S")
                    {
                        for (int i = 0; i < Frds.Count; i++)
                        {
                            if (Frds[i].ID == id)
                            {
                                Frds.RemoveAt(i);
                            }
                        }
                    }
                    //更新界面
                    item.Remove();
                }
            }
        }
        //--查询好友
        private void button_find_Click(object sender, EventArgs e)
        {
            string idname = textBox_find.Text;
            if (idname == user.ID)
            {
                MessageBox.Show("嘟嘟嘟……\r\n打通了自己的电话", "自问自答",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                for (int i = 0; i < Frds.Count; i++)
                {
                    if (Frds[i].ID == user.ID)
                    {
                        return;
                    }
                }
                //把自己删掉了？
                Friend newfrd = new Friend(FriendsQuery(user.ID), true, user.ID, "我", null);
                Frds.Add(newfrd);
                Chat newchat = new Chat(newfrd);
                UpdateChats();
                return;
            }
            string result = FriendsQuery(idname);
            if ("n" != result && !IsIP(result))
            {
                MessageBox.Show("您拨打的电话是空号", "出错啦",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(result);
            }
            else
            {
                bool state = false;
                if ("n" == result)
                {
                    MessageBox.Show("您拨打的电话不在服务器，请稍后再试", "信息提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    //好友在线
                    state = true;
                    MessageBox.Show("好友电话是：" + result, "信息提示",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //新的好友？
                int ind = Frds.FindIndex(x => x.ID == idname);
                bool isnew = ind==-1;
                if (isnew)
                {
                    Friend newfrd = new Friend(FriendsQuery(idname), state, idname, idname, null);
                    Frds.Add(newfrd);
                    Chat newchat = new Chat(newfrd);
                    Chats.Add(newchat);
                }
                else
                {
                    int cd = Chats.FindIndex(x => x.ID == idname);
                    if (cd != -1)
                    {
                        //考虑直接开启对话？
                        MessageBox.Show("该好友可以通过通讯录找到哦", "信息提示",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Frds[ind].IP = result;
                        Frds[ind].online = state;
                    }
                    else
                    {
                        Chats.Add(new Chat(Frds[ind]));
                    }
                }
                //新的好友 
                UpdateChats();
            }
        }
        //轮询好友
        //TODO 定时更新？可能需要考虑占用的问题
        void updateState()
        {
            foreach (ListViewItem item in listView1.Items)
            {
                string result = FriendsQuery(item.SubItems[2].Text);
                if (result == "n")
                {
                    item.SubItems[1].Text = "";
                }
                else
                {
                    item.SubItems[1].Text = "嗯";
                }
            }
        }
        #endregion
        #region 服务器
        //-查询好友
        string FriendsQuery(string IDnum)
        {
            SendMsg2('q' + IDnum, SocketToSever);
            return ReceiveFromSever();
        }
        string ReceiveFromSever(int size = 1024)//接收服务器信息
        {
            byte[] arrRecvmsg = new byte[size];  //内存缓冲区，临时性存储接收到的消息 
                                                 //将客户端套接字接收到的数据存入内存缓冲区，并获取长度  
            int length = SocketToSever.Receive(arrRecvmsg);
            string rev = Encoding.UTF8.GetString(arrRecvmsg, 0, length);
            if (length == 0)
            {
                //如果客户端正常关闭后，会向服务端发送长度为0的空数据，利用这一点将这个客户端关闭
                SocketToSever.Close();
                MessageBox.Show("与服务器连接中断", "!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                MainLoad();
                return null;
            }
            Console.WriteLine(rev);
            return rev;
        }
        #endregion
        #region 接受链接
        //TCP Socket - 大门 异步接受监听
        //https://blog.csdn.net/weixin_40271181/article/details/78981701
        //https://blog.csdn.net/mss359681091/article/details/51790931
        public void StartListening()
        {
            IPAddress userIP = GetLocalIP();
            user.IP = userIP.ToString();
            IPEndPoint serverEp = new IPEndPoint(userIP, localPort);
            Socket serverSocket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);   //监听服务器
            serverSocket.Bind(serverEp);
            serverSocket.Listen(20);   //排队等待连接的最大数量，注意这个数量不包含已经连接的数量
            //开始异步连接   
            AsynAccept(serverSocket);
        }
        //异步接受链接
        public void AsynAccept(Socket serverSocket)
        {
            //BeginAccept()调用AcceptCallBack
            serverSocket.BeginAccept(asyncResult =>
            {//AcceptCallBack内容
                //一个新的clientSocket与接入的计算机通信
                Socket clientSocket = serverSocket.EndAccept(asyncResult);
                AsynAccept(serverSocket);     //继续监听其他连接 循环接收
                AsynRecive(clientSocket);  //接收监听到的这条连接的广播信息
            }, null);
        }
        //异步接收客户端消息
        public void AsynRecive(Socket clientSocket)
        {
            Console.WriteLine(string.Format("连接：来自 {0}", clientSocket.RemoteEndPoint));
            byte[] data = new byte[1024];
            try
            {
                clientSocket.BeginReceive(data, 0, data.Length, SocketFlags.None,
                asyncResult =>
                {
                    int length = clientSocket.EndReceive(asyncResult);
                    string Recv = Encoding.UTF8.GetString(data, 0, length);
                    if(Recv.Length<80)
                        Console.WriteLine(clientSocket.RemoteEndPoint.ToString() + ": " + Recv);
                    if (length == 0)
                    {
                        Console.WriteLine(clientSocket.RemoteEndPoint.ToString() + " 已断开连接");
                        clientSocket.Close();
                        return;
                    }
                    GateMsgTrans(Recv, clientSocket);
                }, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "数据接收失败",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //大门信息处理
        public void GateMsgTrans(string Recv, Socket clientSocket)
        {
            string type = Recv.Substring(0, 3);

            if (type != Message.CON) return;        //软件统一才能使用啊
            string remoteID = Recv.Substring(3, 10);
            bool isSingle = Recv[13] == '0';
            string ChatID = remoteID;
            //黑名单
            if (BlackList.Contains(remoteID)) return;
            string Gname = "";
            if (!isSingle)
            {
                int length = int.Parse(Recv.Substring(14, 2)) * 10;
                ChatID = Recv.Substring(16, length);
                Gname = Recv.Substring(16 + length);
            }
            Friend theFrd = null;
            while (ChatsUsing) { }
            ChatsUsing = true;
            int fdind = Frds.FindIndex(x => x.ID == remoteID);
            int chind = Chats.FindIndex(x => x.ID == ChatID);
            bool isnewFrd = (fdind == -1);
            bool isnewChat = (chind == -1);
            if (!isnewFrd)
            {
                //老朋友 - 也可能不那么老 比如群聊创建的朋友不在显示的通讯录中
                theFrd = Frds[fdind];
                if (theFrd.link != null)
                {
                    //原先连接正常
                    if (theFrd.link.Connected)
                        Console.WriteLine("对方软件出错！");
                    //删掉重新连接
                    theFrd.link.Dispose();
                }
                theFrd.link = clientSocket;
                //老朋友默认通过了
                FrdAsynRecive(theFrd);
                if (!isnewChat)
                {
                    //无事 收连接就行
                    ChatsUsing =false;
                    return;
                }
                //断了之后重新发起了新的会话 - 单切群or others …… 为新会话创建
                //TODO
            }
            else//新朋友
                theFrd = new Friend("", true, remoteID, remoteID, clientSocket);
            //新会话
            if (isnewChat)
            {
                //新会话
                Chat theChat;
                if (isSingle)
                {
                    if (isnewFrd)
                    {
                        DialogResult rs = MessageBox.Show(remoteID + " 向您发起会话\r\n选择否可以将其加入黑名单"
                            , "会话请求", MessageBoxButtons.YesNoCancel
                             , MessageBoxIcon.Question);
                        if (rs == DialogResult.Yes)
                        {
                            //将新好友加入通讯录
                            FrdAsynRecive(theFrd);
                            Frds.Add(theFrd);
                        }
                        else
                        {
                            //中断本次对话 不回应
                            string msg = Message.RFS + user.ID;
                            SendMsg2(msg, clientSocket);
                            clientSocket.Dispose();
                            //TODO test dispose之后是不是null 还有测试shutdown
                            theFrd.link = null;
                            if (rs == DialogResult.No)
                            {
                                //拒绝聊天——加入黑名单
                                BlackList.Add(remoteID);
                            }
                            ChatsUsing = false;
                            return;
                        }
                    }
                    //加入会话列表
                    theChat = new Chat(theFrd);
                }
                else //群聊
                {
                    string promptMsg = "来自群聊：" + Gname+ "的"+remoteID+"的连接请求";
                    if (isnewFrd) promptMsg = "新的朋友！/r/n " + promptMsg;
                    DialogResult rs = MessageBox.Show(promptMsg, "会话请求"
                        , MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rs == DialogResult.Yes)
                    {
                        //将发起者加入通讯录
                        Frds.Add(theFrd);
                        //建立新的会话
                        int l = ChatID.Length / 10;
                        List<Friend> newFrds = new List<Friend>();
                        bool need2con = false;
                        string conMsg = Message.CON + user.ID + "1" + Recv.Substring(14);//因为单独聊天不需要再次发送
                        //为新成员建立Friend类
                        for (int j = 0; j < l; j++)
                        {
                            string idname = ChatID.Substring(j * 10, 10);
                            if (idname == user.ID)
                            {
                                //在本人ID之后的ID需要自己发起连接
                                //或者其实应该不需要的 但是比较怕撞就是了
                                need2con = true;
                            }
                            //新的好友？
                            int ind = Frds.FindIndex(x => (x.ID == idname));
                            Friend Gfrd;
                            if (ind == -1)
                            {
                                //新的好友
                                Gfrd = new Friend("", true, idname, idname, null);
                            }
                            else Gfrd = Frds[ind];
                            if (Gfrd == user) continue;
                            if (Gfrd.link == null && need2con)
                            {
                                //给未连接的对象进行连接
                                //建立socket
                                try
                                {
                                    Gfrd.link = connect2other(Gfrd.ID, conMsg);
                                    Gfrd.online = true;
                                    FrdAsynRecive(Gfrd);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                    MessageBox.Show("发生错误： 群聊成员" + idname + " 不在线/r/n连接失败");
                                    continue;
                                }
                            }
                            newFrds.Add(Gfrd);
                        }                            
                        //建立会话
                        theChat = new Chat(newFrds, Gname);
                    }
                    else
                    {//拒绝加入
                        ChatsUsing = false;
                        return;
                    }
                }
                Chats.Add(theChat);
                theChat.state = Chat.CHATSTATE.LINK;
                switchChat2(theChat);
                ChatsUsing = false;
                return;
            }
            //新朋友 旧会话 - 出错
            //群聊的新朋友应该在收到时就创建了 是旧朋友了
            Console.WriteLine("ERROR:新朋友 旧会话");
            ChatsUsing = false;
            //FrdAsynRecive(theChat);
            //创建一个通信线程      
            //Thread thread = new Thread(FrdAsynRecive);
            ////设置为后台线程，随着主线程退出而退出 
            //thread.IsBackground = true;
            ////启动线程     
            //thread.Start(theFrd);
        }
        #endregion
        #region 已连接 信息接收操作
        //对话监听
        bool ChatsUsing = false;
        void FrdAsynRecive(object obj)
        {
            Friend theFrd = obj as Friend;
            Socket link = theFrd.link;
            byte[] data = new byte[1024];
            try
            {
                if (link == null || !link.Connected) return;
                link.BeginReceive(data, 0, data.Length, SocketFlags.None,
                asyncResult =>
                {
                    string Recv = "";
                    if (link == null||!link.Connected) return;
                    int length = 0;
                    try
                    {
                        length = link.EndReceive(asyncResult);
                        Recv = Encoding.UTF8.GetString(data, 0, length);
                        if (Recv.Length < 80)
                            Console.WriteLine(link.RemoteEndPoint.ToString() + " : " + Recv);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        MessageBox.Show("信息接收错误，和" + link.RemoteEndPoint.ToString() + " 连接中断", "信息提示",
                                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        link.Shutdown(SocketShutdown.Both);
                        link.Close();
                        theFrd.link = null;
                        theFrd.online = false;
                    }
                    if (length == 0)
                    {
                            //删掉
                            MessageBox.Show("好友" + theFrd.Name + "退出了会话", "信息提示",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                        theFrd.online = false;
                        theFrd.link = null;
                        UpdateChats();   //会自动判断有该frd的Chat下线
                        return;
                    }
                    if (Message.check(Recv))
                    {
                        string type = Recv.Substring(0, 3);
                        bool isSingle = Recv[13] == '0';
                        string ChatID = theFrd.ID;
                        //黑名单
                        if (BlackList.Contains(theFrd.ID)) return;
                        string Gname = "";
                        if (!isSingle)
                        {
                            int len = int.Parse(Recv.Substring(14, 2)) * 10;
                            ChatID = Recv.Substring(16, len);
                            Gname = Recv.Substring(16 + len);
                        }
                        while (ChatsUsing) { }
                        ChatsUsing = true;
                        int chid = Chats.FindIndex(x => x.ID == ChatID);
                        Chat theChat;
                        if (chid == -1)
                        {
                            ChatsUsing = true;
                            if (Recv.Substring(0, 3) == Message.CON)
                            {
                                //建立新的会话
                                if (isSingle)
                                {
                                    DialogResult rs = MessageBox.Show(
                                        theFrd.Name + " 向您发起会话\r\n选择否可以将其加入黑名单"
                                        , "会话请求", MessageBoxButtons.YesNoCancel
                                         , MessageBoxIcon.Question);
                                    if (rs == DialogResult.Yes)
                                    {
                                        //新建会话
                                        theChat = new Chat(theFrd);
                                    }
                                    else
                                    {
                                        //中断本次对话 不回应
                                        string msg = Message.RFS + user.ID;
                                        SendMsg2(msg, theFrd.link);
                                        if (rs == DialogResult.No)
                                        {
                                            link.Shutdown(SocketShutdown.Both);
                                            link.Close();
                                            theFrd.link = null;
                                            //拒绝聊天——加入黑名单
                                            BlackList.Add(theFrd.ID);
                                        }
                                        ChatsUsing = false;
                                        return;
                                    }
                                }
                                else //群聊
                                {
                                    DialogResult rs = MessageBox.Show("朋友 " +
                                        theFrd.Name + "邀请您加入群聊：" + Gname, "会话请求"
                                        , MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (rs == DialogResult.Yes)
                                    {
                                        //建立新的会话
                                        int l = ChatID.Length / 10;
                                        List<Friend> newFrds = new List<Friend>();
                                        newFrds.Add(user);
                                        newFrds.Add(theFrd);
                                        bool need2con = false;
                                        string conMsg = Message.CON + user.ID + "1" + Recv.Substring(14);
                                        //为新成员建立Friend类
                                        for (int j = 0; j < l; j++)
                                        {
                                            string idname = ChatID.Substring(j * 10, 10);
                                            if (idname == user.ID)
                                            {
                                                need2con = true;
                                                continue;
                                            }
                                                if(idname == theFrd.ID) continue;
                                            //新的好友？
                                            int fdin = Frds.FindIndex(x => x.ID == idname);
                                            Friend Gfrd;
                                            if (fdin == -1)
                                            {
                                                //新的好友
                                                Gfrd = new Friend("", true, idname, idname
                                                    , null);
                                            }
                                            else
                                            {
                                                Gfrd = Frds[fdin];
                                            }
                                            if (Gfrd == user) continue;
                                            if (Gfrd.link == null && need2con)
                                            {
                                                //给未连接的对象进行连接
                                                //建立socket
                                                try
                                                {
                                                    Gfrd.link = connect2other(Gfrd.ID, conMsg);
                                                    Gfrd.online = true;
                                                    FrdAsynRecive(Gfrd);
                                                }
                                                catch (Exception ex)
                                                {
                                                    Console.WriteLine(ex);
                                                    MessageBox.Show("发生错误： 群聊成员" + idname + " 不在线/r/n连接失败");
                                                    continue;
                                                }
                                            }
                                            newFrds.Add(Gfrd);
                                        }
                                        //建立会话
                                        theChat = new Chat(newFrds, Gname);
                                    }
                                    else
                                    {//拒绝加入
                                        ChatsUsing = false;
                                        return;
                                    }
                                }
                                Chats.Add(theChat);
                                switchChat2(theChat);
                                ChatsUsing = false;
                            }
                            //新会话不是CON 不接受
                        }
                        else
                        {   //已有的对话
                            theChat = Chats[chid];
                            switch (type)
                            {
                                case Message.CON:
                                    {
                                        //已经连上又发一遍？
                                        //新的聊天的内容 
                                        //修改群名？
                                        if (theChat.isGroup)
                                        {
                                            theChat.Name = Recv.Substring(16 + 10 * theChat.memNum);
                                        }
                                        if (theChat.state < Chat.CHATSTATE.LINK) { theChat.state = Chat.CHATSTATE.LINK;
                                            Console.WriteLine("Warning!已连接的Chat转台<link 且 对面重新发送了CON");
                                        }
                                        break;
                                    }
                                case Message.MSG:
                                    {
                                        //务必先取得连接之后才能对话
                                        if (theChat.state > Chat.CHATSTATE.ONLINE)
                                        {
                                            System.Media.SystemSounds.Beep.Play();
                                            int startInd = 18;
                                            if (theChat.isGroup)
                                            {
                                                startInd = 20 + 10 * theChat.memNum;
                                            }
                                            string Msgs = Recv.Substring(startInd);
                                            //增加聊天记录
                                            chatData newDa = new chatData(theFrd.Name,
                                                false, Msgs, DateTime.Now);
                                            theChat.Datas.Add(newDa);
                                            theChat.unRead++;

                                            //当前对话直接将消息绘制,即增加最后一条
                                            if (theChat.state == Chat.CHATSTATE.ONCHAT)
                                            {
                                                addOutputBox(newDa);
                                                theChat.unRead = 0;
                                            }
                                            else
                                            {
                                                theChat.state = Chat.CHATSTATE.NEWS;
                                                //重绘 更新会话列表
                                                UpdateChats();
                                            }
                                        }
                                        else
                                        {//未确认方发送的连接 
                                        }
                                        break;
                                    }
                                case Message.SHK:
                                    {
                                        System.Media.SystemSounds.Beep.Play();
                                        //增加聊天记录
                                        chatData newDa = new chatData(theFrd.Name,
                                            false, "--向您发送了一个窗口抖动--\n\r", DateTime.Now);
                                        theChat.Datas.Add(newDa);
                                        theChat.unRead++;
                                        //当前对话直接将消息绘制,即增加最后一条
                                        if (theChat.state == Chat.CHATSTATE.ONCHAT)
                                        {
                                            addOutputBox(newDa);
                                            theChat.unRead = 0;
                                        }
                                        else
                                        {
                                            theChat.state = Chat.CHATSTATE.NEWS;
                                            //重绘 更新会话列表
                                            UpdateChats();
                                        }
                                        Shake shake = new Shake(Window_Shake);
                                        Invoke(shake, new object[] { });
                                        break;
                                    }
                                //https://blog.csdn.net/fsdad/article/details/73991751
                                case Message.FLE:
                                    {
                                        if (theChat.state > Chat.CHATSTATE.ONLINE)
                                        {
                                            int startInd = 14;
                                            if (theChat.isGroup)
                                            {
                                                startInd = 16 + 10 * theChat.memNum;
                                            }
                                            string FlieInf = Recv.Substring(startInd);
                                            string filename = FlieInf.Split('-').First();       //文件名
                                            long fileLength = Convert.ToInt64(FlieInf.Split('-').Last());//文件长度
                                                                                                         //委托主线程接收

                                            FileSave r_s = new FileSave(fileRecieve);
                                            Invoke(r_s, new object[] { filename, fileLength, theFrd, theChat });
                                        }
                                        break;
                                    }
                                case Message.RFS:
                                    {
                                        //被拒绝
                                        if (theChat.state > Chat.CHATSTATE.ONLINE)
                                        {
                                            string msgShow = "";
                                            if (theChat.isGroup)
                                            {
                                                msgShow = theChat.Name + "-";
                                            }
                                            msgShow += theFrd.Name + " 拒绝了您的会话";
                                            MessageBox.Show(msgShow);
                                        }
                                        theFrd.link.Close(); theFrd.online = false;theFrd.link = null;
                                        break;
                                    }
                                default:
                                    {
                                        //不知道对方发的啥
                                        break;
                                    }
                            }
                        }
                        ChatsUsing = false;
                    }
                    FrdAsynRecive(theFrd);
                }, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("和" + theFrd.Name + " 连接中断", "信息提示",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                theFrd.online = false;
                UpdateChats();
            }
        }
        //文件接收
        void fileRecieve(string filename,long fileLength,Friend curFrd,Chat theChat)
        {
            string fileNameSuffix = filename.Substring(filename.LastIndexOf('.')); //文件后缀
            SaveFileDialog sfDialog = new SaveFileDialog()
            {
                Filter = "(*" + fileNameSuffix + ")|*" + fileNameSuffix + "", //文件类型
                FileName = filename
            };
            if (sfDialog.ShowDialog(this) == DialogResult.OK)
            {
                Console.WriteLine("正在保存来自" + curFrd.Name + "的文件");
                chatData newda = new chatData(curFrd.Name, false, "发送了 " + filename, DateTime.Now);
                theChat.Datas.Add(newda);
                addOutputBox(newda);

                byte[] buffer = new byte[1000000];
                string savePath = sfDialog.FileName; //获取文件的全路径
                                                     //保存文件
                int received = 0;
                long receivedTotalFilelength = 0;
                using (FileStream fs = new FileStream(savePath, FileMode.Create, FileAccess.Write))
                {
                    while (receivedTotalFilelength < fileLength) //收到的文件字节数组
                    {
                        received = curFrd.link.Receive(buffer); //每次收到的文件字节数组 可以直接写入文件
                        fs.Write(buffer, 0, received);
                        fs.Flush();
                        receivedTotalFilelength += received;
                    }
                    fs.Close();
                }

                string fName = savePath.Substring(savePath.LastIndexOf("\\") + 1); //文件名 不带路径
                string fPath = savePath.Substring(0, savePath.LastIndexOf("\\")); //文件路径 不带文件名
                newda = new chatData(user.Name, true, "接收了 " + fName + "\r\n保存路径为:" + fPath, DateTime.Now);
                theChat.Datas.Add(newda);
                addOutputBox(newda);
            }
        }
        private delegate void FileSave(string filename, long fileLength, Friend curFrd, Chat theChat);

        #endregion
        #region 发起连接&信息发送

        // 对某个ID对应的学号发起连接，并传递信息
        public Socket connect2other(string ID, string Msg)
        {
            string IPstr = FriendsQuery(ID);
            Console.WriteLine("Connect to ID: " + ID + "  IPstr: " + IPstr);
            if (!IsIP(IPstr))
            {
                Console.WriteLine("Connect to ID: " + ID + "  IPstr: " + IPstr);
                throw new Exception("IP地址异常");
            }
            int destPort = int.Parse(ID.Substring(ID.Length - 5)) + 2000;
            IPEndPoint serverIp = new IPEndPoint(IPAddress.Parse(IPstr), destPort);
            Socket tcpClient = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            tcpClient.Connect(serverIp);
            SendMsg2(Msg, tcpClient);
            return tcpClient;
        }
        //--发起聊天--

        //单独
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var selected = listView1.SelectedItems;
            //非单人
            if (selected.Count != 1)
            {
                return;
            }
            if (selected[0].SubItems[2].Text == user.ID) return;//或者本机聊天？

            string destID = selected[0].SubItems[2].Text;
            int destInd = Chats.FindIndex(x => x.ID == destID);
            Chat theChat = Chats[destInd];

            if (theChat.state < Chat.CHATSTATE.LINK)
            {
                //发起连接请求                
                string conMsg = Message.CON + user.ID;
                if (theChat.isGroup)
                {
                    string len = theChat.memNum.ToString();
                    while (len.Length < 2) len = "0" + len;
                    conMsg += "1" + len + theChat.ID + theChat.Name;
                }
                else conMsg += "0";
                foreach (Friend fd in theChat.friends)
                {
                    try
                    {
                        Socket p2p = connect2other(fd.ID, conMsg);
                        fd.online = true;
                        fd.link = p2p;
                        FrdAsynRecive(fd);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        MessageBox.Show("连接失败😔", "发生错误",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            //将该对话置为聊天框
            if (theChat.state != Chat.CHATSTATE.ONCHAT)
            {
                switchChat2(theChat);
            }
            UpdateChats();
        }
        //群聊       
        private void button_initGrp_Click(object sender, EventArgs e)
        {
            //复选框和选择是不一样的 Checked != selected
            //https://blog.csdn.net/lucky51222/article/details/41892429
            var selected = listView1.CheckedItems;
            int n = selected.Count;
            foreach (ListViewItem item in selected)
            {
                if (item.Tag.ToString() == "G")
                {
                    MessageBox.Show("群上加群😵", "信息提示",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (item.SubItems[1].Text == user.ID) n--;
                //Debug
                Console.WriteLine(item.SubItems[2].Text);
                //end of Debug
            }
            if (n < 1)
            {
                MessageBox.Show("你要找谁聊天呀嘿？在列表里勾选哦", "信息提示",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }else if (n == 1)
            {
                //单人
                return;
            }
            //新建会话
            List<Friend> Gfrds = new List<Friend>();
            string GID = "";
            foreach (ListViewItem item in selected)
            {
                int ind = Frds.FindIndex(x => x.ID == item.SubItems[2].Text);
                Gfrds.Add(Frds[ind]);
            }
            if (!Gfrds.Contains(user)) { Gfrds.Insert(0,user); } ;//用户是第一个
            for (int i = 0; i < Gfrds.Count; i++)
                GID += Gfrds[i].ID;
            Console.WriteLine("GroupID: "+GID);
            int theind = Chats.FindIndex(x => x.ID == GID);
            if (theind == -1)
            {
                //新的会话
                text inp = new text("群名:");
                DialogResult dr = inp.ShowDialog();
                if (dr == DialogResult.OK && inp.Value.Length > 0)
                {
                    string Gna = inp.Value;
                    Chat newGp = new Chat(Gfrds, Gna);

                    string len = newGp.memNum.ToString();
                    while (len.Length < 2) len = "0" + len;
                    string conMsg = Message.CON + user.ID + "1" + len + newGp.ID + newGp.Name;

                    for (int i = 0; i < Gfrds.Count; i++)
                    {
                        if (Gfrds[i] == user) continue;
                        //给未建立连接的好友建立连接
                        try
                        {
                            if (!Gfrds[i].linked)
                            {
                                Gfrds[i].link = connect2other(Gfrds[i].ID, conMsg);
                                Gfrds[i].online = true;
                                FrdAsynRecive(Gfrds[i]);
                            }
                                SendMsg2(conMsg, Gfrds[i].link);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                            MessageBox.Show("与" + Gfrds[i].ID + "的连接不成功", "连接失败",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }
                    }
                    Chats.Add(newGp);
                    switchChat2(newGp);
                }
            }
            else
            {
                MessageBox.Show("该群已存在...", "信息提示",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        #endregion
        #region 聊天 - 右侧界面
        //--绘制聊天框内容

            //切换到该对话
        void switchChat2(Chat theChat)
        {
            int count = 0;
            foreach (Chat c in Chats)
                if (c.state == Chat.CHATSTATE.ONCHAT) count++;
            if (count == 1 && theChat.state == Chat.CHATSTATE.ONCHAT) return;

            OutputBoxClear();
            addOutputBox(theChat.Datas);

            foreach (Chat c in Chats)   //清除正在聊天
                if (c.state == Chat.CHATSTATE.ONCHAT)
                    c.state = Chat.CHATSTATE.LINK;
            theChat.state = Chat.CHATSTATE.ONCHAT;

            if (label_RoomName.InvokeRequired)
            {
                // 当一个控件的InvokeRequired属性值为真时，说明有一个创建它以外的线程想访问它
                Action<string> actionDelegate = (x) => { label_RoomName.Text = x; };
                this.label_RoomName.Invoke(actionDelegate, theChat.Name);
            }
            else
            {
                label_RoomName.Text = theChat.Name;
            }
            theChat.unRead = 0;
            UpdateChats();
        }
        private delegate void UpdateOutputBox(List<chatData> cd);
        bool outputBoxWritting = false;
        void addOutputBox(chatData cd)
        {
            while (outputBoxWritting) { };
            outputBoxWritting = true;   //占用之
            UpdateOutputBox rb_s = new UpdateOutputBox(DrawChatOutput);
            List<chatData> drawC = new List<chatData>
            {
                cd
            };
            this.Invoke(rb_s, new object[] { drawC });
            outputBoxWritting = false;  //恢复不被占用
        }
        void addOutputBox(List<chatData> cd)
        {
            while (outputBoxWritting) { };
            outputBoxWritting = true;   //占用之
            UpdateOutputBox rb_s = new UpdateOutputBox(DrawChatOutput);
            this.Invoke(rb_s, new object[] { cd });
            outputBoxWritting = false;  //恢复不被占用
        }
        //清空 //委托的两种写法
        void OutputBoxClear()
        {
            while (outputBoxWritting) { };
            outputBoxWritting = true;
            if (richTextBox_output.InvokeRequired)
            {
                // 当一个控件的InvokeRequired属性值为真时，说明有一个创建它以外的线程想访问它
                Action actionDelegate = () => { richTextBox_output.Clear(); };
                this.richTextBox_output.Invoke(actionDelegate);
            }
            else
            {
                richTextBox_output.Clear();
            }
            outputBoxWritting = false;  //恢复不被占用
        }
        //绘制聊天记录
        void DrawChatOutput(List<chatData> cd)
        {
            HorizontalAlignment ha;
            for (int i = 0; i < cd.Count; i++)
            {
                ShowMsg_inRichTextBox(cd[i].time.ToShortTimeString() + "\r\n", Color.Gray, HorizontalAlignment.Center);
                if (cd[i].isself) ha = HorizontalAlignment.Right; else ha = HorizontalAlignment.Left;
                ShowMsg_inRichTextBox(cd[i].speakerName + "\r\n", Color.Black, ha);
                ShowMsg_inRichTextBox(cd[i].context + "\r\n", Color.Black, ha);
            }
        }
        //在richtextBox中添加
        void ShowMsg_inRichTextBox(string str, Color color, HorizontalAlignment direction)
        {
            richTextBox_output.SelectionColor = color;
            richTextBox_output.SelectionAlignment = direction;
            //向文本框的文本追加文本
            richTextBox_output.AppendText(str);
            //滑到最下面
            richTextBox_output.SelectionStart = richTextBox_output.TextLength;
            richTextBox_output.ScrollToCaret();
        }
        
        //发送信息
        private void button_send_Click(object sender, EventArgs e)
        {
            if (richTextBox_Input.Text == "")
            {
                MessageBox.Show("空消息已发送", "假的");
                return;
            }
            Chat theChat = null;
            foreach (Chat c in Chats)
                if (c.state == Chat.CHATSTATE.ONCHAT)
                    theChat = c;
            if (theChat == null)
            {
                MessageBox.Show("先建立会话哦", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string inputMsg = richTextBox_Input.Text;
            string msg = Message.MSG + user.ID;
            if (theChat.isGroup)
            {
                msg += '1';
                string le = theChat.memNum.ToString();
                while (le.Length < 2) le = "0" + le;
                msg += le+theChat.ID;
            }
            else msg += '0';
            string len = inputMsg.Length.ToString();
            while (len.Length < 4) len = "0" + len;
            msg += len + inputMsg;
            foreach (Friend fd in theChat.friends)
            {
                try
                {
                    if (fd.link == null) {
                        if(fd!=user)
                            Console.WriteLine(fd.ID+" 发送失败 没有连接");
                        continue; }
                    SendMsg2(msg, fd.link);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    MessageBox.Show(fd.ID+"连接已关闭", "发送失败",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    theChat.state = Chat.CHATSTATE.OFFLINE;
                    updateState();
                    return;
                }
            }
            chatData nda = new chatData(user.Name, true, inputMsg, DateTime.Now);
            theChat.Datas.Add(nda);
            addOutputBox(nda);
            richTextBox_Input.Text = "";
        }
        
        //--聊天记录
        private void button_data_Click(object sender, EventArgs e)
        {
            text inp = new text("查找聊天记录:");
            DialogResult dr = inp.ShowDialog();
            if (dr == DialogResult.OK && inp.Value.Length > 0)
            {
                string keyS = inp.Value;
                int index = richTextBox_output.Find(keyS,RichTextBoxFinds.None);//调用find方法，并设置区分全字匹配
                int startPos = index;
                if (index != -1)
                {
                    richTextBox_output.SelectionStart = index;
                    richTextBox_output.SelectionLength = keyS.Length;
                    richTextBox_output.Focus();
                    button_data.Tag = keyS;
                    button_next.Tag = index;
                    button_next.Visible = true;
                }
            }
        }
        private void button_next_Click(object sender, EventArgs e)
        {
            string keyS = button_data.Tag.ToString();
            int index = int.Parse(button_next.Tag.ToString());
            index = richTextBox_output.Find(keyS, index + keyS.Length, RichTextBoxFinds.None);
            button_next.Tag = index.ToString();
            if (index == -1)//若查到文件末尾
            {
                MessageBox.Show("已搜索完毕");
                button_next.Visible = false;
            }else
            {
                richTextBox_output.SelectionStart = index;
                richTextBox_output.SelectionLength = keyS.Length;
                richTextBox_output.Focus();
            }
        }

        //窗口抖动
        private void buttonShake_Click(object sender, EventArgs e)
        {
            Chat theChat = null;
            foreach (Chat c in Chats)
                if (c.state == Chat.CHATSTATE.ONCHAT)
                    theChat = c;
            if (theChat == null)
            {
                MessageBox.Show("先建立会话哦", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string msg = Message.SHK + user.ID;
            if (theChat.isGroup)
            {
                msg += '1';
                string le = theChat.memNum.ToString();
                while (le.Length < 2) le = "0" + le;
                msg += le + theChat.ID;
            }
            else msg += '0';
            try
            {
                for (int i = 0; i < theChat.memNum; i++)
                {
                    SendMsg2(msg, theChat.friends[i].link);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        private delegate void Shake();
        private void Window_Shake()
        {
            count = 0;
            timer1.Start();
        }
        int count = 0;
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            int count1 = count % 12;
            if (count1 < 3)
            {
                Point new_loc = new Point(this.Location.X - 1, this.Location.Y - 1);
                this.Location = new_loc;
                count++;
            }
            else if (count1 < 6)
            {
                Point new_loc = new Point(this.Location.X + 1, this.Location.Y + 1);
                this.Location = new_loc;
                count++;
            }
            else if (count1 < 9)
            {
                Point new_loc = new Point(this.Location.X + 1, this.Location.Y - 1);
                this.Location = new_loc;
                count++;
            }
            else if (count1 < 12)
            {
                Point new_loc = new Point(this.Location.X - 1, this.Location.Y + 1);
                this.Location = new_loc;
                count++;
            }
            if (count == 40)
                timer1.Stop();
        }
        //文件
        private void button_file_Click(object sender, EventArgs e)
        {
            Chat theChat = null;
            foreach (Chat c in Chats)
                if (c.state == Chat.CHATSTATE.ONCHAT)
                { theChat = c; break; }
            if (theChat == null)
            {
                MessageBox.Show("先建立会话哦", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string filePath = null;   //文件的全路径
            string fileName = null;   //文件名称(不包含路径) 
            OpenFileDialog ofDialog = new OpenFileDialog();
            if (ofDialog.ShowDialog(this) == DialogResult.OK)
            {
                fileName = ofDialog.SafeFileName; //获取选取文件的文件名
                filePath = ofDialog.FileName;     //获取包含文件名的全路径
            }else
            {
                MessageBox.Show(@"请选择需要发送的文件!");
                return;
            }
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show(@"请选择需要发送的文件!");
                return;
            }
            label1.Text = "发送 "+fileName;      //将文件名显示在文本框上 
            label1.Visible = true;

            //发送文件之前 将文件名字和长度发送过去
            long fileLength = new FileInfo(filePath).Length;
            string msg = Message.FLE + user.ID;
            if (theChat.isGroup)
            {
                msg += '1';
                string le = theChat.memNum.ToString();
                while (le.Length < 2) le = "0" + le;
                msg += le + theChat.ID;
            }
            else msg += '0';
            msg += fileName + "-" +fileLength;
            for(int i = 0; i < theChat.memNum; i++)
            {
                if (theChat.friends[i].linked)
                {
                    SendMsg2(msg, theChat.friends[i].link);
                    byte[] buffer = new byte[1000000];
                    try
                    {
                        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                        {
                            int readLength = 0;
                            long sentFileLength = 0;
                            while ((readLength = fs.Read(buffer, 0, buffer.Length)) > 0 && sentFileLength < fileLength)
                            {
                                sentFileLength += readLength;
                                theChat.friends[i].link.Send(buffer,
                                    0, readLength, SocketFlags.None);
                            }
                            fs.Close();
                            theChat.friends[i].link.SendFile(filePath,
                            null, null, TransmitFileOptions.UseDefaultWorkerThread);
                        }
                        chatData newda = new chatData(theChat.friends[i].Name, false, "接收了 " + fileName, DateTime.Now);
                        theChat.Datas.Add(newda);
                        addOutputBox(newda);
                    }
                    catch
                    {
                        Console.WriteLine("发送失败： To" + theChat.friends[i].Name);
                    }
                }
            }
            label1.Visible = false;
            MessageBox.Show("文件 "+ filePath + "传输成功", "信息提示");            
        }
        
        //进度条
        //https://www.w3cschool.cn/csharp/csharp-6z9g2pls.html

        #endregion
        class Chat
        {
            //不在线，在线，已建立连接，新消息，当前聊天框 →递进
            public enum CHATSTATE { OFFLINE, ONLINE,LINK, NEWS, ONCHAT };
            public List<chatData> Datas;
            public int unRead;
            public string ID;   //聊天编号- Single=ID - Group=IDs
                                //public string serverID;
            public string Name; //聊天名字
            public List<Friend> friends;
            public bool isGroup;
            public int memNum;
            public bool listening;
            public CHATSTATE state;
            public bool FileTransing;
            Chat()
            { }
            public Chat(List<Friend> _friends, string name)//, string _sever
            {
                isGroup = true;
                Name = name;
                friends = _friends;
                ID = "";
                //serverID = _sever;
                memNum = friends.Count;
                listening = false;
                unRead = 0;
                Datas = new List<chatData>();
                FileTransing = false;
               

                bool allon = friends[0].online;
                for (int i = 0; i < memNum; i++)
                {
                    if (!friends[i].online) allon = false;
                    ID += friends[i].ID;
                }
                if (allon) state = CHATSTATE.ONLINE;
                else state = CHATSTATE.OFFLINE;
            }
            public Chat(Friend _friend)
            {
                friends = new List<Friend> { _friend };
                ID = _friend.ID;
                isGroup = false;
                //serverID = "";
                memNum = 1;
                listening = false;
                unRead = 0;
                Datas = new List<chatData>();
                Name = _friend.Name;
                FileTransing = false;
                if (_friend.online) state = CHATSTATE.ONLINE;
                else state = CHATSTATE.OFFLINE;
            }
            public Chat(string id)
            {
                ID = id;
            }
            //生成显示的item
            public ListViewItem item;
            public ListViewItem setItem()
            {
                string[] str = new string[3];
                string tag = "";
                if (isGroup)
                {
                    string mem = friends[0].Name;
                    string ids = friends[0].ID;
                    bool allon = friends[0].online;
                    bool alllink = friends[0].linked;
                    for (int i = 1; i < friends.Count; i++)
                    {
                        mem += "," + friends[i].Name;
                        ids += friends[i].ID;
                        if (!friends[i].online) allon = false;
                        if (!friends[i].linked) alllink = false;
                    }
                    str[0] = Name;
                    str[1] = mem;
                    str[2] = ids;
                    tag = "G";
                    if (alllink)
                    {
                        if (state < CHATSTATE.LINK) state = CHATSTATE.LINK;
                    }
                    else
                    {
                        if (allon)
                        {
                            if (state < CHATSTATE.ONLINE) state = CHATSTATE.ONLINE;
                        }
                        else
                        {
                            state = CHATSTATE.OFFLINE;
                        }
                    }                    
                }
                else
                {
                    str[0] = friends[0].Name;
                    str[1] = friends[0].ID;
                    str[2] = friends[0].ID;
                    tag = "S";
                    if (!friends[0].online) state = CHATSTATE.OFFLINE;
                    else
                    {
                        if (!friends[0].linked)
                        { if (state < CHATSTATE.ONLINE) state = CHATSTATE.ONLINE; }
                        else
                            if (state < CHATSTATE.LINK) state = CHATSTATE.LINK;
                    }
                }
                item = new ListViewItem(str)
                {
                    Tag = tag
                };
                Color bkcl = Color.Red;
                switch (state)
                {
                    case CHATSTATE.OFFLINE:
                        {
                            bkcl = Color.Transparent;
                            break;
                        }
                    case CHATSTATE.ONLINE:
                        {
                            bkcl = Color.LightPink;
                            break;
                        }
                    case CHATSTATE.LINK:
                        {
                            bkcl = Color.LightSkyBlue;
                            break;
                        }
                    case CHATSTATE.NEWS:
                        {
                            bkcl = Color.Yellow;
                            break;
                        }
                    case CHATSTATE.ONCHAT:
                        {
                            bkcl = Color.LightBlue;
                            break;
                        }
                }
                item.BackColor = bkcl;
                return item;
            }
            

        }

        //好友类
        class Friend
        {
            public string IP { get; set; }
            public DateTime IP_udtime { get; set; }
            public static int freshTime = 300;
            public bool online { get;set;}
            public bool linked { 
                    get {
                        if (link != null && link.Connected) return true;
                        return false;
                    } }

            public string ID { get; set; }
            public string Name { get; set; }
            public Socket link
            {
                get; set;
            }
            Friend() { }
            public Friend(string _IP, bool _online, string _ID, string _Name, Socket _link)
            {
                online = _online;
                IP = _IP;
                ID = _ID;
                Name = _Name;
                link = _link;
                IP_udtime = DateTime.Now;
                if (link != null&&link.Connected)
                {
                    IP = ((System.Net.IPEndPoint)link.RemoteEndPoint).Address.ToString();
                }
            }
        }
        //对话类

        public class chatData
        {
            public string speakerName;
            public bool isself;
            public string context;
            public DateTime time;
            public string get1stLine()
            {
                return  speakerName+ " " + time.ToShortTimeString();
            }
            public string get2rdLine()
            {
                return context;
            }
            chatData() { }
            public chatData(string speaker, bool _isself, string _context, DateTime _time)
            {
                speakerName = speaker;
                isself = _isself;
                context = _context;
                time = _time;
            }
        }
        public class chatDataComparer : IComparer<chatData>
        {
            public int Compare(chatData x, chatData y)
            {
                if (x == null && y == null) return 0;
                if (x == null) return -1;
                if (y == null) return 1;
                //按时间
                return DateTime.Compare(x.time, y.time);
            }
        }

        /*
            * 连接：
            * - Single
            * CON+userIDXXXXX+0
            * - Group
            * CON+userIDXXXXX+1+length(2)+othersIDXXX+GroupName
            * 信息： 1--i'm sever
            * - Single | Group.Client
            * MSG+userIDXXXXX+0+length(4)+messages 故发送时间以发送方为准
            * - Group.Sever
            * MSG+userIDXXXXX+1+length(4)+messages
            */
        class Message
        {
            public static bool check(string msg)
            {
                if (msg.Length < 14) return false;
                //string type = msg.Substring(0, 3);
                //if (type != CON || type != MSG || type != FLE || type != RFS || type != SHK) return false;
                //string remoteID = msg.Substring(3, 10);
                //int id;
                //if (!int.TryParse(remoteID, out id)) return false;
                //bool isG = false;
                //if (msg[13] == '1') isG = true;
                //else if (msg[13] != '0') return false;
                //if (isG)
                //{
                //    if (msg.Length < 27) return false;
                //    string len = msg.Substring(14, 2);
                //    if (!int.TryParse(len, out id)) return false;
                //    if (msg.Length < 17+id*10) return false;
                //}
                return true;
            }
            public const string CON = "早早早";
            public const string MSG = "嘀嘀嘀";
            public const string SHK = "来摇摆";
            public const string FLE = "发文件";
            public const string RFS = "哥屋恩";
        }

        //确认按键 Enter
        private void richTextBox_Input_Enter(object sender, EventArgs e)
        {
            AcceptButton = button_send;
        }
        private void richTextBox_Input_Leave(object sender, EventArgs e)
        {
            AcceptButton = null;
        }
        private void textBox_find_Enter(object sender, EventArgs e)
        {
            AcceptButton = button_find;
        }
        private void button_detail_Click(object sender, EventArgs e)
        {

            Chat theChat = null;
            foreach (Chat c in Chats)
                if (c.state == Chat.CHATSTATE.ONCHAT)
                { theChat = c; break;
                }
            if (theChat == null)
                return;
            string detail = "群聊名称:"+theChat.Name+"\r\n";
            for(int i = 0; i < theChat.memNum; i++)
            {
                detail += theChat.friends[i].Name + "(" + theChat.friends[i].ID+ ")"+"\r\n";
            }
                MessageBox.Show(detail);
        }

    }
}
/*SoundPlayer player = new SoundPlayer();
player.SoundLocation = @"D:\test.wav";
player.Load(); //同步加载声音
player.Play(); //启用新线程播放
//player.PlayLooping(); //循环播放模式
//player.PlaySync(); //UI线程同步播放
--------------------- 
作者：believe209 
来源：CSDN 
原文：https://blog.csdn.net/wangzhen209/article/details/53285651 
版权声明：本文为博主原创文章，转载请附上博文链接！
 */
