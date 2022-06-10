using System;
using System.ComponentModel.DataAnnotations;

namespace AnimalShelterApi.Models
{
  public class Cat
  {
    public int CatId { get; set; }
    [Required]
    [StringLength(20, ErrorMessage = "Name must be 20 or less characters")]
    public string Name { get; set; }
    [Required]
    [StringLength(50, ErrorMessage = "Species must be 50 or less characters")]
    public string Species { get; set; }
    [Range(1, 200)]
    public int Age { get; set; }
    public string Gender { get; set; }
    [Required]
    public DateTime DateCreated  { get; set; }
    public Cat()
    {
      DateCreated = DateTime.Now;
    }

  }
}