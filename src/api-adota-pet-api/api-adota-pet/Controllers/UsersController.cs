using api_adota_pet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_adota_pet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]

        public async Task<ActionResult> Create(UsuarioDto model)
        {
            Usuario usuario = new Usuario()
            {
                ExternalId = "123",

                EAdmin = model.EAdmin,
                Email = model.Email,
                Senha = model.Senha,
                Status = StatusUsuario.Habilitado,
                //Name = model.Name,
                //Senha = BCrypt.Net.BCrypt.HashPassword(model.Senha),
                //Perfil = model.Perfil,
            };
            _context.Usuarios.Add(usuario);

            await _context.SaveChangesAsync();

            Pessoa pessoa = new Pessoa()
            {
                ExternalId = "123",
                Documento = model.Documento,
                Nome = model.Nome,
                Telefone = model.Telefone,
                UsuarioId = usuario.Id,
            };

            _context.Pessoas.Add(pessoa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = usuario.Id }, usuario);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var model = await _context.Pessoas
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(usuario => usuario.UsuarioId == id);


            if (model == null) return NotFound();

            //GerarLinks(model);

            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UsuarioDto model)
        {

            if (id != model.Id) return BadRequest();

            var modelDb = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(usuario => usuario.Id == id);

            var modelPessoaDb = await _context.Pessoas.AsNoTracking().FirstOrDefaultAsync(pessoa => pessoa.UsuarioId == id);

            if (modelDb == null || modelPessoaDb ==null) return NotFound();

            Usuario usuario = new Usuario()
            {
                
                Id = id,
                ExternalId = "123",

                EAdmin = model.EAdmin,
                Email = model.Email,
                Senha = model.Senha,
                Status = modelDb.Status
                //Senha = BCrypt.Net.BCrypt.HashPassword(model.Senha),
        
            };

            Pessoa pessoa = new Pessoa()
            {
                Id = modelPessoaDb.Id,
                ExternalId = "123",
                Documento = model.Documento,
                Nome = model.Nome,
                Telefone = model.Telefone,
                UsuarioId = id,
            };

            _context.Usuarios.Update(usuario);
            _context.Pessoas.Update(pessoa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{id}/{status}")]
        public async Task<ActionResult> UpdateStatus(int id, string status)
        {
            var model = await _context.Usuarios
                .AsNoTracking()
                .FirstOrDefaultAsync(usuario => usuario.Id == id);

            if (model == null) return NotFound();

            if (status == null) return BadRequest(new { message = "Status não encontrado." });

            StatusUsuario? statusVerificado = null;


            if (status.ToUpper() == StatusUsuario.Habilitado.ToString().ToUpper())
            {
                statusVerificado = StatusUsuario.Habilitado;
            } else if (status.ToUpper() == StatusUsuario.Desabilitado.ToString().ToUpper())
            {
                statusVerificado = StatusUsuario.Desabilitado;
            } else 
            {
                return BadRequest(new { message = "Status não encontrado." });
            }

                Usuario usuario = new Usuario()
                {

                    Id = model.Id,
                    ExternalId = model.ExternalId,

                    EAdmin = model.EAdmin,
                    Email = model.Email,
                    Senha = model.Senha,
                    Status= statusVerificado ?? StatusUsuario.Habilitado,
                    //Senha = BCrypt.Net.BCrypt.HashPassword(model.Senha),

                };

            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            return Ok(model);
        }


        //private void GerarLinks(Usuario model)
        //{
        //    model.Links.Add(new LinkDto(id: model.Id, href: Url.ActionLink(), rel: "self", metodo: "GET"));
        //    model.Links.Add(new LinkDto(id: model.Id, href: Url.ActionLink(), rel: "update", metodo: "PUT"));
        //    model.Links.Add(new LinkDto(id: model.Id, href: Url.ActionLink(), rel: "delete", metodo: "DELETE"));

        //}
    }
}
