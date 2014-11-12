namespace GraphicsEditor
{
    partial class MainWindow
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.открытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.buttonPointer = new System.Windows.Forms.ToolStripButton();
            this.buttonCurve = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.labelCMD = new System.Windows.Forms.ToolStripLabel();
            this.textCMD = new System.Windows.Forms.ToolStripTextBox();
            this.buttonRun = new System.Windows.Forms.ToolStripButton();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.labelX = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelY = new System.Windows.Forms.ToolStripStatusLabel();
            this.drawArea = new GraphicsEditor.DrawArea();
            this.menuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(935, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.открытьToolStripMenuItem,
            this.сохранитьToolStripMenuItem,
            this.toolStripMenuItem2,
            this.выходToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // открытьToolStripMenuItem
            // 
            this.открытьToolStripMenuItem.Name = "открытьToolStripMenuItem";
            this.открытьToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.открытьToolStripMenuItem.Text = "Открыть";
            this.открытьToolStripMenuItem.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.сохранитьToolStripMenuItem.Text = "Сохранить";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(149, 6);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buttonPointer,
            this.buttonCurve,
            this.toolStripSeparator1,
            this.labelCMD,
            this.textCMD,
            this.buttonRun});
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(935, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip1";
            // 
            // buttonPointer
            // 
            this.buttonPointer.Checked = true;
            this.buttonPointer.CheckOnClick = true;
            this.buttonPointer.CheckState = System.Windows.Forms.CheckState.Checked;
            this.buttonPointer.Image = ((System.Drawing.Image)(resources.GetObject("buttonPointer.Image")));
            this.buttonPointer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonPointer.Name = "buttonPointer";
            this.buttonPointer.Size = new System.Drawing.Size(81, 22);
            this.buttonPointer.Text = "Указатель";
            this.buttonPointer.Click += new System.EventHandler(this.buttonPointer_Click);
            // 
            // buttonCurve
            // 
            this.buttonCurve.CheckOnClick = true;
            this.buttonCurve.Image = ((System.Drawing.Image)(resources.GetObject("buttonCurve.Image")));
            this.buttonCurve.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonCurve.Name = "buttonCurve";
            this.buttonCurve.Size = new System.Drawing.Size(99, 22);
            this.buttonCurve.Text = "Кривая Безье";
            this.buttonCurve.Click += new System.EventHandler(this.buttonCurve_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // labelCMD
            // 
            this.labelCMD.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.labelCMD.Name = "labelCMD";
            this.labelCMD.Size = new System.Drawing.Size(111, 22);
            this.labelCMD.Text = "Командная строка:";
            // 
            // textCMD
            // 
            this.textCMD.BackColor = System.Drawing.SystemColors.Window;
            this.textCMD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textCMD.Name = "textCMD";
            this.textCMD.Size = new System.Drawing.Size(200, 25);
            this.textCMD.ToolTipText = "Командная строка";
            this.textCMD.Click += new System.EventHandler(this.textCMD_Click);
            // 
            // buttonRun
            // 
            this.buttonRun.Image = ((System.Drawing.Image)(resources.GetObject("buttonRun.Image")));
            this.buttonRun.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(89, 22);
            this.buttonRun.Text = "Выполнить";
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelX,
            this.labelY});
            this.statusStrip.Location = new System.Drawing.Point(0, 607);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(935, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // labelX
            // 
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(39, 17);
            this.labelX.Text = "labelX";
            // 
            // labelY
            // 
            this.labelY.Name = "labelY";
            this.labelY.Size = new System.Drawing.Size(39, 17);
            this.labelY.Text = "labelY";
            // 
            // drawArea
            // 
            this.drawArea.ActiveTool = GraphicsEditor.DrawArea.ToolType.Pointer;
            this.drawArea.BackColor = System.Drawing.Color.White;
            this.drawArea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.drawArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drawArea.Figures = null;
            this.drawArea.Location = new System.Drawing.Point(0, 49);
            this.drawArea.Name = "drawArea";
            this.drawArea.Owner = null;
            this.drawArea.Points = null;
            this.drawArea.Size = new System.Drawing.Size(935, 558);
            this.drawArea.TabIndex = 3;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 629);
            this.Controls.Add(this.drawArea);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Графический редактор";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem открытьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton buttonCurve;
        private DrawArea drawArea;
        private System.Windows.Forms.ToolStripButton buttonPointer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripTextBox textCMD;
        private System.Windows.Forms.ToolStripLabel labelCMD;
        private System.Windows.Forms.ToolStripButton buttonRun;
        public System.Windows.Forms.ToolStripStatusLabel labelX;
        public System.Windows.Forms.ToolStripStatusLabel labelY;



    }
}

