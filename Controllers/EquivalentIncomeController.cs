using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using planty_compare_portal.Data;

namespace planty_compare_portal.Controllers
{
    [Route("api/equivalent-income")]
    [ApiController]
    public class EquivalentIncomeController : ControllerBase
    {
        // private readonly MyDataContext _context;
        private readonly MyDbContext _context;

        // public PurchasingPowerController(MyDataContext context)
        public EquivalentIncomeController(MyDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        // GET: api/equivalent-income
        //          ?targetCity=Berlin&targetCurrency=USD&baseCity=Kuala%20Lumpur&baseIncomeAmount=4000&baseCurrency=USD
        [HttpGet]
        public async Task<ActionResult<Decimal>> CalculateEquivalentIncome(
            [FromQuery] string targetCity, [FromQuery] string targetCurrency, 
            [FromQuery] string baseCity, [FromQuery] decimal baseIncomeAmount, [FromQuery] string baseCurrency)
        {
            var currencies = new string[] { targetCurrency, baseCurrency };
            // TODO: Add support for more currencies!
            if (currencies.Any(c => c != "USD")) {
                return BadRequest("Currently, only USD is supported!");
            }

            var purchasingPowers = await Task
            .WhenAll(
                _context.PurchasingPower
                    .Where(row => row.Category == "N" && row.City == targetCity)
                    .OrderByDescending(row => row.Year)
                    .FirstAsync(),
                _context.PurchasingPower
                    .Where(row => row.Category == "N" && row.City == baseCity)
                    .OrderByDescending(row => row.Year)
                    .FirstAsync());

            if (purchasingPowers.Any(x => x == null))
            {
                return NotFound();
            }

            var targetPurchasingPower = purchasingPowers[0];
            var basePurchasingPower = purchasingPowers[1];

            return (baseIncomeAmount / basePurchasingPower.Value) * targetPurchasingPower.Value;
        }
    }
}
