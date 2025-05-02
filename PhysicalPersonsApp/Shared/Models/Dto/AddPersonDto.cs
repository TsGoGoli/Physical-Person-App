using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Shared.Models.Dto;

public class AddPersonDto
{

    [Required]
    [Length(2, 50)]
    public string Name { get; set; }


    [Required]
    [Length(2, 50)]
    public string Surname { get; set; }


    [AllowedValues("ქალი", "კაცი")]
    public string Gender { get; set; }

    [Required]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "PersonalNumber must be exactly 11 characters.")]

    public string PersonalNumber { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }
    public int CityID { get; set; }
    public IFormFile Image { get; set; }
}
