using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace person_money.Models;

public partial class Report
{
    public int Id { get; set; }
    [Display(Name = "Пользователь")]
    public int IdClient { get; set; }
    [Display(Name = "Доходы")]
    public string IncomesRep { get; set; } = null!;
    [Display(Name = "Расходы")]
    public string WastesRep { get; set; } = null!;
    [Display(Name = "Описание")]
    public string Content { get; set; } = null!;
    [Display(Name = "Пользователь")]
    public virtual User? IdClientNavigation { get; set; } = null!;
}
