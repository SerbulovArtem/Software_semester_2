using ActionManager.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;
using WEB_MVC.Models;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace WEB_MVC.App
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TblTypeAction, TblTypeActionModel>().ReverseMap();
            CreateMap<TblProduct, TblProductModel>().ReverseMap();
            CreateMap<TblAction, TblActionModel>().ReverseMap();
            CreateMap<TblUser,  TblUserModel>().ReverseMap();
        }
    }
}
