using System.Collections.Generic;
using System.Linq;

namespace GerenciamentoDeConferencias.Models
{
    /// <summary>
    ///     Sessão de Palestras
    /// </summary>
    public class Sessao
    {
        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="duracao"></param>
        public Sessao(string nome, int duracao)
        {
            this.Nome = nome;
            this.Duracao = duracao;
            this.Palestras = new List<Palestra>();
        }

        /// <summary>
        ///     Tempo disponível para palestras na sessão
        /// </summary>
        public int TempoDisponivel => this.Duracao - this.Palestras.Sum(p => p.Duracao);

        /// <summary>
        ///     Indica se a sessão está cheia
        /// </summary>
        public bool Cheia => this.TempoDisponivel == 0;

        /// <summary>
        ///     Nome da sessão (Manhã/Tarde)
        /// </summary>
        public string Nome
        {
            get;
            set;
        }

        /// <summary>
        ///     Duração da Sessão
        /// </summary>
        public int Duracao
        {
            get;
            set;
        }

        /// <summary>
        ///     Palestras da sessão
        /// </summary>
        public List<Palestra> Palestras
        {
            get;
            set;
        }
    }
}