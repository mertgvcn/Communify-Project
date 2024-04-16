using CommunifyLibrary.Models;
using System.ComponentModel.DataAnnotations;

namespace LethalCompany_Backend.Models.AuthenticationModels;

public class UserRegisterRequest
{
    [MaxLength(64), Required]
    public string FirstName { get; set; }

    [MaxLength(64), Required]
    public string LastName { get; set; }

    [MaxLength(16), Required]
    public string PhoneNumber { get; set; }

    [MaxLength(64), Required]
    public string Email { get; set; }

    [Required]
    public DateTime BirthDate { get; set; }

    [Required]
    public Genders Gender { get; set; }

    [MaxLength(64), Required]
    public string BirthCountry { get; set; }

    [MaxLength(64), Required]
    public string BirthCity { get; set; }

    [MaxLength(64), Required]
    public string CurrentCountry { get; set; }

    [MaxLength(64), Required]
    public string CurrentCity { get; set; }

    [MaxLength(256)]
    public string Address { get; set; }

    [MaxLength(5)]
    public int[] InterestIdList { get; set; }
}
