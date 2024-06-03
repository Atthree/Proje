using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ZayiflamaMerkeziTakip.Models;

[Table("Login")]
public partial class Login
{
    [Key]
    public int UserId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Username { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Password { get; set; }

    public bool IsActive { get; set; }
}
