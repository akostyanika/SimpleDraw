using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Drawing;

namespace GraphicsEditor
{
    public class ToolCurve : ToolObject
    {
        int counter = 0;

        Point p1;
        Point p2;
        Point cp1;
        Point cp2;

        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            // Последняя опорная точка: добавление фигуры
            if (counter == 3)
            {
                cp2.X = e.X;
                cp2.Y = e.Y;

                counter = 0;

                drawArea.Points.Clear();

                AddNewObject(drawArea, new FigureCurve(p1, cp1, p2, cp2));
            }
            // Первая опорная точка
            else if (counter == 0)
            {
                p1.X = e.X;
                p1.Y = e.Y;

                counter++;

                drawArea.Points.Add(new Point(p1.X, p1.Y));
                drawArea.Refresh();
            }
            // Вторая
            else if (counter == 1)
            {
                cp1.X = e.X;
                cp1.Y = e.Y;
                
                counter++;

                drawArea.Points.Add(new Point(cp1.X, cp1.Y));
                drawArea.Refresh();
            }
            // Третья
            else if (counter == 2)
            {
                p2.X = e.X;
                p2.Y = e.Y;
                counter++;

                drawArea.Points.Add(new Point(p2.X, p2.Y));
                drawArea.Refresh();
            }
        }
        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            // Когда добавлена последняя опорная точка
            if (counter == 0)
            {
                drawArea.Figures[drawArea.Figures.Count-1].Normalize();
                drawArea.ActiveTool = DrawArea.ToolType.Pointer;

                drawArea.Capture = false;
                drawArea.Refresh();
            }
        }

        public override void OnMouseMove(DrawArea drawArea, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point point = new Point(e.X, e.Y);

                int i = 0;

                //drawArea.Figures[drawArea.Figures.Count-1].MoveHandleTo(point, i);
                
                drawArea.Refresh();
            }
        }
    }
}
