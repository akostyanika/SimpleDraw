using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace GraphicsEditor
{
    public abstract class ToolObject : Tool
    {
        /// <summary>
        /// Left mouse is released.
        /// New object is created and resized.
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnMouseUp(DrawArea drawArea, MouseEventArgs e)
        {
            drawArea.Figures[0].Normalize();
            drawArea.ActiveTool = DrawArea.ToolType.Pointer;

            drawArea.Capture = false;
            drawArea.Refresh();
        }

        /// <summary>
        /// Добавить фигуру к DrawArea
        /// Вызывается при нажатии левой кнопкой мыши по DrawArea, когда
        /// один из инструментов (потомок ToolObject) активен.
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="o"></param>
        protected void AddNewObject(DrawArea drawArea, FigureBase o)
        {
            drawArea.Figures.UnselectAll();

            o.Selected = true;
            drawArea.Figures.Add(o);

            drawArea.Capture = true;
            drawArea.Refresh();

            //drawArea.SetDirty();
        }
    }
}
