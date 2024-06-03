using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ZayiflamaMerkeziTakip.Models;

[Table("Tedaviler")]
public partial class Tedaviler
{
    [Key]
    [Column("TedaviID")]
    public int TedaviId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? TedaviAdi { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Aciklama { get; set; }

    [Column(TypeName = "money")]
    public decimal? Ucret { get; set; }

    [InverseProperty("Tedavi")]
    public virtual ICollection<HastaTedavileri> HastaTedavileris { get; set; } = new List<HastaTedavileri>();

    [InverseProperty("Tedavi")]
    public virtual ICollection<Hastalar> Hastalars { get; set; } = new List<Hastalar>();
}
