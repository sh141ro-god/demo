using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace test.Models;

[Table("Job")]
public partial class Job
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    [InverseProperty("IdJobNavigation")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
