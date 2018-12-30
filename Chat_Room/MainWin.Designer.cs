namespace Chat_Room
{
    partial class MainWin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWin));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_chgName = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Friends = new System.Windows.Forms.TabPage();
            this.listView_Frds = new System.Windows.Forms.ListView();
            this.昵称 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.在线 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Group = new System.Windows.Forms.TabPage();
            this.listView1 = new System.Windows.Forms.ListView();
            this.Names = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Members = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button_delete = new System.Windows.Forms.Button();
            this.button_find = new System.Windows.Forms.Button();
            this.button_initGrp = new System.Windows.Forms.Button();
            this.textBox_find = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button_detail = new System.Windows.Forms.Button();
            this.label_RoomName = new System.Windows.Forms.Label();
            this.richTextBox_output = new System.Windows.Forms.RichTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox_Input = new System.Windows.Forms.RichTextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button_img = new System.Windows.Forms.Button();
            this.button_send = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.Friends.SuspendLayout();
            this.Group.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_chgName);
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.button_delete);
            this.panel1.Controls.Add(this.button_find);
            this.panel1.Controls.Add(this.button_initGrp);
            this.panel1.Controls.Add(this.textBox_find);
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(689, 1104);
            this.panel1.TabIndex = 0;
            // 
            // button_chgName
            // 
            this.button_chgName.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.button_chgName.FlatAppearance.BorderSize = 0;
            this.button_chgName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_chgName.Font = new System.Drawing.Font("方正清刻本悦宋简体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_chgName.Location = new System.Drawing.Point(258, 1052);
            this.button_chgName.Name = "button_chgName";
            this.button_chgName.Size = new System.Drawing.Size(153, 43);
            this.button_chgName.TabIndex = 11;
            this.button_chgName.Text = "修改昵称";
            this.button_chgName.UseVisualStyleBackColor = false;
            this.button_chgName.Click += new System.EventHandler(this.button_chgName_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Friends);
            this.tabControl1.Controls.Add(this.Group);
            this.tabControl1.Location = new System.Drawing.Point(4, 51);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(682, 995);
            this.tabControl1.TabIndex = 10;
            // 
            // Friends
            // 
            this.Friends.Controls.Add(this.listView_Frds);
            this.Friends.Location = new System.Drawing.Point(8, 39);
            this.Friends.Name = "Friends";
            this.Friends.Padding = new System.Windows.Forms.Padding(3);
            this.Friends.Size = new System.Drawing.Size(666, 948);
            this.Friends.TabIndex = 0;
            this.Friends.Text = "好友列表";
            this.Friends.UseVisualStyleBackColor = true;
            // 
            // listView_Frds
            // 
            this.listView_Frds.AllowColumnReorder = true;
            this.listView_Frds.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("listView_Frds.BackgroundImage")));
            this.listView_Frds.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView_Frds.CheckBoxes = true;
            this.listView_Frds.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.昵称,
            this.在线,
            this.ID,
            this.IP});
            this.listView_Frds.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listView_Frds.ForeColor = System.Drawing.Color.DarkGreen;
            this.listView_Frds.Location = new System.Drawing.Point(6, 6);
            this.listView_Frds.Name = "listView_Frds";
            this.listView_Frds.ShowItemToolTips = true;
            this.listView_Frds.Size = new System.Drawing.Size(654, 936);
            this.listView_Frds.TabIndex = 7;
            this.listView_Frds.UseCompatibleStateImageBehavior = false;
            this.listView_Frds.View = System.Windows.Forms.View.Details;
            this.listView_Frds.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView_Frds_MouseDoubleClick);
            // 
            // 昵称
            // 
            this.昵称.Text = "昵称";
            this.昵称.Width = 100;
            // 
            // 在线
            // 
            this.在线.Text = "在线";
            this.在线.Width = 50;
            // 
            // ID
            // 
            this.ID.Text = "ID";
            this.ID.Width = 100;
            // 
            // IP
            // 
            this.IP.Text = "IP";
            this.IP.Width = 120;
            // 
            // Group
            // 
            this.Group.Controls.Add(this.listView1);
            this.Group.Location = new System.Drawing.Point(8, 39);
            this.Group.Name = "Group";
            this.Group.Padding = new System.Windows.Forms.Padding(3);
            this.Group.Size = new System.Drawing.Size(666, 948);
            this.Group.TabIndex = 1;
            this.Group.Text = "群聊";
            this.Group.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("listView1.BackgroundImage")));
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Names,
            this.Members});
            this.listView1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listView1.ForeColor = System.Drawing.Color.SeaGreen;
            this.listView1.Location = new System.Drawing.Point(7, 4);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(653, 938);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // Names
            // 
            this.Names.Text = "群聊名称";
            this.Names.Width = 132;
            // 
            // Members
            // 
            this.Members.Text = "成员";
            this.Members.Width = 513;
            // 
            // button_delete
            // 
            this.button_delete.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.button_delete.FlatAppearance.BorderSize = 0;
            this.button_delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_delete.Font = new System.Drawing.Font("方正清刻本悦宋简体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_delete.Location = new System.Drawing.Point(519, 1052);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new System.Drawing.Size(153, 43);
            this.button_delete.TabIndex = 9;
            this.button_delete.Text = "删除好友";
            this.button_delete.UseVisualStyleBackColor = false;
            this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
            // 
            // button_find
            // 
            this.button_find.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.button_find.FlatAppearance.BorderSize = 0;
            this.button_find.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_find.Font = new System.Drawing.Font("方正清刻本悦宋简体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_find.Location = new System.Drawing.Point(593, 6);
            this.button_find.Name = "button_find";
            this.button_find.Size = new System.Drawing.Size(93, 43);
            this.button_find.TabIndex = 4;
            this.button_find.Text = "查找";
            this.button_find.UseVisualStyleBackColor = false;
            this.button_find.Click += new System.EventHandler(this.button_find_Click);
            // 
            // button_initGrp
            // 
            this.button_initGrp.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.button_initGrp.FlatAppearance.BorderSize = 0;
            this.button_initGrp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_initGrp.Font = new System.Drawing.Font("方正清刻本悦宋简体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_initGrp.Location = new System.Drawing.Point(18, 1052);
            this.button_initGrp.Name = "button_initGrp";
            this.button_initGrp.Size = new System.Drawing.Size(153, 43);
            this.button_initGrp.TabIndex = 8;
            this.button_initGrp.Text = "发起群聊";
            this.button_initGrp.UseVisualStyleBackColor = false;
            this.button_initGrp.Click += new System.EventHandler(this.button_initGrp_Click);
            // 
            // textBox_find
            // 
            this.textBox_find.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_find.Font = new System.Drawing.Font("微软雅黑", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_find.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.textBox_find.Location = new System.Drawing.Point(3, 8);
            this.textBox_find.Name = "textBox_find";
            this.textBox_find.Size = new System.Drawing.Size(584, 36);
            this.textBox_find.TabIndex = 0;
            this.textBox_find.Text = "学号/群号";
            this.textBox_find.Click += new System.EventHandler(this.textBox_find_Click);
            this.textBox_find.Leave += new System.EventHandler(this.textBox_find_Leave);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button_detail);
            this.panel2.Controls.Add(this.label_RoomName);
            this.panel2.Controls.Add(this.richTextBox_output);
            this.panel2.Location = new System.Drawing.Point(708, 17);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1057, 879);
            this.panel2.TabIndex = 1;
            // 
            // button_detail
            // 
            this.button_detail.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.button_detail.FlatAppearance.BorderSize = 0;
            this.button_detail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_detail.Font = new System.Drawing.Font("方正清刻本悦宋简体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_detail.Location = new System.Drawing.Point(1000, 4);
            this.button_detail.Name = "button_detail";
            this.button_detail.Size = new System.Drawing.Size(54, 48);
            this.button_detail.TabIndex = 4;
            this.button_detail.Text = "...";
            this.button_detail.UseVisualStyleBackColor = false;
            // 
            // label_RoomName
            // 
            this.label_RoomName.AutoSize = true;
            this.label_RoomName.Font = new System.Drawing.Font("微软雅黑", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_RoomName.Location = new System.Drawing.Point(3, 4);
            this.label_RoomName.Name = "label_RoomName";
            this.label_RoomName.Size = new System.Drawing.Size(131, 48);
            this.label_RoomName.TabIndex = 5;
            this.label_RoomName.Text = "聊天室";
            // 
            // richTextBox_output
            // 
            this.richTextBox_output.AcceptsTab = true;
            this.richTextBox_output.BackColor = System.Drawing.SystemColors.Window;
            this.richTextBox_output.EnableAutoDragDrop = true;
            this.richTextBox_output.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBox_output.Location = new System.Drawing.Point(3, 55);
            this.richTextBox_output.Name = "richTextBox_output";
            this.richTextBox_output.ReadOnly = true;
            this.richTextBox_output.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox_output.Size = new System.Drawing.Size(1051, 821);
            this.richTextBox_output.TabIndex = 4;
            this.richTextBox_output.Text = "";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.Window;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.progressBar1);
            this.panel3.Controls.Add(this.button2);
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.richTextBox_Input);
            this.panel3.Controls.Add(this.button3);
            this.panel3.Controls.Add(this.button_img);
            this.panel3.Controls.Add(this.button_send);
            this.panel3.Location = new System.Drawing.Point(705, 899);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1057, 218);
            this.panel3.TabIndex = 2;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(6, 186);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(210, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 6;
            this.progressBar1.Visible = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.DarkCyan;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("方正清刻本悦宋简体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(168, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(48, 48);
            this.button2.TabIndex = 5;
            this.button2.Text = "💬";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DarkCyan;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("方正清刻本悦宋简体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(114, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(48, 48);
            this.button1.TabIndex = 4;
            this.button1.Text = "🖐";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // richTextBox_Input
            // 
            this.richTextBox_Input.AcceptsTab = true;
            this.richTextBox_Input.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox_Input.EnableAutoDragDrop = true;
            this.richTextBox_Input.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBox_Input.Location = new System.Drawing.Point(0, 58);
            this.richTextBox_Input.Name = "richTextBox_Input";
            this.richTextBox_Input.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox_Input.Size = new System.Drawing.Size(1048, 98);
            this.richTextBox_Input.TabIndex = 0;
            this.richTextBox_Input.Text = "";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.DarkCyan;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("方正清刻本悦宋简体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.Location = new System.Drawing.Point(6, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(48, 48);
            this.button3.TabIndex = 3;
            this.button3.Text = "🙂";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button_img
            // 
            this.button_img.BackColor = System.Drawing.Color.DarkCyan;
            this.button_img.FlatAppearance.BorderSize = 0;
            this.button_img.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_img.Font = new System.Drawing.Font("方正清刻本悦宋简体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_img.Location = new System.Drawing.Point(60, 4);
            this.button_img.Name = "button_img";
            this.button_img.Size = new System.Drawing.Size(48, 48);
            this.button_img.TabIndex = 2;
            this.button_img.Text = "📁";
            this.button_img.UseVisualStyleBackColor = false;
            // 
            // button_send
            // 
            this.button_send.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.button_send.FlatAppearance.BorderSize = 0;
            this.button_send.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_send.Font = new System.Drawing.Font("方正清刻本悦宋简体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_send.Location = new System.Drawing.Point(924, 161);
            this.button_send.Name = "button_send";
            this.button_send.Size = new System.Drawing.Size(130, 48);
            this.button_send.TabIndex = 1;
            this.button_send.Text = "发送";
            this.button_send.UseVisualStyleBackColor = false;
            this.button_send.Click += new System.EventHandler(this.button_send_Click);
            // 
            // MainWin
            // 
            this.AcceptButton = this.button_find;
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1774, 1129);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWin";
            this.Text = "网上聊天室";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWin_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.Friends.ResumeLayout(false);
            this.Group.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_find;
        private System.Windows.Forms.TextBox textBox_find;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label_RoomName;
        private System.Windows.Forms.RichTextBox richTextBox_output;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RichTextBox richTextBox_Input;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button_img;
        private System.Windows.Forms.Button button_send;
        private System.Windows.Forms.Button button_detail;
        private System.Windows.Forms.ListView listView_Frds;
        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.ColumnHeader IP;
        private System.Windows.Forms.ColumnHeader 在线;
        private System.Windows.Forms.ColumnHeader 昵称;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Button button_initGrp;
        private System.Windows.Forms.Button button_chgName;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Friends;
        private System.Windows.Forms.TabPage Group;
        private System.Windows.Forms.ColumnHeader Names;
        private System.Windows.Forms.ColumnHeader Members;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar progressBar1;
        public System.Windows.Forms.ListView listView1;
    }
}