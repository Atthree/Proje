using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ZayiflamaMerkeziTakip.Models;

[Table("HastaTedavileri")]
public partial class HastaTedavileri
{
    [Key]
    [Column("HastaTedaviID")]
    public int HastaTedaviId { get; set; }

    [Column("HastaID")]
    public int? HastaId { get; set; }

    [Column("TedaviID")]
    public int? TedaviId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? BaslangicTarihi { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? BitisTarihi { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Notlar { get; set; }

    [ForeignKey("HastaId")]
    [InverseProperty("HastaTedavileris")]
    public virtual Hastalar? Hasta { get; set; }

    [ForeignKey("TedaviId")]
    [InverseProperty("HastaTedavileris")]
    public virtual Tedaviler? Tedavi { get; set; }
}
