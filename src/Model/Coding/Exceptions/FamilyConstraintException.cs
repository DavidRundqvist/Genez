using System;
using System.Linq;
using System.Runtime.Serialization;

namespace Model.Coding.Exceptions
{
    /// <summary>
    /// This exception represents impossible family combinations, such as a person having several biological (certain) mothers
    /// </summary>
    [Serializable]
    public class FamilyConstraintException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //


        public FamilyConstraintException(string[] messages) : base(string.Join(", ", messages))
        {
        }

        public FamilyConstraintException(string message, Exception inner) : base(message, inner)
        {
        }


        protected FamilyConstraintException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }

}