using System;
using System.Collections.Generic;
using GerenciamentoDeConferencias.Business;
using GerenciamentoDeConferencias.Models;

namespace GerenciamentoDeConferencias
{
    internal class Program
    {
        private static void Main()
        {
            try
            {
                Console.WriteLine("Olá vindo ao Gerenciamento de Conferencias!");

                //Lê os dados digitados para gerar as palestras
                string comandoEntrada;
                List<string> palestrasDigitadas = new List<string>();
                do
                {
                    Console.WriteLine(
                        "Dígite o nome e o tempo de duração da palestra (em minutos) ou [G] para gerar o a agenda.");
                    comandoEntrada = Console.ReadLine();

                    if (comandoEntrada != "G")
                    {
                        palestrasDigitadas.Add(comandoEntrada);
                    }
                } while (comandoEntrada != "G");

                //Com base nos dados digitado gera as trilhas e imprime o calendário das trilhas
                if (palestrasDigitadas.Count > 0)
                {
                    ConferenciaBusiness conferenciaBusiness = new ConferenciaBusiness();
                    List<Trilha> trilhas = conferenciaBusiness.GerarTrilhas(palestrasDigitadas);

                    ImpressaoBusiness impressaoBusiness = new ImpressaoBusiness();
                    List<string> agendaTrilhasImpressao = impressaoBusiness.GerarImpressaoTrilhas(trilhas);

                    foreach (string item in agendaTrilhasImpressao)
                    {
                        Console.WriteLine(item);
                    }
                }

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro durante a execução. {ex.Message}");
            }
        }
    }
}