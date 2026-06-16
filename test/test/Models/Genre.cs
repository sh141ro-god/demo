using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace test.Models;

public partial class Genre
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [ForeignKey("IdGenre")]
    [InverseProperty("IdGenres")]
    public virtual ICollection<Book> IdBooks { get; set; } = new List<Book>();
}
