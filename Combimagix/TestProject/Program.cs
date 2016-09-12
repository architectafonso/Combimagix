using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Combimagix;

namespace TestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] i = new int[] { 1, 2, 3, 4 };
            Loops.ForCombo<int>(2, i, (int[] a) => { Console.Write(a[0] + " " + a[1] + "   "); }, false, false);
            Console.WriteLine();
            Loops.ForCombo<int>(2, i, (int[] a) => { Console.Write(a[0] + " " + a[1] + "   "); }, true, false);
            Console.WriteLine();
            Loops.ForCombo<int>(2, i, (int[] a) => { Console.Write(a[0] + " " + a[1] + "   "); }, false, true);
            Console.WriteLine();
            Loops.ForCombo<int>(2, i, (int[] a) => { Console.Write(a[0] + " " + a[1] + "   "); }, true, true);
            Console.WriteLine("6! test, should be 6x5x4x3x2x1=720: " + ComboMath.Factorial(6));
            Console.WriteLine("Simple combination calculator test: chose 2 out of 4, should be 6: " + ComboMath.NumberOfSimpleCombinations(2, 4));
            Console.WriteLine("Combination with replacement calculator test: chose 2 out of 4, should be 10: " + ComboMath.NumberOfCombinationsWithReplacement(2, 4));
            Console.WriteLine("Simple permutation calculator test: chose 2 out of 4, should be 12: " + ComboMath.NumberOfSimplePermutations(2, 4));
            Console.WriteLine("Permutation with replacement calculator test: chose 2 out of 4, should be 16: " + ComboMath.NumberOfPermutationsWithReplacement(2, 4));
            int[][] ii = ComboMath.GetSimpleCombinations<int>(i, 2);
            foreach (int[] ia in ii)
            {
                foreach (int ie in ia) Console.Write(ie);
                Console.WriteLine();
            }
            int[] b = Loops.Append<int>(new int[] { 1, 2, 3, 4, 5 }, 12);
            foreach (int ia in b) Console.Write(ia);
        
        }
    }
}
