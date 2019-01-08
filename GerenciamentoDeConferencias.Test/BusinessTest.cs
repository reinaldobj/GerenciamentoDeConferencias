using System;
using System.Collections.Generic;
using System.Linq;
using GerenciamentoDeConferencias.Business;
using GerenciamentoDeConferencias.Models;
using Xunit;

namespace GerenciamentoDeConferencias.Test
{
    public class BusinessTest
    {
        private readonly string[] _palestras =
        {
            "Writing Fast Tests Against Enterprise Rails 60min",
            "Overdoing it in Python 45min",
            "Lua for the Masses 30min",
            "Ruby Errors from Mismatched Gem Versions 45min",
            "Common Ruby Errors 45min",
            "Rails for Python Developers lightning",
            "Communicating Over Distance 60min",
            "Accounting-Driven Development 45min",
            "Woah 30min",
            "Sit Down and Write 30min",
            "Pair Programming vs Noise 45min",
            "Rails Magic 60min",
            "Ruby on Rails: Why We Should Move On 60min",
            "Clojure Ate Scala (on my project) 45min",
            "Programming in the Boondocks of Seattle 30min",
            "Ruby vs. Clojure for Back-End Development 30min",
            "Ruby on Rails Legacy App Maintenance 60min",
            "A World Without HackerNews 30min",
            "User Interface CSS in Rails Apps 30min"
        };

        /// <summary>
        ///     Ao tentar agendar uma palesta com mais que 240 minutos deve gerar exceção
        /// </summary>
        [Fact]
        public void ValidarAgendaPalestraComDuracaoMaiorQue240Minutos()
        {
            ConferenciaBusiness conferenciaBusiness = new ConferenciaBusiness();
            Exception exception =
                Assert.Throws<Exception>(() => conferenciaBusiness.GerarTrilhas(new[] { "5min" }.ToList()));
            Assert.Equal("Não foi possível encontrar o nome da trilha no texto digitado.", exception.Message);
        }

        /// <summary>
        ///     Ao cadastrar uma palestra com o tempo "lightning" a palestra deve ter duração de 5 minutos
        /// </summary>
        [Fact]
        public void ValidarAgendaPalestraLighting()
        {
            ConferenciaBusiness conferenciaBusiness = new ConferenciaBusiness();
            List<Trilha> trilhas =
                conferenciaBusiness.GerarTrilhas(new[] { "Rails for Python Developers lightning" }.ToList());

            Palestra palestra = trilhas.First().Manha.Palestras.FirstOrDefault();

            Assert.True(palestra.Duracao == 5);
        }

        /// <summary>
        ///     Ao tentar agendar uma palestra sem duração deve gerar exceção
        /// </summary>
        [Fact]
        public void ValidarAgendaPalestraSemDuracao()
        {
            ConferenciaBusiness conferenciaBusiness = new ConferenciaBusiness();
            Exception exception = Assert.Throws<Exception>(() =>
                conferenciaBusiness.GerarTrilhas(new[] { "User Interface CSS in Rails Apps" }.ToList()));
            Assert.Equal("Não foi possível encontrar a duração da trilha no texto digitado.", exception.Message);
        }

        /// <summary>
        ///     Ao tentar agendar uma palesta sem título deve gerar exceção
        /// </summary>
        [Fact]
        public void ValidarAgendaPalestraSemTitulo()
        {
            ConferenciaBusiness conferenciaBusiness = new ConferenciaBusiness();
            Exception exception =
                Assert.Throws<Exception>(() => conferenciaBusiness.GerarTrilhas(new[] { "5min" }.ToList()));
            Assert.Equal("Não foi possível encontrar o nome da trilha no texto digitado.", exception.Message);
        }

        /// <summary>
        ///     Valida se todas as palestras são agendadas, incluíndo almoço e networking
        /// </summary>
        [Fact]
        public void ValidarAgendaTrilhas()
        {
            ConferenciaBusiness conferenciaBusiness = new ConferenciaBusiness();
            List<Trilha> trilhas = conferenciaBusiness.GerarTrilhas(this._palestras.ToList());

            ImpressaoBusiness impressaoBusiness = new ImpressaoBusiness();
            List<string> impressaoTrilhas = impressaoBusiness.GerarImpressaoTrilhas(trilhas);

            Assert.True(impressaoTrilhas.Count == 25);
        }

        /// <summary>
        ///     Evento de networking deve iniciar entre as 04:00 e 05:00
        /// </summary>
        [Fact]
        public void ValidarHorarioNetworking()
        {
            ConferenciaBusiness conferenciaBusiness = new ConferenciaBusiness();

            List<Trilha> trilhas = conferenciaBusiness.GerarTrilhas(new[]
                {
                    "Writing Fast Tests Against Enterprise Rails  60min",
                    "Overdoing it in Python  45min",
                    "Lua for the Masses  30min",
                    "Ruby Errors from Mismatched Gem Versions  45min",
                    "Common Ruby Errors  45min",
                    "Rails for Python Developers lightning",
                    "Communicating Over Distance  60min",
                    "Accounting - Driven Development  45min",
                    "Woah  30min",
                    "Sit Down and Write  30min"
                }
                .ToList());

            ImpressaoBusiness impressaoBusiness = new ImpressaoBusiness();
            List<string> impressaoTrilhas = impressaoBusiness.GerarImpressaoTrilhas(trilhas);

            string networkingEvent = impressaoTrilhas.FirstOrDefault(i => i.Contains("Evento de Networking"));
            TimeSpan horarioNetworking = TimeSpan.Parse(networkingEvent.Substring(0, 5));

            Assert.True(horarioNetworking >= TimeSpan.FromHours(4) && horarioNetworking <= TimeSpan.FromHours(5));
        }

        /// <summary>
        ///     Trilha da manhã deve iniciar as 09:00
        /// </summary>
        [Fact]
        public void ValidarHorarioSessaoManha()
        {
            ConferenciaBusiness conferenciaBusiness = new ConferenciaBusiness();

            //É necessário duas trilhas
            List<Trilha> trilhas = conferenciaBusiness.GerarTrilhas(new[]
                {
                    "Rails for Python Developers 40min"
                }
                .ToList());

            Palestra palestra = trilhas.First().Manha.Palestras.First();

            Assert.True(palestra.Horario == TimeSpan.FromHours(9));
        }

        /// <summary>
        ///     Sessão da tarde deve iniciar as 13:00
        /// </summary>
        [Fact]
        public void ValidarHorarioSessaoTarde()
        {
            ConferenciaBusiness conferenciaBusiness = new ConferenciaBusiness();

            //É necessário duas trilhas
            List<Trilha> trilhas = conferenciaBusiness.GerarTrilhas(new[]
                {
                    "Rails for Python Developers 180min",
                    "Overdoing it in Python 60min"
                }
                .ToList());

            Palestra palestra = trilhas.First().Tarde.Palestras.First();

            Assert.True(palestra.Horario == TimeSpan.FromHours(13));
        }

        /// <summary>
        ///     Quantidade de trilhas necessárias deve ser calculado de acordo com o tempo das palestras
        /// </summary>
        [Fact]
        public void ValidarQuantidadeDeTrilhasGeradas()
        {
            ConferenciaBusiness conferenciaBusiness = new ConferenciaBusiness();

            //É necessário duas trilhas
            List<Trilha> trilhas = conferenciaBusiness.GerarTrilhas(new[]
                {
                    "Rails for Python Developers 180min",
                    "Overdoing it in Python 240min",
                    "Accounting-Driven Development 45min"
                }
                .ToList());

            Assert.True(trilhas.Count == 2);
        }
    }
}