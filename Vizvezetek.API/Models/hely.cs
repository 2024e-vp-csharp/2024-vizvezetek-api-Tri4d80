using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vizvezetek.API.Models;

public partial class hely
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [StringLength(50)]
    public string telepules { get; set; } = null!;

    [StringLength(75)]
    public string utca { get; set; } = null!;

    [InverseProperty("hely")]
    public virtual ICollection<munkalap> munkalap { get; set; } = new List<munkalap>();
}
