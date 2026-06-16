using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace test.Models;

[Table("Reader")]
public partial class Reader
{
    [Key]
    public int Id { get; set; }

    [Column("Full_name")]
    [StringLength(100)]
    [Unicode(false)]
    public string FullName { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Number { get; set; } = null!;

    [Column(TypeName = "money")]
    public decimal Credit { get; set; }

    [InverseProperty("IdReaderNavigation")]
    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();
}
