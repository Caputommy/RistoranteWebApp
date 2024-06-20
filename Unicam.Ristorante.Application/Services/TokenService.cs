using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using Unicam.Ristorante.Application.Abstractions.Services;
using Unicam.Ristorante.Application.Models.Requests;
using Unicam.Ristorante.Application.Options;
using Unicam.Ristorante.Models.Entities;
using Unicam.Ristorante.Models.Repositories;

namespace Unicam.Ristorante.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtAuthenticationOption _jwtAuthOption;
        private readonly IPasswordHasher<Utente> _passwordHasher;
        private readonly UtenteRepository _utenteRepository;

        public TokenService(UtenteRepository utenteRepository, IPasswordHasher<Utente> passwordHasher, IOptions<JwtAuthenticationOption> jwtAuthOption)
        {
            _utenteRepository = utenteRepository;
            _passwordHasher = passwordHasher;
            _jwtAuthOption = jwtAuthOption.Value;
        }

        public async Task<string> CreateTokenAsync(CreateTokenRequest createTokenRequest)
        {
            var utente = await _utenteRepository.GetUtenteByEmailAsync(createTokenRequest.Email);

            if (utente == null)
            {
                throw new InvalidCredentialException($"Utente con email '{createTokenRequest.Email}'  non trovato");
            }
            if (!IsValidPassword(utente, createTokenRequest.Password))
            {
                throw new InvalidCredentialException($"La password non corrisponde alla password dell'utente");
            }

            return CreateToken(utente);
        }

        private Boolean IsValidPassword(Utente utente, string password)
        {
            return _passwordHasher.VerifyHashedPassword(utente, utente.Password, password) == PasswordVerificationResult.Success;
        }

        private string CreateToken(Utente utente)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("Id", utente.Id.ToString()),
                new Claim("Email", utente.Email),
                new Claim("Nome", utente.Nome),
                new Claim("Cognome", utente.Cognome),
                new Claim("Ruolo", utente.Ruolo.ToString())
            };

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtAuthOption.Key)
                );
            var credentials = new SigningCredentials(securityKey
                , SecurityAlgorithms.HmacSha256);

            var securityToken = new JwtSecurityToken(
                _jwtAuthOption.Issuer,
                null,
                claims,
                expires: DateTime.Now.AddMinutes(_jwtAuthOption.MinutesToExpiration),
                signingCredentials: credentials
                );

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }   
    }
}
