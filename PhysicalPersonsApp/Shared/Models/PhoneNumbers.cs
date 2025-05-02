using System.ComponentModel.DataAnnotations;

namespace Shared.Models;

public class PhoneNumbers
{
    public int PhoneNumberId { get; set; }
    public int PersonId { get; set; }
    [AllowedValues("მობილური", "ოფისის", "სახლის")]
    public string PhoneType { get; set; }
    [Length(4, 50)]
    public string PhoneNumber { get; set; }

}
