using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MegaDesk.Models;
public class DeskQuote
{
    private const decimal BASE_DESK_PRICE = 200.0M;
    private const decimal SURFACE_AREA_COST = 1.0M;
    private const decimal DRAWER_COST = 50.0M;

    public int DeskQuoteId { get; set; }

    [Required]
    [Display(Name = "Customer Name")]
    public string CustomerName { get; set; }

    [Display(Name = "Quote Date")]
    public DateTime QuoteDate { get; set; }

    [Display(Name = "Quote Price")]
    [DisplayFormat(DataFormatString = "{0:C}")]
    public decimal QuotePrice { get; set; }

    [Required]
    public int DeskId { get; set; }

    [Display(Name = "Delivery Type")]
    public int DeliveryTypeId { get; set; }

    public Desk Desk { get; set; }

    public DeliveryType DeliveryType { get; set; }
}