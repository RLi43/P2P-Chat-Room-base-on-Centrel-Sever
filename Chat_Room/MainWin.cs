﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
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
        IPAddress userIP;
        List<Chat> Chats = new List<Chat>();
        List<Friend> Frds = new List<Friend>();
        List<string> ChattingID = new List<string>();
        int localPort;
        public MainWin()
        {
            InitializeComponent();
            load();
        }
        public void load()
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
            userID = log.userID;
            SocketToSever = Login.SocketClient;
            localPort = int.Parse(userID.Substring(userID.Length - 5)) + 2000;

            StartListening();
            outputBoxWritting = false;
            //---增加本人为好友---
            Friend newfrd = new Friend("", true, userID, "我", null);
            Frds.Add(newfrd);
            //显示对话
            Chat newchat = new Chat(newfrd);
            Chats.Add(newchat);
            listViewUpdate();
        }
        #region CS
        //发送字符信息到指定socket 
        public static void SendMsg2(string sendMsg, Socket send2)
        {
            //将输入的内容字符串转换为机器可以识别的字节数组     
            byte[] arrClientSendMsg = Encoding.UTF8.GetBytes(sendMsg);
            //调用客户端套接字发送字节数组     
            send2.Send(arrClientSendMsg);
            Console.WriteLine("Send to " + send2.RemoteEndPoint.ToString() + ": " + sendMsg);
        }
        string receiveFromSever(int size = 1024)
        {
            byte[] arrRecvmsg = new byte[size];  //内存缓冲区，临时性存储接收到的消息 
                                                 //将客户端套接字接收到的数据存入内存缓冲区，并获取长度  
            int length = SocketToSever.Receive(arrRecvmsg);
            string rev = Encoding.UTF8.GetString(arrRecvmsg, 0, length);
            if (length == 0)
            {
                //如果客户端正常关闭后，会向服务端发送长度为0的空数据，利用这一点将这个客户端关闭
                //TODO test
                SocketToSever.Close();
                MessageBox.Show("与服务器连接中断", "!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                load();
                return null;
            }
            Console.WriteLine(rev);
            return rev;
        }
        #endregion
        #region 本机服务
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
                textBox_find.Text = "查找学号";
            } else if (textBox_find.Text == "查找学号") find_text_empty = true;
            else find_text_empty = false;
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

        //--查询好友--
        string friendsQuery(string IDnum)
        {
            SendMsg2('q' + IDnum, SocketToSever);
            return receiveFromSever();
        }
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
            string idname = textBox_find.Text;
            if (idname == userID)
            {
                MessageBox.Show("嘟嘟嘟……\r\n打通了自己的电话", "自问自答",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                for (int i = 0; i < Frds.Count; i++)
                {
                    if (Frds[i].ID == userID)
                    {
                        return;
                    }
                }
                //把自己删掉了？
                Friend newfrd = new Friend(friendsQuery(userID), true, userID, userID, null);
                Frds.Add(newfrd);
                Chat newchat = new Chat(newfrd);
                listViewUpdate();
                return;
            }
            string result = friendsQuery(idname);
            if ("n" != result && !isIP(result))
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
                bool isnew = true;
                //新的好友？
                for (int i = 0; i < Frds.Count; i++)
                {
                    if (idname == Frds[i].ID)
                    {
                        //考虑直接开启对话？
                        MessageBox.Show("该好友可以通过通讯录找到哦", "信息提示",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        isnew = false;
                        Frds[i].IP = result;
                        Frds[i].online = state;
                        //TODO: renew ChatList
                        break;
                    }
                }
                //新的好友 
                if (isnew)
                {
                    Friend newfrd = new Friend(friendsQuery(idname), state, idname, idname, null);
                    Frds.Add(newfrd);
                    Chat newchat = new Chat(newfrd);
                    Chats.Add(newchat);
                    listViewUpdate();
                }
            }
        }//轮询好友
        //可能需要考虑占用的问题
        void updateState()
        {
            foreach (ListViewItem item in listView1.Items)
            {
                string result = friendsQuery(item.SubItems[2].Text);
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
        //--end of 查询好友--

        //--信息操作---
        //修改昵称
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
                foreach (ListViewItem item in selected)
                {
                    //更新界面
                    item.SubItems[0].Text = inp.Value;
                    string id = item.SubItems[2].Text;
                    //更新对话列表
                    for (int i = 0; i < Chats.Count; i++)
                    {
                        if (Chats[i].ID == id)
                        {
                            Chats[i].Name = inp.Value;
                        }
                    }
                    //更新通讯录
                    if (item.Tag.ToString() == "S")
                    {
                        for (int i = 0; i < Frds.Count; i++)
                        {
                            if (Frds[i].ID == id)
                            {
                                Frds[i].Name = inp.Value;
                            }
                        }
                    }
                }
            }
            inp.Dispose();
        }
        //删除好友
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
        //--end of 信息操作---
        #endregion
        #region 接受链接
        //获取本机地址
        //https://www.cnblogs.com/iack/p/3685680.html
        public static IPAddress GetLocalIP()
        {
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
        //TCP Socket异步接受监听
        //https://blog.csdn.net/weixin_40271181/article/details/78981701
        //https://blog.csdn.net/mss359681091/article/details/51790931
        public void StartListening()
        {
            userIP = GetLocalIP();
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
                    Console.WriteLine(clientSocket.RemoteEndPoint.ToString() + ": " + Recv);
                    if (length == 0)
                    {
                        Console.WriteLine(clientSocket.RemoteEndPoint.ToString() + " 已断开连接");
                        //TODO：clientSocket.Close();
                        //return;
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
                
        //根据接收到的信息操作
        private delegate void UpdateChatList(List<Chat> cs);
        void DrawChatList(List<Chat> cs)
        {
            listView1.Items.Clear();
            foreach (Chat c in Chats)
            {
                listView1.Items.Add(c.setItem());
            }
        }
        bool listviewUsing = false;
        //大门
        public void GateMsgTrans(string Recv, Socket clientSocket)
        {

            //TODO： 黑名单需要吗

            //message 检查

            string type = Recv.Substring(0, 3);
            if (type != Message.CON) return;        //软件统一才能使用啊
            string remoteID = Recv.Substring(3, 10);
            bool isSingle = Recv[13] == '0';
            string ChatID = remoteID;
            string Gname = "";
            string conMsg = Message.CON + userID + "1" + Recv.Substring(14);//因为单独聊天不需要再次发送
            if (!isSingle)
            {
                int length = int.Parse(Recv.Substring(14, 2)) * 10;
                ChatID = Recv.Substring(16, length);
                Gname = Recv.Substring(16 + length);
            }
            Chat theChat;
            int destInd = Chats.FindIndex(x => x.ID == ChatID);
            if (destInd == -1)
            {
                //新会话
                if (isSingle)
                {
                    DialogResult rs = MessageBox.Show(remoteID + " 向您发起会话"
                        , "会话请求", MessageBoxButtons.YesNoCancel
                         , MessageBoxIcon.Question);
                    if (rs == DialogResult.Yes)
                    {
                        //增加好友并开启对话
                        Friend newfrd = new Friend("", true, remoteID, remoteID, clientSocket);
                        Frds.Add(newfrd);
                        //显示对话
                        Chat newchat = new Chat(newfrd);
                        Chats.Add(newchat);
                        theChat = newchat;
                    }
                    else if (rs == DialogResult.No)
                    {
                        //中断本次对话 不回应
                        return;
                    }
                    else
                    {
                        //拒绝聊天——加入黑名单
                        return;
                    }
                    /*
                    //TODO 不知道这里的内容有没有改
                    string debug = "Something";
                    if (theChat.friends[0].link == null) debug = "null";
                    Console.WriteLine("Now theChat.friends[0].link is" + debug);

                    theChat.friends[0].link = clientSocket;*/
                }
                else //群聊
                {
                    DialogResult rs = MessageBox.Show(
                        remoteID + "邀请您加入群聊：" + Gname, "会话请求"
                        , MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (rs == DialogResult.OK)
                    {
                        //建立新的会话
                        int l = ChatID.Length / 10;
                        List<Friend> newFrds = new List<Friend>();
                        //为新成员建立Friend类
                        for (int j = 0; j < l; j++)
                        {
                            string idname = ChatID.Substring(j * 10, 10);
                            if (idname == userID) continue;
                            //新的好友？
                            bool isnew = true;
                            for (int i = 0; i < Frds.Count; i++)
                            {
                                //已有的好友
                                if (idname == Frds[i].ID)
                                {
                                    newFrds.Add(Frds[i]);
                                    isnew = false;
                                    string ip = friendsQuery(idname);
                                    if (!isIP(ip))
                                    {
                                        Console.WriteLine("发生错误 " + idname + " 不在线");
                                        return;
                                    }
                                    Frds[i].IP = ip;
                                    Frds[i].online = true;
                                    if (idname == remoteID)
                                    {
                                        Frds[i].link = clientSocket;
                                        break;
                                    }
                                    //建立socket
                                    if(Frds[i].link==null)
                                        Frds[i].link = connect2other(idname, conMsg);
                                    break;
                                }
                            }
                            if (isnew)
                            {
                                //新的好友
                                Friend newfrd = new Friend("", true, idname, idname
                                    , connect2other(idname, conMsg));
                                Frds.Add(newfrd);
                                newFrds.Add(newfrd);
                            }
                        }
                        //建立会话
                        Chat newchat = new Chat(newFrds, Gname);
                        Chats.Add(newchat);
                        theChat = newchat;
                        //theChat.state = Chat.CHATSTATE.ONCHAT;
                        listViewUpdate();
                    }
                    else
                    {//拒绝加入
                        return;
                    }
                }
                //似乎这里不需要重绘了
                listViewUpdate();
                switchChat2(theChat);

                //TODO 回复ACK 发送方成功发送/而不是被拒收
            }
            else
            {
                theChat = Chats[destInd];
                if (theChat.state > Chat.CHATSTATE.ONLINE)
                {
                    //已经连上又发一遍？干啥呢？ 修改群名？
                    if (!isSingle)
                    {
                        theChat.Name = Gname;
                    }
                    //TODO ACK确认一下
                    return;
                }
                else
                {
                    //尚未建立连接
                    if (isSingle)
                    {
                        theChat.friends[0].link = clientSocket;

                    }
                    else //群聊
                    {
                        theChat.Name = Gname;
                        //建立会话
                        foreach (Friend fd in theChat.friends)
                        {
                            if (fd.link == null)
                            {
                                //建立连接
                                try
                                {
                                    fd.link=connect2other(fd.ID, conMsg);
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                    MessageBox.Show("链接失败！to " + fd.ID, "失败"
                                        , MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    return;
                                }
                            }
                        }
                    }
                }
                theChat.state = Chat.CHATSTATE.LINK;
            }
                    //TODO 回复ACK 发送方成功发送/而不是被拒收
            //TODO
            //新开一个线程收听
            theChat.listening = true;
            //ParameterizedThreadStart pts = new ParameterizedThreadStart(ChatAsynRecive);
            //Thread thread = new Thread(pts);
            ////设置为后台线程，随着主线程退出而退出 
            //thread.IsBackground = true;
            ////启动线程     
            //thread.Start(theChat);

            //ChatAsynRecive(theChat);
            //创建一个通信线程      
            Thread thread = new Thread(ChatAsynRecive);
            //设置为后台线程，随着主线程退出而退出 
            thread.IsBackground = true;
            //启动线程     
            thread.Start(theChat);
        }
        //对话监听
        void ChatAsynRecive(object obj)
        {
            Chat theChat = obj as Chat;
            byte[] data = new byte[1024];
            int cnt = 0;
            try
            {
                for (cnt = 0; cnt < theChat.friends.Count; cnt++)
                {
                    Friend curFrd = theChat.friends[cnt];
                    Socket link = curFrd.link;
                    if (link == null) break;
                    link.BeginReceive(data, 0, data.Length, SocketFlags.None,
                    asyncResult =>
                    {
                        int length = 0;
                        string Recv = "";
                        try
                        {
                            length = link.EndReceive(asyncResult);
                            Recv = Encoding.UTF8.GetString(data, 0, length);
                            Console.WriteLine(link.RemoteEndPoint.ToString() + " : " + Recv);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            theChat.state = Chat.CHATSTATE.OFFLINE;
                            MessageBox.Show("和" + theChat.Name + " 连接中断", "信息提示",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //TODO 关闭线程
                            link.Close();
                        }
                        if (length == 0)
                        {
                            //删掉
                            MessageBox.Show("好友退出了会话", "信息提示",
                                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                            theChat.state = Chat.CHATSTATE.OFFLINE;
                            listViewUpdate();
                            //TODO:关闭线程
                            //
                            return;
                        }

                        string type = Recv.Substring(0, 3);
                        switch (type)
                        {
                            case Message.CON:
                                {
                                    //TODO : send ACK
                                    break;
                                }
                            case Message.FIN:
                                {
                                    //可以什么都不做来着
                                    break;
                                }
                            case Message.ACK:
                                {
                                    //TODO 
                                    break;
                                }
                            case Message.MSG:
                                {
                                    //务必先取得连接之后才能对话
                                    if (theChat.state > Chat.CHATSTATE.ONLINE)
                                    {
                                        int startInd = 18;
                                        if (theChat.isGroup)
                                        {
                                            startInd = 20 + 10 * theChat.memNum;
                                        }
                                        //int l = int.Parse(Recv.Substring(startInd-4, 4));
                                        string Msgs = Recv.Substring(startInd);
                                        //增加聊天记录
                                        chatData newDa = new chatData(curFrd.Name,
                                                false, Msgs, DateTime.Now);
                                        theChat.Datas.Add(newDa);
                                        theChat.unRead++;

                                        //当前对话直接将消息绘制,即增加最后一条
                                        if (theChat.state == Chat.CHATSTATE.ONCHAT)
                                        {
                                            while (outputBoxWritting) { };
                                            outputBoxWritting = true;   //占用之
                                            RichBox_Show rb_s = new RichBox_Show(DrawChatOutput);
                                            List<chatData> drawC = new List<chatData>();
                                            drawC.Add(newDa);
                                            this.Invoke(rb_s, new object[] { drawC });
                                            outputBoxWritting = false;  //恢复不被占用
                                            theChat.unRead = 0;
                                        }
                                        else
                                        {
                                            theChat.state = Chat.CHATSTATE.NEWS;
                                            //重绘 更新会话列表
                                            listViewUpdate();
                                        }
                                    }
                                    else
                                    {//未确认方发送的连接
                                        return;
                                    }
                                    break;
                                }
                            case Message.SHK:
                                {
                                    Shake shake = new Shake(Window_Shake);
                                    this.Invoke(shake, new object[] { });
                                    break;
                                }
                        }

                        /*
                        //文件操作
                        if (Recv == "<__cmd__transfer__file__>")
                        {
                            allDone.Reset();
                            receive_save r_s = new receive_save(ReceiveFileConnect);
                            this.Invoke(r_s, new object[] { link });
                            allDone.WaitOne();
                        }
                        else if (Recv == "<__cmd__shake__>")
                        {
                            Shake shake = new Shake(Window_Shake);
                            this.Invoke(shake, new object[] { });
                        }
                        else
                        {
                            //如果当前写字框没有被占用
                            while (richTextBox_show_writing) { };
                            //等到其他线程解除了写字框的占用
                            richTextBox_show_writing = true;   //占用之
                            RichBox_Show rb_s = new RichBox_Show(ShowMsg_inRichTextBox);
                            string show_string = Recv;
                            this.Invoke(rb_s, new object[] { show_string, Color.Black, HorizontalAlignment.Left });
                            richTextBox_show_writing = false;  //恢复不被占用
                        }*/
                        ChatAsynRecive(theChat);


                    }, null);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                MessageBox.Show("和" + theChat.Name + " 连接中断", "信息提示",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                theChat.state = Chat.CHATSTATE.OFFLINE;
                for (int i = 0; i < theChat.memNum; i++)
                {
                    theChat.friends[i].link.Close();
                }
                listViewUpdate();
            }
        }
        

        // 对某个ID对应的学号发起连接，并传递信息
        public Socket connect2other(string ID, string Msg)
        {
            string IPstr = friendsQuery(ID);
            Console.WriteLine("Connect to ID: " + ID + "  IPstr: " + IPstr);
            if (!isIP(IPstr))
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
                //或者直接触发“发起群聊”
                return;
            }
            if (selected[0].SubItems[2].Text == userID) return;//或者本机聊天？

            string destID = selected[0].SubItems[2].Text;
            int destInd = Chats.FindIndex(x => x.ID == destID);
            Chat theChat = Chats[destInd];

            if (theChat.state < Chat.CHATSTATE.LINK)
            {
                //发起连接请求                
                string conMsg = Message.CON + userID + "0";
                //if (theChat.isGroup)
                //{
                //    string len = theChat.memNum.ToString();
                //    while (len.Length < 2) len = "0" + len;
                //    conMsg = Message.CON + userID + "1"+len+theChat.ID+theChat.Name;
                //}
                Socket p2ps = null;
                try
                {
                    p2ps = connect2other(destID, conMsg);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    MessageBox.Show("连接失败😔", "发生错误",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (p2ps == null)//应该到不了这里
                {
                    MessageBox.Show("连接失败😔", "发生错误2",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    //连接成功               
                    //TODO:被拒绝呢？

                    theChat.friends[0].link = p2ps;
                    //Socket[] links = new Socket[1];
                    //links[0] = p2ps;
                    //TODO 新的线程
                    ChatAsynRecive(theChat);
                }
            }
            //将该对话置为聊天框
            if (theChat.state != Chat.CHATSTATE.ONCHAT)
            {
                switchChat2(theChat);
            }
        }
        void switchChat2(Chat theChat)
        {
            int count = 0;
            foreach (Chat c in Chats)  
                if (c.state == Chat.CHATSTATE.ONCHAT) count++;
            if (count == 1&&theChat.state==Chat.CHATSTATE.ONCHAT) return;

                    //清除聊天框
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
            listViewUpdate();
        }
        void listViewUpdate()
        {
            //更新会话列表
            while (listviewUsing) { };
            listviewUsing = true;   //占用之
            UpdateChatList cl = new UpdateChatList(DrawChatList);
            this.Invoke(cl, new object[] { Chats });
            listviewUsing = false;  //恢复不被占用

        }
        //群聊       
        private void button_initGrp_Click(object sender, EventArgs e)
        {
            var selected = listView1.SelectedItems;
            //Debug
            foreach (ListViewItem item in selected)
            {
                Console.WriteLine(item.SubItems[2].Text);
            }
            //end of Debug
            int n = selected.Count;
            if (n < 1)
            {
                MessageBox.Show("你要找谁聊天呀嘿？在列表里点击哦", "信息提示",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }else if (n == 1)
            {
                //单人
                return;
            }
            //存在？
            List<Friend> Gfrds = new List<Friend>();
            string GID = "";
            foreach (ListViewItem item in selected)
            {
                if (item.SubItems[2].Text == userID)
                {
                    if (n == 2) return;//单人
                    continue;
                }
                int ind = Frds.FindIndex(x => x.ID == item.SubItems[2].Text);
                Gfrds.Add(Frds[ind]);
            }
            List<string> sorted = new List<string>();
            for (int i = 0; i < n; i++)
            {
                sorted.Add(Gfrds[i].ID);
            }
            sorted.Sort();
            for (int i = 1; i < n; i++)
            {
                GID += sorted[i];
            }
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
                    string conMsg = Message.CON + userID + "1" + len + newGp.ID + newGp.Name;
                    
                    //Socket[] p2ps = new Socket[n];
                    for(int i = 0; i < n; i++)
                    {
                        if (Gfrds[i].link == null)
                        {
                            //给未建立连接的好友建立连接
                            try
                            {
                                Gfrds[i].link = connect2other(Gfrds[i].ID, conMsg);
                            }catch(Exception ex)
                            {
                                Console.WriteLine(ex);
                                MessageBox.Show("与"+Gfrds[i].ID+"的连接不成功", "连接失败",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        //p2ps[i] = Gfrds[i].link;
                    }
                    ChatAsynRecive(newGp);
                }
            }
            else
            {
                MessageBox.Show("该对话已存在...", "信息提示",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }


        //聊天界面
        bool outputBoxWritting = false;
        private delegate void RichBox_Show(List<chatData> cd);
        public void DrawChatOutput(List<chatData> cd)
        {
            cd.Sort();
            HorizontalAlignment ha;
            for(int i = 0; i < cd.Count; i++)
            {
                if (cd[i].isself) ha = HorizontalAlignment.Right; else ha = HorizontalAlignment.Left;
                ShowMsg_inRichTextBox(cd[i].get1stLine()+"\n", Color.Black, ha);
                ShowMsg_inRichTextBox(cd[i].get2rdLine()+"\n", Color.Black, ha);
            }
        }
        public void ShowMsg_inRichTextBox(string str, Color color, HorizontalAlignment direction)
        {
            richTextBox_output.SelectionColor = color;
            richTextBox_output.SelectionAlignment = direction;
            //向文本框的文本追加文本
            richTextBox_output.AppendText(str);
            richTextBox_output.SelectionStart = richTextBox_output.TextLength;
            richTextBox_output.ScrollToCaret();
        }
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
            string msg = Message.MSG + userID;
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
                    if (fd.link == null) { Console.WriteLine(fd.ID+""); break; }
                    SendMsg2(msg, fd.link);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    MessageBox.Show(fd.ID+"连接已关闭", "发送失败",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //如果当前写字框没有被占用
            while (outputBoxWritting) { };
            //等到其他线程解除了写字框的占用
            outputBoxWritting = true;   //占用之
                                        //新建委托
            RichBox_Show rb_s = new RichBox_Show(DrawChatOutput);
            chatData nda = new chatData(userID, true, inputMsg, DateTime.Now);
            theChat.Datas.Add(nda);
            List<chatData> drawC = new List<chatData>();
            drawC.Add(nda);
            this.Invoke(rb_s, new object[] { drawC });
            outputBoxWritting = false;  //恢复不被占用
            richTextBox_Input.Text = "";
        }


        //窗口抖动
        //发送
        private void buttonShake_Click(object sender, EventArgs e)
        {

            Chat theChat = null;
            foreach (Chat c in Chats)
                if (c.state == Chat.CHATSTATE.ONCHAT)
                    theChat = c;
            if (theChat == null)
            {
                MessageBox.Show("先建立会话哦", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string msg = Message.SHK + userID;
            if (theChat.isGroup)
            {
                msg += '1';
                string le = theChat.memNum.ToString();
                while (le.Length < 2) le = "0" + le;
                msg += le + theChat.ID;
            }
            else msg += '0';

            for (int i = 0; i < theChat.memNum; i++)
            {
                SendMsg2(msg, theChat.friends[i].link);
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
        //--end of 聊天

        //编码与发送

        //
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
                //ID 标识需要整理
                List<string> sorted = new List<string>();
                for (int i = 0; i < memNum; i++)
                {
                    sorted.Add(friends[i].ID);
                }
                sorted.Sort();

                bool allon = friends[0].online;
                for (int i = 1; i < memNum; i++)
                {
                    if (!friends[i].online) allon = false;
                    ID += sorted[i];
                }
                if (allon) state = CHATSTATE.ONLINE;
                else state = CHATSTATE.OFFLINE;
            }
            public Chat(Friend _friend)
            {
                friends = new List<Friend>();
                friends.Add(_friend);
                ID = _friend.ID;
                isGroup = false;
                //serverID = "";
                memNum = 1;
                listening = false;
                unRead = 0;
                Datas = new List<chatData>();
                Name = _friend.Name;
                if (_friend.online) state = CHATSTATE.ONLINE;
                else state = CHATSTATE.OFFLINE;
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
                    for (int i = 1; i < friends.Count; i++)
                    {
                        mem += "," + friends[i].Name;
                        ids += friends[i].ID;
                        if (!friends[i].online) allon = false;
                    }
                    str[0] = Name;
                    str[1] = mem;
                    str[2] = ids;
                    tag = "G";
                }
                else
                {
                    str[0] = friends[0].Name;
                    str[1] = friends[0].ID;
                    str[2] = friends[0].ID;
                    tag = "S";
                }
                item = new ListViewItem(str);
                item.Tag = tag;
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
                            bkcl = Color.Gold;
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

            //在此可以设置黑名单屏蔽
            /*
            public void recvNewCON(string msg, Socket client)
            {
                bool isSingle = msg[13] == '0';
                string remoteID = msg.Substring(3, 10);
                if (state>CHATSTATE.ONLINE)
                {
                    //已经连上又发一遍？干啥呢？ 修改群名？
                    if (!isSingle)
                    {
                        string newname = msg.Substring(16 + ID.Length);
                        Name = newname;
                    }
                    //TODO ACK确认一下
                    return;
                }
                else
                {
                    if (isSingle)
                    {
                        bool isFrd = false;
                        for (int i = 0; i < Frds.Count; i++)
                        {
                            if (Frds[i].ID == ID)
                            {
                                isFrd = true;
                                //顺便更新IP？
                                Frds[i].online = true;
                                Frds[i].link = client;
                            }
                        }
                        if (!isFrd)
                        {//新朋友
                            DialogResult rs = MessageBox.Show(ID + " 向您发起会话"
                                , "会话请求", MessageBoxButtons.YesNoCancel
                                 , MessageBoxIcon.Question);
                            if (rs == DialogResult.OK)
                            {
                                //增加好友并开启对话
                                Friend newfrd = new Friend("", true, ID, ID, client);
                                Frds.Add(newfrd);
                                //显示对话
                                Chat newchat = new Chat(newfrd);
                listViewUpdate();
                            }
                            else if (rs == DialogResult.Cancel)
                            {
                                //中断本次对话 不回应
                                return;
                            }
                            else
                            {
                                //拒绝聊天——加入黑名单
                                return;
                            }
                        }
                        //TODO 不知道这里的内容有没有改
                        string debug = "Something";
                        if (friends[0].link == null) debug = "null";
                        Console.WriteLine("Now friends[0].link is"+debug);

                        friends[0].link = client;
                    }
                    else //群聊
                    {
                        string Gname = msg.Substring(16 + ID.Length);
                        DialogResult rs = MessageBox.Show(
                            remoteID + "邀请您加入群聊：" + Gname, "会话请求"
                            , MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (rs == DialogResult.OK)
                        {
                            //建立会话
                            //对面是主服务器
                            state = CHATSTATE.ONCHAT;
                            //TODO,将其他ONCHAT关掉
                            serverID = remoteID;

                            //将群聊成员加入Friend表内，但没有新的会话显示
                            int l = ID.Length / 10;
                            for (int j = 0; j < l; j++)
                            {
                                string idname = ID.Substring(j * 10, 10);
                                if (idname == userID) continue;
                                bool isnew = true;
                                //新的好友？
                                for (int i = 0; i < Frds.Count; i++)
                                {
                                    if (idname == Frds[i].ID)
                                    {
                                        isnew = false;
                                        string ip = friendsQuery(idname);
                                        if (!isIP(ip))
                                        {
                                            Console.WriteLine("发生错误 " + idname + " 不在线");
                                            return;
                                        }
                                        Frds[i].IP = ip;
                                        Frds[i].online = true;
                                        //建立socket
                                        //TODO

                                        break;
                                    }
                                }
                                if (isnew)
                                {
                                    Friend newfrd = new Friend("", true, idname, idname, null);
                                    Frds.Add(newfrd);
                                }
                            }
                        }
                        else
                        {//拒绝加入
                            return;
                        }
                    }
                    //TODO 回复ACK 发送方成功发送/而不是被拒收
                }
                Socket[] links = null;
                for (int i = 0; i < memNum; i++)
                {
                    links[i] = friends[i].link;
                }
                state = CHATSTATE.LINK;
                //TODO
                //新开一个线程收听
                listening = true;
                AsynRecive(links);
            }*/

        }

        //好友类
        class Friend
        {
            public string IP { get; set; }
            public DateTime IP_udtime { get; set; }
            public static int freshTime = 300;
            public bool online { get; set; }

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
                if (link != null)
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
            public string msg;

            public const string CON = "早早早";
            public const string FIN = "晚晚晚";
            public const string ACK = "好的哥";
            public const string MSG = "嘀嘀嘀";
            public const string SHK = "来摇摆";
        }
        
    }
}
