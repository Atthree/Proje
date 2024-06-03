using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ZayiflamaMerkeziTakip.Models;

[Table("Doktorlar")]
public partial class Doktorlar
{
    [Key]
    [Column("DoktorID")]
    public int DoktorId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? İsim { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Soyisim { get; set; }

    [Column("UzmanlikID")]
    public int? UzmanlikId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? TelefonNo { get; set; }
    [NotMapped]
    [DisplayName("Upload Image File")]
    public IFormFile? ImageFile { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Photo { get; set; }

    [InverseProperty("Doktor")]
    public virtual ICollection<Randevu> Randevus { get; set; } = new List<Randevu>();

    [ForeignKey("UzmanlikId")]
    [InverseProperty("Doktorlars")]
    public virtual Uzmanlik? Uzmanlik { get; set; }
    
}
