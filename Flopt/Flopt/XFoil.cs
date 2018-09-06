using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flopt
{
    class XFoil
    {
        public static float[] Query(string airfoil,float liftCoefficient, float reynoldsNumber, float machNumber)
        {
            List<string> lines = new List<string>();
            lines.Add("load " + airfoil);
            lines.Add("");
            lines.Add("oper");
            lines.Add("iter");
            lines.Add("150");
            lines.Add("");
            lines.Add("");
            lines.Add("");
            lines.Add("oper");
            lines.Add("re " + reynoldsNumber.ToString());
            lines.Add("mach " + machNumber.ToString());
            lines.Add("visc");
            lines.Add("pacc");
            lines.Add("");
            lines.Add("");
            lines.Add("cl "+liftCoefficient);
            lines.Add("pwrt");
            lines.Add("polar.txt");
            lines.Add("plis");
            lines.Add("");
            lines.Add("quit");
            File.WriteAllLines("C:/users/jonathan.platner/Desktop/xfoilinput.txt", lines.ToArray());
            System.Diagnostics.Process cmd =  System.Diagnostics.Process.Start("CMD.exe", "/C cd C:/Users/jonathan.platner/Desktop && xfoil.exe < xfoilinput.txt > xfoiloutput.txt");
            cmd.WaitForExit(5000);
            string[] polar = File.ReadAllLines("C:/users/jonathan.platner/Desktop/polar.txt");
            string[] data = polar[polar.Length - 1].Split(new char[0],StringSplitOptions.RemoveEmptyEntries);
            float[] values = new float[7];
            for (int i = 0; i < 7; i++)
            {
                values[i] = (float)Double.Parse(data[i]);
            }
            return values;
        }
    }
}
