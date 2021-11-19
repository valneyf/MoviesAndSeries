using System;
using MoviesAndSeries.Classes;
using static System.Console;

using Spectre.Console;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.IO.Ports;

namespace MoviesAndSeries
{
  class Program
  {
    static MovieRepository repoMovie = new MovieRepository();
    static SerieRepository repoSerie = new SerieRepository();

    static void Main(string[] args)
    {
      Clear();
      string option = userMenu();

      while (option != "Sair")
      {
        Clear();
        switch (option)
        {
          // case "1":
          case "Listar Mídias":
            ListMedia();
            break;
          // case "2":
          case "Inserir Filme/Série":
            InsertMedia();
            break;
          // case "3":
          case "Atualizar Filme/Série":
            UpdateMedia();
            break;
          case "Remover Filme/Série":
            DeleteMedia();
            break;
          case "Visualizar Filme/Série":
            ShowMedia();
            break;

          default:
            WriteLine("Escolha uma das alternativas listadas.");
            break;
        }
        option = userMenu();
      }
      WriteLine();
      WriteLine();

      var rule = new Rule("[bold green]Obrigado por utilizar nossos serviços![/]");
      AnsiConsole.Write(rule);
      WriteLine();
    }

    private static void ListMedia()
    {
      Clear();
      var rule1 = new Rule("[bold red]Filmes Cadastrados[/]");
      AnsiConsole.Write(rule1);
      WriteLine();

      var listMovies = repoMovie.toList();

      var table1 = new Table();
      table1.Centered();

      if (listMovies.Count == 0)
      {
        table1.AddColumn(new TableColumn("[yellow]Nenhum filme cadastrado.[/]").Centered());
        AnsiConsole.Write(table1);
      }
      else
      {
        ViewAllMovies(listMovies, false);
      }

      WriteLine();

      var rule2 = new Rule("[bold red]Séries Cadastradas[/]");
      AnsiConsole.Write(rule2);
      WriteLine();

      var listSeries = repoSerie.toList();

      var table2 = new Table();
      table2.Centered();

      if (listSeries.Count == 0)
      {
        table2.AddColumn(new TableColumn("[yellow]Nenhuma série cadastrada.[/]").Centered());
        AnsiConsole.Write(table2);
      }
      else
      {
        ViewAllSeries(listSeries, false);
      }

      return;
    }

    private static void InsertMedia()
    {
      var tableInsert = new Table();
      tableInsert.AddColumn("[bold yellow]Inserir Filme / Série[/]");
      AnsiConsole.Write(tableInsert);

    Question:
      WriteLine();
      Write("Digite 1 para FILME ou 2 para SÉRIE: ");
      int userMedia = int.Parse(ReadLine());

      switch (userMedia)
      {
        case 1:
          WriteLine();
          var rule = new Rule("[red]Novo Filme[/]");
          rule.LeftAligned();
          AnsiConsole.Write(rule);

          string movieTitle = "";
          try
          {
            var mediaGenre = chooseGenre();

            WriteLine("Gênero escolhido: " + mediaGenre);
            int movieGenre = (int)Enum.Parse(typeof(Genre), mediaGenre);

            Write("Digite o título do filme: ");
            movieTitle = ReadLine();

            Write("Digite o ano de lançamento do filme: ");
            int movieYear = int.Parse(ReadLine());

            Write("Digite a duração do filme(Ex: 1h 25min): ");
            string movieDuration = ReadLine();

            Movie userMovie = new Movie(repoMovie.NextID(), movieTitle, movieYear, (Genre)movieGenre, movieDuration);

            repoMovie.toInsert(userMovie);

            WriteLine($"Filme {movieTitle} cadastrado com sucesso!");

            break;
          }
          catch (System.FormatException)
          {
            WriteLine("Foi informado valor não numérico para o campo Ano.");
            WriteLine($"Filme {movieTitle} não será cadastrado.");

            break;
          }

        case 2:
          WriteLine();
          var rule2 = new Rule("[red]Nova Série[/]");
          rule2.LeftAligned();
          AnsiConsole.Write(rule2);

          string serieTitle = "";
          try
          {
            var mediaGenre = chooseGenre();

            WriteLine("Gênero escolhido: " + mediaGenre);
            int serieGenre = (int)Enum.Parse(typeof(Genre), mediaGenre);

            Write("Digite o título da série: ");
            serieTitle = ReadLine();

            Write("Digite qual a temporada da série(1, 2, 3): ");
            int serieSeason = int.Parse(ReadLine());

            Write("Digite o ano desta temporada da série: ");
            int serieYear = int.Parse(ReadLine());

            Serie userSerie = new Serie(repoSerie.NextID(), serieTitle, serieYear, (Genre)serieGenre, serieSeason);

            repoSerie.toInsert(userSerie);

            WriteLine($"Série {serieTitle} cadastrada com sucesso!");

            break;
          }
          catch (System.FormatException)
          {
            WriteLine("Foi informado valor não numérico para o campo Temporada ou campo Ano.");
            WriteLine($"Série {serieTitle} não será cadastrada.");

            break;
          }

        default:
          WriteLine("Escolha uma das alternativas indicadas.");
          WriteLine();
          goto Question;
      }
    }

