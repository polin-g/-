using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace person_money.Models;

public partial class Role
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Укажите дату пополнения")]
    [Display(Name = "Роль пользователя")]
    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; } = new List<User>();
}
