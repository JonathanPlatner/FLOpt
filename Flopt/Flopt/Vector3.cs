using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flopt
{
    class Vector3
    {
        private float _x;
        private float _y;
        private float _z;
        public Vector3(float x, float y)
        {
            _x = x;
            _y = y;
            _z = 0;
        }
        public Vector3(float x, float y,float z)
        {
            _x = x;
            _y = y;
            _z = z;
        }
        public float X { get { return _x; } }
        public float Y { get { return _y; } }
        public float Z { get { return _z; } }
        public float Product()
        {
            return _x * _y * _z;
        }
        public Vector3 Zero { get { return new Vector3(0, 0, 0); } }
    }
}
