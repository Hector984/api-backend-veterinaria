using API_Veterinaria.Data;
using API_Veterinaria.Utilities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeterinariaTest.Utilities
{
    public class BasePruebas
    {
        protected VeterinariaDbContext ConstruirContext(string nombreDB)
        {
            var opciones = new DbContextOptionsBuilder<VeterinariaDbContext>()
                .UseInMemoryDatabase(nombreDB).Options;

            var dbContext = new VeterinariaDbContext(opciones);

            return dbContext;
        }

        protected IMapper ConfigurarAutoMapper()
        {
            var config = new MapperConfiguration(opciones =>
            {
                opciones.AddProfile(new AutoMapperProfiles());
            });

            return config.CreateMapper();
        }
    }
}
