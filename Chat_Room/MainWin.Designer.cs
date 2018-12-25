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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("我");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("我的好友", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("李姜帆");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("星标", new System.Windows.Forms.TreeNode[] {
            treeNode3});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWin));
            this.panel1 = new System.Windows.Forms.Panel();
            this.treeView_Frds = new System.Windows.Forms.TreeView();
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
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.treeView_Frds);
            this.panel1.Controls.Add(this.button_find);
            this.panel1.Controls.Add(this.textBox_find);
            this.panel1.Location = new System.Drawing.Point(13, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(385, 904);
            this.panel1.TabIndex = 0;
            // 
            // treeView_Frds
            // 
            this.treeView_Frds.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeView_Frds.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.treeView_Frds.LabelEdit = true;
            this.treeView_Frds.LineColor = System.Drawing.Color.DarkCyan;
            this.treeView_Frds.Location = new System.Drawing.Point(3, 4);
            this.treeView_Frds.Name = "treeView_Frds";
            treeNode1.Name = "self";
            treeNode1.Text = "我";
            treeNode2.Name = "Root";
            treeNode2.NodeFont = new System.Drawing.Font("方正宋刻本秀楷简体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            treeNode2.Text = "我的好友";
            treeNode3.Name = "2016011819";
            treeNode3.Text = "李姜帆";
            treeNode4.Name = "star";
            treeNode4.NodeFont = new System.Drawing.Font("方正宋刻本秀楷简体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            treeNode4.Text = "星标";
            this.treeView_Frds.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode4});
            this.treeView_Frds.ShowLines = false;
            this.treeView_Frds.Size = new System.Drawing.Size(379, 849);
            this.treeView_Frds.TabIndex = 5;
            // 
            // button_find
            // 
            this.button_find.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.button_find.FlatAppearance.BorderSize = 0;
            this.button_find.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_find.Font = new System.Drawing.Font("方正清刻本悦宋简体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button_find.Location = new System.Drawing.Point(289, 857);
            this.button_find.Name = "button_find";
            this.button_find.Size = new System.Drawing.Size(93, 43);
            this.button_find.TabIndex = 4;
            this.button_find.Text = "查找";
            this.button_find.UseVisualStyleBackColor = false;
            // 
            // textBox_find
            // 
            this.textBox_find.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_find.Font = new System.Drawing.Font("微软雅黑", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox_find.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.textBox_find.Location = new System.Drawing.Point(3, 859);
            this.textBox_find.Name = "textBox_find";
            this.textBox_find.Size = new System.Drawing.Size(280, 36);
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
            this.panel2.Location = new System.Drawing.Point(405, 13);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1057, 788);
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
            this.richTextBox_output.Size = new System.Drawing.Size(1051, 730);
            this.richTextBox_output.TabIndex = 4;
            this.richTextBox_output.Text = "";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.richTextBox_Input);
            this.panel3.Controls.Add(this.button3);
            this.panel3.Controls.Add(this.button_img);
            this.panel3.Controls.Add(this.button_send);
            this.panel3.Location = new System.Drawing.Point(405, 807);
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
            // 
            // MainWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1474, 929);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWin";
            this.Text = "网上聊天室";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView treeView_Frds;
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
    }
}