    private static void UpdateMedia()
    {
      Clear();
      var tableUpdate = new Table();
      tableUpdate.AddColumn("[bold yellow]Atualizar Filme / Série[/]");
      AnsiConsole.Write(tableUpdate);

    Question:
      Write("Digite 1 para FILME ou 2 para SÉRIE: ");
      int userMedia = int.Parse(ReadLine());

      switch (userMedia)
      {
        case 1:
          WriteLine();
          var rule = new Rule("[red]Atualizar Filme[/]");
          rule.LeftAligned();
          AnsiConsole.Write(rule);

          WriteLine();
          var listMovies = repoMovie.toList();

          if (listMovies.Count == 0)
          {
            var table1 = new Table();
            table1.Centered();
            table1.AddColumn(new TableColumn("[yellow]Nenhum filme cadastrado.[/]").Centered());
            AnsiConsole.Write(table1);

            WriteLine();
            WriteLine("Pressione qualquer tecla para voltar ao menu principal.");
            ReadKey();

            Clear();
            break;
          }
          else
          {
            ViewAllMovies(listMovies, false);
          }

          string input = "";

          try
          {
            WriteLine();
            AnsiConsole.Write(new Markup("Digite o [bold yellow]ID[/] do filme a ser atualizado: "));
            int indexMovie = int.Parse(ReadLine());

            Movie userMovie = repoMovie.ReturnByID(indexMovie);

            var mediaGenre = chooseGenre();

            WriteLine("Gênero escolhido: " + mediaGenre);
            int userGenre = (int)Enum.Parse(typeof(Genre), mediaGenre);

            Write("Digite o título do filme (Atual: {0}): ", userMovie.getTitle());
            input = ReadLine();
            string userTitle = input.Equals("") ? userMovie.getTitle() : input;

            Write("Digite o ano de lançamento do filme (Atual: {0}): ", userMovie.getYear());
            input = ReadLine();
            int userYear = input.Equals("") ? userMovie.getYear() : int.Parse(input);

            Write("Digite a duração do filme (Atual: {0}): ", userMovie.getDuration());
            input = ReadLine();
            string userDuration = input.Equals("") ? userMovie.getDuration() : input;

            Movie updatedMovie = new Movie(indexMovie, userTitle, userYear, (Genre)userGenre, userDuration);

            repoMovie.toUpdate(indexMovie, updatedMovie);

            WriteLine();
            WriteLine($"Filme #{indexMovie} - {userTitle} atualizado");

            break;
          }
          catch (System.FormatException)
          {

            WriteLine("Foi informado um valor não numérico para o campo Ano.");
            WriteLine($"A atualização será cancelada.");

            break;
          }
          catch (System.ArgumentOutOfRangeException)
          {

            WriteLine("Foi informado uma ID não cadastrada.");
            WriteLine($"A atualização será cancelada.");

            break;
          }

        case 2:
          WriteLine();
          var rule2 = new Rule("[red]Atualizar Série[/]");
          rule2.LeftAligned();
          AnsiConsole.Write(rule2);

          WriteLine();
          var listSeries = repoSerie.toList();

          if (listSeries.Count == 0)
          {
            var table2 = new Table();
            table2.Centered();
            table2.AddColumn(new TableColumn("[yellow]Nenhuma série cadastrada.[/]").Centered());
            AnsiConsole.Write(table2);

            WriteLine();
            WriteLine("Pressione qualquer tecla para voltar ao menu principal.");
            ReadKey();

            Clear();
            break;
          }
          else
          {
            ViewAllSeries(listSeries, false);
          }

          try
          {
            WriteLine();
            AnsiConsole.Write(new Markup("Digite o [bold yellow]ID[/] da série a ser atualizada: "));

            int indexSerie = int.Parse(ReadLine());

            Serie userSerie = repoSerie.ReturnByID(indexSerie);

            var mediaGenre = chooseGenre();

            WriteLine("Gênero escolhido: " + mediaGenre);
            int serieGenre = (int)Enum.Parse(typeof(Genre), mediaGenre);

            Write("Digite o título da série(Atual: {0}): ", userSerie.getTitle());
            input = ReadLine();
            string serieTitle = input.Equals("") ? userSerie.getTitle() : input;

            Write("Digite qual a temporada da série(1, 2, 3) (Atual: {0}): ", userSerie.getSeason());
            input = ReadLine();
            int serieSeason = input.Equals("") ? userSerie.getSeason() : int.Parse(input);

            Write("Digite o ano desta temporada da série (Atual: {0}): ", userSerie.getYear());
            input = ReadLine();
            int serieYear = input.Equals("") ? userSerie.getYear() : int.Parse(input);

            Serie updatedSerie = new Serie(indexSerie, serieTitle, serieYear, (Genre)serieGenre, serieSeason);

            repoSerie.toUpdate(indexSerie, updatedSerie);

            WriteLine();
            WriteLine($"Série #{indexSerie} - {serieTitle} atualizada");

            break;
          }
          catch (System.FormatException)
          {

            WriteLine("Foi informado um valor não numérico para o campo Temporada ou campo Ano.");
            WriteLine($"A atualização será cancelada.");

            break;
          }
          catch (System.ArgumentOutOfRangeException)
          {

            WriteLine("Foi informado uma ID não cadastrada.");
            WriteLine($"A atualização será cancelada.");

            break;
          }

        default:
          WriteLine("Escolha uma das alternativas indicadas.");
          WriteLine();
          goto Question;
      }
    }

