using System;
using System.Linq;

namespace GitAutoCommit
{
    public class ValueEventArgs<T> : EventArgs
    {
        public ValueEventArgs(T value)
        {
            Value = value;
        }

        public T Value { get; set; }
    }
}