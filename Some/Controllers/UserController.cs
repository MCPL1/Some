using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CourseProject.Data;
using CourseProject.Data.Repositories;
using CourseProject.Models;
using CourseProject.Models.DataModels;
using CourseProject.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace CourseProject.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IOptions<Settings> _option;
        private string st;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<User> _userRepository;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IOptions<Settings> option, IRepository<Order> orderRepository, IRepository<User> userRepository)
        {
            st = option.Value.DbConnection;
            this._userManager = userManager;
            this._signInManager = signInManager;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                    {UserName = model.UserName, Name = model.Name, Surname = model.SurName, PhoneNumber = model.PhoneNum};
                await _userManager.AddToRoleAsync(user, Const.User );
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Item");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Item");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View();
                }
            }

            return View();
        }

        public async Task<IActionResult> BestUser()
        {

            var users = new List<UserBestViewModel>();

     
            await using var connection = new SqlConnection(st);
            await using var command = new SqlCommand();
            await connection.OpenAsync();
            command.Connection = connection;
            command.CommandText = $"SELECT \"User\".id,\"User\".userName,phoneNumber, Count(\"Order\".id) as Quantity " +
                                    "From \"User\" JOIN \"Order\" ON \"User\".id = \"Order\".user_id Group By \"User\".Id, " +
                                    " \"User\".userName,phoneNumber;";
            
            
            SqlDataReader reader;
            try
            {
                reader = await command.ExecuteReaderAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            while (await reader.ReadAsync())
            {
                var user = new UserBestViewModel()
                {
                    Id = ((int)(reader.GetValue(0))),
                    UserName = reader.GetValue(1).ToString(),
                    PhoneNumber = reader.GetValue(2).ToString(),
                    OrderQuantity = (int)(reader.GetValue(3)),
                };
                users.Add(user);
            }

            await connection.CloseAsync();

           
            return View("BestBuyers", users.OrderByDescending(o=>o.OrderQuantity).Take(5));
        }
    }
}