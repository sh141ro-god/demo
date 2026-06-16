using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace test.Models;

[Table("Edition")]
public partial class Edition
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("Number_of_pages")]
    public int NumberOfPages { get; set; }

    [Column("Year_of_publication")]
    public int YearOfPublication { get; set; }

    [Column("Id_Book")]
    public int IdBook { get; set; }

    [Column("Id_Publishing_house")]
    public int IdPublishingHouse { get; set; }

    [InverseProperty("IdEditionNavigation")]
    public virtual ICollection<Copy> Copies { get; set; } = new List<Copy>();

    [ForeignKey("IdBook")]
    [InverseProperty("Editions")]
    public virtual Book IdBookNavigation { get; set; } = null!;

    [ForeignKey("IdPublishingHouse")]
    [InverseProperty("Editions")]
    public virtual PublishingHouse IdPublishingHouseNavigation { get; set; } = null!;
}
