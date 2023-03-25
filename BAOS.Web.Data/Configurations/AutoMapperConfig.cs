using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BAOS.Web.Domain.Models;
using BAOS.Web.Domain.ViewModels;

namespace BAOS.Web.Data.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<LoginViewModel, User>().ReverseMap();
            CreateMap<RegisterViewModel, User>().ReverseMap();

        }
    }
}
