namespace CountryInfo
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aPIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restcountriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.htmlwebToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ipgeolocationapiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(284, 65);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Show info";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button_GetCountryByAPI);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 66);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(246, 22);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = "Ukraine";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(408, 65);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Save to DB";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_SaveToDB);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 124);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(345, 178);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(408, 124);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(345, 178);
            this.richTextBox2.TabIndex = 4;
            this.richTextBox2.Text = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aPIToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // aPIToolStripMenuItem
            // 
            this.aPIToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restcountriesToolStripMenuItem,
            this.htmlwebToolStripMenuItem,
            this.ipgeolocationapiToolStripMenuItem});
            this.aPIToolStripMenuItem.Name = "aPIToolStripMenuItem";
            this.aPIToolStripMenuItem.Size = new System.Drawing.Size(45, 24);
            this.aPIToolStripMenuItem.Text = "API";
            // 
            // restcountriesToolStripMenuItem
            // 
            this.restcountriesToolStripMenuItem.Checked = true;
            this.restcountriesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.restcountriesToolStripMenuItem.Name = "restcountriesToolStripMenuItem";
            this.restcountriesToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.restcountriesToolStripMenuItem.Text = "restcountries";
            this.restcountriesToolStripMenuItem.Click += new System.EventHandler(this.RestcountriesToolStripMenuItem_Click);
            // 
            // htmlwebToolStripMenuItem
            // 
            this.htmlwebToolStripMenuItem.Enabled = false;
            this.htmlwebToolStripMenuItem.Name = "htmlwebToolStripMenuItem";
            this.htmlwebToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.htmlwebToolStripMenuItem.Text = "htmlweb";
            this.htmlwebToolStripMenuItem.Click += new System.EventHandler(this.HtmlwebToolStripMenuItem_Click);
            // 
            // ipgeolocationapiToolStripMenuItem
            // 
            this.ipgeolocationapiToolStripMenuItem.Name = "ipgeolocationapiToolStripMenuItem";
            this.ipgeolocationapiToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.ipgeolocationapiToolStripMenuItem.Text = "ipgeolocationapi";
            this.ipgeolocationapiToolStripMenuItem.Click += new System.EventHandler(this.IpgeolocationapiToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Enter country title";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aPIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restcountriesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem htmlwebToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ipgeolocationapiToolStripMenuItem;
        private System.Windows.Forms.Label label1;
    }
}

