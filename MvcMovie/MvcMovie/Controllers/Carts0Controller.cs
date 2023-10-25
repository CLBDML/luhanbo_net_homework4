using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class Carts0Controller : Controller
    {
        private readonly MvcMovieContext _context;

        public Carts0Controller(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Carts0
        //public async Task<IActionResult> Index()
        //{
        //      return _context.Cart != null ? 
        //                  View(await _context.Cart.ToListAsync()) :
        //                  Problem("Entity set 'MvcMovieContext.Cart'  is null.");
        //}

        //public async Task<IActionResult> Index(string searchstring)
        //{
        //    if (String.IsNullOrEmpty(searchstring))
        //    {
        //        searchstring = "";
        //    }
        //    var carts = from m in _context.Cart
        //                 where m.product_id.Contains(searchstring)
        //                 select m;
        //    return View(await carts.ToListAsync());
        //}

        public async Task<IActionResult> Index(string searchstring, double? low, double? high)
        {
            if (String.IsNullOrEmpty(searchstring)) { searchstring = ""; }
            if (low == null) { low = 0.0; }
            if (high == null) { high = 999.0; }
            var carts = from m in _context.Cart
                         where m.product_id.Contains(searchstring) && Convert.ToDecimal(low) <= m.product_price
                         && m.product_price <= Convert.ToDecimal(high)
                         select m;
            return View(await carts.ToListAsync());
        }

        // GET: Carts0/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cart == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Carts0/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Carts0/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,cart_id,product_id,product_num,product_price,user_id,createtime")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cart);
        }

        // GET: Carts0/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cart == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            return View(cart);
        }

        // POST: Carts0/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,cart_id,product_id,product_num,product_price,user_id,createtime")] Cart cart)
        {
            if (id != cart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cart);
        }

        // GET: Carts0/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cart == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts0/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cart == null)
            {
                return Problem("Entity set 'MvcMovieContext.Cart'  is null.");
            }
            var cart = await _context.Cart.FindAsync(id);
            if (cart != null)
            {
                _context.Cart.Remove(cart);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
          return (_context.Cart?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
