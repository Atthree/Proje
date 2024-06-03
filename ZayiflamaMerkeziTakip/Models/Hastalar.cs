using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ZayiflamaMerkeziTakip.Models;

[Table("Hastalar")]
public partial class Hastalar
{
    [Key]
    [Column("HastaID")]
    public int HastaId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Isim { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Soyisim { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DogumTarihi { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Cinsiyet { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? TelefonNo { get; set; }
    [NotMapped]
    [DisplayName("Upload Image File")]
    public IFormFile? ImageFile { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Photo { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? KayitTarihi { get; set; }

    [Column("TedaviID")]
    public int? TedaviId { get; set; }

    [InverseProperty("Hasta")]
    public virtual ICollection<HastaTedavileri> HastaTedavileris { get; set; } = new List<HastaTedavileri>();

    [InverseProperty("Hasta")]
    public virtual ICollection<Olcumler> Olcumlers { get; set; } = new List<Olcumler>();

    [InverseProperty("Hasta")]
    public virtual ICollection<Randevu> Randevus { get; set; } = new List<Randevu>();

    [ForeignKey("TedaviId")]
    [InverseProperty("Hastalars")]
    public virtual Tedaviler? Tedavi { get; set; }
}
