using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleApp.Data;

namespace SampleApp.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly SampleDBContext _dbContext;
    public ProductsController(SampleDBContext dBContext)
    {
        _dbContext = dBContext;
    }

    [HttpGet("get-with-response-cache")]
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> GetWithResponseCache()
    {
        var result = await _dbContext.Products.ToListAsync();
        return Ok(result);
    }
}
