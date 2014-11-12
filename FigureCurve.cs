using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Drawing.Drawing2D;

namespace GraphicsEditor
{
    public class FigureCurve : GraphicsEditor.FigureBase
    {
        #region Поля и свойства
        /// <summary>
        /// Массив опорных точек
        /// </summary>
        PointF[] ControlPoints;
        PointF ScalePoint = new PointF();
        PointF RotatePoint = new PointF();

        PointF min = new PointF();
        PointF max = new PointF();

        // Объекты для метода проверки на попадание
        GraphicsPath areaPath;
        Region areaRegion;
        Pen areaPen;

        public override float Height
        {
            get
            {
                return max.Y - min.Y;
            }

        }
        public override float Width
        {
            get
            {
                return max.X - min.X;
            }

        }
        public override PointF Min
        {
            get
            {
                return min;
            }
        }
        protected GraphicsPath AreaPath
        {
            get
            {
                return areaPath;
            }
            set
            {
                areaPath = value;
            }
        }
        protected Region AreaRegion
        {
            get
            {
                return areaRegion;
            }
            set
            {
                areaRegion = value;
            }
        }
        protected Pen AreaPen
        {
            get
            {
                return areaPen;
            }
            set
            {
                areaPen = value;
            }
        }
        #endregion

        #region Конструктор
            /// <summary>
            /// Конструктор
            /// </summary>
            public FigureCurve()
            {
                ControlPoints = new PointF[HandleCount];

                Initialize();

                updateMinMax();
            }

            /// <summary>
            /// Конструктор
            /// </summary>
            /// <param name="p1">Точка начала кривой</param>
            /// <param name="cp1">Первая контрольная</param>
            /// <param name="p2">Точка конца кривой</param>
            /// <param name="cp2">Вторая контрольная</param>
            public FigureCurve(Point p1, Point cp1, Point p2, Point cp2)
            {
                ControlPoints = new PointF[HandleCount];

                ControlPoints[0].X = p1.X;
                ControlPoints[0].Y = p1.Y;
                ControlPoints[1].X = cp1.X;
                ControlPoints[1].Y = cp1.Y;
                ControlPoints[2].X = p2.X;
                ControlPoints[2].Y = p2.Y;
                ControlPoints[3].X = cp2.X;
                ControlPoints[3].Y = cp2.Y;

                Initialize();

                updateMinMax();

                ScalePoint.X = max.X + 10;
                RotatePoint.X = ScalePoint.X + 10;
                ScalePoint.Y = max.Y;
                RotatePoint.Y = max.Y;
            }
        #endregion

        #region Работа с опорными точками
            /// <summary>
            /// Возвращает количество опорных точек фигуры
            /// </summary>
            public override int HandleCount
            {
                get
                {
                    return 4;
                }
            }

            /// <summary>
            /// Координаты опорной точки
            /// </summary>
            /// <param name="handleNumber"></param>
            /// <returns></returns>
            public override PointF GetHandle(int handleNumber)
            {
                if (handleNumber > 0 && handleNumber <= HandleCount)
                    return ControlPoints[handleNumber - 1];
                else
                    return ControlPoints[0];
            }
        #endregion

        /// <summary>
        /// Прорисовка фигуры
        /// </summary>
        /// <param name="g">Холст</param>
        public override void Draw(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Pen pen = new Pen(Color, PenWidth);

            g.DrawBezier(pen, ControlPoints[0], ControlPoints[1], ControlPoints[2], ControlPoints[3]);

            pen.Dispose();
        }

        #region Работа с попаданием указателя мыши в область
        /// <summary>
        /// Принадлежит ли точка объекту
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        protected override bool PointInObject(Point point)
        {
            CreateObjects();
            return AreaRegion.IsVisible(point);
        }

        /// <summary>
        /// Тест на попадание
        /// </summary>
        /// <param name="point"></param>
        /// <returns>-1 - промах, 1 и более - номер опорной точки, 0 - попадание по фигуре</returns>
        public override int HitTest(Point point)
        {
            if (Selected)
            {
                // Проверка на попадание в опорные точки
                for (int i = 1; i <= HandleCount; i++)
                {
                    if (GetHandleRectangle(i).Contains(point))
                        return i;
                }
            }

            // Если нет попадания в опорные точки,
            // проверка на попадание внутрь самой фигуры
            if (PointInObject(point))
                return 0;

            return -1;
        }

