using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;

using OnlineBanking.Models;
using OnlineBanking.Services;

namespace OnlineBanking.Controllers
{
     [ApiController]
 [Route("[controller]")]
    public class AccountUserController:Controller

    {
        private readonly AccountUserServices accountuserService;

public AccountUserController(AccountUserServices accountUserServices)
{
    this.accountuserService = accountUserServices;
}


[HttpPost]
[AllowAnonymous]
[Route("addAccount")]
public async Task<ActionResult> AddAccount([FromBody]dynamic data)//[FromBody]
{
    try
    {
        var temp=data.GetProperty("acProfile").GetRawText();
         var formData = JsonSerializer.Deserialize<AccountProfile>(temp);
         var TEMPDOCSDATA=formData.ValidationDocsData;
         //formData.ValidationDocsData=null;

        // Convert base64 string to byte array
        if (TEMPDOCSDATA!=null)
        {
           // formData.ValidationDocsData = Convert.FromBase64String(TEMPDOCSDATA);
            //formData.ValidationDocsData = formData.ValidationDocsDataByteArray;
            //formData.ValidationDocsDataByteArray=null;
        }
        var acProfile=formData;
        var res = await accountuserService.AddAccount(acProfile);
        if(res == null)
        {
            return BadRequest();
        }
        return Ok(acProfile);
    }
    catch (Exception ex)
    {
        return StatusCode(500, ex.Message);
    }
}



[HttpPost]
[Authorize]
[Route("NewTransaction")]
public async Task<ActionResult> NewTransaction([FromBody]Transaction transaction)//Add [FormBody]
{
    try
    {
        var res = await accountuserService.AddTransaction(transaction);
        if(res == null)
        {
            return BadRequest();
        }
        return Ok(res);
    }
    catch (Exception ex)
    {
        return StatusCode(500, ex.Message);
    }
}


[HttpPost]
[Authorize]
[Route("NewPaypee")]
public async Task<ActionResult> NewPaypee([FromBody]Paypee paypee)//Add [FormBody]
{
    try
    {
        var res = await accountuserService.NewPaypee(paypee);
        if(res == null)
        {
            return BadRequest();
        }
        return Ok(res);
    }
    catch (Exception ex)
    {
        return StatusCode(500, ex.Message);
    }
}



[HttpPost]
[Authorize]
[Route("GetTransaction")]
public async Task<ActionResult>  GetAllTransaction([FromBody]string AccountNumber)//Add [FromBody]
{
    try
    {
        var res = await accountuserService.GetAllTransaction(AccountNumber);
        if(res == null)
        {
            return BadRequest();
        }
        return Ok(res);
    }
    catch (Exception ex)
    {
        return StatusCode(500, ex.Message);
    }
}

    }
}