using AutoMapper;
using Data.Models;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interface.Initializer
{
    public static class MapperInitializer
    {
        public static void MapperConfiguration()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Category, CategoryDO>()
                .ForMember(dest => dest.ParentCategory, opt => opt.ResolveUsing(fa => fa.Parent))
                .ForMember(dest => dest.ProductList, opt => opt.ResolveUsing(fa => fa.Product))
                .ForMember(dest => dest.SubCategoryList, opt => opt.ResolveUsing(fa => fa.InverseParent))
                .ReverseMap();

                config.CreateMap<Product, ProductDO>().ReverseMap();
            });
        }
    }
}
