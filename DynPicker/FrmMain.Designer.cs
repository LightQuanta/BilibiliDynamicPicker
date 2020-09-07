namespace DynPicker
{
    partial class FrmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lstUsers = new System.Windows.Forms.ListBox();
            this.lnkShow = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGet = new System.Windows.Forms.Button();
            this.txtB = new System.Windows.Forms.TextBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstUsers
            // 
            this.lstUsers.DisplayMember = "Key";
            this.lstUsers.Font = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lstUsers.FormattingEnabled = true;
            this.lstUsers.ItemHeight = 20;
            this.lstUsers.Location = new System.Drawing.Point(12, 46);
            this.lstUsers.Name = "lstUsers";
            this.lstUsers.Size = new System.Drawing.Size(201, 224);
            this.lstUsers.TabIndex = 1;
            this.lstUsers.ValueMember = "Key";
            // 
            // lnkShow
            // 
            this.lnkShow.AutoSize = true;
            this.lnkShow.Location = new System.Drawing.Point(12, 9);
            this.lnkShow.Name = "lnkShow";
            this.lnkShow.Size = new System.Drawing.Size(365, 17);
            this.lnkShow.TabIndex = 2;
            this.lnkShow.TabStop = true;
            this.lnkShow.Text = "项目地址 https://github.com/LightQuanta/BilibiliDynamicPicker";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "转发+评论用户数量:";
            // 
            // btnGet
            // 
            this.btnGet.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnGet.Location = new System.Drawing.Point(12, 276);
            this.btnGet.Name = "btnGet";
            this.btnGet.Size = new System.Drawing.Size(201, 162);
            this.btnGet.TabIndex = 4;
            this.btnGet.Text = "获取转发+评论用户";
            this.btnGet.UseVisualStyleBackColor = true;
            this.btnGet.Click += new System.EventHandler(this.btnGet_Click);
            this.btnGet.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnGet_MouseDown);
            // 
            // txtB
            // 
            this.txtB.BackColor = System.Drawing.SystemColors.HighlightText;
            this.txtB.Font = new System.Drawing.Font("Microsoft YaHei UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtB.Location = new System.Drawing.Point(219, 218);
            this.txtB.Multiline = true;
            this.txtB.Name = "txtB";
            this.txtB.ReadOnly = true;
            this.txtB.Size = new System.Drawing.Size(569, 220);
            this.txtB.TabIndex = 5;
            this.txtB.Text = "ん？";
            this.txtB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnSelect
            // 
            this.btnSelect.Enabled = false;
            this.btnSelect.Font = new System.Drawing.Font("Microsoft YaHei UI", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnSelect.Location = new System.Drawing.Point(219, 46);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(569, 166);
            this.btnSelect.TabIndex = 6;
            this.btnSelect.Text = "   抽！";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.txtB);
            this.Controls.Add(this.btnGet);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lnkShow);
            this.Controls.Add(this.lstUsers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.Text = "字幕组万粉动态抽奖程序 v1.0 by Light_Quanta";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox lstUsers;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGet;
        private System.Windows.Forms.LinkLabel lnkShow;
        private System.Windows.Forms.TextBox txtB;
        private System.Windows.Forms.Button btnSelect;
    }
}

