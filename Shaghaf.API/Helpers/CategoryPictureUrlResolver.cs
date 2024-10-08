﻿using AutoMapper;
using Shaghaf.Core.Dtos;
using Shaghaf.Core.Entities.HomeEntities;

namespace Shaghaf.API.Helpers
{
    public class CategoryPictureUrlResolver : IValueResolver<Category, CategoryToReturnDto, string>
    {
        private readonly IConfiguration _configuration;

        public CategoryPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Category source, CategoryToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ImageUrl))

                return $"{_configuration["ApiBaseUrl"]}/{source.ImageUrl}";

            return string.Empty;
        }

    }
}
