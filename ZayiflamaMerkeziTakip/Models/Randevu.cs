using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ZayiflamaMerkeziTakip.Models;

[Table("Randevu")]
public partial class Randevu
{
    [Key]
    [Column("RandevuID")]
    public int RandevuId { get; set; }

    [Column("HastaID")]
    public int? HastaId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RandevuTarihi { get; set; }

    [Column("UzmanlikID")]
    public int? UzmanlikId { get; set; }

    [Column("DoktorID")]
    public int? DoktorId { get; set; }

    [ForeignKey("DoktorId")]
    [InverseProperty("Randevus")]
    public virtual Doktorlar? Doktor { get; set; }

    [ForeignKey("HastaId")]
    [InverseProperty("Randevus")]
    public virtual Hastalar? Hasta { get; set; }

    [ForeignKey("UzmanlikId")]
    [InverseProperty("Randevus")]
    public virtual Uzmanlik? Uzmanlik { get; set; }
}
