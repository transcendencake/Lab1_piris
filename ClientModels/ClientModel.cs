namespace Lab1_piris.ClientModels;

public class ClientModel
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public DateTime BirthDate { get; set; }
    public bool IsMale { get; set; }
    public string PassportSeries { get; set; }
    public string PassportNumber { get; set; }
    public string PassportIssuedBy { get; set; }
    public DateTime PassportIssuedAt { get; set; }
    public string PassportId { get; set; }
    public string BirthPlace { get; set; }
    public SelectedItemModel LivingCity { get; set; }
    public string LivingAddress { get; set; }
    public string? HomePhone { get; set; }
    public string? MobilePhone { get; set; }
    public string? Email { get; set; }
    public string? PlaceOfWork { get; set; }
    public string? WorkingPosition { get; set; }
    public SelectedItemModel RegistrationCity { get; set; }
    public SelectedItemModel FamilyState { get; set; }
    public SelectedItemModel Сitizenship { get; set; }
    public SelectedItemModel Disability { get; set; }
    public bool Pensioner { get; set; }
    public decimal? MonthIncome { get; set; }
}