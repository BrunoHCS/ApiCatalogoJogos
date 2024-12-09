
namespace ApiCatalagoJogos.Services
{
    [Serializable]
    internal class JogoNaoCadastradoException : Exception
    {
        public JogoNaoCadastradoException()
        {
        }

        public JogoNaoCadastradoException(string? message) : base(message)
        {
        }

        public JogoNaoCadastradoException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}