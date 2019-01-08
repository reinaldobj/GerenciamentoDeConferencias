using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace GerenciamentoDeConferencias
{
    /// <summary>
    ///     Classe com métodos úteis para a aplicação
    /// </summary>
    public class Helper
    {
        /// <summary>
        ///     Retorna uma string com o horário formatado
        /// </summary>
        /// <param name="horarioInicioManha">Horário de Início da Sessão</param>
        /// <returns>Horário da Sessão formatado</returns>
        public static string FormatarHorario(TimeSpan horarioInicioManha) => DateTime
            .Parse(horarioInicioManha.ToString()).ToString("hh:mm tt", CultureInfo.GetCultureInfo("en-US"));

        /// <summary>
        ///     Retorna os números de uma string
        /// </summary>
        /// <param name="entrada">Texto de onde serão retirados os números</param>
        /// <returns></returns>
        public static string BuscarNumeros(string entrada) => Regex.Match(entrada, @"\d+").Value;
    }
}