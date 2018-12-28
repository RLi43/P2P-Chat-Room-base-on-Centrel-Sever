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
            this.button_find = new System.Windows.Forms.Button();
            this.textBox_find = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button_detail = new System.Windows.Forms.Button();
            this.label_RoomName = new System.Windows.Forms.Label();
            this.richTextBox_output = new System.Windows.Forms.RichTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.richTextBox_Input = new System.Windows.Forms.RichTextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button_img = new System.Windows.Forms.Button();
            this.button_send = new System.Windows.Forms.Button();
            this.listView_Frds = new System.Windows.Forms.ListView();
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.在线 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.昵称 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listView_Frds);
            this.panel1.Controls.Add(this.button_find);
            this.panel1.Controls.Add(this.textBox_find);
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(689, 1104);
            this.panel1.TabIndex = 0;
            // 
            // button_find
            // 
            this.button_find.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.button_find.FlatAppearance.BorderSize = 0;
            this.button_find.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_find.Font = new System.Drawing.Font("方正清刻本悦宋简体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_find.Location = new System.Drawing.Point(593, 1052);
            this.button_find.Name = "button_find";
            this.button_find.Size = new System.Drawing.Size(93, 43);
            this.button_find.TabIndex = 4;
            this.button_find.Text = "查找";
            this.button_find.UseVisualStyleBackColor = false;
            this.button_find.Click += new System.EventHandler(this.button_find_Click);
            // 
            // textBox_find
            // 
            this.textBox_find.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_find.Font = new System.Drawing.Font("微软雅黑", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_find.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.textBox_find.Location = new System.Drawing.Point(3, 1059);
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
            this.panel2.Size = new System.Drawing.Size(1057, 984);
            this.panel2.TabIndex = 1;
            // 
            // button_detail
            // 
            this.button_detail.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.button_detail.FlatAppearance.BorderSize = 0;
            this.button_detail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_detail.Font = new System.Drawing.Font("方正清刻本悦宋简体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_detail.Location = new System.Drawing.Point(924, 4);
            this.button_detail.Name = "button_detail";
            this.button_detail.Size = new System.Drawing.Size(130, 48);
            this.button_detail.TabIndex = 4;
            this.button_detail.Text = "详细";
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
            this.richTextBox_output.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox_output.EnableAutoDragDrop = true;
            this.richTextBox_output.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBox_output.Location = new System.Drawing.Point(3, 55);
            this.richTextBox_output.Name = "richTextBox_output";
            this.richTextBox_output.ReadOnly = true;
            this.richTextBox_output.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox_output.Size = new System.Drawing.Size(1051, 926);
            this.richTextBox_output.TabIndex = 4;
            this.richTextBox_output.Text = "";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.richTextBox_Input);
            this.panel3.Controls.Add(this.button3);
            this.panel3.Controls.Add(this.button_img);
            this.panel3.Controls.Add(this.button_send);
            this.panel3.Location = new System.Drawing.Point(705, 1007);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1057, 110);
            this.panel3.TabIndex = 2;
            // 
            // richTextBox_Input
            // 
            this.richTextBox_Input.AcceptsTab = true;
            this.richTextBox_Input.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox_Input.EnableAutoDragDrop = true;
            this.richTextBox_Input.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBox_Input.Location = new System.Drawing.Point(3, 3);
            this.richTextBox_Input.Name = "richTextBox_Input";
            this.richTextBox_Input.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox_Input.Size = new System.Drawing.Size(918, 103);
            this.richTextBox_Input.TabIndex = 0;
            this.richTextBox_Input.Text = "";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.DarkCyan;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("方正清刻本悦宋简体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.Location = new System.Drawing.Point(1006, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(48, 48);
            this.button3.TabIndex = 3;
            this.button3.UseVisualStyleBackColor = false;
            // 
            // button_img
            // 
            this.button_img.BackColor = System.Drawing.Color.DarkCyan;
            this.button_img.FlatAppearance.BorderSize = 0;
            this.button_img.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_img.Font = new System.Drawing.Font("方正清刻本悦宋简体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_img.Location = new System.Drawing.Point(927, 4);
            this.button_img.Name = "button_img";
            this.button_img.Size = new System.Drawing.Size(48, 48);
            this.button_img.TabIndex = 2;
            this.button_img.UseVisualStyleBackColor = false;
            // 
            // button_send
            // 
            this.button_send.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.button_send.FlatAppearance.BorderSize = 0;
            this.button_send.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_send.Font = new System.Drawing.Font("方正清刻本悦宋简体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_send.Location = new System.Drawing.Point(927, 58);
            this.button_send.Name = "button_send";
            this.button_send.Size = new System.Drawing.Size(130, 48);
            this.button_send.TabIndex = 1;
            this.button_send.Text = "发送";
            this.button_send.UseVisualStyleBackColor = false;
            this.button_send.Click += new System.EventHandler(this.button_send_Click);
            // 
            // listView_Frds
            // 
            this.listView_Frds.AllowColumnReorder = true;
            this.listView_Frds.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.昵称,
            this.在线,
            this.ID,
            this.IP});
            this.listView_Frds.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listView_Frds.ForeColor = System.Drawing.Color.DarkGreen;
            this.listView_Frds.Location = new System.Drawing.Point(3, 4);
            this.listView_Frds.MultiSelect = false;
            this.listView_Frds.Name = "listView_Frds";
            this.listView_Frds.ShowItemToolTips = true;
            this.listView_Frds.Size = new System.Drawing.Size(683, 1042);
            this.listView_Frds.TabIndex = 7;
            this.listView_Frds.UseCompatibleStateImageBehavior = false;
            this.listView_Frds.View = System.Windows.Forms.View.Details;
            // 
            // ID
            // 
            this.ID.Text = "ID";
            this.ID.Width = 117;
            // 
            // IP
            // 
            this.IP.Text = "IP";
            this.IP.Width = 133;
            // 
            // 在线
            // 
            this.在线.Text = "在线状态";
            this.在线.Width = 97;
            // 
            // 昵称
            // 
            this.昵称.Text = "昵称";
            this.昵称.Width = 92;
            // 
            // MainWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(192F, 192F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1774, 1129);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWin";
            this.Text = "网上聊天室";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWin_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
    }
}