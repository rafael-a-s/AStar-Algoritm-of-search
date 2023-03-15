class Program
{
  static void Main(string[] args)
  {
    List<string> map = Maps.map4();

    var start = new Grid();
    start.Y = map.FindIndex(x => x.Contains("A"));
    start.X = map[start.Y].IndexOf("A");

    var finish = new Grid();
    finish.Y = map.FindIndex(x => x.Contains("B"));
    finish.X = map[finish.Y].IndexOf("B");

    start.SetDistance(finish.X, finish.Y);

    var gridsActives = new List<Grid>();
    gridsActives.Add(start);

    var gridsVisiteds = new List<Grid>();

    while (gridsActives.Any())
    {
      var checkeLados = gridsActives.OrderBy(x => x.CustoDistancia).First();

      if (checkeLados.X == finish.X && checkeLados.Y == finish.Y)
      {
        //Resultado encontrado
        var bloco = checkeLados;
        Console.WriteLine("Refazendo o caminho contrario");

        while (true)
        {
          Console.WriteLine($"{bloco.X} : {bloco.Y}");
          if (map[bloco.Y][bloco.X] == ' ')
          {
            var novaLihaMapa = map[bloco.Y].ToCharArray();
            novaLihaMapa[bloco.X] = '*';
            map[bloco.Y] = new string(novaLihaMapa);
          }
          bloco = bloco.Filho;
          if (bloco == null)
          {
            Console.WriteLine("Esborço do Mapa");
            map.ForEach(x => Console.WriteLine(x));
            Console.WriteLine("Está feito!");
            return;
          }
        }
      }

      gridsVisiteds.Add(checkeLados);
      gridsActives.Remove(checkeLados);

      var gridPercorriveis = Grid.getWalkableGrids(map, checkeLados, finish);

      foreach (var blocoCaminhavel in gridPercorriveis)
      {
        //Verifica se ja visitamos esse bloco
        if (gridsVisiteds.Any(x => x.X == blocoCaminhavel.X && x.Y == blocoCaminhavel.Y))
          continue;

        //Ja esta na lista de blocos ativos, mas pode ter um custo menor
        if (gridsActives.Any(x => x.X == blocoCaminhavel.X && x.Y == blocoCaminhavel.Y))
        {
          var existeBloco = gridsActives.First(x => x.X == blocoCaminhavel.X && x.Y == blocoCaminhavel.Y);
          if (existeBloco.CustoDistancia > checkeLados.CustoDistancia)
          {
            //Teve um custo menor, então remove o bloco da vez e adiciona o navegavel
            gridsActives.Remove(existeBloco);
            gridsActives.Add(blocoCaminhavel);
          }

        }
        else
        {
          //Ainda não vimos esse bloco, então devemos guarda-lo
          gridsActives.Add(blocoCaminhavel);
        }
      }
    }

    Console.WriteLine("Caminho não encontrado!");
  }

}
