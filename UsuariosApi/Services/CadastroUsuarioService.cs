using AutoMapper;
using Dapper;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using UsuariosApi.Data.Dto;
using UsuariosApi.Data.Request;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class CadastroUsuarioService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public CadastroUsuarioService(IMapper mapper, UserManager<CustomIdentityUser> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Result> CadastraUsuario(CreateUsuarioDto createDto)
        {
            var endereco = await BuscarEnderecoPorCep(createDto.CEP);
            createDto.Logradouro = endereco.Logradouro;
            createDto.Bairro = endereco.Bairro;
            createDto.Localidade = endereco.Localidade;
            createDto.UF = endereco.UF;

            bool cpfValido = VerificaCPF(createDto.CPF);
            bool dataNascimentoValida = DataDeNascimentoValida(createDto.DataNascimento);

            Usuario usuario = _mapper.Map<Usuario>(createDto);
            CustomIdentityUser usuarioIdentity = _mapper.Map<CustomIdentityUser>(usuario);
            Task<IdentityResult> resultadoIdentity = _userManager
                .CreateAsync(usuarioIdentity, createDto.Password);
            var usuarioRoleResult = _userManager
                .AddToRoleAsync(usuarioIdentity, "lojista").Result;
            var createRoleResult = _roleManager
                .CreateAsync(new IdentityRole<int>("lojista")).Result;
            if (cpfValido != true)
            {
                return Result.Fail("CPF inválido! Tente novamente com um CPF válido.");
            }

            if (dataNascimentoValida != true)
            {
                return Result.Fail("A data de nascimento informada não é permitida! Informe uma data menor que a data atual.");
            }

            if (resultadoIdentity.Result.Succeeded)
            {
                var code = _userManager.GenerateEmailConfirmationTokenAsync(usuarioIdentity).Result;
                var encodedCode = HttpUtility.UrlEncode(code);
                return Result.Ok().WithSuccess(encodedCode);
            }

            return Result.Fail("Falha ao cadastrar usuário");
        }

        public async Task<List<ReadUsuarioDto>> PesquisaUsuarios(FiltroDto filtroDto)
        {
            var usuarios = await _userManager.Users.ToListAsync();
            List<ReadUsuarioDto> readDto = new();

            foreach (var usuario in usuarios)
            {
                var user = _mapper.Map<ReadUsuarioDto>(usuario);
                readDto.Add(user);
            }

            if (filtroDto.UserName != null)
            {
                return readDto.Where(usuario => usuario.UserName.ToLower().Contains(filtroDto.UserName.ToLower())).ToList();
            }
            if (filtroDto.CPF != null)
            {
                return readDto.Where(usuario => usuario.CPF == filtroDto.CPF).ToList();
            }
            if (filtroDto.Email != null)
            {
                return readDto.Where(usuario => usuario.Email.ToLower().Contains(filtroDto.Email.ToLower())).ToList();
            }
            if (filtroDto.Status != null)
            {
                return readDto.Where(usuario => usuario.Status == filtroDto.Status).ToList();
            }
            return null;
        }

        public Result AtivaContaUsuario(AtivaContaRequest request)
        {
            var identityUser = _userManager
                .Users
                .FirstOrDefault(user => user.Id == request.UsuarioId);
            var identityResult = _userManager
                .ConfirmEmailAsync(identityUser, request.CodigoDeAtivacao).Result;
            if (identityResult.Succeeded)
            {
                return Result.Ok();
            }
            return Result.Fail("Falha ao ativar conta de usuário!");

        }

        public async Task<Result> EditaUsuario(int id, EditarUsuarioDto editarDto)
        {
            var usuario = _userManager.Users.FirstOrDefault(usuario => usuario.Id == id);
            if (usuario != null)
            {
                if (editarDto.CEP != null)
                {
                    var endereco = await BuscarEnderecoPorCep(editarDto.CEP);
                    editarDto.Logradouro = endereco.Logradouro;
                    editarDto.Bairro = endereco.Bairro;
                    editarDto.Localidade = endereco.Localidade;
                    editarDto.UF = endereco.UF;

                    var mapeamento= _mapper.Map(editarDto, usuario);
                    await _userManager.UpdateAsync(usuario);
                    return Result.Ok();
                }
                var user = _mapper.Map(editarDto, usuario);
                await _userManager.UpdateAsync(usuario);
                return Result.Ok();
            }
            return Result.Fail("Não foi possível identificar o usuário informado!");

        }

        public async Task<CreateUsuarioDto> BuscarEnderecoPorCep(string cep)
        {
            HttpClient client = new HttpClient();

            var resultado = await client.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
            var informacoes = await resultado.Content.ReadAsStringAsync();

            var endereco = JsonConvert.DeserializeObject<CreateUsuarioDto>(informacoes);

            return endereco;

        }

        public bool DataDeNascimentoValida(DateTime dataNascimento)
        {
            if (dataNascimento < DateTime.Today)
            {
                return true;
            }
            return false;
        }

        public static bool VerificaCPF(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
    }
}
