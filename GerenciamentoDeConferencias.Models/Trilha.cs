namespace GerenciamentoDeConferencias.Models
{
    /// <summary>
    ///     Trilha
    /// </summary>
    public class Trilha
    {
        /// <summary>
        ///     Construtor da trilha(ao construir a trilha já gera as sessões de manhã e tarde)
        /// </summary>
        /// <param name="numeroTrilha">Número da trilha</param>
        public Trilha(int numeroTrilha)
        {
            this.NumeroTrilha = numeroTrilha;
            this.Manha = new Sessao("Manhã", 180);
            this.Tarde = new Sessao("Tarde", 240);
        }

        /// <summary>
        ///     Número da trilha
        /// </summary>
        public int NumeroTrilha
        {
            get;
            set;
        }

        /// <summary>
        ///     Sessão da Manhã
        /// </summary>
        public Sessao Manha
        {
            get;
            set;
        }

        /// <summary>
        ///     Sessão da Tarde
        /// </summary>
        public Sessao Tarde
        {
            get;
            set;
        }
    }
}