using Castle.Core.Smtp;
using DI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Security.Principal;
using WebApplication13.Model;
using WebApplication13.Security;
using WebApplication13.Service;
using WebApplication13.ViewModel;

namespace WebApplication13.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarningController : ControllerBase
    {

        private readonly UserDBService _userService;
        private readonly IConfiguration _config;
        private readonly string connectionString;
        private readonly WarningDBService _warningService;


        public WarningController(UserDBService userService, IConfiguration config, WarningDBService warningService)
        {

            _userService = userService;
            _config = config;
            connectionString = _config.GetConnectionString("Local");
            _warningService = warningService;

        }


        #region 自動發送警示Email
        [HttpGet]
        [Route("Warning_sendemail")]
        public IActionResult Warning_sendemail()
        {
            var result = _warningService.SendWarningEmail();

            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);
        }
        #endregion

     

       
    }
}
