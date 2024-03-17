using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnlineBankingMVC.Models;
using OnlineBankingMVC.Repo;

namespace OnlineBankingMVC.Controllers
{
    public class UserController:Controller
    {
        private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor; 
        public UserController(HttpClient httpClient, IConfiguration configuration,IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _configuration = configuration;
         _httpContextAccessor = httpContextAccessor;
    }

          public IActionResult Index()
    {
        return View();
    } 
[AllowAnonymous]

 public ViewResult Create()
 {
     return View();
 }
   [AllowAnonymous]
  [HttpPost]

  public async Task<ActionResult> Create(UserProfileViewModel user)
  {
    Console.WriteLine(user);
      if (user!=null){
          UserProfileViewModel newUser = new UserProfileViewModel();
          var service = new ServiceRepository(_httpClient,_configuration,_httpContextAccessor);
          {
            using (var response = service.PostResponse("UserProfile/Create", user))
            {
                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    newUser = JsonConvert.DeserializeObject<UserProfileViewModel>(apiResponse);
                    return RedirectToAction("Index");
                }
                else return RedirectToAction("Login");
            }
          }

          return RedirectToAction("Home/Index");
          }

          return View(user);
      }
      


[AllowAnonymous]
 public ViewResult Login()
 {
     return View();
 }

[AllowAnonymous]
[HttpPost]
  public async Task<ActionResult> Login(UserProfileViewModel user)
  {
      if (user!=null){
          UserProfileViewModel newUser = new UserProfileViewModel();
          var service = new ServiceRepository(_httpClient,_configuration,_httpContextAccessor);
          {
            using (var response = service.PostResponse("UserProfile/Login", user))
            {
                if (response.IsSuccessStatusCode)
                {
                  string jsonResponse = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON response to extract the token and UserProfile
                var responseObject = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                
                // Extract token from the response
                string token = responseObject.token;

                HttpContext.Session.SetString("JWTToken", token); 
                    return RedirectToAction("Index","AccountProfile");
                }
            }
          }

          return View(user);
          }

          return View(user);
      }
      [AllowAnonymous]
      [HttpGet]
      public ActionResult Logout()
    {
        // Clear JWT token from session
        HttpContext.Session.Remove("JWTToken");

        // Redirect to the login page
        return RedirectToAction("Index");
    }

// Handling ForgotPassword

      

[AllowAnonymous]
 public ViewResult GenerateOTP()
 {
     return View();
 }

[AllowAnonymous]
[HttpPost]
  public async Task<ActionResult> GenerateOTP(Token token)
  {
      if (token!=null){
        var AccountNumber=token.AccountNumber;
          UserProfileViewModel newUser = new UserProfileViewModel();
          var service = new ServiceRepository(_httpClient,_configuration,_httpContextAccessor);
          {
            using (var response = service.PostResponse("UserProfile/GenerateOtp", AccountNumber))
            {
                if (response.IsSuccessStatusCode)
                {
                  string jsonResponse = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON response to extract the token and UserProfile
                //var responseObject = JsonConvert.DeserializeObject<Token>(jsonResponse);
                
             
                    return RedirectToAction("CheckOtp");
                }
            }
          }

          return View(token);
          }

          return View(token);
      }



      [AllowAnonymous]
 public ViewResult CheckOtp()
 {
     return View();
 }

[AllowAnonymous]
[HttpPost]
  public async Task<ActionResult> CheckOtp(Token token)
  {
      if (token!=null){
        token.ExpiryDate=DateTime.Now;
          UserProfileViewModel newUser = new UserProfileViewModel();
          var service = new ServiceRepository(_httpClient,_configuration,_httpContextAccessor);
          {
            using (var response = service.PostResponse("UserProfile/CheckOtp", token))
            {
                if (response.IsSuccessStatusCode)
                {
                  string jsonResponse = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON response to extract the token and UserProfile
                //var responseObject = JsonConvert.DeserializeObject<Token>(jsonResponse);
                
             
                    return RedirectToAction("ResetPassword");
                }
            }
          }

          return View(token);
          }

          return View(token);
      }
  
    
 [AllowAnonymous]
 public ViewResult ResetPassword()
 {
     return View();
 }

[AllowAnonymous]
[HttpPost]
  public async Task<ActionResult> ResetPassword(UserProfileViewModel user)
  {
      if (user!=null){
        
          UserProfileViewModel newUser = new UserProfileViewModel();
          var service = new ServiceRepository(_httpClient,_configuration,_httpContextAccessor);
          {
            using (var response = service.PostResponse("UserProfile/ResetPassword", user))
            {
                if (response.IsSuccessStatusCode)
                {
                  string jsonResponse = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON response to extract the token and UserProfile
                //var responseObject = JsonConvert.DeserializeObject<Token>(jsonResponse);
                
             
                    return RedirectToAction("Login");
                }
            }
          }

          return View(user);
          }

          return View(user);
      }
    
    }
    
}