using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MegaDesk.Models;
public class DeliveryType
{
    public int DeliveryTypeId { get; set; }

    [Display(Name = "Delivery")]
    public string DeliveryName { get; set; }

    public decimal PriceUnder1000 { get; set; }

    public decimal PriceBetween1000And2000 { get; set; }

    public decimal PriceOver2000 { get; set; }
}