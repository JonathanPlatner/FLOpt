using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flopt
{
    class Airfoil
    {
        //Airfoils are defined by thickness as percent of total chord length and a camber line. 
        //Since this airfoil will be used on a flying wing, we will build in some reflex on the trailing edge to counteract the negative moment of the airfoil
        //The airfoil will have five parameters: camber height, camber thickness, reflex, reflex position, and thickness
        private float[] _x;
        private float[] _y;
        private float _area;
        private Vector2 _centroid;
        private string _name;
        public Airfoil(float thickness, float yCamberWeight, float xCamberWeight, float yReflexWeight, float xReflexWeight)
        {
            Initialize(thickness,  yCamberWeight,  xCamberWeight,  yReflexWeight,  xReflexWeight, "jptemp");
            _name = "jptemp";
        }
        public Airfoil(float thickness, float yCamberWeight, float xCamberWeight, float yReflexWeight, float xReflexWeight,string name)
        {
            Initialize(thickness, yCamberWeight, xCamberWeight, yReflexWeight, xReflexWeight,name);
            _name = name;
        }
        private void Initialize(float thickness, float yCamberWeight, float xCamberWeight, float yReflexWeight, float xReflexWeight,string name)
        {
            Matrix camberLineSystem = new Matrix(3, 4, new float[] {
            xCamberWeight,(float)Math.Pow(xCamberWeight,2),(float)Math.Pow(xCamberWeight,3),yCamberWeight,
            xReflexWeight,(float)Math.Pow(xReflexWeight,2),(float)Math.Pow(xReflexWeight,3),yReflexWeight,
            1,1,1,0});

            Matrix.Vector camberCoefficients = camberLineSystem.RREF().Columns[3];
            _x = new float[180];
            _y = new float[180];

            for (int i = 0; i < _x.Length; i++)
            {
                _x[i] = 1 - (float)Math.Cos((i - 89.5f) / 179 * Math.PI);
            }
            //Top half
            for (int i = 0; i < 90; i++)
            {
                _y[i] = 5 * thickness * (float)(0.2969 * Math.Sqrt(_x[i]) - 0.126 * _x[i] - 0.3516 * Math.Pow(_x[i], 2) + 0.2843 * Math.Pow(_x[i], 3) - 0.1015 * Math.Pow(_x[i], 4)) + // NACA Base Foil
                    camberCoefficients.Elements[0] * _x[i] + camberCoefficients.Elements[1] * (float)Math.Pow(_x[i], 2) + camberCoefficients.Elements[2] * (float)Math.Pow(_x[i], 3); // Camber Line
            }
            //Bottom half
            for (int i = 90; i < 180; i++)
            {
                _y[i] = -5 * thickness * (float)(0.2969 * Math.Sqrt(_x[i]) - 0.126 * _x[i] - 0.3516 * Math.Pow(_x[i], 2) + 0.2843 * Math.Pow(_x[i], 3) - 0.1015 * Math.Pow(_x[i], 4)) + // NACA Base Foil
                    (camberCoefficients.Elements[0] * _x[i] + camberCoefficients.Elements[1] * (float)Math.Pow(_x[i], 2) + camberCoefficients.Elements[2] * (float)Math.Pow(_x[i], 3)); // Camber Line
            }
            string[] lines = new string[181];
            lines[0] = name;
            for (int i = 0; i < _x.Length; i++)
            {
                lines[i + 1] = Math.Round(_x[i], 5, MidpointRounding.ToEven).ToString() + "  " + Math.Round(_y[i], 5, MidpointRounding.ToEven).ToString();

            }
            System.IO.File.WriteAllLines("C:/Users/jonathan.platner/Desktop/"+name+".dat", lines);

            //Compute centroids and area of the foil
            float xCentroidalArea = 0;
            float yCentroidalArea = 0;
            for (int i = 0; i < 89; i++)
            {
                float yavg = (_y[i] + _y[i + 1]) / 2 - (_y[179 - i] + _y[179 - i - 1]) / 2;
                _area += yavg * (_x[i] - _x[i + 1]);

                float a = _y[i] - _y[179 - i];
                float b = _y[i + 1] - _y[179 - i - 1];
                float h = _x[i] - _x[i + 1];
                float c = _y[179 - i] - _y[179 - i - 1];
                float xCentroid = h * (2 * a + b) / (3 * (a + b)) + _x[i + 1];
                float yCentroid = (2 * a * c + (float)Math.Pow(a, 2) + c * b + a * b + (float)Math.Pow(b, 2)) / (3 * (a + b)) + _y[179 - i - 1];
                xCentroidalArea += yavg * h * xCentroid;
                yCentroidalArea += yavg * h * yCentroid;
            }
            _centroid = new Vector2(xCentroidalArea / _area, yCentroidalArea / _area);
        }
        public float[] X { get { return _x; } }
        public float[] Y { get { return _y; } }
        public float Area { get { return _area; } }
        public Vector2 Centroid { get { return _centroid; } }
        public string Name { get { return _name; } }
    }
}
