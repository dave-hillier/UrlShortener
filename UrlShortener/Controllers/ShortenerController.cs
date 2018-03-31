using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UrlShortener.Controllers
{
  [Route("/")]
  public class ShortenerController : Controller
  {
    [HttpGet("{shortUrl}")]
    public IActionResult Get(string shortUrl)
    {
      try
      {
        var decodedTxt = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(shortUrl));
        return new RedirectResult(decodedTxt);
      }
      catch (FormatException)
      {
        return new NotFoundResult();
      }
    }

    // TODO: authorize
    [HttpPost]
    public IActionResult Post([FromQuery]string longUrl)
    {
      string v = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(longUrl));
      string url = Url.Action("Get", new { s = v });
      return new OkObjectResult(new { Long = longUrl, Short = url });
    }

  }
}