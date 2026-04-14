using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuantityMeasurementRepoLayer.Entities;

[Table("Users")]
public class User
{
    [Key]
    public int Id {get;set;}
    [Required]
    public string Username {get; set;} = string.Empty;

    public string PasswordHash {get; set;} = string.Empty;

    public ICollection<MeasurementHistory> MeasurementHistories { get; set; } = new List<MeasurementHistory>();
}