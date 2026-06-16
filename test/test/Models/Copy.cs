using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace test.Models;

public partial class Copy
{
    [Key]
    public int Id { get; set; }

    [Column("Id_Edition")]
    public int IdEdition { get; set; }

    [Column("Id_Condition")]
    public int IdCondition { get; set; }

    [ForeignKey("IdCondition")]
    [InverseProperty("Copies")]
    public virtual Condition IdConditionNavigation { get; set; } = null!;

    [ForeignKey("IdEdition")]
    [InverseProperty("Copies")]
    public virtual Edition IdEditionNavigation { get; set; } = null!;

    [InverseProperty("IdCopyNavigation")]
    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();
}
