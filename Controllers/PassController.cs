using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PassManager.Data;
using PassManager.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace PassManager.Controllers
{
    [Authorize]
    public class PassController : Controller
    {
        private readonly MyContext _context;
        private readonly UserManager<AppUser> _userManager;

        public PassController(MyContext context, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Pass/Create
        public IActionResult Create(int? id)
        {
            TempData["ID"] = id;
            return View();
        }

        // POST: Pass/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, [Bind("passName,Pass,URL,UserID,GroupID")] PassModel passModel)
        {
            // var group = _context.Groups.Where(x=>x.ID==id);
            // passModel.Group=(GroupModel)group;
            if (ModelState.IsValid)
            {
                passModel.GroupID = (int)id;
                passModel.ModifiedDate = DateTime.Now;
                passModel.UserID = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                _context.Add(passModel);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Group");
            }
            return View(passModel);
        }

        // GET: Pass/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passModel = await _context.Passwords.FindAsync(id);
            if (passModel.isPassive==true)
            {
                return RedirectToAction("Index", "Group");
            }
            if (passModel.UserID!=this.User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return RedirectToAction("Index", "Group");
            }
            if (passModel == null)
            {
                return NotFound();
            }
            ViewData["GroupID"] = new SelectList(_context.Groups, "ID", "ID", passModel.GroupID);
            return View(passModel);
        }

        // POST: Pass/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,passName,UserID,URL,Pass,GroupID")] PassModel passModel)
        {
            if (id != passModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    passModel.ModifiedDate = DateTime.Now;
                    passModel.UserID=this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    _context.Update(passModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PassModelExists(passModel.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index","Group");
            }
            ViewData["GroupID"] = new SelectList(_context.Groups, "ID", "ID", passModel.GroupID);
            return View(passModel);
        }

        // GET: Pass/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passModel = await _context.Passwords
                .Include(p => p.Group)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (passModel.UserID!=this.User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return RedirectToAction("Index", "Group");
            }
            if (passModel == null)
            {
                return NotFound();
            }

            return View(passModel);
        }

        // POST: Pass/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var passModel = await _context.Passwords.FindAsync(id);
            // _context.Passwords.Remove(passModel);
            passModel.isPassive = true;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Group");
        }

        private bool PassModelExists(int id)
        {
            return _context.Passwords.Any(e => e.ID == id);
        }
    }
}
