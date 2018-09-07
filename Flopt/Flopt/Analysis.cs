using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flopt
{
    class Analysis
    {
        private static float ParasiticDrag(Wing wing, float liftCoefficient, float velocity)
        {
            float reynoldsNumber = Environment.AirDensity * velocity * wing.MeanAerodynamicChord / Environment.Viscosity(300);
            float machNumber = velocity / 343;
            float[] data = XFoil.Query(wing.Foil.Name + ".dat", liftCoefficient, reynoldsNumber, machNumber);
            return Environment.DynamicPressure(velocity) * wing.Area * data[2];
        }
        private static float InducedDrag(Wing wing, float liftCoefficient, float velocity)
        {
            return Environment.DynamicPressure(velocity) * wing.Area * (float)(Math.Pow(liftCoefficient, 2) / (Math.PI * wing.AspectRatio * 0.9f));
        }
        public static float Drag(Airframe af, float velocity)
        {
            float liftCoefficient = af.Weight * Environment.Gravity / Environment.DynamicPressure(velocity) / af.Wing.Area;
            float parasiticDrag = ParasiticDrag(af.Wing, liftCoefficient, velocity);
            float inducedDrag = InducedDrag(af.Wing, liftCoefficient, velocity);
            Console.WriteLine("Parasite Drag: " + parasiticDrag.ToString());
            Console.WriteLine("Induced Drag: " + inducedDrag.ToString());
            Console.WriteLine("Total Drag: " + (inducedDrag + parasiticDrag).ToString());
            return inducedDrag + parasiticDrag;
        }
        public static float AeroSample(float[] inputs) //{foil thickness, foil camber, foil reflex, wing span, wing area, wing taper, velocity}
        {
            Airfoil foil = new Airfoil(inputs[0], inputs[1], 0.4f, inputs[2], 0.7f);
            Wing wing = new Wing(inputs[3], inputs[4], inputs[5], 0, 0, foil);
            Airframe airframe = new Airframe(wing);
            return Drag(airframe, inputs[6]);
        }
        public static float Weight(Airframe af)
        {
            return 0;
        }
    }
}
