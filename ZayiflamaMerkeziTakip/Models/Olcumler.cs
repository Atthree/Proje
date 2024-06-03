using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ZayiflamaMerkeziTakip.Models;

[Table("Olcumler")]
public partial class Olcumler
{
    [Key]
    [Column("OlcumID")]
    public int OlcumId { get; set; }

    [Column("HastaID")]
    public int? HastaId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? OlcumTarihi { get; set; }

    public double? Kilo { get; set; }

    public double? Boy { get; set; }

    [Column("BMI")]
    public double? Bmi { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Notlar { get; set; }

    [ForeignKey("HastaId")]
    [InverseProperty("Olcumlers")]
    public virtual Hastalar? Hasta { get; set; }
}
