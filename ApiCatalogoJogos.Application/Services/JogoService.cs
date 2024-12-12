using ApiCatalogoJogos.Application.Exceptions;
using ApiCatalogoJogos.Application.InputModel;
using ApiCatalogoJogos.Application.ViewModel;
using ApiCatalogoJogos.Domain.Entities;
using ApiCatalogoJogos.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogoJogos.Application.Services
{
    public class JogoService : IJogoService
    {
        private readonly JogosContext _context;

        public JogoService(JogosContext context)
        {
            _context = context;
        }

        public async Task<List<JogoViewModel>> Obter(int pagina, int quantidade)
        {
            var listaJogos = await _context.Jogos.Skip((pagina - 1) * quantidade).Take(quantidade).ToListAsync();

            return listaJogos.Select(jogo => new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            }).ToList();
        }

        public async Task<JogoViewModel> Obter(Guid id)
        {
            var jogo = await _context.Jogos.FindAsync(id);

            if (jogo == null)
                throw new JogoNaoCadastradoException();

            return new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };
        }

        public async Task<JogoViewModel> Inserir(JogoInputModel jogo)
        {
            var entidadeJogo = await _context.Jogos.SingleOrDefaultAsync(j => j.Nome == jogo.Nome && j.Produtora == jogo.Produtora);

            if (entidadeJogo != null)
                throw new JogoJaCadastradoException();

            var jogoInsert = new Jogo
            {
                Id = Guid.NewGuid(),
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };

            _context.Jogos.Add(jogoInsert);
            await _context.SaveChangesAsync();

            return new JogoViewModel
            {
                Id = jogoInsert.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };
        }

        public async Task Atualizar(Guid id, JogoInputModel jogo)
        {            
            var entidadeJogo = await _context.Jogos.FindAsync(id);

            if (entidadeJogo == null)
                throw new JogoNaoCadastradoException();

            entidadeJogo.Nome = jogo.Nome;
            entidadeJogo.Produtora = jogo.Produtora;
            entidadeJogo.Preco = jogo.Preco;

            _context.Jogos.Update(entidadeJogo);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(Guid id, double preco)
        {
            var entidadeJogo = await _context.Jogos.FindAsync(id);

            if (entidadeJogo == null)
                throw new JogoNaoCadastradoException();

            entidadeJogo.Preco = preco;

            _context.Jogos.Update(entidadeJogo);
            await _context.SaveChangesAsync();
        }

        public async Task Remover(Guid id)
        {
            var jogo = await _context.Jogos.FindAsync(id);

            if (jogo == null)
                throw new JogoNaoCadastradoException();

            _context.Jogos.Remove(jogo);
            await _context.SaveChangesAsync();
        }
    }
}
