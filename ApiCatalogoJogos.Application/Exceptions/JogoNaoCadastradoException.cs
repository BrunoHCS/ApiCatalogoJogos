namespace ApiCatalogoJogos.Application.Exceptions
{
    public class JogoNaoCadastradoException : Exception
    {
        public JogoNaoCadastradoException() : base("Esta jogo não está cadastrado") { }
    }
}
