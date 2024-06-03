using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ZayiflamaMerkeziTakip.Models;

[Table("Uzmanlik")]
public partial class Uzmanlik
{
    public Uzmanlik()
    {
        Doktorlars = new HashSet<Doktorlar>();
        Randevus = new HashSet<Randevu>();
    }
    [Key]
    [Column("UzmanlikID")]
    public int UzmanlikId { get; set; }

    [Column("Uzmanlik")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Uzmanlik1 { get; set; }

    [InverseProperty("Uzmanlik")]
    public virtual ICollection<Doktorlar> Doktorlars { get; set; } = new List<Doktorlar>();

    [InverseProperty("Uzmanlik")]
    public virtual ICollection<Randevu> Randevus { get; set; } = new List<Randevu>();
}
