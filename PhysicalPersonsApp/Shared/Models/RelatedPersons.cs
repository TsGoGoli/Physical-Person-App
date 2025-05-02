using System.ComponentModel.DataAnnotations;

namespace Shared.Models;

public class RelatedPersons
{
    public int PersonId { get; set; }
    public int RelatedPersonId { get; set; }
    [AllowedValues("კოლეგა", "ნაცნობი", "ნათესავი", "სხვა")]
    public string RelationshipType { get; set; }
}
