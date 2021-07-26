//----------------------------------------------------------------------- 
// Copyright (c) 2019 All rights reserved.
// </copyright>
// <author>Jorge Pertuz Egea/Jpertuz</author>
// <date>Julio 2021</date>
//-----------------------------------------------------------------------

using BCrypt.Net;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Proyecto_BLL.Interfaces;
using Proyecto_DAO.Entitys;
using Proyecto_DAO.Interfaces;
using Proyecto_DAO.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Proyecto_BLL
{

    public class Bll_Users : I_Bll_Users
    {
        private readonly IUnitOfWork unitofwork;
        private readonly IConfiguration configuration;
        private readonly ILogger<Bll_Users> logger;
        private readonly IMemoryCache memoryCache;

        private static readonly HttpClient client = new HttpClient();

        public Bll_Users(IUnitOfWork _unitofwork, IConfiguration _configuration, ILogger<Bll_Users> logger, IMemoryCache _memoryCache)
        {
            this.unitofwork = _unitofwork;
            this.configuration = _configuration;
            this.logger = logger;
            this.memoryCache = _memoryCache;
        }


        public async Task<ResultModel> UserAdd(Users UserModel)
        {
            ResultModel ResultModel = new ResultModel();

            try
            {
                Users User = (await unitofwork.GetRepository<Users>().Get(x => x.UserName == UserModel.UserName)).FirstOrDefault();

                if (User != null)
                {
                    ResultModel.HasError = true;
                    ResultModel.Messages = "Nombre ya Existe";
                    ResultModel.Data = string.Empty;
                    return ResultModel;
                }

                UserModel.Password = ComputeHash(UserModel.Password, HashType.SHA256);// Encripta la clave  
                unitofwork.GetRepository<Users>().Add(UserModel);

                if (!unitofwork.SaveChanges())
                {
                    ResultModel.HasError = true;
                    ResultModel.Messages = "User No Creado";
                    ResultModel.Data = string.Empty;
                    return ResultModel;
                }

                ResultModel.HasError = false;
                ResultModel.Messages = "User Creado Con Exito";
                ResultModel.Data = string.Empty;

                return ResultModel;
            }
            catch (Exception Error)
            {
                ResultModel.HasError = true;
                ResultModel.Messages = $"Error Técnico Al Guardar User: {Error.Message}";
                ResultModel.Data = Error;

                logger.LogError($"Error: {Error.ToString()}");

                return ResultModel;
            }
        }

        public async Task<ResultModel> UserList()
        {
            ResultModel ResultModel = new ResultModel();

            try
            {
                IEnumerable<Users> List = await unitofwork.GetRepository<Users>().Get();
                ResultModel.HasError = false;

                Users[] DTOList = List.Select(item =>
                {
                    item.Password = "";
                    return item;
                }).ToArray();

                ResultModel.Messages = "User Listados Con Exito";
                ResultModel.Data = DTOList;

                return ResultModel;
            }
            catch (Exception Error)
            {
                ResultModel.HasError = true;
                ResultModel.Messages = "Error Técnico Listando User";
                ResultModel.Data = Error;

                logger.LogError($"Error: {Error.ToString()}");

                return ResultModel;
            }
        }

        public async Task<ResultModel> GetUserByUserId(int Id)
        {
            ResultModel ResultModel = new ResultModel();

            try
            {
                ResultModel Result = await UserList();

                if (!Result.HasError)
                {
                    Users[] List = (Users[])Result.Data;

                    Users User = List.Where(x => x.Id == Id).FirstOrDefault();

                    if (User != null)
                    {

                        ResultModel.HasError = false;
                        ResultModel.Messages = "User Encontrado";
                        ResultModel.Data = User;
                    }
                    else
                    {
                        ResultModel.HasError = false;
                        ResultModel.Messages = "User No Encontrado";
                        ResultModel.Data = string.Empty;
                    }
                }

                return ResultModel;
            }
            catch (Exception Error)
            {
                ResultModel.HasError = true;
                ResultModel.Messages = "Error Técnico Buscando Usuario";
                ResultModel.Data = Error;

                logger.LogError($"Error: {Error.ToString()}");

                return ResultModel;
            }
        }

        public async Task<string> GetFullNameByUserId(int? Id)
        {
            try
            {
                if (Id == null || Id <= 0)
                {
                    return "";
                }

                ResultModel Result = await GetUserByUserId((int)Id);
                return ((Users)Result.Data).UserName.ToString();
            }
            catch (Exception Error)
            {
                logger.LogError($"Error: {Error.ToString()}");
                return "";
            }
        }

        public async Task<ResultModel> UserUpdate(Users UserModel)
        {
            ResultModel ResultModel = new ResultModel();
            try
            {
                Users UserUpdate = (await unitofwork.GetRepository<Users>().Get(x => x.Id == UserModel.Id)).FirstOrDefault();

                if (UserUpdate == null)
                {
                    ResultModel.HasError = true;
                    ResultModel.Messages = "Usuario NO Encontrado";
                    ResultModel.Data = string.Empty;
                    return ResultModel;
                }

                Users User = (await unitofwork.GetRepository<Users>().Get(x => x.UserName == UserModel.UserName && x.Id != UserModel.Id)).FirstOrDefault();

                if (User != null)
                {
                    ResultModel.HasError = true;
                    ResultModel.Messages = "Nombre ya Existe";
                    ResultModel.Data = string.Empty;
                    return ResultModel;
                }

                UserUpdate.UserName = UserModel.UserName;
                UserUpdate.Password = ComputeHash(UserModel.Password, HashType.SHA256);

                unitofwork.GetRepository<Users>().Update(UserUpdate);

                if (!unitofwork.SaveChanges())
                {
                    ResultModel.HasError = true;
                    ResultModel.Messages = "User No Modificado";
                    ResultModel.Data = string.Empty;
                    return ResultModel;
                }

                ResultModel.HasError = false;
                ResultModel.Messages = "User Modificado Con Exito";
                ResultModel.Data = string.Empty;

                return ResultModel;
            }
            catch (Exception Error)
            {
                ResultModel.HasError = true;
                ResultModel.Messages = $"Error Técnico Al Modificar User: {Error.Message}";
                ResultModel.Data = Error;

                logger.LogError($"Error: {Error.ToString()}");

                return ResultModel;
            }
        }

        public async Task<ResultModel> Sincronizate()
        {
            ResultModel ResultModel = new ResultModel();

            try
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
                client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

                var stringTask = client.GetStringAsync("https://fakerestapi.azurewebsites.net/api/v1/Users");

                var result = await stringTask;
                var objListUser = JsonConvert.DeserializeObject<List<Users>>(result);
                IEnumerable<Users> Data = await unitofwork.GetRepository<Users>().Get();

                objListUser.Select(item =>
                {
                    var Exists = Data.FirstOrDefault(c => c.UserName == item.UserName && c.Password == ComputeHash(item.Password, HashType.SHA256));
                    if (Exists == null)
                    {
                        Users User = new Users();
                        User.UserName = item.UserName;
                        User.Password = ComputeHash(item.Password, HashType.SHA256);
                        unitofwork.GetRepository<Users>().Add(User);
                        unitofwork.SaveChanges();
                    }
                    return item;
                }).ToList();

                ResultModel.Messages = "User Sincronizados Con Exito";
                ResultModel.Data = string.Empty;

                return ResultModel;
            }
            catch (Exception Error)
            {
                ResultModel.HasError = true;
                ResultModel.Messages = "Error Técnico Sincronizando Users";
                ResultModel.Data = Error;

                logger.LogError($"Error: {Error.ToString()}");

                return ResultModel;
            }
        }


        public async Task<ResultModel> LogIn(LoginModel LoginModel)
        {
            ResultModel ResultModel = new ResultModel();

            if (LoginModel.UserName == null || LoginModel.Password == null)
            {// si el objeto es diferente de nulo

                ResultModel.HasError = false;
                ResultModel.Data = string.Empty;
                ResultModel.Messages = "User y Clave son requeridos";
                return ResultModel;
            }

            try
            {
                LoginModel.Password = ComputeHash(LoginModel.Password, HashType.SHA256);

                Users Users = (await unitofwork.GetRepository<Users>().Get(x => x.UserName == LoginModel.UserName && x.Password == LoginModel.Password)).FirstOrDefault();

                if (Users != null)
                {
                    LoginModel.IsLogued = true;
                    LoginModel.Token = ConstruirToken(Users);
                    LoginModel.Password = "";

                    ResultModel.HasError = false;
                    ResultModel.Data = LoginModel;
                    ResultModel.Messages = "User Logueado Con Exito";
                }
                else
                {
                    LoginModel.IsLogued = false;
                    LoginModel.Token = "";
                    LoginModel.Password = "";

                    ResultModel.HasError = true;
                    ResultModel.Data = LoginModel;
                    ResultModel.Messages = "User NO Logueado";
                }

                return ResultModel;
            }
            catch (Exception Error)
            {
                ResultModel.HasError = true;
                ResultModel.Messages = $"Error Técnico Al Iniciar Sesion: {Error.Message}";
                ResultModel.Data = Error;

                logger.LogError($"Error: {Error.ToString()}");

                return ResultModel;
            }
        }

        private string ConstruirToken(Users User)
        {
            var Claims = new[] {

                new Claim(JwtRegisteredClaimNames.UniqueName, User.UserName),
                new Claim("UserId", User.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Genera un GUID por cada token
            };

            JWTAuthentication JWTAuthenticationSection = configuration.GetSection("JWTAuthentication").Get<JWTAuthentication>();

            SymmetricSecurityKey Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTAuthenticationSection.Secret));
            var Credenciales = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            DateTime Expiration = DateTime.Now.AddMinutes(JWTAuthenticationSection.ExpirationInMinutes);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: "",
               audience: "",
               claims: Claims,
               expires: Expiration,
               signingCredentials: Credenciales,
               notBefore: DateTime.Now.AddMilliseconds(3000)
               );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string ComputeHash(string input, HashType hashType)
        {
            byte[] numArray;
            byte[] bytes = Encoding.ASCII.GetBytes(input);

            using (HashAlgorithm hashAlgorithm = HashAlgorithm.Create(hashType.ToString().ToUpperInvariant()))
            {
                numArray = hashAlgorithm.ComputeHash(bytes);
            }
            return BitConverter.ToString(numArray).Replace("-", string.Empty).ToLower();
        }


    }
}
