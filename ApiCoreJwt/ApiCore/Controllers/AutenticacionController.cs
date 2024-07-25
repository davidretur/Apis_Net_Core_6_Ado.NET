using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using ApiCore.Models;

using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

using System.Text;

namespace ApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : ControllerBase
    {
        private readonly string secretKey;

        public AutenticacionController(IConfiguration config)
        {
            secretKey = config.GetSection("settings").GetSection("secretKey").ToString();
        }

        // metodod para autenticar el usuario

        [HttpPost] // tipo dde verbo de solicitud
        [Route("Validar")] // nombre de la ruta de ejecucion
        public IActionResult Validar([FromBody] Usuario request) // indicamos por que metodo va resibir los datos
        {
            // iniciamos la autenticacion
            if (request.correo == "c@gmail.com" && request.clave == "123")
            {
                // agregamos la referencias a la clavesecreta
                var keyBytes = Encoding.ASCII.GetBytes(secretKey);
                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, request.correo));

                // creamos la configuarcion de token
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes),
                    SecurityAlgorithms.HmacSha256Signature)
                };
                // validamos la configuracion anterior
                var tokenHandler = new JwtSecurityTokenHandler();
                // creamos el token
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                string tokencreado = tokenHandler.WriteToken(tokenConfig);


                return StatusCode(StatusCodes.Status200OK, new { token = tokencreado });

            }
            else
            {

                return StatusCode(StatusCodes.Status401Unauthorized, new { token = "" });
            }
        }


    }
}
