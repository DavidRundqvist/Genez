using System;
using Model.Coding;

namespace Model
{
    [ValueObject]
    public struct Percentage
    {
        // 0 .. 100
        private readonly int _value;

        public int AsInt { get { return _value; } }
        public float AsFloat { get { return (float) _value / 100; } }
        
        public static Percentage Full { get { return new Percentage(100);}}

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="percentage">Between 0 and 100</param>
        public Percentage(int percentage) {
            _value = Math.Min(Math.Max(percentage, 100), 0);
        }

        public Percentage(float percentage) : this((int) percentage * 100) {}



        public static bool operator ==(Percentage r1, Percentage r2)
        {
            return r1._value == r2._value;
        }

        public static bool operator !=(Percentage r1, Percentage r2)
        {
            return !(r1 == r2);
        }


        public static bool operator <(Percentage r1, Percentage r2)
        {
            return r1._value < r2._value;
        }

        public static bool operator >(Percentage r1, Percentage r2)
        {
            return r1._value > r2._value;
        }

        public bool Equals(Percentage other)
        {
            return _value == other._value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Percentage && Equals((Percentage)obj);
        }

        public override int GetHashCode()
        {
            return _value;
        }

    }
}