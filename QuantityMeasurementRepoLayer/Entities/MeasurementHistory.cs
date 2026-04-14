using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuantityMeasurementRepoLayer.Entities;

[Table("MeasurementHistory")]
public class MeasurementHistory
{
    [Key]
    public int Id {get; set;}
    public string OperationType {get; set;} = string.Empty;
    public string InputDetails { get; set; } = string.Empty;
    public string Result { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public int? UserId{get; set;}

    [ForeignKey("UserId")]
    public User User {get;set;} = null!;


}