using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeaveManagementSystemPractice.web.Data;
using LeaveManagementSystemPractice.web.Data.Entities;
using LeaveManagementSystemPractice.web.Models.LeaveTypes;

namespace LeaveManagementSystemPractice.web.Controllers
{
    public class LeaveTypesController(ApplicationDbContext context) : Controller
    {
        private const string NameExistsValidationMessage = "Name already exists in the database";
        
        // GET: LeaveTypes
        public async Task<IActionResult> Index()
        {
            var leaveTypes = await context.LeaveType.ToListAsync();
            IEnumerable<LeaveTypeReadOnlyVM> leaveTypeReadOnlyVMs = leaveTypes.Select(leaveType => new LeaveTypeReadOnlyVM
            {
                Id = leaveType.Id,
                Name = leaveType.Name,
                NumberOfDays = leaveType.NumberOfDays
            });
            return View(leaveTypeReadOnlyVMs);
        }

        // GET: LeaveTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType = await context.LeaveType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveType == null)
            {
                return NotFound();
            }
            
            LeaveTypeReadOnlyVM leaveTypeReadOnlyVM = new LeaveTypeReadOnlyVM
            {
                Id = leaveType.Id,
                Name = leaveType.Name,
                NumberOfDays = leaveType.NumberOfDays
            };
            return View(leaveTypeReadOnlyVM);
        }

        // GET: LeaveTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveTypeCreateVM leaveTypeCreateVM)
        {
            if (await context.LeaveType.AnyAsync(e => e.Name.ToLower().Equals(leaveTypeCreateVM.Name.ToLower())))
            {
                ModelState.AddModelError(nameof(leaveTypeCreateVM.Name), NameExistsValidationMessage);
            }
            if (ModelState.IsValid)
            {
                LeaveType leaveType = new LeaveType
                {
                    Name = leaveTypeCreateVM.Name,
                    NumberOfDays = leaveTypeCreateVM.NumberOfDays
                };
                context.Add(leaveType);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(leaveTypeCreateVM);
        }

        // GET: LeaveTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType = await context.LeaveType.FindAsync(id);
            if (leaveType == null)
            {
                return NotFound();
            }
            LeaveTypeEditVM leaveTypeEditVM = new LeaveTypeEditVM
            {
                Id = leaveType.Id,
                Name = leaveType.Name,
                NumberOfDays = leaveType.NumberOfDays
            };
            return View(leaveTypeEditVM);
        }

        // POST: LeaveTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LeaveTypeEditVM leaveTypeEditVM)
        {
            if (id != leaveTypeEditVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    LeaveType leaveType = new LeaveType
                    {
                        Id = leaveTypeEditVM.Id,
                        Name = leaveTypeEditVM.Name,
                        NumberOfDays = leaveTypeEditVM.NumberOfDays
                    };
                    context.Update(leaveType);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveTypeExists(leaveTypeEditVM.Id))
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
            return View(leaveTypeEditVM);
        }

        // GET: LeaveTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveType = await context.LeaveType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveType == null)
            {
                return NotFound();
            }

            LeaveTypeReadOnlyVM leaveTypeReadOnlyVM = new LeaveTypeReadOnlyVM
            {
                Id = leaveType.Id,
                Name = leaveType.Name,
                NumberOfDays = leaveType.NumberOfDays
            };
            return View(leaveTypeReadOnlyVM);
        }

        // POST: LeaveTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaveType = await context.LeaveType.FindAsync(id);
            if (leaveType != null)
            {
                context.LeaveType.Remove(leaveType);
            }

            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveTypeExists(int id)
        {
            return context.LeaveType.Any(e => e.Id == id);
        }
    }
}