        /// <summary>
        /// Проверка на попадание в элементы контейнера
        /// </summary>
        /// <param name="point">Точка</param>
        /// <returns>-1 - промах, 1 - масштабирование, 2 - поворот</returns>
        public override int ContainerHitTest(Point point)
        {
            if (Selected)
            {
                if (GetRotateHandleRectangle().Contains(point))
                    return 2;
                else if (GetScaleHandleRectangle().Contains(point))
                    return 1;
            }

            return -1;
        }
        #endregion



        #region Прорисовка служебных элементов
        /// <summary>
        /// Прорисовка опорных точек
        /// </summary>
        /// <param name="g">Холст</param>
        public override void DrawControlPoints(Graphics g)
        {
            // Если фигура не выбрана, контейнер не прорисовывается
            if (!Selected)
                return;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            SolidBrush brush = new SolidBrush(Color.DarkSlateGray);

            Pen pen = new Pen(Color.DimGray, 1);

            // контрольные линии
            g.DrawLine(pen, ControlPoints[0], ControlPoints[1]);
            g.DrawLine(pen, ControlPoints[2], ControlPoints[3]);
           
            // опорные точки
            g.FillRectangle(brush, GetHandleRectangle(1));
            g.FillRectangle(brush, GetHandleRectangle(4));
            g.FillRectangle(brush, GetHandleRectangle(2));
            g.FillRectangle(brush, GetHandleRectangle(3));
            
            // освобождение ресурсов
            brush.Dispose();
            pen.Dispose();

        }

        public override void DrawIndex(Graphics g, int i)
        {
            g.DrawString(i.ToString(), new Font("Arial", 12), new SolidBrush(Color.Firebrick), new Point( (int)min.X + 10, (int)min.Y));
        }

        private void updateMinMax()
        {
            // координаты для контейнера
            min.X = Math.Min(Math.Min(ControlPoints[0].X, ControlPoints[1].X), Math.Min(ControlPoints[2].X, ControlPoints[3].X));
            min.Y = Math.Min(Math.Min(ControlPoints[0].Y, ControlPoints[1].Y), Math.Min(ControlPoints[2].Y, ControlPoints[3].Y));
            max.X = Math.Max(Math.Max(ControlPoints[0].X, ControlPoints[1].X), Math.Max(ControlPoints[2].X, ControlPoints[3].X));
            max.Y = Math.Max(Math.Max(ControlPoints[0].Y, ControlPoints[1].Y), Math.Max(ControlPoints[2].Y, ControlPoints[3].Y));
        }

        public override void DrawContainer(Graphics g)
        {
            if (!Selected)
                return;

            g.SmoothingMode = SmoothingMode.AntiAlias;

            Pen pen = new Pen(Color.Gray, 1);

            // контейнер
            g.DrawLine(pen, min.X, min.Y, min.X, max.Y);
            g.DrawLine(pen, min.X, min.Y, max.X, min.Y);
            g.DrawLine(pen, max.X, max.Y, min.X, max.Y);
            g.DrawLine(pen, max.X, max.Y, max.X, min.Y);

            SolidBrush brush_rotate = new SolidBrush(Color.Red);
            SolidBrush brush_scale = new SolidBrush(Color.Green);

            g.FillRectangle(brush_rotate, GetRotateHandleRectangle());
            g.FillRectangle(brush_scale, GetScaleHandleRectangle());

            pen.Dispose();
            brush_rotate.Dispose();
            brush_scale.Dispose();
        }

        public RectangleF GetRotateHandleRectangle()
        {
            return new RectangleF(RotatePoint.X - 3, RotatePoint.Y - 3, 7, 7);
        }

        public RectangleF GetScaleHandleRectangle()
        {
            return new RectangleF(ScalePoint.X - 3, ScalePoint.Y - 3, 7, 7);
        }


        /// <summary>
        /// Обновление объектов
        /// При обновлении путь для теста на попадание
        /// освобождается и должен быть создан заново
        /// </summary>
        protected void Invalidate()
        {
            if (AreaPath != null)
            {
                AreaPath.Dispose();
                AreaPath = null;
            }

            if (AreaPen != null)
            {
                AreaPen.Dispose();
                AreaPen = null;
            }

            if (AreaRegion != null)
            {
                AreaRegion.Dispose();
                AreaRegion = null;
            }

            // Перерасчет минимумов и максимумов
            updateMinMax();

            // Новые координаты для служебных точек
            ScalePoint.X = max.X + 10;
            RotatePoint.X = ScalePoint.X + 10;
            ScalePoint.Y = max.Y;
            RotatePoint.Y = max.Y;
        }

