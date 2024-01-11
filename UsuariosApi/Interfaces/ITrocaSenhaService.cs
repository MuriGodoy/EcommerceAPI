using FluentResults;
using System;
using UsuariosApi.Data.Request;

namespace UsuariosApi.Interfaces
{
    public interface ITrocaSenhaService : IDisposable
    {
        public Result TrocaSenha(TrocaSenhaRequest request);
    }
}
