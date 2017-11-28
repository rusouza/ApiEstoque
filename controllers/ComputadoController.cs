using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using restApi.Model;
using System.Linq;

namespace restApi.Controllers {
    [Route("api/[controller]")]
    //[EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class ComputadorController : Controller {
        private readonly ComputadorContext _context;

        public ComputadorController(ComputadorContext context) {
            _context = context;

            if (_context.Computadores.Count() == 0)
            {
                _context.Computadores.Add(new Computador { marca = "Intel" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<Computador> GetAll() {
            return _context.Computadores.ToList();
        }

        [HttpGet("{id}", Name = "GetComputador")]
        public IActionResult GetById(long id) {
            var item = _context.Computadores.FirstOrDefault(t => t.id == id);
            if (item == null) {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Computador item) {
            if (item == null) {
                return BadRequest();
            }

            _context.Computadores.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetComputador", new { id = item.id }, item);
        }    

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Computador item) {
            if (item == null || item.Id != id) {
                return BadRequest();
            }

            var computador = _context.Computadores.FirstOrDefault(t => t.id == id);
            if (computador == null) {
                return NotFound();
            }

            computador.marca = item.marca;
            computador.modelo = item.modelo;
            computador.placaMae = item.placaMae;
            computador.placaMae = item.placaMae;
            computador.memoriaRam = item.memoriaRam;
            computador.hd = item.hd;
            computador.processador = item.processador;

            _context.Computador.Update(computador);
            _context.SaveChanges();
            return new NoContentResult();
        }  

        [HttpDelete("{id}")]
        public IActionResult Delete(long id) {
            var computador = _context.Computadores.FirstOrDefault(t => t.id == id);
            if (computador == null) {
                return NotFound();
            }

            _context.Computadores.Remove(computador);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}