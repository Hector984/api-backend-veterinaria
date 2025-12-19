using API_Veterinaria.Business.Interfaces;
using API_Veterinaria.Business.Services;
using API_Veterinaria.Controllers;
using API_Veterinaria.Core.DTOs.Cliente;
using API_Veterinaria.Core.Entities;
using API_Veterinaria.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinariaTest.Utilities;

namespace VeterinariaTest.Controllers
{
    [TestClass]
    public class VeterinariaControllerTest: BasePruebas
    {
        [TestMethod]
        public async Task ObtenerVeterinariaPorId_Retorna404()
        {
            // Preparamos las dependencias que requiere el controlador Veterinarias
            var nombreBD = Guid.NewGuid().ToString();
            var context = ConstruirContext(nombreBD);
            var mapper = ConfigurarAutoMapper();
            var veterinariaRepository = new VeterinariaRepository(context);

            var usuarioService = Substitute.For<IUsuarioService>();

            usuarioService.GetUsuarioByEmail()
                .Returns((Usuario)null);

            var veterinariaService = new VeterinariaService(
                veterinariaRepository,
                usuarioService, 
                mapper
               );

            var controller = new VeterinariasController(veterinariaService);

            // Ejecutamos la prueba
            var respuesta = await controller.ObtenerVeterinariaPorId(1);

            // Verificamos el resultado
            Assert.IsNotNull(respuesta);
            Assert.IsInstanceOfType(respuesta.Result, typeof(NotFoundResult));
            //var resultado = respuesta.Result as StatusCodeResult;
            //Assert.AreEqual(expected: 404, actual: resultado!.StatusCode);
        }
    }
}
