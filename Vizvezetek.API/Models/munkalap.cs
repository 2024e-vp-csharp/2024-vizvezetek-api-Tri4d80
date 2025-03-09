using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vizvezetek.API.Models;

[Index("hely_id", Name = "hely_id")]
[Index("szerelo_id", Name = "szerelo_id")]
public partial class munkalap
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    public DateOnly beadas_datum { get; set; }

    public DateOnly javitas_datum { get; set; }

    [Column(TypeName = "int(11)")]
    public int hely_id { get; set; }

    [Column(TypeName = "int(11)")]
    public int szerelo_id { get; set; }

    [Column(TypeName = "int(11)")]
    public int munkaora { get; set; }

    [Column(TypeName = "int(11)")]
    public int anyagar { get; set; }

    [ForeignKey("hely_id")]
    [InverseProperty("munkalap")]
    public virtual hely hely { get; set; } = null!;

    [ForeignKey("szerelo_id")]
    [InverseProperty("munkalap")]
    public virtual szerelo szerelo { get; set; } = null!;
}
