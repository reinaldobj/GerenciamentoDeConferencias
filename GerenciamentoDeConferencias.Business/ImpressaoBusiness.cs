using System;
using System.Collections.Generic;
using System.Linq;
using GerenciamentoDeConferencias.Models;

namespace GerenciamentoDeConferencias.Business
{
    /// <summary>
    ///     Classe utilizada para geração da impressão da conferencia
    /// </summary>
    public class ImpressaoBusiness
    {
        /// <summary>
        ///     Gera a impressão do calendário da conferencia
        /// </summary>
        /// <param name="trilhas">Trilhas da conferencia</param>
        /// <returns></returns>
        public List<string> GerarImpressaoTrilhas(List<Trilha> trilhas)
        {
            List<string> trilhasImpressao = new List<string>();

            foreach (Trilha trilha in trilhas)
            {
                trilhasImpressao.Add($"Track {trilha.NumeroTrilha}:");

                //Sessão da manhã inícia as 9
                trilhasImpressao.AddRange(this.GerarImpressaoSessao(trilha.Manha));

                //Almoço deve ser ao meio dia
                string horarioAlmoco = Helper.FormatarHorario(TimeSpan.FromHours(12));
                trilhasImpressao.Add($"{horarioAlmoco} Almoço");

                //Sessão da manhã inícia as 13
                trilhasImpressao.AddRange(this.GerarImpressaoSessao(trilha.Tarde));

                //Busca a última palestra do dia para agendar o evento de networking
                Palestra ultimaPalestraDaSessaoDaTarde =
                    trilha.Tarde.Palestras.OrderByDescending(p => p.Horario).First();
                TimeSpan finalDaUltimaPalestraDaTarde =
                    ultimaPalestraDaSessaoDaTarde.Horario.Add(
                        TimeSpan.FromMinutes(ultimaPalestraDaSessaoDaTarde.Duracao));

                //Networking deve iniciar depois das 16h e antes das 17h
                string horarioNetworking;
                if (finalDaUltimaPalestraDaTarde < TimeSpan.FromHours(16))
                {
                    horarioNetworking = Helper.FormatarHorario(TimeSpan.FromHours(16));
                }
                else if (finalDaUltimaPalestraDaTarde > TimeSpan.FromHours(17))
                {
                    horarioNetworking = Helper.FormatarHorario(TimeSpan.FromHours(17));
                }
                else
                {
                    horarioNetworking = Helper.FormatarHorario(finalDaUltimaPalestraDaTarde);
                }

                trilhasImpressao.Add($"{horarioNetworking} Evento de Networking");
            }

            return trilhasImpressao;
        }

        /// <summary>
        ///     Gera a impressão da sessão (Manhã ou Tarde)
        /// </summary>
        /// <param name="sessao">Sessão para impressão</param>
        /// <returns></returns>
        private IEnumerable<string> GerarImpressaoSessao(Sessao sessao)
        {
            List<string> trilhasImpressao = new List<string>();

            foreach (Palestra palestra in sessao.Palestras)
            {
                trilhasImpressao.Add(
                    $"{Helper.FormatarHorario(palestra.Horario)} {palestra.Titulo} {palestra.Duracao}min");
            }

            return trilhasImpressao;
        }
    }
}