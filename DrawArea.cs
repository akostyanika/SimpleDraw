using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GraphicsEditor
{
    public partial class DrawArea : UserControl
    {
        #region Перечисления
        /// <summary>
        /// Перечисление инструментов
        /// </summary>
        public enum ToolType
        {
            Pointer,
            Curve,
            NumberOfTools
        };
        #endregion

        #region Члены класса

        /// <summary>
        /// Список фигур.
        /// </summary>
        FiguresList figures;

        /// Контрольные точки создаваемой кривой
        ArrayList points;

        /// <summary>
        /// Активный инструмент
        /// </summary>
        private ToolType activeTool;

        /// <summary>
        /// Массив инструментов
        /// </summary>
        private Tool[] tools;

        /// <summary>
        /// Форма, в которой отображается элемент управления
        /// </summary>
        private MainWindow owner;

        #endregion

        #region Конструктор

        public DrawArea()
        {
            InitializeComponent();
        }

        #endregion

        #region Свойства

        public MainWindow Owner
        {
            get
            {
                return owner;
            }
            set
            {
                owner = value;
            }
        }

        public FiguresList Figures
        {
            get
            {
                return figures;
            }
            set
            {
                figures = value;
            }
        }

        public ArrayList Points
        {
            get
            {
                return points;
            }
            set
            {
                points = value;
            }
        }

        public ToolType ActiveTool
        {
            get
            {
                return activeTool;
            }
            set
            {
                activeTool = value;
            }
        }

        #endregion

        #region Обработчики событий

        private void DrawArea_Paint(object sender, PaintEventArgs e)
        {
            SolidBrush brush = new SolidBrush(Color.FromArgb(255, 255, 255));
            e.Graphics.FillRectangle(brush, this.ClientRectangle);

            if (Figures != null)
            {
                Figures.Draw(e.Graphics);
            }

            if (Points != null)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                SolidBrush brush_point = new SolidBrush(Color.Black);
                for (int i = 0; i < Points.Count; i++)
                {
                    e.Graphics.FillRectangle(brush_point, new Rectangle((Point) Points[i], new Size(7, 7)));
                }
                brush_point.Dispose();
            }

            //DrawNetSelection(e.Graphics);

            brush.Dispose();
        }

        private void DrawArea_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                tools[(int)activeTool].OnMouseDown(this, e);
        }

        private void DrawArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.None)
                tools[(int)activeTool].OnMouseMove(this, e);
        }

        private void DrawArea_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                tools[(int)activeTool].OnMouseUp(this, e);
        }

        #endregion

        /// <summary>
        /// Инициализация
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="docManager"></param>
        public void Initialize(MainWindow owner)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

            // Keep reference to owner form
            this.Owner = owner;

            // set default tool
            activeTool = ToolType.Pointer;

            // create list of graphic objects
            figures = new FiguresList();
            points = new ArrayList();

            // create array of drawing tools
            tools = new Tool[(int)ToolType.NumberOfTools];
            tools[(int)ToolType.Pointer] = new ToolPointer();
            tools[(int)ToolType.Curve] = new ToolCurve();
        }
    }
}
