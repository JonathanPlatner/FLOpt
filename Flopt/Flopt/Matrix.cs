using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flopt
{
    class Matrix
    {
        public struct Vector
        {
            private float[] _elements;
            public Vector(int length)
            {
                _elements = new float[length];
            }
            public Vector(float[] elements)
            {
                _elements = elements;
            }
            public float[] Elements { get { return _elements; } set { _elements = value; } }
            public int Length { get { return _elements.Length; } }
            public static Vector operator +(Vector a, Vector b)
            {
                Vector c = new Vector(a.Length);
                if (a.Length != b.Length)
                {
                    throw new InvalidOperationException("Row addition is not valid for rows of different lengths.");
                }
                else
                {
                    for (int i = 0; i < c.Length; i++)
                    {
                        c.Elements[i] = a.Elements[i] + b.Elements[i];
                    }
                }
                return c;
            }
            public static Vector operator -(Vector a, Vector b)
            {
                Vector c = new Vector(a.Length);
                if (a.Length != b.Length)
                {
                    throw new InvalidOperationException("Row addition is not valid for rows of different lengths.");
                }
                else
                {
                    for (int i = 0; i < c.Length; i++)
                    {
                        c.Elements[i] = a.Elements[i] - b.Elements[i];
                    }
                }
                return c;
            }
            public static Vector operator *(float k, Vector a)
            {
                Vector b = new Vector(a.Length);

                for (int i = 0; i < b.Length; i++)
                {
                    b.Elements[i] = k * a.Elements[i];
                }

                return b;
            }
            public static Vector operator *(Vector a,float k)
            {
                Vector b = new Vector(a.Length);

                for (int i = 0; i < b.Length; i++)
                {
                    b.Elements[i] = k * a.Elements[i];
                }

                return b;
            }
        }

        private Vector[] _rows;
        private Vector[] _cols;
        private float[] _elements;
        public Matrix(int numRows, int numColumns)
        {
            _rows = new Vector[numRows];
            for (int i = 0; i < numRows; i++)
            {
                _rows[i] = new Vector(numColumns);
            }
            _cols = new Vector[numColumns];
            for (int i = 0; i < numColumns; i++)
            {
                _cols[i] = new Vector(numRows);
            }
            _elements = new float[numRows * numColumns];
        }
        public Matrix(int numRows, int numColumns, float[] elements)
        {
            _rows = new Vector[numRows];
            for (int i = 0; i < numRows; i++)
            {
                _rows[i] = new Vector(numColumns);
                for (int j = 0; j < numColumns; j++)
                {
                    _rows[i].Elements[j] = elements[j + i * numColumns];
                }
            }
            _cols = new Vector[numColumns];
            for (int i = 0; i < numColumns; i++)
            {
                _cols[i] = new Vector(numRows);
                for (int j = 0; j < numRows; j++)
                {
                    _cols[i].Elements[j] = elements[i + j * numColumns];
                }
            }
            _elements = elements;
        }

        public float[] Elements { get { return _elements; } }
        public Vector[] Rows { get { return _rows; } }
        public Vector[] Columns { get { return _cols; } }

        public float GetElement(int rowNum, int colNum)
        {
            return _rows[rowNum].Elements[colNum];
        }
        public void SetElement(float val, int rowNum, int colNum)
        {
            _elements[colNum + rowNum * _cols.Length] = val;
            _rows[rowNum].Elements[colNum] = val;
            _cols[colNum].Elements[rowNum] = val;
        }
        public void SetRow(float[] elements, int rowNum)
        {
            if (elements.Length != _cols.Length)
            {
                throw new InvalidOperationException("New row must be the same length as existing rows.");
            }
            _rows[rowNum].Elements = elements;
            for (int i = 0; i < _cols.Length; i++)
            {
                _cols[i].Elements[rowNum] = elements[i];
                _elements[i+rowNum*_cols.Length] = elements[i];
            }
        }
        public string[] Display()
        {
            string[] rows = new string[_rows.Length];
            for (int i = 0; i < rows.Length; i++)
            {
                for (int j = 0; j < _cols.Length; j++)
                {
                    rows[i] += _rows[i].Elements[j].ToString() + " ";
                }
            }
            return rows;
        }
        public Matrix SubMatrix(int rowNum, int colNum)
        {
            float[] elements = new float[(_rows.Length - 1) * (_cols.Length - 1)];
            int k = 0;
            for (int j = 0; j < _cols.Length; j++)
            {
                for (int i = 0; i < _rows.Length; i++)
                {

                    if (i != rowNum && j != colNum)
                    {
                        elements[k] = GetElement(i, j);
                        k++;
                    }
                }
            }
            return new Matrix(_rows.Length - 1, _cols.Length - 1, elements);
        }
        public Matrix SelectColumns(int[] colNum)
        {
            //TODO: Repopulate rows and elements based on columns
            Matrix newMat = new Matrix(_rows.Length,colNum.Length);
            for (int i = 0; i < colNum.Length; i++)
            {
                newMat._cols[i] = Columns[i];
            }
            return newMat;
        }
        public static Matrix Identity(int size)
        {
            Matrix id = new Matrix(size, size);
            for (int i = 0; i < size; i++)
            {
                id.SetElement(1, i, i);
            }
            return id;
        }
        public Matrix HorizontalAppend(Matrix mat)
        {
            Matrix appended = new Matrix(_rows.Length, mat.Columns.Length + _cols.Length);
            if (mat.Rows.Length != _rows.Length)
            {
                throw new InvalidOperationException("Matrices must have the same number of rows to be horizontally appended.");
            }
            for (int i = 0; i < _cols.Length; i++)
            {
                appended._cols[i] = _cols[i];
            }
            for (int i = 0; i < mat._cols.Length; i++)
            {
                appended._cols[i+_cols.Length] = mat._cols[i];
            }
            for (int i = 0; i < appended._cols.Length; i++)
            {
                for (int j = 0; j < appended._rows.Length; j++)
                {
                    appended._rows[j].Elements[i] = appended._cols[i].Elements[j];
                    appended._elements[i + j * appended._cols.Length] = appended._cols[i].Elements[j];
                }
            }
            return appended;
        }
        public Matrix RREF()
        {
            Matrix reduced = new Matrix(_rows.Length, _cols.Length, _elements);
            for (int i = 0; i < Math.Min(reduced.Rows.Length,reduced.Columns.Length); i++)
            {
                reduced.SetRow((reduced.Rows[i] * (1/ reduced.Rows[i].Elements[i])).Elements, i);
                for (int j = 0; j < reduced.Rows.Length; j++)
                {
                    if (j != i)
                    {
                        reduced.SetRow((reduced.Rows[j]-reduced.Rows[i]*reduced.Rows[j].Elements[i]).Elements, j);
                    }
                }
            }
            return reduced;
        }
        public Matrix Invert()
        {
            if (_cols.Length != _rows.Length)
            {
                throw new InvalidOperationException("Matrix must be square.");
            }
            return HorizontalAppend(Identity(_rows.Length)).RREF();
        }

        //private float[,] det2x2()
        //{
        //    // Take the determinates of the top two rows, so we can use them to compute 3x3, 4x4, etc
        //    float[] row1 = new float[nCol];
        //    float[] row2 = new float[nCol];
        //    float[] detrow = new float[2 * (nCol - 1) - 1];
        //    for (int i = 0; i < row1.Length; i++)
        //    {
        //        row1[i] = GetElement(0, i);
        //        row2[i] = GetElement(1, i);
        //    }


        //    float[,] dets = new float[nRow, nCol];
        //    for (int i = 0; i < nCol - 1; i++)
        //    {
        //        for (int j = i + 1; j < i + 3 && j < nCol; j++)
        //        {
        //            dets[i, j] = row1[i] * row2[j] - row2[i] * row1[j];

        //        }
        //    }
        //    return dets;
        //}

        //public float Determinate()
        //{
        //    if (nRow != nCol)
        //    {
        //        throw new InvalidOperationException("Matrix must be square");
        //    }
        //    float[,] dets = det2x2();

        //    if (nCol > 2)
        //    {
        //        for (int n = 3; n < nCol + 1; n++)
        //        {


        //            float[] newdetrow = new float[n * (nCol - n + 1) - (n - 1)];
        //            for (int i = 0; i < nCol - n + 1; i++)
        //            {

        //                for (int j = i + 1; j < i + n + 1 && j < nCol; j++)
        //                {
        //                    int p = 0;
        //                    int[] ind = new int[n];
        //                    for (int k = i; k < i + n + 1 && k < nCol; k++)
        //                    {
        //                        if (k != j)
        //                        {
        //                            ind[p] = k;
        //                            p++;
        //                        }
        //                    }

        //                    for (int k = 0; k < ind.Length; k++)
        //                    {

        //                    }
        //                }
        //            }
        //        }

        //        return 0;
        //    }
        //    else
        //    {
        //        return 0;
        //    }




        //}
        //public float[] Elements { get { return elements; } set { elements = value; } }
    }
}
