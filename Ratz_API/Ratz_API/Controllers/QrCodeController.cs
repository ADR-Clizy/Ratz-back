using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Ratz_API.Models.DataTransferObjects;
using Ratz_API.QrCodeAggregate.Database;

namespace Ratz_API.Controllers;

public class QrCodeController : ControllerBase
{
    private readonly IQrCodeRepository _qrCodeRepository;

    public QrCodeController(IQrCodeRepository qrCodeRepository) 
        => _qrCodeRepository = qrCodeRepository;

    [HttpPost]
    [Route("api/qrcode")]
    public IActionResult CreateQrCode([FromBody] string data)
    {
        try
        {
            return Ok(_qrCodeRepository.NewQrCode(data));
        }
        catch
        {
            return BadRequest(JsonConvert.SerializeObject(new ErrorResponse("Une erreur est survenue, veuillez réessayer"), Formatting.Indented));

        }
    }
}