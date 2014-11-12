using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using System.Collections;

namespace GraphicsEditor
{
    /// <summary>
    /// Список фигур
    /// </summary>
    public class FiguresList
    {
        private ArrayList figuresList;

        /// <summary>
        /// Количество фигур в списке
        /// </summary>
        public int Count
        {
            get
            {
                return figuresList.Count;
            }
        }

        /// <summary>
        /// Индексатор
        /// </summary>
        /// <param name="index">Порядковый номер, начиная от нуля</param>
        /// <returns>Фигура, унаследованная от FigureBase, либо null в случае некорректного индекса</returns>
        public FigureBase this[int index]
        {
            get
            {
                if (index < 0 || index >= figuresList.Count)
                    return null;

                return ((FigureBase)figuresList[index]);
            }
        }

        public FiguresList()
        {
            figuresList = new ArrayList();
        }

        /// <summary>
        /// Прорисовка всех фигур в списке
        /// </summary>
        /// <param name="g">Холст</param>
        public void Draw(Graphics g)
        {
            int n = figuresList.Count;
            FigureBase o;

            // Проход с конца списка - рисуем с нижнего слоя
            for (int i = n - 1; i >= 0; i--)
            {
                o = (FigureBase) figuresList[i];

                o.Draw(g);

                if (o.Selected == true)
                {
                    o.DrawContainer(g);
                    o.DrawControlPoints(g);    
                }

                o.DrawIndex(g, i);
            }
        }

        /// <summary>
        /// Добавление фигуры
        /// </summary>
        /// <param name="o">Фигура (наследник FigureBase)</param>
        public void Add(FigureBase o)
        {
            figuresList.Insert(0, o);
        }

        /// <summary>
        /// Удаление выделенных фигур
        /// </summary>
        /// <returns>true если хотя бы одна фигура была удалена, иначе - false</returns>
        public bool Delete()
        {
            bool result = false;

            int n = figuresList.Count;

            for (int i = n - 1; i >= 0; i--)
            {
                if ( ((FigureBase) figuresList[i]).Selected)
                {
                    figuresList.RemoveAt(i);
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// Удаление всех фигур
        /// </summary>
        /// <returns>true, если фигуры были в списке</returns>
        public bool Clear()
        {
            bool result = (figuresList.Count > 0);
            figuresList.Clear();
            return result;
        }

        // Количество выбранных
        public int SelectionCount
        {
            get
            {
                int n = 0;

                foreach (FigureBase o in figuresList)
                {
                    if (o.Selected)
                        n++;
                }

                return n;
            }
        }

        public FigureBase GetSelectedFigure (int index)
        {
            int n = -1;

            foreach (FigureBase o in figuresList)
            {
                if (o.Selected)
                {
                    n++;

                    if (n == index)
                        return o;
                }
            }

            return null;
        }

        public void UnselectAll()
        {
            foreach (FigureBase o in figuresList)
            {
                o.Selected = false;
            }
        }

        public void SelectAll()
        {
            foreach (FigureBase o in figuresList)
            {
                o.Selected = true;
            }
        }
    }
}
