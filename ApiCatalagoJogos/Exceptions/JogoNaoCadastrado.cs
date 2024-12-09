namespace ApiCatalagoJogos.Exceptions
{
    public class JogoNaoCadastrado : Exception
    {
        public JogoNaoCadastrado() : base("Esta jogo não está cadastrado") { }
    }
}
