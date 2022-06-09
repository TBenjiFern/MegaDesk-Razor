using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MegaDesk.Models;
public class Desk
{
    public int DeskId { get; set; }

    [Range(24, 96)]
    [Required]
    public decimal Width { get; set; }

    [Range(12, 48)]
    [Required]
    public decimal Depth { get; set; }

    [Range(0, 7)]
    [Required]
    [Display(Name = "Number of Drawers")]
    public int NumOfDrawers { get; set; }


    [Display(Name = "Desktop Material")]
    public int DesktopMaterialId { get; set; }
    public DesktopMaterial DesktopMaterial { get; set; }

    public DeskQuote DeskQuote { get; set; }
}