using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;

namespace GraphicsEditor
{
    public partial class MainWindow : Form
    {
        //DrawArea drawArea;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            drawArea.Initialize(this);
            Application.Idle += new EventHandler(Application_Idle);
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            SetStateOfControls();
        }

        public void SetStateOfControls()
        {
            // Select active tool
            buttonPointer.Checked = (drawArea.ActiveTool == DrawArea.ToolType.Pointer);
            buttonCurve.Checked = (drawArea.ActiveTool == DrawArea.ToolType.Curve);

            /*
            menuDrawPointer.Checked = (drawArea.ActiveTool == DrawArea.DrawToolType.Pointer);
            menuDrawRectangle.Checked = (drawArea.ActiveTool == DrawArea.DrawToolType.Rectangle);
            menuDrawEllipse.Checked = (drawArea.ActiveTool == DrawArea.DrawToolType.Ellipse);
            menuDrawLine.Checked = (drawArea.ActiveTool == DrawArea.DrawToolType.Line);
            menuDrawPolygon.Checked = (drawArea.ActiveTool == DrawArea.DrawToolType.Polygon);\
            */

            bool objects = (drawArea.Figures.Count > 0);
            bool selectedObjects = (drawArea.Figures.SelectionCount > 0);

            // File operations
            /*
            menuFileSave.Enabled = objects;
            menuFileSaveAs.Enabled = objects;
            tbSave.Enabled = objects;
            */

            // Edit operations
            /*
            menuEditDelete.Enabled = selectedObjects;
            menuEditDeleteAll.Enabled = objects;
            menuEditSelectAll.Enabled = objects;
            menuEditUnselectAll.Enabled = objects;
            menuEditMoveToFront.Enabled = selectedObjects;
            menuEditMoveToBack.Enabled = selectedObjects;
            menuEditProperties.Enabled = selectedObjects;
            */
        }

        private void CommandPointer()
        {
            drawArea.ActiveTool = DrawArea.ToolType.Pointer;
        }

        private void CommandCurve()
        {
            drawArea.ActiveTool = DrawArea.ToolType.Curve;
        }

        private void buttonPointer_Click(object sender, EventArgs e)
        {
            CommandPointer();
        }

        private void buttonCurve_Click(object sender, EventArgs e)
        {
            CommandCurve();
        }

