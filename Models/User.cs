using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace person_money.Models;

public partial class User
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Укажите логин")]

    [Display(Name = "Логин")]
    public string Login { get; set; } = null!;
    [Required(ErrorMessage = "Укажите пароль")]
    [Display(Name = "Пароль")]
    public string Password { get; set; } = null!;
    [Display(Name = "Роль пользователя")]
    public int IdRole { get; set; }
    [Display(Name = "Роль пользователя")]
    public virtual Role? IdRoleNavigation { get; set; } = null!;

    public virtual ICollection<InCome> InComes { get; } = new List<InCome>();

    public virtual ICollection<Report> Reports { get; } = new List<Report>();

    public virtual ICollection<Waste> Wastes { get; } = new List<Waste>();
}
