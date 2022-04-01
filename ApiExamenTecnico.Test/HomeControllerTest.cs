using ApiExamenTecnico.Controllers;
using ApiExamenTecnico.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace ApiExamenTecnico.Test
{
    public class HomeControllerTest
    {
        private readonly IConfiguration _config;
        HomeController controller;
        User user;


        public HomeControllerTest()
        {
            var configurationBuilder = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                ["User"] = "flock",
                ["Password"] = "1234",
                ["Token"] = "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiPz"
            });

            _config = configurationBuilder.Build();
            controller = new HomeController(new NullLogger<HomeController>(), _config);
            user = new User();
        }

        [Fact]
        public void TestLogin()
        {
            var result = controller.Login(user);

            Assert.IsType<BadRequestObjectResult>(result.Result);

            var response = result.Result as BadRequestObjectResult;

            Assert.IsType<LoginResponse>(response.Value);
        }

        [Fact]
        public void TestLoginValidUser()
        {
            user.UserName = "flock";
            user.Password = "1234";

            var result = controller.Login(user);

            Assert.IsType<OkObjectResult>(result.Result);

            var response = result.Result as OkObjectResult;

            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public void TestLoginInvalidUser()
        {
            user.UserName = "userTest";
            user.Password = "0000";

            var result = controller.Login(user);

            Assert.IsType<BadRequestObjectResult>(result.Result);

            var response = result.Result as BadRequestObjectResult;

            Assert.Equal(400, response.StatusCode);
        }

        [Fact]
        public void TestGetLatitudLongitudBadRequest()
        {
            string token = "";
            string provincia = "";
            var result = controller.GetLatitudLongitud(token, provincia);

            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public void TestGetLatitudLongitudValidProvince()
        {
            string token = "PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiPz";
            string provincia = "Buenos Aires";
            var result = controller.GetLatitudLongitud(token, provincia);

            Assert.IsType<OkObjectResult>(result.Result);
            
            var response = result.Result as OkObjectResult;

            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public void TestGetLatitudLongitudInvalidToken()
        {
            string token = "testwrongtoken";
            string provincia = "Buenos Aires";
            var result = controller.GetLatitudLongitud(token, provincia);

            Assert.IsType<BadRequestObjectResult>(result.Result);

            var response = result.Result as BadRequestObjectResult;

            Assert.Equal(400, response.StatusCode);
        }
    }
}
