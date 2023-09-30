using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace person_money.Models;

public partial class Waste
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Укажите сумму")]
    [Display(Name = "Сумма")]
    public string Sum { get; set; } = null!;
    [Required(ErrorMessage = "Укажите дату")]
    [Display(Name = "Дата покупки")]
    public DateTime DateWaste { get; set; }
    [Required(ErrorMessage = "Укажите описание")]
    [Display(Name = "Описание")]
    public string Description { get; set; } = null!;
    [Required(ErrorMessage = "Укажите категорию")]
    [Display(Name = "Категория расходов")]
    public int IdCategory { get; set; }
    [Display(Name = "Пользователь")]
    public int IdClient { get; set; }
    [Display(Name = "Категория расходов")]
    public virtual WasteCategory? IdCategoryNavigation { get; set; } = null!;
    [Display(Name = "Пользователь")]
    public virtual User? IdClientNavigation { get; set; } = null!;
}
