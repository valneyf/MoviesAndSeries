using System;
using MoviesAndSeries.Classes;
using static System.Console;

namespace MoviesAndSeries
{
  class Program
  {
    static MovieRepository repoMovie = new MovieRepository();
    static SerieRepository repoSerie = new SerieRepository();

    static void Main(string[] args)
    {
      string option = userMenu();

      while (option.ToLower() != "x")
      {
        switch (option)
        {
          case "1":
            ListMedia();
            break;
          case "2":
            InsertMedia();
            break;
          case "3":
            UpdateMedia();
            break;
          case "4":
            DeleteMedia();
            break;
          case "5":
            ShowMedia();
            break;
          case "c":
            Clear();
            break;

          default:
            WriteLine("Escolha uma das alternativas listadas.");
            break;
        }
        option = userMenu();
      }

      WriteLine("Obrigado por utilizar nossos serviços!");
      WriteLine();
    }

    private static void ListMedia()
    {
      WriteLine("Listando filmes cadastrados");

      var listMovies = repoMovie.toList();

      if (listMovies.Count == 0)
      {
        WriteLine("Nenhum filme cadastrado.");
      }
      else
      {
        WriteLine("|{0,5} | {1,20} | {2,5} | {3,20} | {4,10} |  {5,10} |", "ID", "Título", "Ano", "Gênero", "Duração", "Situação");
        WriteLine("|---------------------------------------------------------------------------------------|");

        foreach (var movie in listMovies)
        {
          WriteLine("|{0,5} | {1,20} | {2,5} | {3,20} | {4,10} |  {5,10} |", movie.getID(), movie.getTitle(), movie.getYear(), movie.getGenre(), movie.getDuration(), (movie.getSituation() ? " Excluído" : " Ativo"));
        }
      }

      WriteLine();
      WriteLine("Listando séries cadastradas");

      var listSeries = repoSerie.toList();

      if (listSeries.Count == 0)
      {
        WriteLine("Nenhuma série cadastrada.");
      }

      else
      {
        WriteLine("|{0,5} | {1,20} | {2,5} | {3,20} | {4,10} |  {5,10} |", "ID", "Título", "Ano", "Gênero", "Temporada", "Situação");
        WriteLine("|---------------------------------------------------------------------------------------|");

        foreach (var serie in listSeries)
        {
          WriteLine("|{0,5} | {1,20} | {2,5} | {3,20} | {4,10} |  {5,10} |", serie.getID(), serie.getTitle(), serie.getYear(), serie.getGenre(), serie.getSeason(), (serie.getSituation() ? " Excluída" : " Ativa"));
        }
      }

      return;
    }

    private static void InsertMedia()
    {
    Question:
      Write("Digite 1 para inserir um FILME ou 2 para inserir uma SÉRIE: ");
      int userMedia = int.Parse(ReadLine());

      switch (userMedia)
      {
        case 1:
          WriteLine("Novo Filme");

          foreach (int item in Enum.GetValues(typeof(Genre)))
          {
            WriteLine("{0}-{1}", item, Enum.GetName(typeof(Genre), item));
          }

          Write("Digite o número do gênero entre as opções acima: ");
          int movieGenre = int.Parse(ReadLine());

          Write("Digite o título do filme: ");
          string movieTitle = ReadLine();

          Write("Digite o ano de lançamento do filme: ");
          int movieYear = int.Parse(ReadLine());

          Write("Digite a duração do filme(Ex: 1h 25min): ");
          string movieDuration = ReadLine();

          Movie userMovie = new Movie(repoMovie.NextID(), movieTitle, movieYear, (Genre)movieGenre, movieDuration);

          repoMovie.toInsert(userMovie);

          WriteLine($"Filme {movieTitle} cadastrado com sucesso!");

          WriteLine();

          break;

        case 2:
          WriteLine("Nova Série");

          foreach (int item in Enum.GetValues(typeof(Genre)))
          {
            WriteLine("{0}-{1}", item, Enum.GetName(typeof(Genre), item));
          }

          Write("Digite o número do gênero entre as opções acima: ");
          int serieGenre = int.Parse(ReadLine());

          Write("Digite o título da série: ");
          string serieTitle = ReadLine();

          Write("Digite qual a temporada da série(1, 2, 3): ");
          int serieSeason = int.Parse(ReadLine());

          Write("Digite o ano desta temporada da série: ");
          int serieYear = int.Parse(ReadLine());

          Serie userSerie = new Serie(repoMovie.NextID(), serieTitle, serieYear, (Genre)serieGenre, serieSeason);

          repoSerie.toInsert(userSerie);

          WriteLine($"Série {serieTitle} cadastrada com sucesso!");

          WriteLine();

          break;

        default:
          WriteLine("Escolha uma das alternativas indicadas.");
          WriteLine();
          goto Question;
      }
    }

