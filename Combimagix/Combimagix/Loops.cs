using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combimagix
{

    public static class Loops
    {
        /// <summary>
        /// Executes custom Action<T[]> against every combination of elements in collection.
        /// </summary>
        /// <typeparam name="T"> The type of elements in the colection. </typeparam>
        /// <param name="numberOfItems"> The number of items in each combination to be returned. </param>
        /// <param name="collection"> The collection from each to return the items. </param>
        /// <param name="customAction"> Action that has an array as parameter and executes actions upon the item in the combination. </param>
        /// <param name="isPermutation"> If set true, the order will matter (both AB and BA will be returned). </param>
        /// <param name="withReplacement"> If set true, the same objet will appear more than once in an combination (AA, AB, BB ...). </param>
        public static void ForCombo<T>(int numberOfItems, ICollection<T> collection, Action<T[]> customAction, bool isPermutation, bool withReplacement)
        {
            if (isPermutation && withReplacement) PermutationWithReplacement<T>(new T[0], numberOfItems, collection, customAction);
            if (isPermutation && !withReplacement) SimplePermutation<T>(new T[0], numberOfItems, collection, customAction);
            if (!isPermutation && withReplacement) CombinationWithReplacement<T>(new T[0], numberOfItems, collection, customAction);
            if (!(isPermutation || withReplacement)) SimpleCombination(new T[0], numberOfItems, collection, customAction);
        }

        /// <summary>
        /// Executes the customAction once for each permutation with replacement of
        /// specified number of items of the specified collection.
        /// Example: {2,3,4}, 2 = 22, 23, 24, 32, 33, 34, 42, 43, 44.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="numberOfItems"></param>
        /// <param name="collection"></param>
        /// <param name="customAction"></param>
        private static void PermutationWithReplacement<T>(T[] array, int numberOfItems, ICollection<T> collection, Action<T[]> customAction)
        {
            if (numberOfItems == 0) return;
            if (numberOfItems < 0) throw new Exception("Gimme a number I can work with, you idiot!");
            // If the number of items requested is 1, simply does a foreach, appends to the array and
            // executes customAction with that array as parameter.
            if (numberOfItems == 1)
            {
                foreach (T t in collection) customAction(Append(array, t));
            }
            else
            {
                // If the number is greater than 1, then foreach member of the collection, appends to
                // the array and calls this method recursively.
                foreach (T t in collection)
                    PermutationWithReplacement<T>(Append(array, t), numberOfItems - 1, collection, customAction);
            }
        }

        /// <summary>
        /// Executes the customAction once for each simple permutation of
        /// specified number of items of the specified collection.
        /// Example: {2,3,4}, 2 = 23, 24, 32, 34, 42, 43.
        /// </summary>
        /// <typeparam name="T"> The type of items in the collection. </typeparam>
        /// <param name="array"> An array, inicially empty, to get the values and be passed to customAction</param>
        /// <param name="numberOfItems"></param>
        /// <param name="collection"></param>
        /// <param name="customAction"></param>
        private static void SimplePermutation<T>(T[] array, int numberOfItems, ICollection<T> collection, Action<T[]> customAction)
        {
            if (numberOfItems == 0) return;
            if (numberOfItems < 0) throw new Exception("Gimme a number I can work with, you idiot!");
            // If the requested number of items is 1, then foreach member of the collection checks if it is already in 
            // the array, and if not appends it to the array and calls customAction giving the array as parameter.
            if (numberOfItems == 1)
            {
                foreach (T t in collection)
                {
                    if (!array.Contains(t)) customAction(Append(array, t));
                }
            }
            // If the requested number of items is greater than 1, then foreach member of the collection checks if
            // it is already in the collection, if it isn't then appends to the array.
            else
            {
                foreach (T t in collection)
                {
                    if (!array.Contains<T>(t))
                        SimplePermutation<T>(Append(array, t), numberOfItems - 1, collection, customAction);
                }
            }
        }


        private static void CombinationWithReplacement<T>(T[] array, int numberOfItems, ICollection<T> collection, Action<T[]> customAction)
        {
            if (numberOfItems == 0) return;
            if (numberOfItems < 0) throw new Exception("Gimme a number I can work with, you idiot!");
            // If the number of requested objects in the combination is 1, then foreach object in the collection
            // appends the object to the array and calls custom action giving the array as parameter.
            if (numberOfItems == 1)
            {
                T[] ar = new T[array.Length + 1];
                for (int index = 0; index < array.Length; index++) ar[index] = array[index];
                foreach (T t in collection) { ar[array.Length] = t; customAction(ar); }
            }
            // If the number of requested objects per combination is greateer than 1, then foreach object in the
            // collection it will be appended to the array and then NextCombinationWithReplacement will be called
            // receiving the array and other variables from this method plus the reference to the object put into
            // the array as the parameter "last".
            else
            {
                T[] ar = new T[array.Length + 1];
                for (int index = 0; index < array.Length; index++) ar[index] = array[index];

                foreach (T t in collection)
                { ar[array.Length] = t; NextCombinationWithReplacement<T>(ar, numberOfItems - 1, collection, customAction, t); }
            }
        }

        private static void NextCombinationWithReplacement<T>(T[] array, int numberOfItems, ICollection<T> collection, Action<T[]> customAction, T last)
        {
            if (numberOfItems == 0) return;
            if (numberOfItems < 0) throw new Exception("Gimme a number I can work with, you idiot!");
            // If the number of requested objects per combination is one, then foreach object in the collection
            // in a position after the last object, it will be appended to the array and then custom action
            // will be called with the array as parameter.
            if (numberOfItems == 1)
            {
                bool reached = false;
                foreach (T t in collection)
                {
                    if (t.Equals(last)) reached = true;
                    if (reached) customAction(Append(array, t)); 
                }
            }
            else
            {
                // If the number os objects per combination is greater than 1, then foreach object in the collection
                // after last, it will be appended to the array, and then this method will be called recursively
                // with that object as last.
                bool reached = false;
                foreach (T t in collection)
                {
                    if (t.Equals(last)) reached = true;
                    if (reached)
                        NextCombinationWithReplacement<T>(
                            Append(array, t), numberOfItems - 1, collection, customAction, t);
                }
            }
        }

        private static void SimpleCombination<T>(T[] array, int numberOfItems, ICollection<T> collection, Action<T[]> customAction)
        {
            if (numberOfItems == 0) return;
            if (numberOfItems < 0) throw new Exception("Gimme a number I can work with, you idiot!");
            // ...
            if (numberOfItems == 1)
            {
                foreach (T t in collection) customAction(Append(array, t)); 
            }
            else
            {
                foreach (T t in collection)
                NextSimpleCombination<T>(Append(array, t), numberOfItems - 1, collection, customAction, t); 
            }
        }

        private static void NextSimpleCombination<T>(T[] array, int numberOfItems, ICollection<T> collection, Action<T[]> customAction, T last)
        {
            if (numberOfItems == 0) return;
            if (numberOfItems < 0) throw new Exception("Gimme a number I can work with, you idiot!");
            if (numberOfItems == 1)
            {
                bool reached = false;
                foreach (T t in collection)
                {
                    if (t.Equals(last)) reached = true;
                    if (reached && !array.Contains(t)) customAction(Append(array, t));
                }
            }
            else
            {
                bool reached = false;
                foreach (T t in collection)
                {
                    if (t.Equals(last)) reached = true;
                    if (reached && !array.Contains(t))
                        NextSimpleCombination<T>(Append(array, t), numberOfItems - 1, collection, customAction, t);
                }
            }
        }

        /// <summary>
        /// Helper method that appends a value to an array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static T[] Append<T>(T[] array, T val)
        {
            int length = array.Length;
            T[] ar = new T[length + 1];
            for (int index = 0; index < length; index++)
            {
                ar[index] = array[index];
            }
            ar[length] = val;
            return ar;
        }
    }

}
