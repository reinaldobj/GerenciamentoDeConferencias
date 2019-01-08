using System;
using System.Collections.Generic;
using System.Linq;
using GerenciamentoDeConferencias.Models;

namespace GerenciamentoDeConferencias.Business
{
    public class ConferenciaBusiness
    {
        /// <summary>
        ///     Gera as trilhas da conferencia com as palestras informadas
        /// </summary>
        /// <param name="titulosPalestras">Títulos das palestras e tempo</param>
        /// <returns></returns>
        public List<Trilha> GerarTrilhas(List<string> titulosPalestras)
        {
            List<Trilha> trilhas;

            //Baseado no texto digitado gera as palestras
            List<Palestra> palestras = new List<Palestra>();
            foreach (string tituloPalestra in titulosPalestras)
            {
                palestras.Add(new Palestra(tituloPalestra));
            }

            //Calcular quantidade de dias necessarios para a conferencia
            int totalTrilhas = this.CalcularTotalDeTrilhasNecessarias(palestras);

            //Gera as trilhas necessarias
            trilhas = this.GerarTrilhas(totalTrilhas);

            foreach (Trilha trilha in trilhas)
            {
                //Sessão da manhã inícia as 9
                TimeSpan horarioManha = TimeSpan.FromHours(9);

                //Gera a sessão do período da manhã
                this.PreencherSessao(palestras, trilha.Manha, horarioManha);

                //Sessão da manhã inícia as 13
                TimeSpan horarioTarde = TimeSpan.FromHours(13);

                //Gera a sesão do período da tarde
                this.PreencherSessao(palestras, trilha.Tarde, horarioTarde);
            }

            return trilhas;
        }

        /// <summary>
        ///     Calcula o total de trilhas necessarias para atender as palestras
        /// </summary>
        /// <param name="palestras">Palestras</param>
        /// <returns></returns>
        private int CalcularTotalDeTrilhasNecessarias(List<Palestra> palestras)
        {
            //Cada dia tem 7 horas disponiveis, 3 da manhã e 4 da tarde
            //Calculo o total de minutos da conferencia
            int tempoDisponivelPorTrilha = 7 * 60;
            int tempoTotalDePalestras = palestras.Sum(t => t.Duracao);
            int totalTrilhas = tempoTotalDePalestras / tempoDisponivelPorTrilha;

            if (tempoTotalDePalestras % tempoDisponivelPorTrilha > 0)
            {
                totalTrilhas++;
            }

            return totalTrilhas;
        }

        /// <summary>
        ///     Gera as trilhas necessárias para a conferência
        /// </summary>
        /// <param name="totalTrilhas">Total de trilhas</param>
        /// <returns></returns>
        private List<Trilha> GerarTrilhas(int totalTrilhas)
        {
            List<Trilha> trilhas = new List<Trilha>();
            for (int i = 0; i < totalTrilhas; i++)
            {
                Trilha trilha = new Trilha(i + 1);
                trilhas.Add(trilha);
            }

            return trilhas;
        }

        /// <summary>
        ///     Preenche a sessão de palestras preenchendo os tempos disponíveis
        /// </summary>
        /// <param name="palestras">Palestras</param>
        /// <param name="sessao">Sessão</param>
        private void PreencherSessao(List<Palestra> palestras, Sessao sessao, TimeSpan horarioInicioPalestra)
        {
            foreach (Palestra palestra in palestras)
            {
                if (sessao.TempoDisponivel >= palestra.Duracao && !sessao.Cheia)
                {
                    palestra.Horario = horarioInicioPalestra;

                    //Horário da próxima palestra é calculada adicionando o tempo de duração
                    //ao horário de início da palestra atual
                    horarioInicioPalestra = horarioInicioPalestra.Add(TimeSpan.FromMinutes(palestra.Duracao));

                    //Adicionando a palestra a sessão
                    sessao.Palestras.Add(palestra);
                }
            }

            //Remove das palestras disponíveis as que já foram agendadas
            sessao.Palestras.ForEach(p => palestras.Remove(p));
        }
    }
}