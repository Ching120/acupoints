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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplication13.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Historical_RecordController : ControllerBase
    {

       
        private readonly IConfiguration _config;
        private readonly string connectionString;
        private readonly Historical_RecordDBService _historical_recordService;
    


        public Historical_RecordController(IConfiguration config, Historical_RecordDBService historical_recordService)
        {
        
            _config = config;
            connectionString = _config.GetConnectionString("Local");
            _historical_recordService = historical_recordService;
       
        }

        #region 統計各乾眼症類型數量

        [HttpGet]
        [Route("GetD_record_TypeMath")]
        public IActionResult GetD_record_TypeMath()
        {
            var result = _historical_recordService.GetD_record_TypeMath();
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);

        }
        #endregion

        #region 	統計今日復健人數

        [HttpGet]
        [Route("GetR_recordP/{R_record_date}")]
        public IActionResult GetR_recordP([FromRoute]DateTime R_record_date)
        {
            var result = _historical_recordService.GetR_recordP(R_record_date);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);

        }
        #endregion

        #region 	總覽使用者診斷復健紀錄日期

        [HttpGet]
        [Route("GetDR_record/{user_id}")]
        public IActionResult GetDR_record([FromRoute] Guid user_id)
        {
            var result = _historical_recordService.GetDR_record(user_id);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);

        }
        #endregion

    }
}
