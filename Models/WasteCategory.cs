using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace person_money.Models;

public partial class WasteCategory
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Укажите категорию")]
    [Display(Name = "Категория расходов")]
    public string Name { get; set; } = null!;

    public virtual ICollection<Waste> Wastes { get; } = new List<Waste>();
}
