using System;

namespace EcommerceAPI.Exceptions
{
    public class NullException : Exception
    {
        private const string NullMessage = "Não foram encontrado dados com as informações fornecidas!";
        public NullException()
            : base(NullMessage)
        {
        }
        public NullException(string mensagem)
            : base(mensagem)
        {
        }
        public NullException(Exception exception)
            : base(NullMessage, exception)
        {
        }
    }
}
