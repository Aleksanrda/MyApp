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
    public IActionResult Index()
    {
        var users = _serviceFactory.UserService.GetAll();
        
        if (users == null)
        {
            return View("NotFound");
        }
        
        return View(users);
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
    public ActionResult Create(User user)
    {
        var users = _serviceFactory.UserService.Create(user);
            
        if (ModelState.IsValid)
        { 
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

        return View(model);
    }

    [HttpPost]
    public ActionResult Edit(User user)
    {
        if (ModelState.IsValid)
        {
            var currentProduct = _serviceFactory.UserService.Update(user);

            return RedirectToAction("Details", new { id = user.Id });
        }

        return View(user);
    }

    [HttpGet]
    public ActionResult Delete(int id)
    {
        var model = _serviceFactory.UserService.GetById(id);

        if (model == null)
        {
            return View("NotFound");
        }

        return PartialView(model);
    }

    [HttpPost]
    public ActionResult Delete(int id, FormCollection form)
    {
        var user = _serviceFactory.UserService.GetById(id);
        
        _serviceFactory.UserService.Delete(user);

        return RedirectToAction("Index");
    }
}