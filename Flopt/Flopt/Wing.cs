using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flopt
{
    class Wing
    {
        private float _span;
        private float _area;
        private float _taper;
        private float _sweep;
        private float _dihedral;
        private Airfoil _foil;
        private Vector3 _centroid;
        public Wing(float span, float area, float taper, float sweep, float dihedral, Airfoil foil)
        {
            _span = span;
            _area = area;
            _taper = taper;
            _sweep = sweep;
            _dihedral = dihedral;
            _foil = foil;
            _centroid = new Vector3(0, GetCentroid(_foil.Centroid.X, _sweep), GetCentroid(_foil.Centroid.Y, _dihedral));

        }

        public float MeanAerodynamicChord { get { return (4 * _area * (1 + _taper + (float)Math.Pow(_taper, 2)) / (3 * _span * (float)Math.Pow(1 + _taper, 2))); } }
        public float Volume { get { return (4 * _foil.Area * (float)Math.Pow(_area, 2) * (1 + _taper + (float)Math.Pow(_taper, 2)) / (3 * _span * (float)Math.Pow(1 + _taper, 2))); } }
        public float Area { get { return _area; } }
        public float AspectRatio { get { return (float)Math.Pow(_span, 2) / _area; } }
        public float Span { get { return _span; } }
        public Vector3 Centroid { get { return _centroid; } }
        public Airfoil Foil { get { return _foil; } }
        private float GetCentroid(float sBar, float angle)
        {
            return (float)(3 * (Math.Pow(_taper, 2) + 2 * _taper / 3 + 1 / 3) * Math.Pow(_span, 2) * Math.Tan(angle * Math.PI / 180) + 12 * sBar * _area * (Math.Pow(_taper, 2) + 1)) /
                (float)(8 * _span * (Math.Pow(_taper, 2) + _taper + 1));
        }


    }
}
