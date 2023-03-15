public class Grid
{

  public int X { get; set; }
  public int Y { get; set; }
  public int Custo { get; set; }
  public int Distancia { get; set; }
  public int CustoDistancia => Custo + Distancia;
  public Grid? Filho { get; set; }

  //Distância estimada, ignorando as paredes do nosso alvo.
  //Então, quantos ladrilhos à esquerda e à direita, para cima e para baixo, ignorando as paredes, para chegar lá.
  public void SetDistance(int targetX, int targetY)
  {
    //Manhattan e é usada para estimar a distância entre dois pontos em um plano. Ela é calculada ignorando as paredes e obstáculos
    this.Distancia = Math.Abs(targetX - X) + Math.Abs(targetY - Y);
    //heurística de Manhattan, é a soma das distâncias horizontais e verticais entre os dois pontos, sem considerar os obstáculos (paredes).
    this.Distancia *= 10;
    //heuristica euclidiana
    //this.Distancia = heuristicaEuclidiana(targetX, targetY);

  }

  private int heuristicaEuclidiana(int x, int y)
  {
    int dx = x - X;
    int dy = y - Y;
    return (int)Math.Sqrt(dx * dx + dy * dy);
  }

  public static List<Grid> getWalkableGrids(List<string> map, Grid currentGrid, Grid targetGrid)
  {

    var possibles = new List<Grid>(){
      new Grid { X = currentGrid.X, Y = currentGrid.Y - 1, Filho = currentGrid, Custo = currentGrid.Custo + 1 },
      new Grid { X = currentGrid.X, Y = currentGrid.Y + 1, Filho = currentGrid, Custo = currentGrid.Custo + 1},
      new Grid { X = currentGrid.X - 1, Y = currentGrid.Y, Filho = currentGrid, Custo = currentGrid.Custo + 1 },
      new Grid { X = currentGrid.X + 1, Y = currentGrid.Y, Filho = currentGrid, Custo = currentGrid.Custo + 1 },
    };

    possibles.ForEach(grid => grid.SetDistance(targetGrid.X, targetGrid.Y));

    var maxX = map.First().Length - 1;
    var maxY = map.Count - 1;

    return possibles.Where(n => n.X >= 0 && n.X <= maxX)
                    .Where(n => n.Y >= 0 && n.Y <= maxY)
                    .Where(n => map[n.Y][n.X] == ' ' || map[n.Y][n.X] == 'B') //verifica se é um espaço em branco ou o char B
			              .ToList();
  }

}