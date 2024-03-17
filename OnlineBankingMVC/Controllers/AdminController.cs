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
    public class AdminController: Controller
    {
          private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor; 
        public AdminController(HttpClient httpClient, IConfiguration configuration,IHttpContextAccessor httpContextAccessor)
    {
        _httpClient = httpClient;
        _configuration = configuration;
         _httpContextAccessor = httpContextAccessor;
    }

       public IActionResult Index()
    {
        return View();
    } 
//Show all unformied Users--------------------------------------------------------------------
[AllowAnonymous]
[HttpGet]
public async Task<ActionResult> ShowUnConfirmedUsers()
 {
    var service = new ServiceRepository(_httpClient,_configuration,_httpContextAccessor);
          {
            using (var response = service.GetResponse("UserProfile/ShowListToConfirm"))
            {
                if (response.IsSuccessStatusCode)
                {
                   string jsonResponse = await response.Content.ReadAsStringAsync();
                    List<UserProfileViewModel> userProfileList = JsonConvert.DeserializeObject<List<UserProfileViewModel>>(jsonResponse);


                // Deserialize the JSON response to extract the token and UserProfile

                return View(userProfileList);
                }
            }
          }

     return View();
 }
//Get DocumentPlease
[AllowAnonymous]
   public async  Task<ActionResult> DownloadDocument(int AccoutUserid)
    {
        // DownloadDocument
         var service = new ServiceRepository(_httpClient,_configuration,_httpContextAccessor);
          {
            AccountProfileViewModel acc=new AccountProfileViewModel();
            using (var response = service.PostResponse("UserProfile/DownloadDocument",AccoutUserid))
            {
                if (response.IsSuccessStatusCode)
                {
                   string jsonResponse = await response.Content.ReadAsStringAsync();
                   //Convert te byte array to a Downloadable file now
                    byte[] fileContent = Convert.FromBase64String(jsonResponse);
                   return File(fileContent, "application/pdf", "document.pdf");


                // Deserialize the JSON response to extract the token and UserProfile

                
                }
                return View ("ShowUnConfirmedUsers");
            }
          }

     return View("ShowUnConfirmedUsers");
       
    }

//Login------------------------------------------------------------------------   
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
                var user_id_admin=responseObject.userProfile.IsAdmin;

                if(user_id_admin==true){

HttpContext.Session.SetString("JWTToken", token); 

                    return RedirectToAction("Index","AccountProfile");
                }
                return View("Login","User");
                }
            }
          }

          return View(user);
          }

          return View(user);
      }
 //Confirm a User Registration
        
[AllowAnonymous]
[HttpPost]
  public async Task<ActionResult> ConfirmUserRegistration(int userid)
  {
      if (userid!=null){
          UserProfileViewModel newUser = new UserProfileViewModel();
          var service = new ServiceRepository(_httpClient,_configuration,_httpContextAccessor);
          {
            using (var response = service.PostResponse("UserProfile/Confirm", userid))
            {
                if (response.IsSuccessStatusCode)
                {
                   string jsonResponse = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON response to extract the token and UserProfile
             

                    return RedirectToAction("ShowUnConfirmedUsers","Admin");
                }
                return View("ShowUnConfirmedUsers","Admin");
                }
            }
          }

          return View("ShowUnConfirmedUsers","Admin");
          }

          
      


    }
}