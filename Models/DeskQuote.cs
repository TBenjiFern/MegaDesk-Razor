using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MegaDesk.Data;

namespace MegaDesk.Models;
public class DeskQuote
{
    private const decimal DEFAULT_DESK_COST = 200.00M;
    private const decimal BASE_SURFACE_AREA_COST = 1.00M;
    private const decimal DRAWER_COST = 50.00M;

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


    public decimal CalcTotalPrice(MegaDeskContext context)
    {
        decimal totalPrice = DEFAULT_DESK_COST;

        decimal surfaceArea = this.Desk.Width * this.Desk.Depth;

        if (surfaceArea > 1000)
        {
            totalPrice += (surfaceArea - 1000) * BASE_SURFACE_AREA_COST;
        }

        totalPrice += DRAWER_COST * this.Desk.NumOfDrawers;

        // context.Desk.DesktopMaterial?
        var desktopMaterialPrices = context.DesktopMaterial
                                    .Where(d => d.DesktopMaterialId == this.Desk.DesktopMaterialId)
                                    .FirstOrDefault();

        totalPrice += desktopMaterialPrices.Cost;

        var shippingPrices = context.DeliveryType
                            .Where(d => d.DeliveryTypeId == this.DeliveryTypeId)
                            .FirstOrDefault();

        if (surfaceArea < 1000) 
        {
            totalPrice += shippingPrices.PriceUnder1000;
        } 
        else if (surfaceArea <= 2000) 
        {
            totalPrice += shippingPrices.PriceBetween1000And2000;
        } 
        else 
        {
            totalPrice += shippingPrices.PriceOver2000;
        }

        return totalPrice;
    }
}