        private void textCMD_Click(object sender, EventArgs e)
        {

        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            string[] command = textCMD.Text.Split(' ');

            if (command.Length > 0)
            {
                // add
                if (command[0].ToLower().Contains("add"))
                {
                    if (command.Length == 9)
                    {
                        Point[] points = new Point[4];

                        for (int i = 0; i < 4; i++)
                        {
                            points[i].X = Convert.ToInt16(command[1 + 2 * i]);
                            points[i].Y = Convert.ToInt16(command[1 + 2 * i + 1]);
                        }

                        drawArea.Figures.Add(new FigureCurve(points[0], points[1], points[3], points[2]));

                        drawArea.Refresh();
                    }
                }
                // remove
                else if (command[0].ToLower().Contains("remove"))
                {
                    drawArea.Figures.Delete();

                    drawArea.Refresh();
                }
                // select
                else if (command[0].ToLower().Contains("select"))
                {
                    if (command.Length == 2)
                    {
                        int i = Convert.ToInt16(command[1]);
                        if (i >= 0 && i < drawArea.Figures.Count)
                        {
                            if (!drawArea.Figures[i].Selected)
                                drawArea.Figures[i].Selected = true;
                            else
                                drawArea.Figures[i].Selected = false;
                        }
                    }
                    drawArea.Refresh();
                }
                // move
                else if (command[0].ToLower().Contains("move"))
                {
                    if (command.Length == 3)
                    {
                        for (int i = 0; i < drawArea.Figures.SelectionCount; i++)
                        {
                            drawArea.Figures[i].Move(Convert.ToInt16(command[1]), Convert.ToInt16(command[2]));
                        }
                    }
                    drawArea.Refresh();
                }
                // scale
                else if (command[0].ToLower().Contains("scale"))
                {
                    if (command.Length == 3)
                    {
                        for (int i = 0; i < drawArea.Figures.SelectionCount; i++)
                        {
                            drawArea.Figures[i].Scale(Convert.ToDouble(command[1].Replace('.', ',')), Convert.ToDouble(command[2].Replace('.', ',')));
                        }
                    }
                    drawArea.Refresh();
                }
                // rotate
                else if (command[0].ToLower().Contains("rotate"))
                {
                    if (command.Length == 2)
                    {
                        for (int i = 0; i < drawArea.Figures.SelectionCount; i++)
                        {
                            drawArea.Figures[i].Rotate(Convert.ToDouble(command[1].Replace('.', ',')));
                        }
                    }
                    drawArea.Refresh();
                }
                // vertex_move
                else if (command[0].ToLower().Contains("vertmv"))
                {
                    if (command.Length == 4)
                    {
                        int h = Convert.ToInt16(command[1]);
                        for (int i = 0; i < drawArea.Figures.SelectionCount; i++)
                        {
                            if (h > 0 && h <= drawArea.Figures[i].HandleCount)
                            {
                                drawArea.Figures.GetSelectedFigure(i).MoveHandleTo(new Point(Convert.ToInt16(command[2]), Convert.ToInt16(command[3])), h);
                            }
                        }
                    }
                    drawArea.Refresh();
                }
                // help
                else if (command[0].ToLower().Contains("help"))
                {
                    System.Windows.Forms.MessageBox.Show("Добавить фигуру\nadd $p1.x $p1.y $cp1.x $cp1.y $cp2.x $cp2.y $p2.x $p2.y\n\nУдалить выделенные фигуры\nremove\n\nПереместить выделенные фигуры\nmove $dx $dy\n\nПереместить опорную точку выделенной фигуры\nvertmv $no $x $y\n\nМасштабировать выделенные фигуры\nscale $kx $ky\n\nПовернуть выделенные фигуры\nrotate $angle\n\nВыделить фигуру или снять выделение\nselect $no");
                }
            }
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "Graphics|*.sge";
            DialogResult r = o.ShowDialog(this);

            if (r == DialogResult.OK)
            {
                FileStream f = new FileStream(o.FileName, FileMode.Open);

                byte[] b = new byte[f.Length];

                f.Read(b, 0, (int) f.Length);

                char[] c = new char[b.Length];

                for (int j = 0; j < c.Length; j++)
                {
                    c[j] = Convert.ToChar(b[j]);
                }

                string s = new string(c);

                string[] fs = s.Split('\n');

                drawArea.Figures.Clear();

                // проход по фигурам
                for (int i = fs.Length-1; i >= 0; i--)
                {
                    string[] fg = fs[i].Split(' ');
                    
                    //fg[0];
                    
                    Point[] p = new Point[4];

                    // проход по фигуре
                    for (int j = 0; j < 4; j++)
                    {
                        p[j].X = Convert.ToInt16(fg[2 * j + 1]);
                        p[j].Y = Convert.ToInt16(fg[2 * j + 2]);
                    }

                    drawArea.Figures.Add(new FigureCurve(p[0], p[1], p[2], p[3]));
                }

                drawArea.Refresh();
            }
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog s = new SaveFileDialog();
            s.Filter = "Graphics|*.sge";
            DialogResult r = s.ShowDialog(this);
            //System.Windows.Forms.MessageBox.Show(s.FileName);

            if (r == DialogResult.OK)
            {
                FileStream f = new FileStream(s.FileName, FileMode.Create);

                for (int i = 0; i < drawArea.Figures.Count; i++)
                {
                    //System.Windows.Forms.MessageBox.Show();

                    if (i != 0)
                        f.WriteByte(Convert.ToByte('\n'));
                    
                    byte[] b = new byte[drawArea.Figures[i].GetType().Name.Length];
                    for (int j = 0; j < drawArea.Figures[i].GetType().Name.Length; j++)
                    {
                        b[j] = Convert.ToByte(drawArea.Figures[i].GetType().Name[j]);
                        
                    }
                    f.Write(b, 0, b.Length);

                    char[] c;

                    for (int j = 0; j < 4; j++)
                    {
                        c = drawArea.Figures[i].GetHandle(j + 1).X.ToString().ToCharArray();
                        b = new byte[c.Length];
                        for (int k = 0; k < c.Length; k++)
                            b[k] = Convert.ToByte(c[k]);

                        f.WriteByte(Convert.ToByte(' '));
                        f.Write(b, 0, b.Length);
                        

                        c = drawArea.Figures[i].GetHandle(j + 1).Y.ToString().ToCharArray();
                        b = new byte[c.Length];
                        for (int k = 0; k < c.Length; k++)
                            b[k] = Convert.ToByte(c[k]);

                        f.WriteByte(Convert.ToByte(' '));
                        f.Write(b, 0, b.Length);                        
                    }

                    f.Flush();

                    
                }

                f.Close();

            }

        }


    }
}