    private static void DeleteMedia()
    {
      Clear();
      var tableDelete = new Table();
      tableDelete.AddColumn("[bold yellow]Remover Filme / Série[/]");
      AnsiConsole.Write(tableDelete);

    Question:
      Write("Digite 1 para FILME ou 2 para SÉRIE: ");
      int userMedia = int.Parse(ReadLine());

      switch (userMedia)
      {
        case 1:
          WriteLine();
          var rule = new Rule("[red]Remover Filme[/]");
          rule.LeftAligned();
          AnsiConsole.Write(rule);

          WriteLine();
          var listMovies = repoMovie.toList();

          if (listMovies.Count == 0)
          {
            var table1 = new Table();
            table1.Centered();
            table1.AddColumn(new TableColumn("[yellow]Nenhum filme cadastrado.[/]").Centered());
            AnsiConsole.Write(table1);

            WriteLine();
            WriteLine("Pressione qualquer tecla para voltar ao menu principal.");
            ReadKey();

            Clear();
            break;
          }
          else
          {
            ViewAllMovies(listMovies, false);
          }

          try
          {
            WriteLine();
            AnsiConsole.Write(new Markup("Digite o [bold red]ID[/] do FILME a ser removido: "));
            int indexMovie = int.Parse(ReadLine());

            repoMovie.toDelete(indexMovie);
            WriteLine("Filme removido.");

            break;
          }
          catch (System.ArgumentOutOfRangeException)
          {

            WriteLine("Foi informado uma ID não cadastrada.");
            WriteLine($"A remoção será cancelada.");

            break;
          }

        case 2:
          var rule2 = new Rule("[red]Remover Filme[/]");
          rule2.LeftAligned();
          AnsiConsole.Write(rule2);

          WriteLine();
          var listSeries = repoSerie.toList();

          if (listSeries.Count == 0)
          {
            var table2 = new Table();
            table2.Centered();
            table2.AddColumn(new TableColumn("[yellow]Nenhuma série cadastrada.[/]").Centered());
            AnsiConsole.Write(table2);

            WriteLine();
            WriteLine("Pressione qualquer tecla para voltar ao menu principal.");
            ReadKey();

            Clear();
            break;
          }
          else
          {
            ViewAllSeries(listSeries, false);
          }

          try
          {
            WriteLine();
            AnsiConsole.Write(new Markup("Digite o [bold red]ID[/] da SÉRIE a ser removida: "));
            int indexSerie = int.Parse(ReadLine());

            repoSerie.toDelete(indexSerie);
            WriteLine("Série removida.");

            break;
          }
          catch (System.ArgumentOutOfRangeException)
          {

            WriteLine("Foi informado uma ID não cadastrada.");
            WriteLine($"A remoção será cancelada.");

            break;
          }
        default:
          WriteLine("Escolha uma das alternativas indicadas.");
          WriteLine();
          goto Question;
      }
    }

