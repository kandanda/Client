using System;

namespace Kandanda.BusinessLayer
{
    public sealed class DataAccessException : Exception
    {
        public DataAccessException(string message)
            : base(message)
        {   
        }

        public DataAccessException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public DataAccessException(Exception innerException)
            : base(innerException.Message, innerException)
        {
        }
    }
}
