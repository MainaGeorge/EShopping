﻿using AutoMapper;

namespace Catalog.Application.Mappers
{
    public static class Mappings
    {
        private static readonly Lazy<IMapper> Lazy = new(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<MappingProfile>();
            });

            var mapper = config.CreateMapper();

            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;
    }
}
