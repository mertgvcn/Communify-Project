using CommunifyLibrary.Models;
using System.ComponentModel.DataAnnotations;

namespace LethalCompany_Backend.Models.AuthenticationModels;

public class UserRegisterRequest
{
    [MaxLength(64)]
    public string FirstName { get; set; }

    [MaxLength(64)]
    public string LastName { get; set; }

    [MaxLength(16)]
    public string PhoneNumber { get; set; }

    [MaxLength(64)]
    public string Email { get; set; }

    public DateTime BirthDate { get; set; }

    public Genders Gender { get; set; }

    [MaxLength(64)]
    public string BirthCountry { get; set; }

    [MaxLength(64)]
    public string BirthCity { get; set; }

    [MaxLength(64)]
    public string CurrentCountry { get; set; }

    [MaxLength(64)]
    public string CurrentCity { get; set; }

    [MaxLength(256)]
    public string Address { get; set; }

    [MaxLength(5)]
    public int[] InterestIdList { get; set; }
}
