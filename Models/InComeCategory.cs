using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace person_money.Models;

public partial class InComeCategory
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Укажите категорию")]
    [Display(Name = "Категория доходов")]
    public string Name { get; set; } = null!;

    public virtual ICollection<InCome> InComes { get; } = new List<InCome>();
}
