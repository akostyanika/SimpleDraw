using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;

namespace GraphicsEditor
{
    public abstract class FigureBase
    {

        #region Члены и свойства

        private bool selected;
        private Color color;
        private int penWidth;

        public virtual float Height
        {
            get { return 0; }
        }
        public virtual float Width
        {
            get { return 0; }
        }
        public virtual PointF Min
        {
            get { return new Point (0, 0); }
        }

        public bool Selected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;
            }
        }
    
        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }

        public int PenWidth
        {
            get
            {
                return penWidth;
            }
            set
            {
                penWidth = value;
            }
        }

        public virtual int HandleCount
        {
            get
            {
                return 0;
            }
        }

        #endregion

        #region Виртуальные методы

        /// <summary>
        /// Прорисовка фигуры
        /// </summary>
        /// <param name="g">Холст</param>
        public virtual void Draw(Graphics g)
        {
        }

        /// <summary>
        /// Взять опорную точку
        /// </summary>
        /// <param name="handleNumber">Порядковый номер опорной точки (начиная с 1)</param>
        /// <returns>Опорная точка</returns>
        public virtual PointF GetHandle(int handleNumber)
        {
            return new PointF(0, 0);
        }

        /// <summary>
        /// Визуальный образ опорной точки
        /// </summary>
        /// <param name="handleNumber">Порядковый номер опорной точки (начиная с 1)</param>
        /// <returns>Прямоугольник-образ опорной точки</returns>
        public virtual RectangleF GetHandleRectangle(int handleNumber)
        {
            PointF point = GetHandle(handleNumber);

            return new RectangleF(point.X - 3, point.Y - 3, 7, 7);
        }

        /// <summary>
        /// Прорисовка опорных точек
        /// </summary>
        /// <param name="g">Холст</param>
        public virtual void DrawControlPoints(Graphics g)
        {
            if (!Selected)
                return;

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            SolidBrush brush = new SolidBrush(Color.Black);

            for (int i = 1; i <= HandleCount; i++)
            {
                g.FillRectangle(brush, GetHandleRectangle(i));
            }

            brush.Dispose();
        }

        /// <summary>
        /// Проверка на попадание в элементы контейнера
        /// </summary>
        /// <param name="point">Точка</param>
        /// <returns>0 - промах, 1 - масштабирование, 2 - поворот</returns>
        public virtual int ContainerHitTest(Point point)
        {
            return 0;
        }

        /// <summary>
        /// Прорисовка контейнера
        /// </summary>
        /// <param name="g"></param>
        public virtual void DrawContainer(Graphics g)
        {
        }

        /// <summary>
        /// Проверка на попадание
        /// </summary>
        /// <param name="point">Точка</param>
        /// <returns>-1 - промах, 0 - где-то по фигуре, 1 и более - номер handle'а</returns>
        public virtual int HitTest(Point point)
        {
            return -1;
        }


        /// <summary>
        /// Принадлежность точки объекту
        /// </summary>
        /// <param name="point">Координата вычисляемой точки</param>
        /// <returns></returns>
        protected virtual bool PointInObject(Point point)
        {
            return false;
        }

        /// <summary>
        /// Метод вызывается в конце масштабирования
        /// </summary>
        public virtual void Normalize()
        {
        }

        /// <summary>
        /// Переместить контрольную точку
        /// </summary>
        /// <param name="point">Координаты новой точки</param>
        /// <param name="handleNumber">Идентификатор контрольной точки</param>
        public virtual void MoveHandleTo(Point point, int handleNumber)
        {
        }

        /// <summary>
        /// Переместить фигуру
        /// </summary>
        /// <param name="deltaX">Разница по x</param>
        /// <param name="deltaY">Разница по y</param>
        public virtual void Move(int deltaX, int deltaY)
        {
        }

        /// <summary>
        /// Масштабирование
        /// </summary>
        /// <param name="kX">Коэффициент по X</param>
        /// <param name="kY">Коэффициент по Y</param>
        public virtual void Scale(double kX, double kY)
        {
        }

        /// <summary>
        /// Поворот
        /// </summary>
        /// <param name="angle">Угол поворота</param>
        public virtual void Rotate(double angle)
        {
        }


        /// <summary>
        /// Инициализация
        /// </summary>
        protected void Initialize()
        {
            color = Color.Black;
            penWidth = 1;
        }
        
        #endregion


        public virtual void DrawIndex(Graphics g, int i)
        {
            
        }
    }
}
