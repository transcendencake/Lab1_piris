using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Lab1_piris.Data.Models;

public class Client
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public DateTime BirthDate { get; set; }
    public bool IsMale { get; set; }
    public string PassportSeries { get; set; }
    [MaxLength(7), MinLength(7)]
    public string PassportNumber { get; set; }
    public string PassportIssuedBy { get; set; }
    public DateTime PassportIssuedAt { get; set; }
    [MaxLength(14), MinLength(14)]
    public string PassportId { get; set; }
    public string BirthPlace { get; set; }
    public City LivingCity { get; set; }
    public long LivingCityId { get; set; }
    public string LivingAddress { get; set; }
    [MaxLength(9), MinLength(9)]
    public string? HomePhone { get; set; }
    [MaxLength(9), MinLength(9)]
    public string? MobilePhone { get; set; }
    public string? Email { get; set; }
    public string? PlaceOfWork { get; set; }
    public string? WorkingPosition { get; set; }
    public City RegistrationCity { get; set; }
    public long RegistrationCityId { get; set; }
    public FamilyState FamilyState { get; set; }
    public long FamilyStateId { get; set; }
    public Citizenship Citizenship { get; set; }
    public long CitizenshipId { get; set; }
    public Disability Disability { get; set; }
    public long DisabilityId { get; set; }
    public bool Pensioner { get; set; }
    public decimal? MonthIncome { get; set; }

    public string Pin { get; set; }
}