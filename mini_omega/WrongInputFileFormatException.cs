using System;
using System.Runtime.Serialization;

namespace mini_omega
{
    public class WrongInputFileFormatException : Exception
    {
         public string FilePath { get; }
        public WrongInputFileFormatException()
        {
        }

        public WrongInputFileFormatException(string message) : base(message)
        {
        }

        public WrongInputFileFormatException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WrongInputFileFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
            public WrongInputFileFormatException(string message, string filePath)
        : this(message)
    {
       FilePath = filePath;
    }


    }
}