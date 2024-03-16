using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OnlineBankingMVC.Models;
using OnlineBankingMVC.Repo;

namespace OnlineBankingMVC.Controllers
{
    [Route("[controller]")]
    public class AccountProfileController : Controller
    {
          private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor; 
        public AccountProfileController(HttpClient httpClient, IConfiguration configuration,IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _configuration = configuration;
         _httpContextAccessor = httpContextAccessor;
    }

[AllowAnonymous]
  public ActionResult Index()
    {
        return View();
    } 
//MAKENEW TRANSACTION-------------------------------------------------------------

[AllowAnonymous]
    [HttpGet]
    [Route("NewTransaction")]
public async Task<ActionResult>NewTransaction(){
    return View();
}

[HttpPost]
[AllowAnonymous]
 [Route("NewTransaction")]
        public async Task<ActionResult>NewTransaction(TransactionViewModel transaction){

        if (transaction!=null){
        TransactionViewModel newUser = new TransactionViewModel();
          var service = new ServiceRepository(_httpClient,_configuration,_httpContextAccessor);
          {
            using (var response = service.PostResponse("AccountUser/NewTransaction", transaction))
            {
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                  // var transactions = JsonConvert.DeserializeObject<TransactionViewModel>(apiResponse);
                    return View(apiResponse);
                }
                if(response.StatusCode==HttpStatusCode.Unauthorized){
                    return RedirectToAction("Login","User");
                }
            }
          }

          return RedirectToAction("Index");
          }

          return View(transaction);
      }



//GetAllTransaction-----------------------------------------------------------
 [AllowAnonymous]
    [HttpGet]
    [Route("GetTransaction")]
public async Task<ActionResult>GetAllTransaction(){
    return View("GetAllTransactionForm");
}

[HttpPost]
[AllowAnonymous]
 [Route("GetTransaction")]
        public async Task<ActionResult>GetAllTransaction(string AccountNumber){

        if (AccountNumber!=null){
        AccountProfileViewModel newUser = new AccountProfileViewModel();
          var service = new ServiceRepository(_httpClient,_configuration,_httpContextAccessor);
          {
            using (var response = service.PostResponse("AccountUser/GetTransaction", AccountNumber))
            {
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                   var transactions = JsonConvert.DeserializeObject<IEnumerable<TransactionViewModel>>(apiResponse);
                    return View("GetTransaction",transactions);
                }
                if(response.StatusCode==HttpStatusCode.Unauthorized){
                    return RedirectToAction("Login","User");
                }
            }
          }

          return RedirectToAction("Index");
          }

          return View("GetAllTransactionForm",AccountNumber);
      }
//Create A new Paypee---------------------------------------------------------------
[AllowAnonymous]
    [HttpGet]
    [Route("New_Paypee")]
public async Task<ActionResult>NewPaypee(){
    return View();
}

[HttpPost]
[AllowAnonymous]
 [Route("New_Paypee")]
        public async Task<ActionResult>NewPaypee(PaypeeViewModel paypee){

        if (paypee!=null){
        PaypeeViewModel newUser = new PaypeeViewModel();
          var service = new ServiceRepository(_httpClient,_configuration,_httpContextAccessor);
          {
            using (var response = service.PostResponse("AccountUser/NewPaypee", paypee))
            {
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                  
                    return View("Index");
                }
                if(response.StatusCode==HttpStatusCode.Unauthorized){
                    return RedirectToAction("Login","User");
                }
            }
          }

          return RedirectToAction("Index");
          }

          return View("Index");
      }



//create A NEW USER------------------------------------------------------------------
    [AllowAnonymous]
    [HttpGet]
    [Route("AddAccount")]
public async Task<ActionResult> AddAccountUser(){
    return View("Create");
}



    [HttpPost]
    [AllowAnonymous]
     [Route("AddAccount")]
        public async Task<ActionResult> AddAccountUser(AccountProfileViewModel AccUser){

        if (AccUser!=null){
        AccountProfileViewModel newUser = new AccountProfileViewModel();
        AccUser.AccountNumber="0";
          var service = new ServiceRepository(_httpClient,_configuration,_httpContextAccessor);
          {
            using (var response = service.PostResponse("AccountUser/addAccount",AccUser))
            {
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    newUser = JsonConvert.DeserializeObject<AccountProfileViewModel>(apiResponse);
                    return RedirectToAction("Create","User");
                }
                return View("Create",AccUser);
            }
          }

          //return View("Create",AccUser);
          }

          return View("Create",AccUser);
      }
    }
}