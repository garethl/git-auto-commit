using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GitAutoCommit
{
    public static class Extensions
    {
        /// <summary>
        /// Move an item in a list
        /// </summary>
        /// <param name="list">List</param>
        /// <param name="index">Index of the item</param>
        /// <param name="difference">Direction to move (-ve for up, +ve for down. The actual value determines how many places)</param>
        public static void Move(this IList list, int index, int difference)
        {
            var newIndex2 = index + difference;

            var temp1 = list[index];

            list.RemoveAt(index);
            list.Insert(newIndex2, temp1);
        }

        /// <summary>
        /// Move an item in a list
        /// </summary>
        /// <typeparam name="T">Type of item</typeparam>
        /// <param name="list">List</param>
        /// <param name="index">Index of the item</param>
        /// <param name="difference">Direction to move (-ve for up, +ve for down. The actual value determines how many places)</param>
        public static void Move<T>(this IList<T> list, int index, int difference)
        {
            var newIndex2 = index + difference;

            var temp1 = list[index];

            list.RemoveAt(index);
            list.Insert(newIndex2, temp1);
        }
    }
}