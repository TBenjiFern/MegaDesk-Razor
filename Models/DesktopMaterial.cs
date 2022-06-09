using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MegaDesk.Models;
public class DesktopMaterial
{
    public int DesktopMaterialId { get; set; }

    public string DesktopMaterialName { get; set; }

    public decimal Cost { get; set; }
}