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
    public async Task<ActionResult<IEnumerable<Animal>>> Get(string name, string species, int age, string gender)
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

      if (age != 0)
      {
        query = query.Where(entry => entry.Age == age);
      } 

      if (gender != null)
      {
        query = query.Where(entry => entry.Gender == gender);
      } 

      return await query.ToListAsync();
    }

    /// <summary>
    /// Returns an individual animal based off it's Id
    /// </summary>
    /// <returns>A single animal based off Id</returns>
    /// <remarks>
    ///
    /// Example request
    /// GET /api/animals/1
    ///
    /// </remarks>
    ///<response code="200">Returns an animal from shelter list</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    [HttpGet("{id}")]
    public async Task<ActionResult<Animal>> GetAnimal(int id)
    {
        var animal = await _db.Animals.FindAsync(id);

        if (animal == null)
        {
            return NotFound();
        }

        return animal;
    }

    /// <summary>
    /// Add Animal to list
    /// </summary>
    /// <returns> Adds a new animal to the API</returns>
    /// <remarks>
    ///
    /// REQUIRED:
    /// Name, Species, DateTime
    /// 
    ///
    /// </remarks>
    ///<response code="201">Animal created successfully</response>

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    [HttpPost]
    public async Task<ActionResult<Animal>> Post(Animal animal)
    {
      _db.Animals.Add(animal);
      await _db.SaveChangesAsync();

      return CreatedAtAction(nameof(GetAnimal), new { id = animal.AnimalId }, animal);
    }

    /// <summary>
    /// Update Animal in list
    /// </summary>
    /// <returns> Updates an animal in the API</returns>
    /// <remarks>
    ///
    /// REQUIRED:
    /// Name, Species, DateTime
    /// 
    ///
    /// </remarks>
    ///<response code="201">Animal updated successfully</response>

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Animal animal)
    {
      if (id != animal.AnimalId)
      {
        return BadRequest();
      }

      _db.Entry(animal).State = EntityState.Modified;

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!AnimalExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    /// <summary>
    /// Delete Animal from list
    /// </summary>
    /// <returns> Delete an animal in the API</returns>
    /// <remarks>
    ///
    /// Example Request:
    /// DELETE /api/animals/1
    ///
    /// </remarks>
    ///<response code="201">Animal updated successfully</response>

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAnimal(int id)
    {
      var animal = await _db.Animals.FindAsync(id);
      if (animal == null)
      {
        return NotFound();
      }

      _db.Animals.Remove(animal);
      await _db.SaveChangesAsync();

      return NoContent();
    }


    private bool AnimalExists(int id) // Checks if animal is in API
    {
      return _db.Animals.Any(e => e.AnimalId == id);
    }
  }
}