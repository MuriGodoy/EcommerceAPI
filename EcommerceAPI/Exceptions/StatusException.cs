using System;

namespace EcommerceAPI.Exceptions
{
    public class StatusException : Exception
    {
        private const string mensagemFixa = "Não é possível cadastrar com status inativo!";

        public StatusException()
            : base(mensagemFixa)
        {
        }
        public StatusException(string mensagem)
            :base(mensagem)
        {
        }
        public StatusException(Exception exception)
            :base(mensagemFixa, exception)
        {
        }
    }
}
