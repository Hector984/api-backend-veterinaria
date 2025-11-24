using System.Runtime.Serialization;

namespace API_Veterinaria.Exceptions
{
    public class NotActiveException : Exception
    {
        public NotActiveException()
        {
        }

        public NotActiveException(string? message) : base(message)
        {
        }

        public NotActiveException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
