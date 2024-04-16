using System.ComponentModel.DataAnnotations;

namespace LethalCompany_Backend.Models.InterestModels;

public class InterestViewModel
{
    [Required]
    public long Id { get; set; }

    [MaxLength(32), Required]
    public string Name { get; set; }

    [Required]
    public bool IsChecked { get; set; }
}
