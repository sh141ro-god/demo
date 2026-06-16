using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace test.Models;

[Index("Login", Name = "UQ_Employees_Login", IsUnique = true)]
public partial class Employee
{
    [Key]
    public int Id { get; set; }

    [Column("Full_name")]
    [StringLength(100)]
    [Unicode(false)]
    public string FullName { get; set; } = null!;

    [Column("Id_Job")]
    public int IdJob { get; set; }

    [Column("INN")]
    [StringLength(16)]
    [Unicode(false)]
    public string Inn { get; set; } = null!;

    [Column("Passport_number")]
    [StringLength(10)]
    [Unicode(false)]
    public string PassportNumber { get; set; } = null!;

    [StringLength(12)]
    [Unicode(false)]
    public string Number { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Login { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    [ForeignKey("IdJob")]
    [InverseProperty("Employees")]
    public virtual Job IdJobNavigation { get; set; } = null!;
}
