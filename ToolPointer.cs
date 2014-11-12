using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace GraphicsEditor
{
    public class ToolPointer : Tool
    {
        private enum SelectionMode
        {
            None,           // Ни один режим не активен
            //NetSelection, // Групповое выделение
            Move,           // Перемещение
            HandleMove,     // Перенос контрольных точек
            Scale,
            Rotate          // Поворот
        }

        private SelectionMode selectMode = SelectionMode.None;

        // Фигура, масштабируемая в данный момент
        private FigureBase selectedFigure;
        private int selectedFigureHandle;

        // Последняя и текущая точки для перемещения и масштабирования фигур
        private Point lastPoint = new Point(0, 0);
        private Point startPoint = new Point(0, 0);
        private Point clickPoint = new Point(0, 0);

        public ToolPointer()
        {
        }

        /// <summary>
        /// Нажата левая кнопка мыши
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnMouseDown(DrawArea drawArea, MouseEventArgs e)
        {
            selectMode = SelectionMode.None;
            clickPoint.X = e.X;
            clickPoint.Y = e.Y;

            // Проверка на перемещение опорной точки (only if control is selected, курсор - на опорной точке)
            int n = drawArea.Figures.SelectionCount;

            bool nohit = true;

            for (int i = 0; i < n; i++)
            {
                FigureBase o = drawArea.Figures.GetSelectedFigure(i);
                int handleNumber = o.HitTest(clickPoint);

                if (handleNumber > 0)
                {
                    selectMode = SelectionMode.HandleMove;

                    // сохраняем фигуру (ссылку на неё) и номер перемещаемой опорной точки
                    selectedFigure = o;
                    selectedFigureHandle = handleNumber;

                    // Так как нужно редактировать только одну фигуру, отменяем выделение
                    drawArea.Figures.UnselectAll();
                    // и выбираем только нужную фигуру
                    o.Selected = true;

                    nohit = false;

                    break;
                }

                int handleScaleRotate = o.ContainerHitTest(clickPoint);

                if (handleScaleRotate == 1)
                {
                    selectMode = SelectionMode.Scale;
                }
                else if (handleScaleRotate == 2)
                {
                    selectMode = SelectionMode.Rotate;
                }
            }

            // Проверка на перемещение (указатель мыши - внутри фигуры)
            if (selectMode == SelectionMode.None)
            {
                int n1 = drawArea.Figures.Count;
                FigureBase o = null;

                for (int i = 0; i < n1; i++)
                {
                    if (drawArea.Figures[i].HitTest(clickPoint) == 0)
                    {
                        o = drawArea.Figures[i];
                        nohit = false;
                        break;
                    }
                }

                if (o != null)
                {
                    selectMode = SelectionMode.Move;

                    // Отменить выделение всех фигур, если не нажата клавиша Ctrl и выбранная фигура не выделена
                    if ((Control.ModifierKeys & Keys.Control) == 0 && !o.Selected)
                        drawArea.Figures.UnselectAll();

                    // Выделить выбранную фигуру
                    o.Selected = true;

                    drawArea.Cursor = Cursors.SizeAll;
                }
                else
                {
                    if (nohit)
                        drawArea.Figures.UnselectAll();
                }
            }

            lastPoint.X = e.X;
            lastPoint.Y = e.Y;
            startPoint.X = e.X;
            startPoint.Y = e.Y;

            drawArea.Capture = true;

            drawArea.Refresh();
        }


        /// <summary>
        /// Перемещение указателя мыши
        /// Либо ни одна кнопка мыши не нажата, либо нажата левая кнопка
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnMouseMove(DrawArea drawArea, MouseEventArgs e)
        {
            Point point = new Point(e.X, e.Y);
            int handleNumber = 0;

            // когда не нажаты кнопки мыши
            if (e.Button == MouseButtons.None)
            {
                // установка курсора
                Cursor cursor = null;

                for (int i = 0; i < drawArea.Figures.Count; i++)
                {
                    int n = drawArea.Figures[i].HitTest(point);
                    handleNumber = n;

                    if (n > 0)
                    {
                        //cursor = drawArea.Figures[i].GetHandleCursor(n);
                        cursor = Cursors.Hand;
                        break;
                    }
                    else if (n == 0)
                    {
                        cursor = Cursors.SizeAll;
                        drawArea.Figures[i].Color = Color.Tomato;
                        drawArea.Refresh();
                        break;
                    }
                    else if (n < 0)
                    {
                        drawArea.Figures[i].Color = Color.Black;
                        drawArea.Refresh();
                    }
                }

                if (cursor == null)
                    cursor = Cursors.Default;

                drawArea.Cursor = cursor;

                return;
            }

            // нажата не левая кнопка
            if (e.Button != MouseButtons.Left)
                return;


            /// Нажата левая кнопка мыши

            // расчет перемещения указателя мыши
            int dx = e.X - lastPoint.X;
            int dy = e.Y - lastPoint.Y;

            int dx_s = e.X - clickPoint.X;
            int dy_s = e.Y - clickPoint.Y;

            // перемещение контрольных точек
            if (selectMode == SelectionMode.HandleMove)
            {
                if (selectedFigure != null)
                {
                    selectedFigure.MoveHandleTo(point, selectedFigureHandle);
                    
                    //drawArea.SetDirty();
                    drawArea.Refresh();
                }
            }

            int n_sel_count = drawArea.Figures.SelectionCount;

            // перемещение
            if (selectMode == SelectionMode.Move)
            {
                for (int i = 0; i < n_sel_count; i++)
                {
                    drawArea.Figures.GetSelectedFigure(i).Move(dx, dy);
                }

                drawArea.Cursor = Cursors.SizeAll;
                //drawArea.SetDirty();
                drawArea.Refresh();
            }

            // масштабирование
            if (selectMode == SelectionMode.Scale)
            {
                for (int i = 0; i < n_sel_count; i++)
                {
                    double kx = 0, ky = 0;
                    
                    FigureBase o = drawArea.Figures.GetSelectedFigure(i);

                    double h = o.Height;
                    double w = o.Width;

                    kx = ((double)w + (double)dx) / (double)w;
                    ky = ((double)h + (double)dy) / (double)h;

                    o.Scale(kx, ky);


                }

                drawArea.Cursor = Cursors.Hand;
                //drawArea.SetDirty();
                drawArea.Refresh();
            }

            // поворот
            if (selectMode == SelectionMode.Rotate)
            {
                for (int i = 0; i < n_sel_count; i++)
                {
                    FigureBase o = drawArea.Figures.GetSelectedFigure(i);

                    double d1x, d1y, d2x, d2y;

                    d1x = e.X - ((o.Min.X + o.Width) / 2);
                    d1y = e.Y - ((o.Min.Y + o.Height) / 2);

                    d2x = lastPoint.X - ((o.Min.X + o.Width) / 2);
                    d2y = lastPoint.Y - ((o.Min.Y + o.Height) / 2);

                    //double cos = (e.X * lastPoint.X + e.Y * lastPoint.Y) / ( Math.Sqrt(Math.Pow(e.X, 2) + Math.Pow(e.Y, 2)) * Math.Sqrt(Math.Pow(lastPoint.X, 2) + Math.Pow(lastPoint.Y, 2)) );
                    double cos = (d1x * d2x + d1y * d2y) / (Math.Sqrt(Math.Pow(d1x, 2) + Math.Pow(d1y, 2)) * Math.Sqrt(Math.Pow(d2x, 2) + Math.Pow(d2y, 2)));

                    double ang = Math.Acos(cos);

                    o.Rotate(ang);
                }

                drawArea.Cursor = Cursors.Hand;
                //drawArea.SetDirty();
                drawArea.Refresh();
            }



            // запись последней позиции
            lastPoint.X = e.X;
            lastPoint.Y = e.Y;
            
            /*
            if (selectMode == SelectionMode.NetSelection)
            {
                drawArea.NetRectangle = DrawRectangle.GetNormalizedRectangle(startPoint, lastPoint);
                drawArea.Refresh();
                return;
            }
            */

        }

        /// <summary>
        /// Right mouse button is released
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            /*
            if (selectMode == SelectionMode.NetSelection)
            {
                // Group selection
                drawArea.Figures.SelectInRectangle(drawArea.NetRectangle);

                selectMode = SelectionMode.None;
                drawArea.DrawNetRectangle = false;
            }
            */

            clickPoint.X = -1;
            clickPoint.Y = -1;

            if (selectedFigure != null)
            {
                // после масштабирования
                selectedFigure.Normalize();
                selectedFigure = null;
            }

            drawArea.Capture = false;
            drawArea.Refresh();
        }
    }
}
