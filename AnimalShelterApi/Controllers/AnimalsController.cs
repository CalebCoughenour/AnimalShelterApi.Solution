using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimalShelterApi.Models;

namespace AnimalShelterApi.Controllers
{
  [Produces("application/json")]
  [Route("api/[controller]")]
  [ApiController]
  public class AnimalsController : ControllerBase
  {
    private readonly AnimalShelterApiContext _db;

    public AnimalsController(AnimalShelterApiContext db)
    {
      _db = db;
    }

    /// <summary>
    /// Returns a list of animals
    /// </summary>
    /// <returns> A List of animals</returns>
    /// <remarks>
    ///
    /// Sample request
    /// GET /api/animals
    ///
    /// </remarks>
    ///<response code="200">Returns a list of animals in shelter</response>

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Animal>>> Get(string name, string species, string age, string gender)
    {
      var query = _db.Animals.AsQueryable();

      if (name != null)
      {
        query = query.Where(entry => entry.Name == name);
      } 

      if (species != null)
      {
        query = query.Where(entry => entry.Species == species);
      }

      if (age != null)
      {
        query = query.Where(entry => entry.Age == age);
      } 

      if (gender != null)
      {
        query = query.Where(entry => entry.Gender == gender);
      } 

      return await query.ToListAsync();
    }
  }
}