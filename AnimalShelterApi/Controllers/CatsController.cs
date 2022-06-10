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
  public class CatsController : ControllerBase
  {
    private readonly AnimalShelterApiContext _db;

    public CatsController(AnimalShelterApiContext db)
    {
      _db = db;
    }

    /// <summary>
    /// Returns a list of cats
    /// </summary>
    /// <returns> A List of cats</returns>
    /// <remarks>
    ///
    /// Sample request
    /// GET /api/cats
    ///
    /// </remarks>
    ///<response code="200">Returns a list of cats in shelter</response>

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cat>>> Get(string name, string species, int age, string gender)
    {
      var query = _db.Cats.AsQueryable();

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
    /// Returns an individual cat based off it's Id
    /// </summary>
    /// <returns>A single cat based off Id</returns>
    /// <remarks>
    ///
    /// Example request
    /// GET /api/cats/1
    ///
    /// </remarks>
     ///<response code="200">Returns a cat from shelter list</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    [HttpGet("{id}")]
    public async Task<ActionResult<Cat>> GetCat(int id)
    {
        var cat = await _db.Cats.FindAsync(id);

        if (cat == null)
        {
            return NotFound();
        }

        return cat;
    }

    /// <summary>
    /// Add cat to list
    /// </summary>
    /// <returns> Adds a new cat to the API</returns>
    /// <remarks>
    ///
    /// REQUIRED:
    /// Name, Species, DateTime
    /// 
    ///
    /// </remarks>
    ///<response code="201">Cat created successfully</response>

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    [HttpPost]
    public async Task<ActionResult<Cat>> Post(Cat cat)
    {
      _db.Cats.Add(cat);
      await _db.SaveChangesAsync();

      return CreatedAtAction(nameof(GetCat), new { id = cat.CatId }, cat);
    }

    /// <summary>
    /// Update cat from list
    /// </summary>
    /// <returns> Updates a cat in the API</returns>
    /// <remarks>
    ///
    /// REQUIRED:
    /// Name, Species, DateTime
    /// 
    ///
    /// </remarks>
    ///<response code="201">Cat updated successfully</response>

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Cat cat)
    {
      if (id != cat.CatId)
      {
        return BadRequest();
      }

      _db.Entry(cat).State = EntityState.Modified;

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!CatExists(id))
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

     private bool CatExists(int id) // Checks if cat is in API
    {
      return _db.Cats.Any(e => e.CatId == id);
    }
  }
}