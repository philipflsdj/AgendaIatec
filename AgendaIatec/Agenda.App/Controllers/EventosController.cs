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
    public class EventosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Eventos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Eventos.Include(e => e.Endereco).Include(e => e.TipoEvento);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Eventos/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .Include(e => e.Endereco)
                .Include(e => e.TipoEvento)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // GET: Eventos/Create
        public IActionResult Create()
        {
            ViewData["EnderecoId"] = new SelectList(_context.Enderecos, "Id", "Logradouro");
            ViewData["TipoEventoId"] = new SelectList(_context.TipoEventos, "Id", "Nome");
            return View();
        }

        // POST: Eventos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Evento evento)
        {

            var dtvalid = new SelectList( _context.Eventos);

            //for (int i = 0; i <  length; i++)
            //{
            //    if(evento.TipoEvento.Nome == "Exclusivo" && dtvalid.SelectedValue.)
            //}

            //if (evento.TipoEvento.Nome == "Exclusivo" && datavalide != dataagora)
            //{
            //    return RedirectToAction("EventoJaExiste");
            //}
            if (ModelState.IsValid)
            {

                evento.Id = Guid.NewGuid();
                _context.Add(evento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnderecoId"] = new SelectList(_context.Enderecos, "Id", "Bairro", evento.EnderecoId);
            ViewData["TipoEventoId"] = new SelectList(_context.TipoEventos, "Id", "Nome", evento.TipoEventoId);
            return View(evento);
        }

        // GET: Eventos/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }
            ViewData["EnderecoId"] = new SelectList(_context.Enderecos, "Id", "Bairro", evento.EnderecoId);
            ViewData["TipoEventoId"] = new SelectList(_context.TipoEventos, "Id", "Nome", evento.TipoEventoId);
            return View(evento);
        }

        // POST: Eventos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Nome,Descricao,DtInicial,DtFinal,StatusEvento,EnderecoId,TipoEventoId,Id")] Evento evento)
        {
            if (id != evento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventoExists(evento.Id))
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
            ViewData["EnderecoId"] = new SelectList(_context.Enderecos, "Id", "Bairro", evento.EnderecoId);
            ViewData["TipoEventoId"] = new SelectList(_context.TipoEventos, "Id", "Nome", evento.TipoEventoId);
            return View(evento);
        }

        // GET: Eventos/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evento = await _context.Eventos
                .Include(e => e.Endereco)
                .Include(e => e.TipoEvento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // POST: Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var evento = await _context.Eventos.FindAsync(id);
            _context.Eventos.Remove(evento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventoExists(Guid id)
        {
            return _context.Eventos.Any(e => e.Id == id);
        }
    }
}
