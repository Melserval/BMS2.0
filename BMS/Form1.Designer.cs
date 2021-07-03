
namespace BMS
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton_start = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton_reset = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label_beeCount = new System.Windows.Forms.Label();
            this.label_flowerCount = new System.Windows.Forms.Label();
            this.label_honeyCount = new System.Windows.Forms.Label();
            this.label_nectarOnFieldCount = new System.Windows.Forms.Label();
            this.label_frameRun = new System.Windows.Forms.Label();
            this.label_frameRate = new System.Windows.Forms.Label();
            this.timer_frame = new System.Windows.Forms.Timer(this.components);
            this.textBox_serviceOutput = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton_start,
            this.toolStripSeparator1,
            this.toolStripButton_reset});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(329, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton_start
            // 
            this.toolStripButton_start.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_start.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_start.Image")));
            this.toolStripButton_start.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_start.Name = "toolStripButton_start";
            this.toolStripButton_start.Size = new System.Drawing.Size(66, 22);
            this.toolStripButton_start.Text = "Запустить";
            this.toolStripButton_start.Click += new System.EventHandler(this.toolStripButton_start_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton_reset
            // 
            this.toolStripButton_reset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton_reset.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_reset.Image")));
            this.toolStripButton_reset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_reset.Name = "toolStripButton_reset";
            this.toolStripButton_reset.Size = new System.Drawing.Size(64, 22);
            this.toolStripButton_reset.Text = "Сбросить";
            this.toolStripButton_reset.Click += new System.EventHandler(this.toolStripButton_reset_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 369);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(329, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(103, 17);
            this.toolStripStatusLabel1.Text = "что происходит...";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label_beeCount, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label_flowerCount, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label_honeyCount, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label_nectarOnFieldCount, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label_frameRun, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label_frameRate, 1, 5);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 30);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(310, 160);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 1);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(1, 5, 5, 5);
            this.label1.Size = new System.Drawing.Size(38, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Пчел";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 26);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(1, 5, 5, 5);
            this.label2.Size = new System.Drawing.Size(50, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Цветов";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 51);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(1, 5, 5, 5);
            this.label3.Size = new System.Drawing.Size(75, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Меда в улье";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1, 76);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(1, 5, 5, 5);
            this.label4.Size = new System.Drawing.Size(98, 23);
            this.label4.TabIndex = 3;
            this.label4.Text = "Нектара на поле";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1, 101);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(1, 5, 5, 5);
            this.label5.Size = new System.Drawing.Size(101, 23);
            this.label5.TabIndex = 4;
            this.label5.Text = "Кадры запущены";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1, 126);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Padding = new System.Windows.Forms.Padding(1, 5, 5, 5);
            this.label6.Size = new System.Drawing.Size(88, 23);
            this.label6.TabIndex = 10;
            this.label6.Text = "Кадры частота";
            // 
            // label_beeCount
            // 
            this.label_beeCount.AutoSize = true;
            this.label_beeCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_beeCount.Location = new System.Drawing.Point(158, 1);
            this.label_beeCount.Name = "label_beeCount";
            this.label_beeCount.Padding = new System.Windows.Forms.Padding(5);
            this.label_beeCount.Size = new System.Drawing.Size(51, 25);
            this.label_beeCount.TabIndex = 5;
            this.label_beeCount.Text = "label6";
            // 
            // label_flowerCount
            // 
            this.label_flowerCount.AutoSize = true;
            this.label_flowerCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_flowerCount.Location = new System.Drawing.Point(158, 26);
            this.label_flowerCount.Name = "label_flowerCount";
            this.label_flowerCount.Padding = new System.Windows.Forms.Padding(5);
            this.label_flowerCount.Size = new System.Drawing.Size(51, 25);
            this.label_flowerCount.TabIndex = 6;
            this.label_flowerCount.Text = "label7";
            // 
            // label_honeyCount
            // 
            this.label_honeyCount.AutoSize = true;
            this.label_honeyCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_honeyCount.Location = new System.Drawing.Point(158, 51);
            this.label_honeyCount.Name = "label_honeyCount";
            this.label_honeyCount.Padding = new System.Windows.Forms.Padding(5);
            this.label_honeyCount.Size = new System.Drawing.Size(51, 25);
            this.label_honeyCount.TabIndex = 7;
            this.label_honeyCount.Text = "label8";
            // 
            // label_nectarOnFieldCount
            // 
            this.label_nectarOnFieldCount.AutoSize = true;
            this.label_nectarOnFieldCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_nectarOnFieldCount.Location = new System.Drawing.Point(158, 76);
            this.label_nectarOnFieldCount.Name = "label_nectarOnFieldCount";
            this.label_nectarOnFieldCount.Padding = new System.Windows.Forms.Padding(5);
            this.label_nectarOnFieldCount.Size = new System.Drawing.Size(51, 25);
            this.label_nectarOnFieldCount.TabIndex = 8;
            this.label_nectarOnFieldCount.Text = "label9";
            // 
            // label_frameRun
            // 
            this.label_frameRun.AutoSize = true;
            this.label_frameRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_frameRun.Location = new System.Drawing.Point(158, 101);
            this.label_frameRun.Name = "label_frameRun";
            this.label_frameRun.Padding = new System.Windows.Forms.Padding(5);
            this.label_frameRun.Size = new System.Drawing.Size(58, 25);
            this.label_frameRun.TabIndex = 9;
            this.label_frameRun.Text = "label10";
            // 
            // label_frameRate
            // 
            this.label_frameRate.AutoSize = true;
            this.label_frameRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_frameRate.Location = new System.Drawing.Point(158, 126);
            this.label_frameRate.Name = "label_frameRate";
            this.label_frameRate.Padding = new System.Windows.Forms.Padding(5);
            this.label_frameRate.Size = new System.Drawing.Size(58, 25);
            this.label_frameRate.TabIndex = 11;
            this.label_frameRate.Text = "label12";
            // 
            // timer_frame
            // 
            this.timer_frame.Interval = 1000;
            this.timer_frame.Tick += new System.EventHandler(this.timer_frame_Tick);
            // 
            // textBox_serviceOutput
            // 
            this.textBox_serviceOutput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_serviceOutput.Cursor = System.Windows.Forms.Cursors.No;
            this.textBox_serviceOutput.Enabled = false;
            this.textBox_serviceOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_serviceOutput.Location = new System.Drawing.Point(8, 233);
            this.textBox_serviceOutput.Multiline = true;
            this.textBox_serviceOutput.Name = "textBox_serviceOutput";
            this.textBox_serviceOutput.ReadOnly = true;
            this.textBox_serviceOutput.Size = new System.Drawing.Size(310, 123);
            this.textBox_serviceOutput.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 391);
            this.Controls.Add(this.textBox_serviceOutput);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.Text = "Behave Management System 2.0";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton_start;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButton_reset;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label_beeCount;
        private System.Windows.Forms.Label label_flowerCount;
        private System.Windows.Forms.Label label_honeyCount;
        private System.Windows.Forms.Label label_nectarOnFieldCount;
        private System.Windows.Forms.Label label_frameRun;
        private System.Windows.Forms.Timer timer_frame;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label_frameRate;
        private System.Windows.Forms.TextBox textBox_serviceOutput;
    }
}