    private static void ShowMedia()
    {
      Clear();
      var tableShow = new Table();
      tableShow.AddColumn("[bold yellow]Visualizar Filme / Série[/]");
      AnsiConsole.Write(tableShow);

    Question:
      Write("Digite 1 para FILME ou 2 para SÉRIE: ");
      int userMedia = int.Parse(ReadLine());

      switch (userMedia)
      {
        case 1:
          WriteLine();
          var rule = new Rule("[red]Visualizar Filme[/]");
          rule.LeftAligned();
          AnsiConsole.Write(rule);

          WriteLine();
          var listMovies = repoMovie.toList();

          if (listMovies.Count == 0)
          {
            var table1 = new Table();
            table1.Centered();
            table1.AddColumn(new TableColumn("[yellow]Nenhum filme cadastrado.[/]").Centered());
            AnsiConsole.Write(table1);

            WriteLine();
            WriteLine("Pressione qualquer tecla para voltar ao menu principal.");
            ReadKey();

            Clear();
            break;
          }
          else
          {
            ViewAllMovies(listMovies, true);
          }

          try
          {
            WriteLine();
            AnsiConsole.Write(new Markup("Digite o [bold yellow]ID[/] do filme que deseja visualizar: "));

            int indexMovie = int.Parse(ReadLine());

            var movie = repoMovie.ReturnByID(indexMovie);
            ViewMovie(movie);

            break;
          }
          catch (System.ArgumentOutOfRangeException)
          {

            WriteLine("Foi informado uma ID não cadastrada.");
            WriteLine($"Não será possível visualizar os detalhes de um FILME.");

            break;
          }

        case 2:
          WriteLine();
          var rule2 = new Rule("[red]Visualizar Filme[/]");
          rule2.LeftAligned();
          AnsiConsole.Write(rule2);

          WriteLine();
          var listSeries = repoSerie.toList();

          if (listSeries.Count == 0)
          {
            var table2 = new Table();
            table2.Centered();
            table2.AddColumn(new TableColumn("[yellow]Nenhuma série cadastrada.[/]").Centered());
            AnsiConsole.Write(table2);

            WriteLine();
            WriteLine("Pressione qualquer tecla para voltar ao menu principal.");
            ReadKey();

            Clear();
            break;
          }
          else
          {
            ViewAllSeries(listSeries, true);
          }

          try
          {
            WriteLine();
            AnsiConsole.Write(new Markup("Digite o [bold yellow]ID[/] da série que deseja visualizar: "));

            int indexSerie = int.Parse(ReadLine());

            var serie = repoSerie.ReturnByID(indexSerie);

            ViewSerie(serie);

            break;
          }
          catch (System.ArgumentOutOfRangeException)
          {

            WriteLine("Foi informado uma ID não cadastrada.");
            WriteLine($"Não será possível visualizar os detalhes de uma SÉRIE.");

            break;
          }

        default:
          WriteLine("Escolha uma das alternativas indicadas.");
          WriteLine();
          goto Question;
      }


    }

