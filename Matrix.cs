using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace GraphicsEditor
{
    public class Transformation
    {
        public void Rotate(float angle)
        {

        }
        public void Move(int dx, int dy)
        {

        }
        public void Resize(float kx, float ky)
        {

        }
    }

    public class Matrix
    {
        double[,] m = new double[3, 3];

        public double this[int i, int j]
        {
            get
            {
                if (i >= 0 && i < 3 && j >= 0 && j < 3)
                {
                    return m[i, j];
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (i >= 0 && i < 3 && j >= 0 && j < 3)
                {
                    m[i, j] = value;
                }
            }
        }

        public static Vector operator * (Vector v, Matrix m)
        {
            return new Vector(
                v[0] * m[0, 0] + v[1] * m[1, 0] + v[2] * m[2, 0],
                v[0] * m[0, 1] + v[1] * m[1, 1] + v[2] * m[2, 1],
                v[0] * m[0, 2] + v[1] * m[1, 2] + v[2] * m[2, 2]
                );
        }
        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            Matrix m = new Matrix();
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    for (int r = 0; r < 3; r++)
                        m[i, j] += m1[i, r] * m2[r, i];

            return m;

        }
    }

    public class Vector
    {
        double[] v = new double[3];

        public Vector(double x, double y, double z)
        {
            v[0] = x; v[1] = y; v[2] = z;
        }

        public Vector()
        {
        }

        public double this[int i]
        {
            get
            {
                if (i >= 0 && i < 3)
                    return v[i];
                else
                    return 0;
            }
            set
            {
                if (i >= 0 && i < 3)
                    v[i] = value;
            }
        }

        public static Vector operator * (Vector v1, Vector v2)
        {
            return new Vector(v1[0] * v2[0], v1[1] * v2[1], v1[2] * v2[2]);
        }
    }
}
