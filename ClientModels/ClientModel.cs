using System.ComponentModel.DataAnnotations;

namespace Lab1_piris.ClientModels;

public class ClientModel
{
    public long Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string MiddleName { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }
    public bool IsMale { get; set; }
    [Required]
    public string PassportSeries { get; set; }
    [Required]
    public string PassportNumber { get; set; }
    [Required]
    public string PassportIssuedBy { get; set; }
    [Required]
    public DateTime PassportIssuedAt { get; set; }
    [Required]
    public string PassportId { get; set; }
    [Required]
    public string BirthPlace { get; set; }
    [Required]
    public SelectedItemModel LivingCity { get; set; }
    [Required]
    public string LivingAddress { get; set; }
    public string? HomePhone { get; set; }
    public string? MobilePhone { get; set; }
    public string? Email { get; set; }
    public string? PlaceOfWork { get; set; }
    public string? WorkingPosition { get; set; }
    [Required]
    public SelectedItemModel RegistrationCity { get; set; }
    [Required]
    public SelectedItemModel FamilyState { get; set; }
    [Required]
    public SelectedItemModel Citizenship { get; set; }
    [Required]
    public SelectedItemModel Disability { get; set; }
    public bool Pensioner { get; set; }
    public decimal? MonthIncome { get; set; }
}