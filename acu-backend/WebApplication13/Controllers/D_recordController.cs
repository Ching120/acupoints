﻿using Castle.Core.Smtp;
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
    public class D_recordController : ControllerBase
    {

       
        private readonly IConfiguration _config;
        private readonly string connectionString;
        private readonly D_recordDBService _d_recordDBService;
    


        public D_recordController(IConfiguration config, D_recordDBService d_recordDBService)
        {
        
            _config = config;
            connectionString = _config.GetConnectionString("Local");
            _d_recordDBService = d_recordDBService;
       
        }

        #region 送出診斷

        [HttpPost]
        [Route("PostD_record{user_id}/{eye_question_id}/{D_record_score}")]
        public IActionResult PostD_record( [FromRoute] Guid user_id,[FromRoute] Guid eye_question_id, [FromRoute] int D_record_score)
        {
            var result = _d_recordDBService.PostD_record(user_id, eye_question_id,D_record_score);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);

        }
        #endregion
        //#region 總覽OSDI分數、類型、推薦中藥材

        //[HttpGet]
        //[Route("GetD_record_STC")]
        //public IActionResult GetD_record_STC()
        //{
        //    var result = _d_recordDBService.GetD_record_STC();
        //    if (result == null)
        //    {
        //        return NotFound("找不到資源");
        //    }
        //    return Ok(result);

        //}
        //#endregion

        #region 總覽診斷紀錄

        [HttpGet]
        [Route("GetD_record/{user_id}/{D_record_date}")]
        public IActionResult GetD_record([FromRoute] Guid user_id,[FromRoute] DateTime D_record_date)
        {
            var result = _d_recordDBService.GetD_record(user_id, D_record_date);
            if (result == null)
            {
                return NotFound("找不到資源");
            }
            return Ok(result);

        }
        #endregion


    }
}
