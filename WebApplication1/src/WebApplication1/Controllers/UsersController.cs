using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Produces("application/json")]
    [Route("api/users")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("{userId}/balance")]
        [HttpGet]
        public async Task<IActionResult> GetBalance([FromRoute] string userId)
        {
            var amountIn = await _context.Transactions.Where(t => t.ReceiverId == userId).SumAsync(t => t.Amount);

            var amountOut = await _context.Transactions.Where(t => t.SenderId == userId).SumAsync(t => t.Amount);

            var balance = amountIn - amountOut;

            return new ObjectResult(balance);
        }

       
    }
}