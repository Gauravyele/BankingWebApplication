using AutoMapper;
using Banking.Models.Models.ViewModels;
using BankingWebApplication.Models;

namespace BankingWebApplication.Profiles
{
    public class MappingProfile:Profile
    {
        public MappingProfile() {
            CreateMap<ApplicationUser, RegisterViewModel>().ReverseMap();
            CreateMap<ApplicationUser, EditUserViewModel>().ReverseMap();
        }
        
    }
}
