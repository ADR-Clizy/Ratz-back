using DatabaseConnection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Ratz_API.Models.DataTransferObjects;
using Ratz_API.QrCodeAggregate.Database;

namespace Ratz_API.Controllers;

public class QrCodeController : ControllerBase
{
    private readonly IQrCodeRepository _qrCodeRepository;

    public QrCodeController(IQrCodeRepository qrCodeRepository)
    { 
        _qrCodeRepository = qrCodeRepository;   
    }
        

    [HttpGet]
    [Route("api/user/{userId}/qrcodes")]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult RetrieveQrCodes(string userId)
    {
        try
        {
            return Ok(_qrCodeRepository.GetAll(Int32.Parse(userId)));
        } catch
        {
            return BadRequest(JsonConvert.SerializeObject(new ErrorResponse("Une erreur est survenue, veuillez réessayer"), Formatting.Indented));
        }
    }

    [HttpPost]
    [Route("api/user/{userId}/qrcodes")]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public IActionResult CreateQrCode(string userId, [FromBody] string data)
    {
        try
        {
            //int aAuthenticatedUserId = Int32.Parse(User.FindFirst("Id").Value);
            return Ok(_qrCodeRepository.NewQrCode(data, Int32.Parse(userId)));
        }
        catch
        {
            return BadRequest(JsonConvert.SerializeObject(new ErrorResponse("Une erreur est survenue, veuillez réessayer"), Formatting.Indented));

        }
    }
}