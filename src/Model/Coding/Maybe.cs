using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Coding
{

    public class Maybe
    {
        private Maybe()
        {
        }

        public static Maybe<T> From<T>(T value)
        {
            // Implicitly convert null to Maybe.Empty
            if (!(value is ValueType))
            {
                if (value == null)
                    return Empty;
            }

            return new Maybe<T>(value);
        }

        public static Maybe<T> From<T>(T? value) where T : struct
        {
            // Implicitly convert null to Maybe.Empty
            if (value == null)
                return Empty;

            return new Maybe<T>(value.Value);
        }
        public static Maybe<TTo> CastStruct<TTo>(object obj) where TTo : struct
        {
            if (obj is TTo)
            {
                return new Maybe<TTo>((TTo)obj);
            }
            return Maybe<TTo>.Empty;
        }

        public static Maybe<TTo> Cast<TTo>(object obj) where TTo : class
        {
            if (obj is TTo)
            {
                return new Maybe<TTo>(obj as TTo);
            }
            return Maybe<TTo>.Empty;
        }

        public static Maybe Empty
        {
            get { return new Maybe(); }
        }

    }
    /// <summary>
    /// Helper class to deal with optional values, 
    /// this supplements and replaces null able value types and references types
    /// 
    /// Construction: Maybe.From(5)
    ///               Maybe{int}.Empty
    /// Usage 1: int value
    ///          if(GetMaybe().Try(out value)
    ///             UsaValue(value)
    /// 
    /// Usage 2: var maybe = GetMaybe()
    ///          if(maybe.HasValue)
    ///             UseValue(maybe.Value)
    /// 
    /// Usage3: GetMaybe().OnValue(v => UseValue(v))
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public struct Maybe<T>
    {
        private readonly bool _hasValue;
        private readonly T _value;

        public Maybe(T value) : this(value, true) { }

        private Maybe(T value, bool hasValue)
        {
            _value = value;
            _hasValue = hasValue;
        }

        public static readonly Maybe<T> Empty = new Maybe<T>(default(T), false);

        /// <summary>
        /// Implicit conversion from untyped maybe
        /// This always represents a empty maybe so implicit convert to the correct generic type
        /// </summary>
        public static implicit operator Maybe<T>(Maybe value)
        {
            return Empty;
        }

        /// <summary>
        /// Implicit conversion are allowed from all types.
        /// Null values are converted to Empty
        /// Implicit conversion is not possible for interfaces
        /// </summary>
        public static implicit operator Maybe<T>(T value)
        {
            return Maybe.From(value);
        }

        public Maybe<TResult> Combine<T2, TResult>(Maybe<T2> p2, Func<T, T2, TResult> action)
        {
            if (_hasValue && p2.HasValue)
            {
                return action(Value, p2.Value);
            }
            return Maybe<TResult>.Empty;
        }

        public Maybe<TResult> Combine<T2, TResult>(Maybe<T2> p2, Func<T, T2, Maybe<TResult>> action)
        {
            if (_hasValue && p2.HasValue)
            {
                return action(Value, p2.Value);
            }
            return Maybe<TResult>.Empty;
        }

        public void Combine<T2>(Maybe<T2> p2, Action<T, T2> action)
        {
            if (_hasValue && p2.HasValue)
            {
                action(Value, p2.Value);
            }
        }


        /// <summary>
        /// Converts the value to another value, or returns empty
        /// </summary>
        /// <typeparam name="TResult">Resulting type</typeparam>
        /// <param name="action">Action that converts T to Tresult</param>
        /// <returns>Result from action, or Empty</returns>
        public Maybe<TResult> Convert<TResult>(Func<T, TResult> action)
        {
            if (_hasValue)
                return new Maybe<TResult>(action(_value));
            return Maybe<TResult>.Empty;
        }

        public Maybe<TResult> Convert<TResult>(Func<T, Maybe<TResult>> action)
        {
            if (_hasValue)
                return action(_value);
            return Maybe<TResult>.Empty;
        }

        /// <summary>
        /// Converts value to another value, or returns the default value
        /// </summary>
        /// <typeparam name="TResult">Type to return</typeparam>
        /// <param name="action">Action to convert</param>
        /// <param name="defaultResult">Default value</param>
        /// <returns>the result from action, or defaultResult</returns>
        public TResult Convert<TResult>(Func<T, TResult> action, TResult defaultResult)
        {
            if (_hasValue)
                return action(_value);
            return defaultResult;
        }

        public Maybe<TResult> Cast<TResult>()
        {

            if (_hasValue)
            {
                if (_value is TResult)
                {
                    // Box & cast
                    return new Maybe<TResult>((TResult)((object)_value));
                }
            }
            return Maybe<TResult>.Empty;
        }

        /// <summary>
        /// Runs function if it has a value
        /// </summary>
        /// <param name="action">action to run</param>
        /// <returns>this</returns>
        public Maybe<T> OnValue(Action<T> action)
        {
            if (_hasValue)
            {
                action(Value);
                return this;
            }
            return this;
        }
        /// <summary>
        /// Runs the action if there is no value
        /// </summary>
        /// <param name="action"> action to run</param>
        /// <returns>this</returns>
        public Maybe<T> OnEmpty(Action action)
        {
            if (!_hasValue)
            {
                action();
            }
            return this;
        }

        /// <summary>
        /// Returns the value, or a default value
        /// </summary>
        public T GetValueOrDefault()
        {
            if (_hasValue)
                return Value;
            return default(T);
        }
        /// <summary>
        /// Returns the value, or a default value
        /// </summary>
        public T GetValueOrDefault(T defaultValue)
        {
            if (_hasValue)
                return Value;
            return defaultValue;
        }

        /// <summary>
        /// Returns the value, or a default value
        /// </summary>
        public T GetValueOrDefault(Func<T> defaultValue)
        {
            if (_hasValue)
                return Value;
            return defaultValue();
        }

        /// <summary>
        /// Returns this if it has a value, or the other 
        /// </summary>
        /// <example>
        /// Returns the first of m1, m2, m3 or m4 that has a value
        /// m1.EmptyCoalesce(m2).EmptyCoalesce(m3).EmptyCoalesce(m4);
        /// </example>
        public Maybe<T> EmptyCoalesce(Maybe<T> other)
        {
            if (_hasValue)
                return this;
            return other;
        }

        /// <summary>
        /// Returns this if it has a value, or the other.
        /// </summary>
        public Maybe<T> EmptyCoalesce(Func<Maybe<T>> other)
        {
            if (_hasValue)
                return this;
            return other();
        }

        /// <summary>
        /// Returns the value modified by a selector, or a default value
        /// </summary>
        public TOut GetValueOrDefault<TOut>(Func<T, TOut> selector, TOut defaultValue)
        {
            if (_hasValue)
                return selector(Value);
            return defaultValue;
        }

        /// <summary>
        /// Tries to get the value
        /// </summary>
        /// <param name="result">value or default(T)</param>
        /// <returns>true if there is a value</returns>
        public bool TryGet(out T result)
        {
            result = _value;
            return _hasValue;
        }

        /// <summary>
        /// Returns true if there is a value set
        /// </summary>
        public bool HasValue
        {
            get { return _hasValue; }
        }

        /// <summary>
        /// The value, or throws exception
        /// </summary>
        /// <exception cref="ArgumentNullException">If there is no value</exception>
        public T Value
        {
            get
            {
                if (!_hasValue)
                    throw new ArgumentNullException("value");
                return _value;
            }
        }

        public override string ToString()
        {
            if (HasValue)
                return _value.ToString();
            return "-";
        }
        public string ToString(string defaultString)
        {
            if (HasValue)
                return _value.ToString();
            return defaultString;
        }
    }

    public static class MaybeExtensions
    {
        public static string GetValueOrEmpty(this Maybe<string> self)
        {
            return self.GetValueOrDefault("");
        }
        public static List<T> GetValueOrEmpty<T>(this Maybe<List<T>> self)
        {
            if (self.HasValue)
                return self.Value;
            return new List<T>();
        }
        public static IEnumerable<T> GetValueOrEmpty<T>(this Maybe<IEnumerable<T>> self)
        {
            if (self.HasValue)
                return self.Value;
            return Enumerable.Empty<T>();
        }
        public static T[] GetValueOrEmpty<T>(this Maybe<T[]> self)
        {
            if (self.HasValue)
                return self.Value;
            return new T[0];
        }

        public static Guid GetValueOrEmpty(this Maybe<Guid> self)
        {
            if (self.HasValue)
                return self.Value;
            return Guid.Empty;
        }
    }
}