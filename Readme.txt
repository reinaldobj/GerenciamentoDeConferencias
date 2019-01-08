Descrição da solução:
  Para resolver o problema eu segui os passos abaixo:
  1 - Crio as palestras de acordo com os dados digitados pelo usuário.
  2 - Calculo o total de trilhas necessárias para atender as palestras.
  3 - Gero as trilhas necessárias com as sessões de manhã e tarde.
  4 - Para cada uma das trilhas é feito o preenchimento das sessões.
  4.A - Para preencher as sessões é percorrida a lista de palestras preenchendo a sessão até que ela não
    tenha mais tempo disponível ou que não tenha mais nenhuma com tamanho menor ou igual ao disponível.
  5 - Após gerar as trilhas e preencher as sessões é realizada a impressão da conferencia.

Teste:
  O teste "ValidarAgendaTrilhas" valida a aplicação de acordo com os dados fornecidos nos requisitos.

Arquitetura:
  Criei a solução com uma camada de apresentação, uma camada business onde estão as regras de negócios,
  uma camada de models com as entidades da aplicação.
  Também foi criada um projeto de teste com alguns testes unitários, além de uma Helper com métodos úteis
  para toda a aplicação.

Para executar a aplicação:
  1 - Abrir a solução "GerenciamentoDeConferencias.sln" no Visual Studio.
  2 - Executar o projeto "GerenciamentoDeConferencias".
  3 - Digitar os nomes das palestras seguidos do tempo de duração em minutos. Ex: "Asp.Net Core 45min".
  4 - Após cadastrar todas as palestras digitar o comando "G" para gerar as trilhas e imprimir os horários.
