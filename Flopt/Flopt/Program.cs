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
            Model.Del thing = Write;
            Model.DoAThing(thing);
            Airfoil af = new Airfoil(0.08f, 0.01f, 0.5f, -0.006f, 0.9f);
            Matrix mat = new Matrix(4, 4, new float[] { 1, 1, 4, 1, 2, 1, 5, 5, 3, 7, 1, 0, 0, 2, 0, 1 });
            string[] disp = mat.Invert().Display();
            //for(int i = 0; i < disp.Length; i++)
            //{
            //    Console.WriteLine(disp[i]);
            //}

            Wing wing = new Wing(0.3f, 0.07f, 0.5f, 15, 0, af);
            float ReynoldsNumber = wing.MeanAerodynamicChord * 3 * Environment.AirDensity / Environment.Viscosity(300);
            float Volume = wing.Volume;
            float Area = af.Area;
            Console.ReadLine();
            XFoil.Query("jptemp.dat", 0.3f, ReynoldsNumber, 0.1f);

            Airframe airframe = new Airframe(wing);
            Analysis.Drag(airframe, 10);
            Console.WriteLine("Total Weight: " + airframe.Weight.ToString());
            Console.ReadLine();
        }
        public static void Write()
        {
            Console.Write("Stuff");
        }
    }
}
