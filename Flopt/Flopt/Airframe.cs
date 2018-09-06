using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flopt
{
    class Airframe
    {
        private float _weight;
        private Wing _wing;
        private Components.Battery _battery;
        private Components.Receiver _receiver;
        private Components.Motor _motor;
        private Components.Propeller _prop;
        private Components.Servo[] _servos;
        public Airframe(Wing wing)
        {
            _wing = wing;
            _weight = ComputeWeight();
        }

        private float ComputeWeight()
        {
            float adjustedWingVolume = _wing.Volume - 2 * Components.Servo.Dimensions.Product() - Components.Receiver.Dimensions.Product() - Components.Battery.Dimensions.Product();
            if (adjustedWingVolume < 0)
            {
                adjustedWingVolume = 0;
            }
             return adjustedWingVolume * Components.Foam.Density + 2 * Components.Servo.Weight + Components.Receiver.Weight + Components.Battery.Weight + Components.Propeller.Weight + Components.Motor.Weight;
        }
        public float Weight { get { return _weight; } }
        public Wing Wing { get { return _wing; } }
    }
}
