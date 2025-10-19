using API_Veterinaria.Business.Interfaces;
using API_Veterinaria.Core.DTOs.Veterinaria;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API_Veterinaria.Controllers
{
    [ApiController]
    [Route("/api/veterinarias")]
    [Authorize]
    public class VeterinariasController : ControllerBase
    {
        private readonly IVeterinariaService _veterinariaService;

        public VeterinariasController(IVeterinariaService veterinariaService)
        {
            _veterinariaService = veterinariaService;
        }

        //GET: VeterinariasController
        [HttpGet]
        public async Task<ActionResult> GetVeterinarias()
        {
            var veterinariasDTO = await _veterinariaService.GetVeterinariasAsync();

            return Ok(veterinariasDTO);
        }

        // GET: VeterinariasController/5
        [HttpGet("{id:int}", Name = "ObtenerVeterinaria")]
        public async Task<ActionResult> GetVeterinaria(int id)
        {
            var veterinaria = await _veterinariaService.GetVeterinariaByIdAsync(id);

            if (veterinaria is null)
            {
                return NotFound();
            }

            return Ok(veterinaria);
        }

        // POST: VeterinariasController
        [HttpPost]
        public async Task<ActionResult> PostVeterinaria(RegistrarVeterinariaDTO registrarVeterinariaDTO)
        {

            var veterinariaDTO = await _veterinariaService.PostVeterinariaAsync(registrarVeterinariaDTO);

            return CreatedAtRoute("ObtenerVeterinaria", new { veterinariaDTO.Id }, veterinariaDTO); 
        }

        // GET: VeterinariasController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        // POST: VeterinariasController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: VeterinariasController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: VeterinariasController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
