﻿namespace Chat_Room
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWin));
            this.panel1 = new System.Windows.Forms.Panel();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button_chgName = new System.Windows.Forms.Button();
            this.button_delete = new System.Windows.Forms.Button();
            this.button_find = new System.Windows.Forms.Button();
            this.button_initGrp = new System.Windows.Forms.Button();
            this.textBox_find = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button_next = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button_send = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.richTextBox_Input = new System.Windows.Forms.RichTextBox();
            this.button_data = new System.Windows.Forms.Button();
            this.button_detail = new System.Windows.Forms.Button();
            this.buttonShake = new System.Windows.Forms.Button();
            this.label_RoomName = new System.Windows.Forms.Label();
            this.richTextBox_output = new System.Windows.Forms.RichTextBox();
            this.button_file = new System.Windows.Forms.Button();
            this.button_Face = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.listView1);
            this.panel1.Controls.Add(this.button_chgName);
            this.panel1.Controls.Add(this.button_delete);
            this.panel1.Controls.Add(this.button_find);
            this.panel1.Controls.Add(this.button_initGrp);
            this.panel1.Controls.Add(this.textBox_find);
            this.panel1.Location = new System.Drawing.Point(22, 12);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 900);
            this.panel1.TabIndex = 0;
            // 
            // listView1
            // 
            this.listView1.CheckBoxes = true;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listView1.ForeColor = System.Drawing.Color.DarkGreen;
            this.listView1.LabelEdit = true;
            this.listView1.LabelWrap = false;
            this.listView1.Location = new System.Drawing.Point(12, 56);
            this.listView1.Margin = new System.Windows.Forms.Padding(6);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(472, 768);
            this.listView1.TabIndex = 12;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "名字";
            this.columnHeader1.Width = 85;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "信息";
            this.columnHeader2.Width = 146;
            // 
            // button_chgName
            // 
            this.button_chgName.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.button_chgName.FlatAppearance.BorderSize = 0;
            this.button_chgName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_chgName.Font = new System.Drawing.Font("方正清刻本悦宋简体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_chgName.Location = new System.Drawing.Point(172, 840);
            this.button_chgName.Margin = new System.Windows.Forms.Padding(4);
            this.button_chgName.Name = "button_chgName";
            this.button_chgName.Size = new System.Drawing.Size(152, 44);
            this.button_chgName.TabIndex = 11;
            this.button_chgName.Text = "修改昵称";
            this.button_chgName.UseVisualStyleBackColor = false;
            this.button_chgName.Click += new System.EventHandler(this.button_chgName_Click);
            // 
            // button_delete
            // 
            this.button_delete.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.button_delete.FlatAppearance.BorderSize = 0;
            this.button_delete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_delete.Font = new System.Drawing.Font("方正清刻本悦宋简体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_delete.Location = new System.Drawing.Point(332, 840);
            this.button_delete.Margin = new System.Windows.Forms.Padding(4);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new System.Drawing.Size(152, 44);
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
            this.button_find.Location = new System.Drawing.Point(392, 8);
            this.button_find.Margin = new System.Windows.Forms.Padding(4);
            this.button_find.Name = "button_find";
            this.button_find.Size = new System.Drawing.Size(92, 44);
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
            this.button_initGrp.Location = new System.Drawing.Point(12, 840);
            this.button_initGrp.Margin = new System.Windows.Forms.Padding(4);
            this.button_initGrp.Name = "button_initGrp";
            this.button_initGrp.Size = new System.Drawing.Size(152, 44);
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
            this.textBox_find.Location = new System.Drawing.Point(12, 12);
            this.textBox_find.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_find.Name = "textBox_find";
            this.textBox_find.Size = new System.Drawing.Size(372, 36);
            this.textBox_find.TabIndex = 0;
            this.textBox_find.Text = "查找学号";
            this.textBox_find.Click += new System.EventHandler(this.textBox_find_Click);
            this.textBox_find.Enter += new System.EventHandler(this.textBox_find_Enter);
            this.textBox_find.Leave += new System.EventHandler(this.textBox_find_Leave);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button_next);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.button_send);
            this.panel2.Controls.Add(this.progressBar1);
            this.panel2.Controls.Add(this.richTextBox_Input);
            this.panel2.Controls.Add(this.button_data);
            this.panel2.Controls.Add(this.button_detail);
            this.panel2.Controls.Add(this.buttonShake);
            this.panel2.Controls.Add(this.label_RoomName);
            this.panel2.Controls.Add(this.richTextBox_output);
            this.panel2.Controls.Add(this.button_file);
            this.panel2.Controls.Add(this.button_Face);
            this.panel2.Location = new System.Drawing.Point(546, 12);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(900, 900);
            this.panel2.TabIndex = 1;
            this.panel2.Visible = false;
            // 
            // button_next
            // 
            this.button_next.BackColor = System.Drawing.Color.DarkCyan;
            this.button_next.FlatAppearance.BorderSize = 0;
            this.button_next.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_next.Font = new System.Drawing.Font("方正清刻本悦宋简体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_next.Location = new System.Drawing.Point(240, 642);
            this.button_next.Margin = new System.Windows.Forms.Padding(4);
            this.button_next.Name = "button_next";
            this.button_next.Size = new System.Drawing.Size(48, 48);
            this.button_next.TabIndex = 8;
            this.button_next.Text = "⏭";
            this.button_next.UseVisualStyleBackColor = false;
            this.button_next.Visible = false;
            this.button_next.Click += new System.EventHandler(this.button_next_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(272, 860);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 36);
            this.label1.TabIndex = 7;
            this.label1.Text = "发送中";
            this.label1.Visible = false;
            // 
            // button_send
            // 
            this.button_send.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.button_send.FlatAppearance.BorderSize = 0;
            this.button_send.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_send.Font = new System.Drawing.Font("方正清刻本悦宋简体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_send.Location = new System.Drawing.Point(766, 848);
            this.button_send.Margin = new System.Windows.Forms.Padding(4);
            this.button_send.Name = "button_send";
            this.button_send.Size = new System.Drawing.Size(130, 48);
            this.button_send.TabIndex = 1;
            this.button_send.Text = "发送";
            this.button_send.UseVisualStyleBackColor = false;
            this.button_send.Click += new System.EventHandler(this.button_send_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 852);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(240, 44);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 6;
            this.progressBar1.Visible = false;
            // 
            // richTextBox_Input
            // 
            this.richTextBox_Input.AcceptsTab = true;
            this.richTextBox_Input.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox_Input.EnableAutoDragDrop = true;
            this.richTextBox_Input.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.richTextBox_Input.Location = new System.Drawing.Point(14, 698);
            this.richTextBox_Input.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBox_Input.Name = "richTextBox_Input";
            this.richTextBox_Input.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox_Input.Size = new System.Drawing.Size(882, 142);
            this.richTextBox_Input.TabIndex = 0;
            this.richTextBox_Input.Text = "";
            this.richTextBox_Input.Enter += new System.EventHandler(this.richTextBox_Input_Enter);
            this.richTextBox_Input.Leave += new System.EventHandler(this.richTextBox_Input_Leave);
            // 
            // button_data
            // 
            this.button_data.BackColor = System.Drawing.Color.DarkCyan;
            this.button_data.FlatAppearance.BorderSize = 0;
            this.button_data.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_data.Font = new System.Drawing.Font("方正清刻本悦宋简体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_data.Location = new System.Drawing.Point(184, 642);
            this.button_data.Margin = new System.Windows.Forms.Padding(4);
            this.button_data.Name = "button_data";
            this.button_data.Size = new System.Drawing.Size(48, 48);
            this.button_data.TabIndex = 5;
            this.button_data.Text = "💬";
            this.button_data.UseVisualStyleBackColor = false;
            this.button_data.Click += new System.EventHandler(this.button_data_Click);
            // 
            // button_detail
            // 
            this.button_detail.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.button_detail.FlatAppearance.BorderSize = 0;
            this.button_detail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_detail.Font = new System.Drawing.Font("方正清刻本悦宋简体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_detail.Location = new System.Drawing.Point(838, 8);
            this.button_detail.Margin = new System.Windows.Forms.Padding(4);
            this.button_detail.Name = "button_detail";
            this.button_detail.Size = new System.Drawing.Size(54, 48);
            this.button_detail.TabIndex = 4;
            this.button_detail.Text = "...";
            this.button_detail.UseVisualStyleBackColor = false;
            this.button_detail.Click += new System.EventHandler(this.button_detail_Click);
            // 
            // buttonShake
            // 
            this.buttonShake.BackColor = System.Drawing.Color.DarkCyan;
            this.buttonShake.FlatAppearance.BorderSize = 0;
            this.buttonShake.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonShake.Font = new System.Drawing.Font("方正清刻本悦宋简体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonShake.Location = new System.Drawing.Point(128, 642);
            this.buttonShake.Margin = new System.Windows.Forms.Padding(4);
            this.buttonShake.Name = "buttonShake";
            this.buttonShake.Size = new System.Drawing.Size(48, 48);
            this.buttonShake.TabIndex = 4;
            this.buttonShake.Text = "🖐";
            this.buttonShake.UseVisualStyleBackColor = false;
            this.buttonShake.Click += new System.EventHandler(this.buttonShake_Click);
            // 
            // label_RoomName
            // 
            this.label_RoomName.AutoSize = true;
            this.label_RoomName.Font = new System.Drawing.Font("微软雅黑", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_RoomName.Location = new System.Drawing.Point(4, 4);
            this.label_RoomName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
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
            this.richTextBox_output.Location = new System.Drawing.Point(14, 56);
            this.richTextBox_output.Margin = new System.Windows.Forms.Padding(4);
            this.richTextBox_output.Name = "richTextBox_output";
            this.richTextBox_output.ReadOnly = true;
            this.richTextBox_output.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBox_output.Size = new System.Drawing.Size(878, 574);
            this.richTextBox_output.TabIndex = 4;
            this.richTextBox_output.Text = "";
            // 
            // button_file
            // 
            this.button_file.BackColor = System.Drawing.Color.DarkCyan;
            this.button_file.FlatAppearance.BorderSize = 0;
            this.button_file.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_file.Font = new System.Drawing.Font("方正清刻本悦宋简体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_file.Location = new System.Drawing.Point(72, 642);
            this.button_file.Margin = new System.Windows.Forms.Padding(4);
            this.button_file.Name = "button_file";
            this.button_file.Size = new System.Drawing.Size(48, 48);
            this.button_file.TabIndex = 2;
            this.button_file.Text = "📁";
            this.button_file.UseVisualStyleBackColor = false;
            this.button_file.Click += new System.EventHandler(this.button_file_Click);
            // 
            // button_Face
            // 
            this.button_Face.BackColor = System.Drawing.Color.DarkCyan;
            this.button_Face.FlatAppearance.BorderSize = 0;
            this.button_Face.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Face.Font = new System.Drawing.Font("方正清刻本悦宋简体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_Face.Location = new System.Drawing.Point(16, 642);
            this.button_Face.Margin = new System.Windows.Forms.Padding(4);
            this.button_Face.Name = "button_Face";
            this.button_Face.Size = new System.Drawing.Size(48, 48);
            this.button_Face.TabIndex = 3;
            this.button_Face.Text = "🙂";
            this.button_Face.UseVisualStyleBackColor = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // MainWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1468, 922);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainWin";
            this.Text = "网上聊天室";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWin_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_find;
        private System.Windows.Forms.TextBox textBox_find;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label_RoomName;
        private System.Windows.Forms.RichTextBox richTextBox_output;
        private System.Windows.Forms.RichTextBox richTextBox_Input;
        private System.Windows.Forms.Button button_Face;
        private System.Windows.Forms.Button button_file;
        private System.Windows.Forms.Button button_send;
        private System.Windows.Forms.Button button_detail;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Button button_initGrp;
        private System.Windows.Forms.Button button_chgName;
        private System.Windows.Forms.Button button_data;
        private System.Windows.Forms.Button buttonShake;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_next;
    }
}