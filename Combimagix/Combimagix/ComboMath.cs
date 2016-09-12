using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combimagix
{
    public static class ComboMath
    {
        public static int Factorial(int value)
        {
            if (value < 0) throw new Exception("Can't accept an negative number, do you even math????");
            if (value == 0 || value == 1) return 1;
            int result = 1;
            while(value > 1)
            {
                result *= value;
                value--;
            }
            return result;
        }


        public static int NumberOfSimpleCombinations(int numberOfElementsPerCombination, int numberOfElementsInTheSet)
        {
            if (numberOfElementsPerCombination < 0 || numberOfElementsInTheSet < 0)
                throw new Exception("Gimme a number I can work with, you idiot!");
            return (Factorial(numberOfElementsInTheSet)) /
                (Factorial(numberOfElementsPerCombination) * 
                Factorial(numberOfElementsInTheSet - numberOfElementsPerCombination));
        }

        public static int NumberOfSimpleCombinations<T>(int numberOfElementsPerCombination, ICollection<T> collection)
        {
            return NumberOfSimpleCombinations(numberOfElementsPerCombination, collection.Count);
        }




        public static int NumberOfCombinationsWithReplacement(int numberOfElementsPerCombination, int numberOfElementsInTheSet)
        {
            if (numberOfElementsPerCombination < 0 || numberOfElementsInTheSet < 0)
                throw new Exception("Gimme a number I can work with, you idiot!");
            return (Factorial(numberOfElementsInTheSet + numberOfElementsPerCombination - 1) / (
                Factorial(numberOfElementsPerCombination) * Factorial(numberOfElementsInTheSet - 1)));
        }

        public static int NumberOfCombinationsWithReplacement<T>(int numberOfElementsPerCombination, ICollection<T> collection)
        {
            return NumberOfCombinationsWithReplacement(numberOfElementsPerCombination, collection.Count);
        }







        public static int NumberOfSimplePermutations(int numberOfElementsPerCombination, int numberOfElementsInTheSet)
        {
            if (numberOfElementsPerCombination < 0 || numberOfElementsInTheSet < 0)
                throw new Exception("Gimme a number I can work with, you idiot!");
            return (Factorial(numberOfElementsInTheSet) / 
                Factorial(numberOfElementsInTheSet - numberOfElementsPerCombination));
        }

        public static int NumberOfSimplePermutations<T>(int numberOfElementsPerCombination, ICollection<T> collection)
        {
            return NumberOfSimplePermutations(numberOfElementsPerCombination, collection.Count);
        }




        public static int NumberOfPermutationsWithReplacement(
            int numberOfElementsPerPermutation, int numberOfElementsInTheSet)
        {
            if (numberOfElementsPerPermutation < 0 || numberOfElementsInTheSet < 0)
                throw new Exception("Gimme a number I can work with, you idiot!");
            return (int)Math.Pow(numberOfElementsInTheSet, numberOfElementsPerPermutation);
        }

        public static int NumberOfPermutationsWithReplacement<T>(
            int numberOfElementsPerPermutation, ICollection<T> collection)
        {
            return NumberOfPermutationsWithReplacement(numberOfElementsPerPermutation, collection.Count);
        }





        public static T[][] GetSimpleCombinations<T>(this ICollection<T> collection, int numberOfElementsPerCombination)
        {
            T[][] result = new T[NumberOfSimpleCombinations(numberOfElementsPerCombination, collection)][];
            int index = 0;
            Loops.ForCombo<T>(numberOfElementsPerCombination, collection,
                (T[] a) => { result[index] = a; index++; }, false, false);
            return result;
        }

        public static T[][] GetCombinationsWithReplacement<T>(this ICollection<T> collection, int numberOfElementsPerCombination)
        {
            T[][] result = new T[NumberOfCombinationsWithReplacement(numberOfElementsPerCombination, collection)][];
            int index = 0;
            Loops.ForCombo<T>(numberOfElementsPerCombination, collection,
                (T[] a) => { result[index] = a; index++; }, false, true);
            return result;
        }

        public static T[][] GetSimplePermutations<T>(this ICollection<T> collection, int numberOfElementsPerCombination)
        {
            T[][] result = new T[NumberOfSimplePermutations(numberOfElementsPerCombination, collection)][];
            int index = 0;
            Loops.ForCombo<T>(numberOfElementsPerCombination, collection,
                (T[] a) => { result[index] = a; index++; }, false, true);
            return result;
        }

        public static T[][] GetPermutationsWithReplacement<T>(this ICollection<T> collection, int numberOfElementsPerCombination)
        {
            T[][] result = new T[NumberOfPermutationsWithReplacement<T>(numberOfElementsPerCombination, collection)][];
            int index = 0;       
            Loops.ForCombo<T>(numberOfElementsPerCombination, collection,
                (T[] a) => { result[index] = a; index++; }, false, true);
            return result;
        }
    }
}
