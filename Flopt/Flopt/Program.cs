using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flopt
{
    class Program
    {
        static void Main(string[] args)
        {
            Model aeroModel = new Model(Analysis.AeroSample, new float[] { 0.05f,-0.03f,-0.005f,0.5f,0.075f,0,4}, new float[] { 0.3f, 0.05f, 0.01f, 5, 1, 1, 20 });
            aeroModel.Build();
            //float result = Model.SampleSpace(Analysis.AeroSample, new float[] {0.1f,0.02f,0,1,0.23f,0.65f,5 });
            Airfoil af = new Airfoil(0.08f, 0.01f, 0.4f, -0.006f, 0.7f);

            Wing wing = new Wing(0.3f, 0.07f, 0.5f, 15, 0, af);
            float ReynoldsNumber = wing.MeanAerodynamicChord * 3 * Environment.AirDensity / Environment.Viscosity(300);
            Console.ReadLine();

            Airframe airframe = new Airframe(wing);
            Analysis.Drag(airframe, 10);
            Console.WriteLine("Total Weight: " + airframe.Weight.ToString());
            Console.ReadLine();
        }

    }
}
