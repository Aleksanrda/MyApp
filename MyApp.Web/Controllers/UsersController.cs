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
    private readonly ILogger<UsersController> _logger;

    public UsersController(IServiceFactory userService, IMapper mapper, ILogger<UsersController> logger)
    {
        _serviceFactory = userService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index(bool? isActive = null)
    {
        _logger.LogInformation("The users page has been accessed");
        _logger.LogInformation("GET Index User method is called");

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
            _logger.LogWarning("No users were found.");
            
            return View("NotFound");
        }

        _logger.LogInformation("Users are successfully  shown.");

        var userViewModels = _mapper.Map<IEnumerable<UserViewModel>>(users);

        ViewBag.IsActive = isActive;

        return View(userViewModels);
    }

    [HttpGet]
    public ActionResult Details(int id)
    {
        _logger.LogInformation("The user page has been accessed.");
        _logger.LogInformation("GET Details User method is called");

        var user = _serviceFactory.UserService.GetById(id);

        if (user == null)
        {
            _logger.LogWarning("No user with Id: {Id} was found.", id);

            return View("NotFound");
        }

        _logger.LogInformation("User with Id: {Id} was successfully found", id);

        var userModel = _mapper.Map<UserViewModel>(user);

        return View(userModel);
    }

    [HttpGet]
    public ActionResult Create()
    {
        _logger.LogInformation("The user creation page has been accessed.");
        _logger.LogInformation("GET Create User method is called");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(CreateUserViewModel createUserViewModel)
    {
        _logger.LogInformation("POST Create User method is called");

        if (ModelState.IsValid)
        {
            _logger.LogInformation(
                "User model is valid. Forename: {Forename}, surname: {Surname}, DateOfBirth: {DateOfBirth}, IsActive: {IsActive}",
                createUserViewModel.Forename,
                createUserViewModel.Surname,
                createUserViewModel.DateOfBirth,
                createUserViewModel.Status);

            var user = _mapper.Map<User>(createUserViewModel);

            var createdUser = _serviceFactory.UserService.Create(user);

            _logger.LogInformation("User with id {id} was successfully created.", createdUser.Id);

            return RedirectToAction("Details", new { id = user.Id });
        }

        _logger.LogWarning(
            "User model is not valid. Forename: {Forename}, Surname: {Surname}, " +
            "DateOfBirth: {DateOfBirth}, IsActive: {IsActive}",
            createUserViewModel.Forename,
            createUserViewModel.Surname,
            createUserViewModel.DateOfBirth,
            createUserViewModel.Status);

        return View();
    }

    [HttpGet]
    public ActionResult Edit(int id)
    {
        _logger.LogInformation("The user edition page has been accessed.");
        _logger.LogInformation("GET Edit User method is called");
        
        var model = _serviceFactory.UserService.GetById(id);

        if (model == null)
        {
            _logger.LogWarning("No user with Id: {Id} was found.", id);
            
            return View("NotFound");
        }

        var editUserViewModel = _mapper.Map<UserViewModel>(model);

        return View(editUserViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(UserViewModel userViewModel)
    {
        _logger.LogInformation("POST Edit User method is called");

        if (ModelState.IsValid)
        {
            _logger.LogInformation(
                "User model is valid. Id: {Id}, Forename: {Forename}, surname: {Surname}, DateOfBirth: {DateOfBirth}, IsActive: {IsActive}",
                userViewModel.Id,
                userViewModel.Forename,
                userViewModel.Surname,
                userViewModel.DateOfBirth,
                userViewModel.IsActive);

            var user = _mapper.Map<User>(userViewModel);

            var currentUser = _serviceFactory.UserService.Update(user);

            _logger.LogInformation("User with id {id} was successfully edited.", currentUser.Id);

            return RedirectToAction("Details", new { id = user.Id });
        }

        _logger.LogWarning(
            "User model is not valid. Id: {Id}, Forename: {Forename}, Surname: {Surname}, DateOfBirth: {DateOfBirth}, IsActive: {IsActive}",
            userViewModel.Id,
            userViewModel.Forename,
            userViewModel.Surname,
            userViewModel.DateOfBirth,
            userViewModel.IsActive);

        return View(userViewModel);
    }

    public ActionResult Delete(int id)
    {
        _logger.LogInformation("The page for user deletion has been accessed.");
        _logger.LogInformation("GET Delete User method is called");
        
        var model = _serviceFactory.UserService.GetById(id);

        if (model == null)
        {
            _logger.LogWarning("No user with Id: {Id} was found.", id);
            
            return View("NotFound");
        }

        var userViewModel = _mapper.Map<UserViewModel>(model);

        return View(userViewModel);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        _logger.LogInformation("POST Delete User method is called");
        
        var user = _serviceFactory.UserService.GetById(id);

        if (user == null)
        {
            _logger.LogWarning("No user with Id: {Id} was found.", id);
            
            return View("NotFound");
        }

        _serviceFactory.UserService.Delete(user);

        _logger.LogInformation("User with Id: {Id} was successfully deleted.", id);
        
        return RedirectToAction("Index");
    }
}