    private static void UpdateMedia()
    {
    Question:
      Write("Digite 1 para atualizar um FILME ou 2 para atualizar uma SÉRIE: ");
      int userMedia = int.Parse(ReadLine());

      switch (userMedia)
      {
        case 1:
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

          repoMovie.toUpdate(indexMovie, updatedMovie);

          WriteLine($"Filme #{indexMovie} - {userTitle} atualizado");

          break;

        case 2:
          Write("Digite o id do série a ser atualizado: ");
          int indexSerie = int.Parse(ReadLine());

          foreach (int item in Enum.GetValues(typeof(Genre)))
          {
            WriteLine("{0}-{1}", item, Enum.GetName(typeof(Genre), item));
          }

          Write("Digite o gênero entre as opções acima: ");
          int serieGenre = int.Parse(ReadLine());

          Write("Digite o título da série: ");
          string serieTitle = ReadLine();

          Write("Digite qual a temporada da série(1, 2, 3): ");
          int serieSeason = int.Parse(ReadLine());

          Write("Digite o ano desta temporada da série: ");
          int serieYear = int.Parse(ReadLine());

          Serie updatedSerie = new Serie(indexSerie, serieTitle, serieYear, (Genre)serieGenre, serieSeason);

          repoSerie.toUpdate(indexSerie, updatedSerie);

          WriteLine($"Série #{indexSerie} - {serieTitle} atualizado");

          break;
        default:
          WriteLine("Escolha uma das alternativas indicadas.");
          WriteLine();
          goto Question;
      }
    }

    private static void DeleteMedia()
    {
    Question:
      Write("Digite 1 para DELETAR um FILME ou 2 para DELETAR uma SÉRIE: ");
      int userMedia = int.Parse(ReadLine());

      switch (userMedia)
      {
        case 1:
          Write("Digite o id do FILME a ser excluído: ");
          int indexMovie = int.Parse(ReadLine());

          repoMovie.toDelete(indexMovie);
          WriteLine("Filme excluído.");

          break;

        case 2:
          Write("Digite o id da SÉRIE a ser excluída: ");
          int indexSerie = int.Parse(ReadLine());

          repoSerie.toDelete(indexSerie);
          WriteLine("Série excluído.");

          break;

        default:
          WriteLine("Escolha uma das alternativas indicadas.");
          WriteLine();
          goto Question;
      }
    }

    private static void ShowMedia()
    {
    Question:
      Write("Digite 1 para visualizar um FILME ou 2 para visualizar uma SÉRIE: ");
      int userMedia = int.Parse(ReadLine());

      switch (userMedia)
      {
        case 1:
          Write("Digite o id do filme que deseja visualizar: ");
          int indexMovie = int.Parse(ReadLine());

          var movie = repoMovie.ReturnByID(indexMovie);

          WriteLine("|{0,5} | {1,20} | {2,5} | {3,20} | {4,10} |  {5,10} |", "ID", "Título", "Ano", "Gênero", "Duração", "Situação");
          WriteLine("|---------------------------------------------------------------------------------------|");

          WriteLine("|{0,5} | {1,20} | {2,5} | {3,20} | {4,10} |  {5,10} |", movie.getID(), movie.getTitle(), movie.getYear(), movie.getGenre(), movie.getDuration(), (movie.getSituation() ? " Excluido" : " Ativo"));

          break;

        case 2:
          Write("Digite o id da série que deseja visualizar: ");
          int indexSerie = int.Parse(ReadLine());

          var serie = repoSerie.ReturnByID(indexSerie);

          WriteLine("|{0,5} | {1,20} | {2,5} | {3,20} | {4,10} |  {5,10} |", "ID", "Título", "Ano", "Gênero", "Temporada", "Situação");
        WriteLine("|---------------------------------------------------------------------------------------|");

          WriteLine("|{0,5} | {1,20} | {2,5} | {3,20} | {4,10} |  {5,10} |", serie.getID(), serie.getTitle(), serie.getYear(), serie.getGenre(), serie.getSeason(), (serie.getSituation() ? " Excluída" : " Ativa"));

          break;

        default:
          WriteLine("Escolha uma das alternativas indicadas.");
          WriteLine();
          goto Question;
      }


    }

    private static string userMenu()
    {
      WriteLine();
      WriteLine("Aplicativo de Filmes e Séries - TakeBlip");
      WriteLine("Informe a opção desejada:");

      WriteLine("1 - Listar mídias");
      WriteLine("2 - Inserir filme/série");
      WriteLine("3 - Atualizar filme/série");
      WriteLine("4 - Excluir filme/série");
      WriteLine("5 - Visualizar filme/série");
      WriteLine("C - Limpar Tela");
      WriteLine("X - Sair");
      WriteLine();

      string option = ReadLine().ToLower();
      WriteLine();
      return option;
    }
  }
}
