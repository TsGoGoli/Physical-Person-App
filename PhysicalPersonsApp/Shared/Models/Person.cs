using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shared.Models;

public class Person
{
    public int PersonId { get; set; }
    [Required]
    [Length(2, 50)]
    public string? Name { get; set; }


    [Required]
    [Length(2, 50)]
    public string? Surname { get; set; }


    [AllowedValues("ქალი", "კაცი")]
    public string? Gender { get; set; }

    [Required]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "PersonalNumber must be exactly 11 characters.")]

    public string? PersonalNumber{ get; set; }
    [Required]
    public DateTime BirthDate { get; set; }
    public City? City { get; set; }
    [JsonIgnore]
    public int? CityId { get; set; }
    public string? Image { get; set; }

    public List<PhoneNumbers> PhoneNumbers { get; set; }
    public List<RelatedPersons> RelatedPersons { get; set; }
}



