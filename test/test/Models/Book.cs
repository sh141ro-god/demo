using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace test.Models;

[Table("Book")]
public partial class Book: ObservableObject
{
    [Key]
    public int Id { get; set; }

    [ObservableProperty]
    [property: StringLength(100)]
    [property: Unicode(false)]
    [property: Display(Name = "Имя")]
    private string _name = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string Description { get; set; } = null!;

    [InverseProperty("IdBookNavigation")]
    public virtual ICollection<Edition> Editions { get; set; } = new List<Edition>();

    [ForeignKey("IdBook")]
    [InverseProperty("IdBooks")]
    public virtual ICollection<Author> IdAuthors { get; set; } = new List<Author>();

    [ForeignKey("IdBook")]
    [InverseProperty("IdBooks")]
    public virtual ICollection<Category> IdCategories { get; set; } = new List<Category>();

    [ForeignKey("IdBook")]
    [InverseProperty("IdBooks")]
    public virtual ICollection<Genre> IdGenres { get; set; } = new List<Genre>();
}
