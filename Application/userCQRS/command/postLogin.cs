using Application.Encryption;
using Application.Responses;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.userCQRS.command
{
   
    public class postLogin : IRequest<Tuple<Tokens, string>>
    {
        public string? Username { get; set; }

        public string? Password { get; set; }
    }

    public class postLoginHandler : IRequestHandler<postLogin, Tuple<Tokens, string>>
    {
        //db
        public readonly IuserDBContext _context;

        //jwt
        private readonly IConfiguration _config;

        //
        private readonly string _pepper;  //password
        private readonly int _iteration = 3; //password


        public postLoginHandler(IuserDBContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
            _pepper = Environment.GetEnvironmentVariable("PasswordHashExamplePepper");

        }

        public async Task<Tuple<Tokens, string>> Handle(postLogin request, CancellationToken cancellationToken)
        {

            EncryptPassUser obj = new EncryptPassUser();


            //find the username in database first
            var user = await _context.UserTable
            .FirstOrDefaultAsync(x => x.Username == request.Username, cancellationToken);

            if (user == null)
            {
                throw new Exception("Username or password did not match.");
            }

            //now check password
            var decryptedPassword = obj.ComputeHash(request.Password,user.PasswordSalt,_pepper, _iteration);

            if (user.Password != decryptedPassword)
                throw new Exception("Username or password did not match.");






            //now generate the token.
            var tokenHandle = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

            var tokenDescripter = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new Claim[]
                    {
                            new Claim(ClaimTypes.Name, user.Username),
                           // new Claim(ClaimTypes.Hash, decryptedPassword),
                        //new Claim(JwtRegisteredClaimNames.Sub, _config([Jwt:"Subject"])),
                        //new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        //new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),

                    }),
                Expires = DateTime.UtcNow.AddMinutes(250),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandle.CreateToken(tokenDescripter);

            return Tuple.Create(new Tokens { Token = tokenHandle.WriteToken(token) }, user.UserType);

        }
         


     

        }
    }

    
