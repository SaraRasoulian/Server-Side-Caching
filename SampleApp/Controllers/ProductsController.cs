using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SampleApp.Data;
using StackExchange.Redis;
using System.Text.Json;

namespace SampleApp.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly SampleDBContext _dbContext;
    private readonly IMemoryCache _memoryCache;
    public ProductsController(SampleDBContext dBContext,
        IMemoryCache memoryCache)
    {
        _dbContext = dBContext;
        _memoryCache = memoryCache;
    }

    [HttpGet("get-with-response-cache")]
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> GetWithResponseCache()
    {
        var result = await _dbContext.Products.ToListAsync();
        return Ok(result);
    }

    [HttpGet("get-with-in-memory-cache")]
    public async Task<IActionResult> GetWithInMemoryCache()
    {
        var cacheProducts = _memoryCache.Get<IList<Product>>("products");
        if (cacheProducts is not null)
        {
            return Ok(cacheProducts);
        }

        var products = await _dbContext.Products.ToListAsync();

        var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
        _memoryCache.Set("products", products, expirationTime);
        return Ok(products);
    }

    [HttpGet("get-with-redis-cache")]
    public async Task<IActionResult> GetWithRedisCache()
    {
        // Connect to the Redis running inside a container
        var redis = ConnectionMultiplexer.Connect("localhost:6379");
        var db = redis.GetDatabase();

        string? fetchedProductsJson = await db.StringGetAsync("products");

        if (fetchedProductsJson is not null)
        {
            var fetchedProducts = JsonSerializer.Deserialize<List<Product>>(fetchedProductsJson);
            return Ok(fetchedProducts);
        }

        var products = await _dbContext.Products.ToListAsync();

        string productsJson = JsonSerializer.Serialize(products);
        await db.StringSetAsync("products", productsJson);

        return Ok(products);
    }
}
