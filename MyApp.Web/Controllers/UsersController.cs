using Microsoft.AspNetCore.Mvc;
using MyApp.Models;
using MyApp.Services.Factories.Interfaces;
using AutoMapper;
using MyApp.Web.Models.Users;

namespace MyApp.Web.Controllers;

public class UsersController : Controller
{
    private readonly IServiceFactory _serviceFactory;
    private readonly IMapper _mapper;
    
    public UsersController(IServiceFactory userService, IMapper mapper)
    {
        _serviceFactory = userService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public IActionResult Index(bool? isActive = null)
    {
        IEnumerable<User> users;
        
        if (isActive.HasValue)
        {
            users = isActive == true
                ? _serviceFactory.UserService.FilterByActive(true)
                : _serviceFactory.UserService.FilterByActive(false);
        }
        else
        {
            users = _serviceFactory.UserService.GetAll();
        }

        if (users == null)
        {
            return View("NotFound");
        }

        var userViewModels = _mapper.Map<IEnumerable<UserViewModel>>(users);
        
        ViewBag.IsActive = isActive;

        return View(userViewModels);
    }
    
    [HttpGet]
    public ActionResult Details(int id)
    {
        var user = _serviceFactory.UserService.GetById(id);

        if (user == null)
        {
            return View("NotFound");
        }

        var userModel = _mapper.Map<UserViewModel>(user);

        return View(userModel);
    }

    [HttpGet]
    public ActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(CreateUserViewModel createUserViewModel)
    {
        if (ModelState.IsValid)
        {
            var user = _mapper.Map<User>(createUserViewModel);
                
            var createdUser = _serviceFactory.UserService.Create(user);
        
            ViewData["Created"] = "User is successfully created";
            
            return RedirectToAction("Details", new { id = user.Id });
        }

        return View();
    }

    [HttpGet]
    public ActionResult Edit(int id)
    {
        var model = _serviceFactory.UserService.GetById(id);

        if (model == null)
        {
            return View("NotFound");
        }

        var editUserViewModel = _mapper.Map<UserViewModel>(model);

        return View(editUserViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(UserViewModel userViewModel)
    {
        if (ModelState.IsValid)
        {
            var user = _mapper.Map<User>(userViewModel);
            var currentProduct = _serviceFactory.UserService.Update(user);

            return RedirectToAction("Details", new { id = user.Id });
        }

        return View(userViewModel);
    }
    
    public ActionResult Delete(int id)
    {
        var model = _serviceFactory.UserService.GetById(id);

        if (model == null)
        {
            return View("NotFound");
        }

        var userViewModel = _mapper.Map<UserViewModel>(model); 

        return View(userViewModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        var user = _serviceFactory.UserService.GetById(id);
        
        if (user == null)
        {
            return View("NotFound");
        }
        
        _serviceFactory.UserService.Delete(user);

        return RedirectToAction("Index");
    }
}