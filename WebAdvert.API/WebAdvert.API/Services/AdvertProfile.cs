﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertAPI.Models;
using AutoMapper;

namespace WebAdvert.API.Services
{
    public class AdvertProfile : Profile
    {
        public AdvertProfile()
        {
            CreateMap<AdvertModel, AdvertDBModel>();
            CreateMap<AdvertModel, AdvertDBModel>().ReverseMap();
        }
    }
}