    private static string chooseGenre()
    {
      string[] vector = new string[14];

      foreach (int item in Enum.GetValues(typeof(Genre)))
      {
        vector[item] = Enum.GetName(typeof(Genre), item);
      }

      var choosenGenre = AnsiConsole.Prompt(
          new SelectionPrompt<string>()
              .Title("Escolha o [green]gênero[/]:")
              .PageSize(10)
              .MoreChoicesText("[grey](Mova para cima ou para baixo para mais opções)[/]")
              .AddChoices(new[] {
                    $"{vector[1]}", $"{vector[2]}", $"{vector[3]}", $"{vector[4]}", $"{vector[5]}",
                    $"{vector[6]}", $"{vector[7]}", $"{vector[8]}", $"{vector[9]}", $"{vector[10]}",
                    $"{vector[11]}", $"{vector[12]}", $"{vector[13]}",
      }
    ));

      return choosenGenre;
    }

    private static void ViewMovie(Movie movie)
    {
      var table1 = new Table();
      table1.Centered();
      table1.AddColumn(new TableColumn("[bold yellow]ID[/]").Centered());
      table1.AddColumn(new TableColumn("[bold yellow]Título[/]").Centered());
      table1.AddColumn(new TableColumn("[bold yellow]Duração[/]").Centered());
      table1.AddColumn(new TableColumn("[bold yellow]Ano[/]").Centered());
      table1.AddColumn(new TableColumn("[bold yellow]Gênero[/]").Centered());
      table1.AddColumn(new TableColumn("[bold yellow]Situação[/]").Centered());

      table1.AddRow($"[yellow]{movie.getID()}[/]", $"[yellow]{movie.getTitle()}[/]", $"[yellow]{movie.getDuration()}[/]", $"[yellow]{movie.getYear()}[/]", $"[yellow]{movie.getGenre()}[/]", $"[yellow]{(movie.getSituation() ? "Removido" : "Ativo")}[/]");

      AnsiConsole.Write(table1);
    }

    private static void ViewAllMovies(List<Movie> repository, bool resumed)
    {
      var table1 = new Table();
      table1.Centered();

      if (resumed)
      {
        table1.AddColumn(new TableColumn("[bold yellow]ID[/]").Centered());
        table1.AddColumn(new TableColumn("[bold yellow]Título[/]").Centered());

        foreach (var movie in repository)
        {
          table1.AddRow($"[yellow]{movie.getID()}[/]", $"[yellow]{movie.getTitle()}[/]");
        }
      }
      else
      {
        table1.AddColumn(new TableColumn("[bold yellow]ID[/]").Centered());
        table1.AddColumn(new TableColumn("[bold yellow]Título[/]").Centered());
        table1.AddColumn(new TableColumn("[bold yellow]Duração[/]").Centered());
        table1.AddColumn(new TableColumn("[bold yellow]Ano[/]").Centered());
        table1.AddColumn(new TableColumn("[bold yellow]Gênero[/]").Centered());
        table1.AddColumn(new TableColumn("[bold yellow]Situação[/]").Centered());

        foreach (var movie in repository)
        {
          table1.AddRow($"[yellow]{movie.getID()}[/]", $"[yellow]{movie.getTitle()}[/]", $"[yellow]{movie.getDuration()}[/]", $"[yellow]{movie.getYear()}[/]", $"[yellow]{movie.getGenre()}[/]", $"[yellow]{(movie.getSituation() ? "Removido" : "Ativo")}[/]");
        }
      }

      AnsiConsole.Write(table1);
    }

