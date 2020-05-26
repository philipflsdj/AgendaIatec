using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Agenda.App.Data;
using Agenda.App.Models;
using Microsoft.AspNetCore.Authorization;

namespace Agenda.App.Controllers
{
    [Authorize]
    public class TipoEventosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TipoEventosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TipoEventos
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoEventos.ToListAsync());
        }

        // GET: TipoEventos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoEvento = await _context.TipoEventos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoEvento == null)
            {
                return NotFound();
            }

            return View(tipoEvento);
        }

        // GET: TipoEventos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TipoEventos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Id")] TipoEvento tipoEvento)
        {
            if (ModelState.IsValid)
            {
                tipoEvento.Id = Guid.NewGuid();
                _context.Add(tipoEvento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoEvento);
        }

        // GET: TipoEventos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoEvento = await _context.TipoEventos.FindAsync(id);
            if (tipoEvento == null)
            {
                return NotFound();
            }
            return View(tipoEvento);
        }

        // POST: TipoEventos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Nome,Id")] TipoEvento tipoEvento)
        {
            if (id != tipoEvento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoEvento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoEventoExists(tipoEvento.Id))
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
            return View(tipoEvento);
        }

        // GET: TipoEventos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoEvento = await _context.TipoEventos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoEvento == null)
            {
                return NotFound();
            }

            return View(tipoEvento);
        }

        // POST: TipoEventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var tipoEvento = await _context.TipoEventos.FindAsync(id);
            _context.TipoEventos.Remove(tipoEvento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoEventoExists(Guid id)
        {
            return _context.TipoEventos.Any(e => e.Id == id);
        }
    }
}
