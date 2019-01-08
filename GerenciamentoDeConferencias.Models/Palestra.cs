using System;

namespace GerenciamentoDeConferencias.Models
{
    /// <summary>
    ///     Modelo de trilha
    /// </summary>
    public class Palestra
    {
        /// <summary>
        ///     Construtor
        /// </summary>
        /// <param name="entrada">Texto de entrada</param>
        public Palestra(string entrada)
        {
            this.PreecherTrilha(entrada);
            this.ValidarTrilha();
        }

        /// <summary>
        ///     Duração da trilha (em minutos)
        /// </summary>
        public int Duracao
        {
            get;
            set;
        }

        /// <summary>
        ///     Nome da trilha
        /// </summary>
        public string Titulo
        {
            get;
            set;
        }

        public TimeSpan Horario
        {
            get;
            set;
        }

        /// <summary>
        ///     Validação da Trilha
        /// </summary>
        private void ValidarTrilha()
        {
            if (this.Duracao == 0)
            {
                throw new Exception("Não foi possível encontrar a duração da trilha no texto digitado.");
            }

            if (string.IsNullOrEmpty(this.Titulo))
            {
                throw new Exception("Não foi possível encontrar o nome da trilha no texto digitado.");
            }

            //Uma palestra não deve ter mais de 240 minutos que é o
            //período disponível na sessão da tarde
            if (this.Duracao > 240)
            {
                throw new Exception("As palestras devem ter duração de no máximo 240 minutos");
            }
        }

        /// <summary>
        ///     Preenche a trilha com as informações enviadas
        /// </summary>
        /// <param name="entrada"></param>
        private void PreecherTrilha(string entrada)
        {
            //Busca o tempo de duração no valor de entrada
            string tempoDuracao = Helper.BuscarNumeros(entrada);

            //Caso não encontre nenhum número no valor digitado busca o lightning
            if (tempoDuracao != "")
            {
                this.Titulo = entrada.Substring(0, entrada.IndexOfAny("0123456789".ToCharArray()));
                this.Duracao = int.Parse(tempoDuracao);
            }
            else
            {
                string lightning = "lightning";
                if (entrada.Contains(lightning, StringComparison.CurrentCultureIgnoreCase))
                {
                    this.Duracao = 5;
                    this.Titulo = entrada.Replace(lightning, string.Empty);
                }
            }
        }
    }
}