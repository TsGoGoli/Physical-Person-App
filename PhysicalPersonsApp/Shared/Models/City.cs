using System.ComponentModel.DataAnnotations;

namespace Shared.Models;

public class City
{
    public int CityId { get; set; }
    [Required]
    [MaxLength(50)]
    public string CityName { get; set; }
}

