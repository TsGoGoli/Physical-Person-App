using System.ComponentModel.DataAnnotations;

namespace Shared.Models.Dto;

public class AddRelationDto
{
    public int PersonId { get; set; }
    public int RelatedPersonId { get; set; }
    [AllowedValues("კოლეგა", "ნაცნობი", "ნათესავი", "სხვა")]
    public string RelationShipType { get; set; }
}