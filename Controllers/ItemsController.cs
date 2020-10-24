using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InventoryManagementSystemJQuery.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystemJQuery.Controllers
{
    public class ItemsController : Controller
    {
        private InventoryDbContext _context;

        public ItemsController(InventoryDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Item> Items = _context.Items.ToList();
            return View(Items);
        }

        public async Task<IActionResult> AddOrEdit(int id = 0)
        {
            if(id == 0) // add new item
            {
                return View(new Item());
            }
            else // edit existing item
            {
                var item = await _context.Items.FindAsync(id);
                if(item == null)
                {
                    return NotFound();
                }
                return View(item);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEdit(int id, Item item)
        {
            if(ModelState.IsValid)
            {
                if(id == 0)
                {
                    item.DateTimeAdded = DateTime.Now;
                    _context.Items.Add(item);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    try
                    {
                        _context.Items.Update(item);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ItemExists(item.ItemId))
                        {
                            return NotFound();
                        }
                        else throw;
                    }
                    
                }

                return Json(new
                {
                    isValid = true,
                    html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Items.ToList())
                });
            }

            return Json(new
            {
                isValid = false,
                html = Helper.RenderRazorViewToString(this, "AddOrEdit", item)
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Items.FindAsync(id);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", _context.Items.ToList()) });
        }

        public IActionResult ViewAllApiTest()
        {
            return View();
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(item => item.ItemId == id);
        }
    }
}
