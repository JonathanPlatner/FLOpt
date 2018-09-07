using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flopt
{
    class Model
    {
        public delegate float Sample(float[] inputs);
        private Sample _sampleFunction;
        private int _numSamples;
        private float[] _lowerBounds;
        private float[] _upperBounds;
        private float[][] _x;
        private float[] _y;
        public Model(Sample sampleFunction,float[] lowerBounds, float[] upperBounds)
        {
            _sampleFunction = sampleFunction;
            _numSamples = (lowerBounds.Length + 2) * (lowerBounds.Length + 1) / 2;
            _lowerBounds = lowerBounds;
            _upperBounds = upperBounds;
            _x = new float[_numSamples][];
            _y = new float[_numSamples];
            for (int i = 0; i < _numSamples; i++)
            {
                _x[i] = new float[_lowerBounds.Length];
                for (int j = 0; j < _lowerBounds.Length; j++)
                {
                    _x[i][j] = _lowerBounds[j] + (_upperBounds[j] - _lowerBounds[j]) * i / (_numSamples - 1);
                }
            }
        }

        public void Build()
        {
            for (int i = 0; i < _numSamples; i++)
            {
                _y[i] = SampleSpace(_sampleFunction, _x[i]);
            }
        }
        public float SampleSpace(Sample sample,float[] inputs)
        {
            return sample(inputs);
        }
    }
}
