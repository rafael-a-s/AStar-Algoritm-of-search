class Program
{
  static void Main(string[] args)
  {
    List<string> map = new List<string>
    {
      "A          ",
      "--| |------",
      "           ",
      "   |-----| ",
      "   |     | ",
      "---|     |B"
    };

    var start = new Grid();
    start.X = map.FindIndex(x => x.Contains("A"));
    start.X = map[start.Y].IndexOf("A");

    var finish = new Grid();
    finish.X = map.FindIndex(x => x.Contains("B"));
    finish.X = map[finish.Y].IndexOf("B");

    start.SetDistance(finish.X, finish.Y);

    var gridsActives = new List<Grid>();
    gridsActives.Add(start);

    var gridsVisiteds = new List<Grid>();

  }

}
