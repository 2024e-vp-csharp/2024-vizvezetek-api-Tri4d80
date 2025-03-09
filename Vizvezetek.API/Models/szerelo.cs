using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vizvezetek.API.Models;

public partial class szerelo
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [StringLength(50)]
    public string nev { get; set; } = null!;

    [Column(TypeName = "int(4)")]
    public int kezdes_ev { get; set; }

    [InverseProperty("szerelo")]
    public virtual ICollection<munkalap> munkalap { get; set; } = new List<munkalap>();
}
