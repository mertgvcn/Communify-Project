using System.ComponentModel.DataAnnotations;

namespace LethalCompany_Backend.Models.InterestModels;

public class InterestViewModel
{
    public long Id { get; set; }

    [MaxLength(32)]
    public string Name { get; set; }

    public bool IsChecked { get; set; }
}
