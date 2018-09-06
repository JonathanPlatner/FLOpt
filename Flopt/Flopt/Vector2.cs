using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flopt
{
    class Vector2
    {
        private float _x;
        private float _y;
        public Vector2(float x, float y)
        {
            _x = x;
            _y = y;
        }
        public float X { get { return _x; } }
        public float Y { get { return _y; } }
    }
}
