using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace person_money.Models;

public partial class InCome
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Укажите средства")]
    [Display(Name = "Средства")]
    public string Sum { get; set; } = null!;
    [Required(ErrorMessage = "Укажите дату пополнения")]
    [Display(Name = "Дата пополнения")]
    public DateTime DateIn { get; set; }
    [Display(Name = "Пользователь")]
    public int IdClient { get; set; }
    [Required(ErrorMessage = "Укажите категорию")]
    [Display(Name = "Категория доходов")]
    public int IdInComeCat { get; set; }
    [Display(Name = "Пользователь")]
    public virtual User? IdClientNavigation { get; set; } = null!;
    [Display(Name = "Категория доходов")]
    public virtual InComeCategory? IdInComeCatNavigation { get; set; } = null!;
}
