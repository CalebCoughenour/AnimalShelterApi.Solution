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
    [Range(1, 30, ErrorMessage = "Age must be less than 30")]
    public int Age { get; set; }
    [StringLength(20, ErrorMessage = "Gender type must be 20 or less characters")]
    public string Gender { get; set; }
    public DateTime DateCreated  { get; set; }
    public Cat()
    {
      DateCreated = DateTime.Now;
    }

  }
}