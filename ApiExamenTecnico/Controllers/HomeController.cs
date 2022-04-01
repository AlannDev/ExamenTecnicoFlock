using ApiExamenTecnico.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;

namespace ApiExamenTecnico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        [HttpPost]
        [Route("login")]
        public ActionResult<LoginResponse> Login(User user)
        {
            _logger.LogInformation("endpoint login");
            _logger.LogInformation("User param: " + JsonConvert.SerializeObject(user));

            if (ValidUser(user))
                return Ok(new LoginResponse { Token = _config["Token"], Status = "OK", StatusCode = "200" });

            return BadRequest(new LoginResponse { Status = "Error", StatusCode = "404", Message = "User not found" });
        }

        private bool ValidUser(User user)
        {
            if (user.UserName == _config["User"] && user.Password == _config["Password"])
                return true;

            return false;
        }

        [HttpGet]
        [Route("getLatitudLongitud/{provincia}")]
        public ActionResult<Centroide> GetLatitudLongitud([Required][FromHeader(Name = "x-autho-token")]string tokenHeader, string provincia)
        {
            _logger.LogInformation("endpoint getLatitudLongitud");
            _logger.LogInformation("Token Header: " + tokenHeader);
            _logger.LogInformation("Provincia: " + provincia);

            if(tokenHeader == _config["Token"])
            {
                var result = CallApiPublica(provincia);

                if (result.StatusCode == "200")
                    return Ok(result);
                else
                    return BadRequest(result);
            }

            return BadRequest(new Centroide { Status = "Error", StatusCode = "500", Message = "Invalid Token" });
        }

        private Centroide CallApiPublica(string provincia)
        {
            try
            {
                using var client = new HttpClient();
                client.BaseAddress = new Uri("https://apis.datos.gob.ar/georef/api/");
                var responseTask = client.GetAsync("provincias");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ProvinciasResponse>();
                    readTask.Wait();

                    var response = readTask.Result;
                    var item = response.provincias.FirstOrDefault(x => x.nombre == provincia);
                    return item == null ? new Centroide { Status = "Error", StatusCode = "404", Message = "Prov. not found" } : new Centroide { lat = item.centroide.lat, lon = item.centroide.lon, Status = "OK", StatusCode = "200" };
                }
                else
                {
                    return new Centroide { Status = "Error", Message = "Api provincias fail", StatusCode = "500" };
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                _logger.LogTrace(e.StackTrace);

                return new Centroide { Status = "Error", Message = e.Message, StatusCode = "500" };
            }
        }
    }
}
