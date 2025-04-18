﻿using api_adota_pet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace api_adota_pet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnunciosController : ControllerBase
    {
        private readonly AppDbContext _context;
        public AnunciosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]

        public async Task<ActionResult> Create(AnuncioDto model)
        {
            Anuncio anuncio = new Anuncio()
            {
                ExternalId = "123",
                IdadeAnimal = model.IdadeAnimal,
                UsuarioId = model.UsuarioId,
                CategoriaAnimal = model.CategoriaAnimal,
                DataPostagem = DateTime.Now,
                Descricao = model.Descricao,
                ImagemCapa = model.ImagemCapa,
                RacaAnimal = model.RacaAnimal,
                Status = Status.Publicado,
                Titulo = model.Titulo,
                };

            _context.Anuncios.Add(anuncio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = anuncio.Id }, anuncio);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var model = await _context.Anuncios
               .ToListAsync();

            if (model.IsNullOrEmpty()) return NoContent();

            return Ok(model);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var model = await _context.Anuncios
                .FirstOrDefaultAsync(anuncio => anuncio.Id == id);

            if (model == null) return NotFound();

            //GerarLinks(model);

            return Ok(model);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, AnuncioDto model)
        {

            if (id != model.Id) return BadRequest();

            var modelDb = await _context.Anuncios.AsNoTracking().FirstOrDefaultAsync(anuncio => anuncio.Id == id);


            if (modelDb == null) return NotFound();

            Anuncio anuncio = new Anuncio()
            {
                Id = modelDb.Id,
                ExternalId = modelDb.ExternalId,
                IdadeAnimal = model.IdadeAnimal,
                UsuarioId = modelDb.UsuarioId,
                CategoriaAnimal = model.CategoriaAnimal,
                DataPostagem = modelDb.DataPostagem,
                Descricao = model.Descricao,
                ImagemCapa = model.ImagemCapa,
                RacaAnimal = model.RacaAnimal,
                Status = Status.Publicado,
                Titulo = model.Titulo,
            };

            _context.Anuncios.Update(anuncio);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpPost("{id}/{status}")]
        public async Task<ActionResult> UpdateStatus(int id, string status)
        {
            var model = await _context.Anuncios
                .AsNoTracking()
                .FirstOrDefaultAsync(anuncio => anuncio.Id == id);

            if (model == null) return NotFound();

            if (status == null) return BadRequest(new { message = "Status não encontrado." });

            Status? statusVerificado = null;


            if (status.ToUpper() == Status.Publicado.ToString().ToUpper())
            {
                statusVerificado = Status.Publicado;
            }
            else if (status.ToUpper() == Status.Deletado.ToString().ToUpper())
            {
                statusVerificado = Status.Deletado;
            }
            else
            {
                return BadRequest(new { message = "Status não encontrado." });
            }

            Anuncio anuncio = new Anuncio()
            {
                Id = model.Id,
                ExternalId = model.ExternalId,
                IdadeAnimal = model.IdadeAnimal,
                UsuarioId = model.UsuarioId,
                CategoriaAnimal = model.CategoriaAnimal,
                DataPostagem = model.DataPostagem,
                Descricao = model.Descricao,
                ImagemCapa = model.ImagemCapa,
                RacaAnimal = model.RacaAnimal,
                Status = statusVerificado ?? Status.Publicado,
                Titulo = model.Titulo,
            };

            _context.Anuncios.Update(anuncio);
            await _context.SaveChangesAsync();

            return Ok(model);
        }

    }
}
