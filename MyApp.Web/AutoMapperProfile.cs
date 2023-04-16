using AutoMapper;
using MyApp.Models;
using MyApp.Web.Models.Users;

namespace MyApp.Web;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserViewModel>();
    }
}