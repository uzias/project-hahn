using Api.Domain.Common.Model;
using Api.Infrastructure.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CryptoController : ControllerBase
{

    private readonly ILogger<CryptoController> _logger;
    private readonly IPairPriceRepository _pairPriceRepository;

    public CryptoController(ILogger<CryptoController> logger, IPairPriceRepository pairPriceRepository)
    {
        _logger = logger;
        _pairPriceRepository = pairPriceRepository;
    }

    [HttpGet(Name = "AvgPrice")]
    public async Task<ActionResult> GetAllAsync()
    {   
        _logger.LogDebug("Getting all pair prices");
        return Ok(await _pairPriceRepository.GetAllAsync());
    }
}
