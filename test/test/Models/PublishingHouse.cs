using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace test.Models;

[Table("Publishing_house")]
public partial class PublishingHouse
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [InverseProperty("IdPublishingHouseNavigation")]
    public virtual ICollection<Edition> Editions { get; set; } = new List<Edition>();
}
