using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flopt
{
    static class Components
    {
        public struct Receiver
        {
            private static float _weight = 0.00640f; //kg
            private static Vector3 _dimensions = new Vector3(0.026f,0.036f,0.008f); //m
            private Vector3 _position;
            public Receiver(Vector3 position)
            {
                _position = position;
            }
            public static float Weight { get { return _weight; } }
            public static Vector3 Dimensions { get { return _dimensions; } }
            public Vector3 Position { get { return _position; } }
        }
        public struct Servo
        {
            private static float _weight = 0.00246f; //kg
            private static Vector3 _dimensions = new Vector3(0.017f, 0.023f, 0.01f); //m
            private Vector3 _position;
            public Servo(Vector3 position)
            {
                _position = position;
            }
            public static float Weight { get { return _weight; } }
            public static Vector3 Dimensions { get { return _dimensions; } }
            public Vector3 Position { get { return _position; } }
        }
        public struct Battery
        {
            private static float _weight = 0.01184f; //kg
            private static Vector3 _dimensions = new Vector3(0.021f, 0.035f, 0.012f); //m
            private Vector3 _position;
            public Battery(Vector3 position)
            {
                _position = position;
            }
            public static float Weight { get { return _weight; } }
            public static Vector3 Dimensions { get { return _dimensions; } }
            public Vector3 Position { get { return _position; } }
        }
        public struct Motor
        {
            private static float _weight = 0.00774f; //kg
            private Vector3 _position;
            public Motor(Vector3 position)
            {
                _position = position;
            }
            public static float Weight { get { return _weight; } }
            public Vector3 Position { get { return _position; } }
        }
        public struct Propeller
        {
            private static float _weight = 0.00156f; //kg
            private Vector3 _position;
            public Propeller(Vector3 position)
            {
                _position = position;
            }
            public static float Weight { get { return _weight; } }
            public Vector3 Position { get { return _position; } }
        }
        public struct Foam
        {
            private static float _density = 0.0963f/(0.21f*0.038f*0.55f); //kg
            public static float Density { get { return _density; } }
        }
    }
}
