using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Models;

namespace UrlShortener.Controllers
{
  [Route("/")]
  public class ShortenerController : Controller
  {
    private readonly UrlStorageDbContext dbContext;
    public ShortenerController(UrlStorageDbContext dbContext)
    {
      this.dbContext = dbContext;
    }

    [HttpGet("{shortUrl}")]
    public async Task<IActionResult> Get(string shortUrl)
    {
      var id = Shortener.Decode(shortUrl);
      if (id == -1)
      {
        return NotFound();
      }
      var mapping = await dbContext.Urls.FindAsync(id);
      if (mapping == null)
      {
        return NotFound();
      }

      return new RedirectResult(mapping.Url);
    }

    // TODO: authorize for creation
    [HttpPost]
    public async Task<IActionResult> Post([FromQuery]string longUrl)
    {
      if (longUrl.Length > 2048)
      {
        return new BadRequestObjectResult("Url too long. Limit 2K");
      }

      var mapping = new UrlMapping { Url = longUrl };
      dbContext.Add(mapping);
      await dbContext.SaveChangesAsync();
      string shortUrl = Shortener.Encode(mapping.Id);
      return new OkObjectResult(new { Long = longUrl, Short = Url.Action("Get", new { shortUrl = shortUrl }) });
    }
  }
}