using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using restApi.Model;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Web.Http.Cors;

namespace restApi.Controllers {
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class ComputadorController : Controller {
        private readonly ComputadorContext _context;

        public IQueryable<Computador> GetData() {
            return _context.Computadores;
        }

        public ComputadorController(ComputadorContext context) {
            _context = context;

            if (_context.Computadores.Count() == 0)
            {
                _context.Computadores.Add(new Computador { marca = "Intel" });
                _context.SaveChanges();
            }
        }

        public IEnumerable<Computador> GetAll() {
            return _context.Computadores.ToList();
        }

        [ResponseType(typeof(Computador))]
        public IActionResult Create(Computador item) {
            if (item == null) {
                return BadRequest();
            }

            _context.Computadores.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetComputador", new { id = item.id }, item);
        }    

        public IHttpActionResult Update(int id, Computador computador) {
            if (id != computador.id) {
                return null;
            }

            _context.Entry(computador).State = (Microsoft.EntityFrameworkCore.EntityState) EntityState.Modified;

            try {
                _context.SaveChanges();
            } catch (DbUpdateConcurrencyException) {
                if (!ComputadorExists(id)) {
                    return null;
                } else {
                    throw;
                }
            }
            return (IHttpActionResult) NoContent();
        }  

        [ResponseType(typeof(Computador))]
        public IHttpActionResult Delete(long id) {
            Computador computador = _context.Computadores.Find(id);
            if (computador == null) {
                return null;
            }

            _context.Computadores.Remove(computador);
            _context.SaveChanges();

            return (IHttpActionResult) Ok(computador);
        }

        private bool ComputadorExists(int id) {
            return _context.Computadores.Count(e => e.id == id) > 0;
        }
    }
}