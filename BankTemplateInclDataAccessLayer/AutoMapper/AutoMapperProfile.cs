using AutoMapper;
using BankApp.ViewModels;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;

namespace Services.Infrastructure.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Källa => Mål
        // CreateEmployeeViewModel => Employee
        CreateMap<CustomerViewModel, Customer>()
            .ReverseMap();

        CreateMap<IdentityUserViewModel, IdentityUser>()
            .ReverseMap();
    }
}