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
  public class DogsController : ControllerBase
  {
    private readonly AnimalShelterApiContext _db;

    public DogsController(AnimalShelterApiContext db)
    {
      _db = db;
    }

    /// <summary>
    /// Returns a list of dogs
    /// </summary>
    /// <returns> A List of dogs</returns>
    /// <remarks>
    ///
    /// Sample request
    /// GET /api/dogs
    ///
    /// </remarks>
    ///<response code="200">Returns a list of dogss in shelter</response>

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Dog>>> Get(string name, string species, int age, string gender)
    {
      var query = _db.Dogs.AsQueryable();

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
    /// Returns an individual dog based off it's Id
    /// </summary>
    /// <returns>A single dog based off Id</returns>
    /// <remarks>
    ///
    /// Example request
    /// GET /api/dogs/1
    ///
    /// </remarks>
    ///<response code="200">Returns a dog from shelter list</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    [HttpGet("{id}")]
    public async Task<ActionResult<Dog>> GetDog(int id)
    {
        var dog = await _db.Dogs.FindAsync(id);

        if (dog == null)
        {
            return NotFound();
        }

        return dog;
    }

    /// <summary>
    /// Add dog to list
    /// </summary>
    /// <returns> Adds a new dog to the API</returns>
    /// <remarks>
    ///
    /// REQUIRED:
    /// Name, Species, DateTime
    /// 
    ///
    /// </remarks>
    ///<response code="201">Dog created successfully</response>

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    [HttpPost]
    public async Task<ActionResult<Dog>> Post(Dog dog)
    {
      _db.Dogs.Add(dog);
      await _db.SaveChangesAsync();

      return CreatedAtAction(nameof(GetDog), new { id = dog.DogId }, dog);
    }

    /// <summary>
    /// Update dog from list
    /// </summary>
    /// <returns> Updates a dog in the API</returns>
    /// <remarks>
    ///
    /// REQUIRED:
    /// Name, Species, DateTime
    /// 
    ///
    /// </remarks>
    ///<response code="201">Dog updated successfully</response>

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Dog dog)
    {
      if (id != dog.DogId)
      {
        return BadRequest();
      }

      _db.Entry(dog).State = EntityState.Modified;

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!DogExists(id))
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
    /// Delete dog from list
    /// </summary>
    /// <returns> Delete a dog in the API</returns>
    /// <remarks>
    ///
    /// Example Request:
    /// DELETE /api/dogs/1
    ///
    /// </remarks>
    ///<response code="201">Dog deleted successfully</response>

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDog(int id)
    {
      var dog = await _db.Dogs.FindAsync(id);
      if (dog == null)
      {
        return NotFound();
      }

      _db.Dogs.Remove(dog);
      await _db.SaveChangesAsync();

      return NoContent();
    }


    private bool DogExists(int id) // Checks if animal is in API
    {
      return _db.Dogs.Any(e => e.DogId == id);
    }
  }
}