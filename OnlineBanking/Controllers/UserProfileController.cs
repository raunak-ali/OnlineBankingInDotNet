using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using OnlineBanking.Models;
using OnlineBanking.Services;

namespace OnlineBanking.Controllers
{
    [Route("[controller]")]
    public class UserProfileController : Controller
    {
       private readonly UserProfileServices UserProfileService;

        public UserProfileController(UserProfileServices UserProfileServices)
{
    this.UserProfileService = UserProfileServices;
}
        

[HttpPost]
[Route("Create")]
[AllowAnonymous]//Change This LAter
public async Task<ActionResult> AddUserProfile([FromBody]UserProfile user)//Add [FromBody]
{
    
    try
    {
        
        var res = await UserProfileService.AddAccount(user);
        if(res == null)
        {

            return BadRequest();
        }
        return Ok(user);
    }
    catch (Exception ex)
    {
        return StatusCode(500, ex.Message);
    }
}


[HttpPost]
[AllowAnonymous]
[Route("Login")]

public async Task<ActionResult>  LoginUserProfile([FromBody]UserProfile user)//Try [FromBody]
{
    try
    {
        
        var res = await UserProfileService.LoginUserProfile(user);
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
[AllowAnonymous]
[Route("ResetPassword")]
public async Task<ActionResult> ResetPassword([FromBody]UserProfile user)
{
    try
    {
        
        var res = await UserProfileService.ResetPassword(user);
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


[Authorize(Policy = "IsAdmin")]
[Route("Confirm")]
[HttpPost]
public async Task<ActionResult> ConfirmUserRegistration([FromBody]int userid)
        {
              try{
 var res = await UserProfileService.ConfirmUserRegistration(userid);
        if(res == null)
        {
            return BadRequest();
        }
        return Ok(res);

    }
    catch(Exception ex){
        return StatusCode(500, ex.Message);
    }
            }

 [Authorize(Policy = "IsAdmin")]
        [HttpGet]
        [Route("ShowListToConfirm")]
        public async Task<ActionResult> ShowUnConfirmedUsers(){
    try{
 var res = await UserProfileService.ShowUnConfirmedUsers();
        if(res == null)
        {
            return BadRequest();
        }
        return Ok(res);

    }
    catch(Exception ex){
        return StatusCode(500, ex.Message);
    }
    }

[Authorize]
[HttpPost]
[Route("DownloadDocument")]
 public async Task<ActionResult> DownloadDocument([FromBody]int AccountUserId)
    {
        // Check if ValidationDocsData is valid
        
try{
//var AccountUserId=acp.AccountUserId;
 var res = await UserProfileService.DownloadDocument(AccountUserId);
        if(res == null)
        {
            return BadRequest();
        }
        return Ok(res);

    }
    catch(Exception ex){
        return StatusCode(500, ex.Message);
    }
    

}

[AllowAnonymous]
[HttpPost]
[Route("GenerateOtp")]
public async Task<ActionResult> GenerateOTP([FromBody]dynamic data){
try{
string AccountNumber = data.GetProperty("AccountNumber").GetString();
//var AccountUserId=acp.AccountUserId;
 var res = await UserProfileService.GenerateOTP(AccountNumber);
        if(res == null)
        {
            return BadRequest();
        }
        return Ok(new{message=res});

    }
    catch(Exception ex){
        return StatusCode(500, ex.Message);
    }
}


[AllowAnonymous]
[HttpPost]
[Route("CheckOtp")]
public async Task<ActionResult> CheckOTP([FromBody]Token token){
try{
//var AccountUserId=acp.AccountUserId;
 var res = await UserProfileService.CheckOTP(token);
        if(res == "OTP Does not match")
        {
            return BadRequest();
        }
        return Ok(res);

    }
    catch(Exception ex){
        return StatusCode(500, ex.Message);
    }
}

    }
}
