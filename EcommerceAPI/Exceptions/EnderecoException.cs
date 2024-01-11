using System;

namespace EcommerceAPI.Exceptions
{
    public class EnderecoException : Exception
    {
        private const string EnderecoMessage = "O endereço informado não é único!";

        public EnderecoException() 
            : base(EnderecoMessage)
        {
        }

        public EnderecoException(string mensagem)
            : base(mensagem)
        {
        }

        public EnderecoException(Exception exception)
            : base(EnderecoMessage, exception)
        {
        }
    }
}
