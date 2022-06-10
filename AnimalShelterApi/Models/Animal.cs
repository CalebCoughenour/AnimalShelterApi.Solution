using System;
using System.ComponentModel.DataAnnotations;

namespace AnimalShelterApi.Models
{
  public class Animal
  {
    public int AnimalId { get; set; }
    [Required]
    [StringLength(20, ErrorMessage = "Name must be 20 or less characters")]
    public string Name { get; set; }
    [Required]
    [StringLength(50, ErrorMessage = "Species must be 50 or less characters")]
    public string Species { get; set; }
    [Range(1, 200)]
    public string Age { get; set; }
    public string Gender { get; set; }
    [Required]
    public DateTime DateCreated  { get; set; }
    public Animal()
    {
      DateCreated = DateTime.Now;
    }

  }
}