        /// <summary>
        /// Создает объекты для метода проверки на попадание
        /// </summary>
        protected virtual void CreateObjects()
        {
            // Создание объектов, только если они отсутствуют
            if (AreaPath != null)
                return;

            // Создание жирного пути
            AreaPath = new GraphicsPath();
            AreaPen = new Pen(Color.Black, 7);
            AreaPath.AddBezier(ControlPoints[0], ControlPoints[1], ControlPoints[2], ControlPoints[3]);
            AreaPath.Widen(AreaPen);

            // Создание зоны из пути
            AreaRegion = new Region(AreaPath);
        }
        #endregion

        #region Операции над фигурой
        /// <summary>
        /// Перемещение опорной точки
        /// </summary>
        /// <param name="point">Новые координаты</param>
        /// <param name="handleNumber">Номер опорной точки [1; HandleCount]</param>
        public override void MoveHandleTo(Point point, int handleNumber)
        {
            if (handleNumber > 0 && handleNumber <= HandleCount)
                ControlPoints[handleNumber - 1] = point;

            Invalidate();
        }

        /// <summary>
        /// Перемещение всей фигуры
        /// </summary>
        /// <param name="deltaX"></param>
        /// <param name="deltaY"></param>
        public override void Move(int deltaX, int deltaY)
        {
            /*
            for (int i = 0; i < 4; i++)
            {
                ControlPoints[i].X += deltaX;
                ControlPoints[i].Y += deltaY;
            }
            */

            Matrix t = new Matrix();
            for (int i = 0; i < 3; i++)
                t[i, i] = 1;
            t[2, 0] = deltaX;
            t[2, 1] = deltaY;

            Vector v = new Vector();
            for (int i = 0; i < HandleCount; i++)
            {
                v[0] = ControlPoints[i].X;
                v[1] = ControlPoints[i].Y;
                v[2] = 1;
                
                v = v * t;

                ControlPoints[i].X = (int) v[0];
                ControlPoints[i].Y = (int) v[1];
            }

            Invalidate();
        }

        /// <summary>
        /// Масштабирование
        /// </summary>
        /// <param name="kX"></param>
        /// <param name="kY"></param>
        public override void Scale(double kX, double kY)
        {
            Matrix t = new Matrix();

            t[0, 0] = kX;
            t[1, 1] = kY;

            t[2, 2] = 1;

            t[2, 0] = (min.X) * (1.0 - kX);
            t[2, 1] = (min.Y) * (1.0 - kY);
            

            Vector v = new Vector();
            for (int i = 0; i < HandleCount; i++)
            {
                v[0] = ControlPoints[i].X;
                v[1] = ControlPoints[i].Y;
                v[2] = 1;

                v = v * t;

                ControlPoints[i].X = (float) v[0];
                ControlPoints[i].Y = (float) v[1];
            }

            Invalidate();
        }

        /// <summary>
        /// Поворот
        /// </summary>
        /// <param name="angle">Угол поворота</param>
        public override void Rotate(double angle)
        {
            Matrix t = new Matrix();

            double cosa = Math.Cos(angle);
            double sina = Math.Sin(angle);

            double ddx = (min.X + max.X) / 2;
            double ddy = (min.Y + max.Y) / 2;

            t[0, 0] = cosa;
            t[1, 1] = cosa;

            t[2, 2] = 1;

            t[1, 0] = -sina;
            t[0, 1] = sina;

            t[2, 0] = -ddx * (cosa - 1) + ddy * sina;
            t[2, 1] = -ddy * (cosa - 1) - ddx * sina;


            Vector v = new Vector();
            for (int i = 0; i < HandleCount; i++)
            {
                v[0] = ControlPoints[i].X;
                v[1] = ControlPoints[i].Y;
                v[2] = 1;

                // Умножение
                v = v * t;

                ControlPoints[i].X = (float)v[0];
                ControlPoints[i].Y = (float)v[1];
            }

            Invalidate();
        }
        #endregion
    }
}