    private static void ViewSerie(Serie serie)
    {
      var table2 = new Table();
      table2.Centered();
      table2.AddColumn(new TableColumn("[bold yellow]ID[/]").Centered());
      table2.AddColumn(new TableColumn("[bold yellow]Título[/]").Centered());
      table2.AddColumn(new TableColumn("[bold yellow]Temporada[/]").Centered());
      table2.AddColumn(new TableColumn("[bold yellow]Ano[/]").Centered());
      table2.AddColumn(new TableColumn("[bold yellow]Gênero[/]").Centered());
      table2.AddColumn(new TableColumn("[bold yellow]Situação[/]").Centered());

      table2.AddRow($"[yellow]{serie.getID()}[/]", $"[yellow]{serie.getTitle()}[/]", $"[yellow]{serie.getSeason()}[/]", $"[yellow]{serie.getYear()}[/]", $"[yellow]{serie.getGenre()}[/]", $"[yellow]{(serie.getSituation() ? "Removida" : "Ativa")}[/]");

      AnsiConsole.Write(table2);
    }

    private static void ViewAllSeries(List<Serie> repository, bool resumed)
    {
      var table2 = new Table();
      table2.Centered();

      if (resumed)
      {
        table2.AddColumn(new TableColumn("[bold yellow]ID[/]").Centered());
        table2.AddColumn(new TableColumn("[bold yellow]Título[/]").Centered());

        foreach (var serie in repository)
        {
          table2.AddRow($"[yellow]{serie.getID()}[/]", $"[yellow]{serie.getTitle()}[/]");
        }
      }
      else
      {
        table2.AddColumn(new TableColumn("[bold yellow]ID[/]").Centered());
        table2.AddColumn(new TableColumn("[bold yellow]Título[/]").Centered());
        table2.AddColumn(new TableColumn("[bold yellow]Temporada[/]").Centered());
        table2.AddColumn(new TableColumn("[bold yellow]Ano[/]").Centered());
        table2.AddColumn(new TableColumn("[bold yellow]Gênero[/]").Centered());
        table2.AddColumn(new TableColumn("[bold yellow]Situação[/]").Centered());

        foreach (var serie in repository)
        {
          table2.AddRow($"[yellow]{serie.getID()}[/]", $"[yellow]{serie.getTitle()}[/]", $"[yellow]{serie.getSeason()}[/]", $"[yellow]{serie.getYear()}[/]", $"[yellow]{serie.getGenre()}[/]", $"[yellow]{(serie.getSituation() ? "Removida" : "Ativa")}[/]");
        }
      }

      AnsiConsole.Write(table2);
    }

    private static string userMenu()
    {
      var table = new Table();
      table.Border(TableBorder.Rounded);
      table.Expand();
      table.AddColumn(new TableColumn("[bold green]Aplicativo de Filmes e Séries - TakeBlip[/]").Centered());

      table.AddRow("[bold yellow]Menu[/]");

      AnsiConsole.Write(table);

      var choosenOption = AnsiConsole.Prompt(
          new SelectionPrompt<string>()
              .Title("[green]Escolha uma opção abaixo[/]:")
              .PageSize(6)
              .MoreChoicesText("[grey](Mova para cima ou para baixo para selecionar opção)[/]")
              .AddChoices(new[] {
                    "Listar Mídias", "Inserir Filme/Série", "Atualizar Filme/Série",
                    "Remover Filme/Série", "Visualizar Filme/Série", "Sair",
      }
    ));

      return choosenOption;
    }
  }
}
