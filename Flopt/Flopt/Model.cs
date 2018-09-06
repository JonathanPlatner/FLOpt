using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flopt
{
    class Model
    {
        public delegate void Del();
        private int _numSamples;
        public Model(int numVars)
        {
            _numSamples = 1 + 2 * numVars + (numVars - 1) * (numVars) / 2;
        }

        public static void DoAThing(Del del)
        {
            del();
        }
    }
}
