using ApiCatalagoJogos.Entities;
using ApiCatalagoJogos.Exceptions;
using ApiCatalagoJogos.InputModel;
using ApiCatalagoJogos.Repositories;
using ApiCatalagoJogos.ViewModel;

namespace ApiCatalagoJogos.Services
{
    public class JogoService : IJogoService
    {
        private readonly IJogoRepository _jogoResitory;

        public JogoService(IJogoRepository jogoResitory)
        {
            _jogoResitory = jogoResitory;
        }

        public async Task<List<JogoViewModel>> Obter(int pagina, int quantidade)
        {
            var jogos = await _jogoResitory.Obter(pagina, quantidade);
            
            return jogos.Select(jogo => new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            }).ToList();
        }

        public async Task<JogoViewModel> Obter(Guid id)
        {
            var jogo = await _jogoResitory.Obter(id);

            if (jogo == null)
                return null;

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
            var entidadeJogo = await _jogoResitory.Obter(jogo.Nome, jogo.Produtora);

            if (entidadeJogo.Count > 0)
                throw new JogoJaCadastradoException();

            var jogoInsert = new Jogo
            {
                Id = Guid.NewGuid(),
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };

            await _jogoResitory.Inserir(jogoInsert);

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
            var entidadeJogo = await _jogoResitory.Obter(id);

            if (entidadeJogo == null)
                throw new JogoNaoCadastradoException();

            entidadeJogo.Nome = jogo.Nome;
            entidadeJogo.Produtora = jogo.Produtora;
            entidadeJogo.Preco = jogo.Preco;

            await _jogoResitory.Atualizar(entidadeJogo);
        }

        public async Task Atualizar(Guid id, double preco)
        {
            var entidadeJogo = await _jogoResitory.Obter(id);

            if (entidadeJogo == null)
                throw new JogoNaoCadastradoException();

            entidadeJogo.Preco = preco;

            await _jogoResitory.Atualizar(entidadeJogo);
        }

        public async Task Remover(Guid id)
        {
            var jogo = _jogoResitory.Obter(id);

            if (jogo == null)
                throw new JogoNaoCadastradoException();

            await _jogoResitory.Remover(id);
        }

        public void Dispose()
        {
            _jogoResitory.Dispose();
        }
    }
}
