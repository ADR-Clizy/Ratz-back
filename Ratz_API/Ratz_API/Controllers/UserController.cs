using DatabaseConnection;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using BCryptNet = BCrypt.Net.BCrypt;
using Newtonsoft.Json;
using Ratz_API.Models.DataTransferObjects;
using Ratz_API.UserAggregate.DataTransferObjects;
using Ratz_API.Models;
using Microsoft.AspNetCore.Authorization;

namespace Ratz_API.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtSettings _jwtSettings;
        public UserController(IUserRepository iUserRepository, JwtSettings iJwtSettings)
        {
            _userRepository = iUserRepository;
            _jwtSettings = iJwtSettings;
        }

        private static bool IsMailCorrect(string iEmail)
        {
            if(string.IsNullOrEmpty(iEmail))
            {
                return false;
            }
            Regex aMailRegex = new Regex(@"\S+@\S+\.\S+");
            return aMailRegex.IsMatch(iEmail);
        }

        private static bool IsPasswordCorrect(string iPassword)
        {
            if (string.IsNullOrEmpty(iPassword))
            {
                return false;
            }
            Regex aMediumPasswordRegex = new Regex(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]))(?=.{6,})");
            return aMediumPasswordRegex.IsMatch(iPassword);
        }

        [HttpGet]
        [Route("/api/user")]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult GetUser()
        {
            return Ok("Authenticated");
        }

        [HttpPost]
        [Route("/api/register")]
        public ActionResult Register([FromBody] UserConnectionDTO iUserConnection)
        {
            //TODO Test login && Check why email in token is null.
            try
            {
                if (!IsPasswordCorrect(iUserConnection.Password) || !IsMailCorrect(iUserConnection.Email))
                {
                    return BadRequest(JsonConvert.SerializeObject(new ErrorResponse("Format incorrect"), Formatting.Indented));
                }
                string aHashedPassword = BCryptNet.HashPassword(iUserConnection.Password);
                User aUser = new User { Email = iUserConnection.Email, Password = aHashedPassword };
                User aNewUser = _userRepository.NewUser(aUser);
                if (aNewUser != null)
                {
                    UserTokens aToken = JwtHelpers.JwtHelpers.GenTokenkey(new UserTokens()
                    {
                        Email = aNewUser.Email,
                        GuidId = Guid.NewGuid(),
                        Id = aNewUser.UserId,
                    }, _jwtSettings);
                    return Ok(aToken);
                }
                else
                {
                    return BadRequest(JsonConvert.SerializeObject(new ErrorResponse("Une erreur est survenue, veuillez réessayer"), Formatting.Indented));
                }
            } catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("/api/login")]
        public ActionResult Login([FromBody] UserConnectionDTO iUserConnection)
        {
            try
            {
                User aUser = _userRepository.GetUserByEmailAddress(iUserConnection.Email);
                if (aUser != null)
                {
                    bool isPasswordCorrect = BCryptNet.Verify(iUserConnection.Password, aUser.Password);
                    if (isPasswordCorrect)
                    {
                        UserTokens aToken = JwtHelpers.JwtHelpers.GenTokenkey(new UserTokens()
                        {
                            Email = aUser.Email,
                            GuidId = Guid.NewGuid(),
                            Id = aUser.UserId,
                        }, _jwtSettings);
                        return Ok(aToken);
                    }
                    else
                    {
                        return BadRequest("Email ou mot de passe incorrect");
                    }
                }
                else
                {
                    return NotFound("Email ou mot de passe incorrect");
                }
            } catch
            {
                throw;
            }
        }

        //To add for authenticate routes :
    }
}
