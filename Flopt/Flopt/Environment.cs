using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flopt
{
    static class Environment
    {
        public const float AirDensity = 1.225f; //kg/m^3
        public const float Gravity = 9.807f; //m/s^2
        public static float Viscosity(float temperature)
        {
            return 0.000008254128431755644f*(float)Math.Exp(0.002724964244738*temperature);
        }
        public static float DynamicPressure(float velocity)
        {
            return (float)Math.Pow(velocity,2)*AirDensity/2;
        }
    }
}
