using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Agenda.App.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Agenda.App.Models
{
    [Authorize]
    public class AgendasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Usuario> _userManager;

        public AgendasController(ApplicationDbContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Agendas
        public async Task<IActionResult> Index()
        {
            var IdUsu = _userManager.GetUserId(HttpContext.User);
            Usuario usuario = _userManager.FindByIdAsync(IdUsu).Result;
            var applicationDbContext = _context.Agendas.Include(a => a.Evento).Include(a => a.Usuario).Where(u => u.UsuarioId == usuario.Id && u.Evento.StatusEvento == "Próximos Eventos" || u.Evento.StatusEvento == "Evento em Andamento");
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Agendas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendar = await _context.Agendas
                .Include(a => a.Evento)
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agendar == null)
            {
                return NotFound();
            }

            return View(agendar);
        }

        // GET: Agendas/Create
        public IActionResult Create()
        {
            ViewData["EventoId"] = new SelectList(_context.Eventos, "Id", "Descricao");
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Agendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid EventoId)
        {
            Agendar agendar = new Agendar();
            if (ModelState.IsValid)
            {
                
                var IdUsu = _userManager.GetUserId(HttpContext.User);
                Usuario usuario = _userManager.FindByIdAsync(IdUsu).Result;
                agendar.Id = Guid.NewGuid();
                agendar.DtEntrada = DateTime.Now;
                agendar.EventoId = EventoId;
                agendar.UsuarioId = usuario.Id;
                _context.Add(agendar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventoId"] = new SelectList(_context.Eventos, "Id", "Descricao", agendar.EventoId);
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id", agendar.UsuarioId);
            return View(agendar);
        }

        // GET: Agendas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendar = await _context.Agendas.FindAsync(id);
            if (agendar == null)
            {
                return NotFound();
            }
            ViewData["EventoId"] = new SelectList(_context.Eventos, "Id", "Descricao", agendar.EventoId);
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id", agendar.UsuarioId);
            return View(agendar);
        }

        // POST: Agendas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("DtEntrada,EventoId,UsuarioId,Id")] Agendar agendar)
        {
            if (id != agendar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agendar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgendarExists(agendar.Id))
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
            ViewData["EventoId"] = new SelectList(_context.Eventos, "Id", "Descricao", agendar.EventoId);
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id", agendar.UsuarioId);
            return View(agendar);
        }

        // GET: Agendas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendar = await _context.Agendas
                .Include(a => a.Evento)
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agendar == null)
            {
                return NotFound();
            }

            return View(agendar);
        }

        // POST: Agendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var agendar = await _context.Agendas.FindAsync(id);
            _context.Agendas.Remove(agendar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgendarExists(Guid id)
        {
            return _context.Agendas.Any(e => e.Id == id);
        }
    }
}
