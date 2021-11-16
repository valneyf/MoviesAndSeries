using System;
using MoviesAndSeries.Classes;
using static System.Console;

namespace MoviesAndSeries
{
  class Program
  {
    static MovieRepository repo = new MovieRepository();

    static void Main(string[] args)
    {
      string option = userMenu();

      while (option.ToLower() != "x")
      {
        switch (option)
        {
          case "1":
            ListMovie();
            break;
          case "2":
            InsertMovie();
            break;
          case "3":
            UpdateMovie();
            break;
          case "4":
            DeleteMovie();
            break;
          case "5":
            // ShowMovie();
            break;
          case "c":
            Clear();
            break;

          default:
            WriteLine("Digite uma opção válida.");
            break;
        }
        option = userMenu();
      }

      WriteLine("Obrigado por utilizar nossos serviços!");
      WriteLine();
    }

    private static void ListMovie()
    {
      WriteLine("Listando filmes cadastrados");

      var list = repo.toList();

      if (list.Count == 0)
      {
        WriteLine("Nenhum filme cadastrado.");
        return;
      }

      WriteLine("|{0,5} | {1,20} | {2,5} | {3,20} | {4,10} |  {5,10} |", "ID", "Título", "Ano", "Gênero", "Duração", "Situação");
      WriteLine("|---------------------------------------------------------------------------------------|");

      foreach (var movie in list)
      {
        WriteLine("|{0,5} | {1,20} | {2,5} | {3,20} | {4,10} |  {5,10} |", movie.getID(), movie.getTitle(), movie.getYear(), movie.getGenre(), movie.getDuration(), (movie.getSituation() ? " Excluido" : " Ativo"));
      }
    }

    private static void InsertMovie()
    {
      WriteLine("Inserir novo filme");

      foreach (int item in Enum.GetValues(typeof(Genre)))
      {
        WriteLine("{0}-{1}", item, Enum.GetName(typeof(Genre), item));
      }

      Write("Digite o número do gênero entre as opções acima: ");
      int userGenre = int.Parse(ReadLine());

      Write("Digite o título do filme: ");
      string userTitle = ReadLine();

      Write("Digite o ano de lançamento do filme: ");
      int userYear = int.Parse(ReadLine());

      Write("Digite a duração do filme(Ex: 1h 25min): ");
      string userDuration = ReadLine();

      Movie userMovie = new Movie(repo.NextID(), userTitle, userYear, (Genre)userGenre, userDuration);

      repo.toInsert(userMovie);
    }

    private static void UpdateMovie()
    {
      Write("Digite o id do filme a ser atualizado: ");
      int indexMovie = int.Parse(ReadLine());

      foreach (int item in Enum.GetValues(typeof(Genre)))
      {
        WriteLine("{0}-{1}", item, Enum.GetName(typeof(Genre), item));
      }

      Write("Digite o gênero entre as opções acima: ");
      int userGenre = int.Parse(ReadLine());

      Write("Digite o título do filme: ");
      string userTitle = ReadLine();

      Write("Digite o ano de lançamento do filme: ");
      int userYear = int.Parse(ReadLine());

      Write("Digite a duração do filme: ");
      string userDuration = ReadLine();

      Movie updatedMovie = new Movie(indexMovie, userTitle, userYear, (Genre)userGenre, userDuration);

      repo.toUpdate(indexMovie, updatedMovie);
    }

    private static void DeleteMovie()
    {
      Write("Digite o id do filme a ser excluído: ");
      int indexMovie = int.Parse(ReadLine());

      repo.toDelete(indexMovie);
    }

    private static string userMenu()
    {
      WriteLine();
      WriteLine("Aplicativo de Filmes - TakeBlip");
      WriteLine("Informe a opção desejada:");

      WriteLine("1 - Listar filmes");
      WriteLine("2 - Inserir nova filme");
      WriteLine("3 - Atualizar filme");
      WriteLine("4 - Excluir filme");
      WriteLine("5 - Visualizar filme");
      WriteLine("C - Limpar Tela");
      WriteLine("X - Sair");
      WriteLine();

      string option = ReadLine().ToLower();
      WriteLine();
      return option;
    }
  }
}
