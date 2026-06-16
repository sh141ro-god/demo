using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace test.Models;

[Table("Log")]
public partial class Log
{
    [Key]
    public int Id { get; set; }

    [Column("Id_Copy")]
    public int IdCopy { get; set; }

    [Column("Id_Reader")]
    public int IdReader { get; set; }

    [Column("Date_of_issue")]
    public DateOnly DateOfIssue { get; set; }

    [Column("Return_date")]
    public DateOnly? ReturnDate { get; set; }

    [Column("Id_Status")]
    public int IdStatus { get; set; }

    [ForeignKey("IdCopy")]
    [InverseProperty("Logs")]
    public virtual Copy IdCopyNavigation { get; set; } = null!;

    [ForeignKey("IdReader")]
    [InverseProperty("Logs")]
    public virtual Reader IdReaderNavigation { get; set; } = null!;

    [ForeignKey("IdStatus")]
    [InverseProperty("Logs")]
    public virtual Status IdStatusNavigation { get; set; } = null!;
}
