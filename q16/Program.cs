using System.IO;
using System.Linq;
using static System.Console;
using System.Collections.Generic;
using System;

/*
1. Qual a média de alugueis de bicicletas em todo período? Sempre
considere os aluguéis casuais junto aos registrados.

2. A empresa parece ter crescido, ou seja, aumentado os alugueis de
cicletas ao longo do tempo? Utilize as médias a cada 30 dias para
analisar isso. Dica: Você pode resolver isso com um GroupBy com
uma divisão.

3. Como a estação, condições de tempo e temperatura impactam nos
resultados? Responda para os três casos separadamente.

4. Qual a média de aluguel de bicicletas nos dias de trabalho? E nos dias
que não se trabalha?

5. Quais são os picos, tanto de alta quanto de baixa para o aluguel de
bicicletas e quais eram as condições (dia de trabalho, condições do
tempo, etc) nesses dias.
*/


var days = getDays();
var bikes = getSharings();

// TODO
Console.WriteLine("1. Qual a média de alugueis de bicicletas em todo período?");

var total = bikes.Sum(b => b.Casual + b.Registred);
var average = total / bikes.Count();
Console.WriteLine($"Média: {average} aluguéis por dia");

Console.WriteLine("2. A empresa parece ter crescido, ou seja, aumentado os alugueis de cicletas ao longo do tempo?");
var averageByMonth = days.GroupBy(b => b.Date.Month).Select(g => new { Month = g.Key, Average = g.Sum(b => b.Casual + b.Registred) / g.Count() });
foreach (var item in averageByMonth)
{
    Console.WriteLine($"Média de aluguéis no mês {item.Month}: {item.Average}");
}

Console.WriteLine("3. Como a estação, condições de tempo e temperatura impactam nos resultados?");
var averageBySeason = bikes.GroupBy(b => b.Season).Select(g => new { Season = g.Key, Average = g.Sum(b => b.Casual + b.Registred) / g.Count() });
foreach (var item in averageBySeason)
{
    Console.WriteLine($"Média de aluguéis na estação {item.Season}: {item.Average}");
}

Console.WriteLine("4. Qual a média de aluguel de bicicletas nos dias de trabalho? E nos dias que não se trabalha?");
var averageByWorkingDay = bikes.GroupBy(b => b.WorkingDay).Select(g => new { WorkingDay = g.Key, Average = g.Sum(b => b.Casual + b.Registred) / g.Count() });
foreach (var item in averageByWorkingDay)
{
    Console.WriteLine($"Média de aluguéis nos dias de trabalho: {item.Average}");
}

Console.WriteLine("5. Quais são os picos, tanto de alta quanto de baixa para o aluguel de bicicletas e quais eram as condições (dia de trabalho, condições do tempo, etc) nesses dias.");
var max = bikes.Max(b => b.Casual + b.Registred);
var min = bikes.Min(b => b.Casual + b.Registred);
var maxBike = bikes.First(b => b.Casual + b.Registred == max);
var minBike = bikes.First(b => b.Casual + b.Registred == min);
Console.WriteLine($"Maior aluguel: {max} ({maxBike.Date})");
Console.WriteLine($"Menor aluguel: {min} ({minBike.Date})");



IEnumerable<DayInfo> getDays()
{
    StreamReader reader = new StreamReader("dayInfo.csv");
    reader.ReadLine();

    while (!reader.EndOfStream)
    {
        var data = reader.ReadLine().Split(',');
        DayInfo info = new DayInfo();

        info.Day = int.Parse(data[0]);
        info.Season = int.Parse(data[1]);
        info.IsWorkingDay = int.Parse(data[2]) == 1;
        info.Weather = int.Parse(data[3]);
        info.Temp = float.Parse(data[4].Replace('.', ','));

        yield return info;
    }

    reader.Close();
}

IEnumerable<BikeSharing> getSharings()
{
    StreamReader reader = new StreamReader("bikeSharing.csv");
    reader.ReadLine();
    
    while (!reader.EndOfStream)
    {
        var data = reader.ReadLine().Split(',');
        BikeSharing sharing = new BikeSharing();

        sharing.Day = int.Parse(data[0]);
        sharing.Casual = int.Parse(data[1]);
        sharing.Registred = int.Parse(data[2]);

        yield return sharing;
    }
    reader.Close();
}

public class DayInfo
{
    public int Day { get; set; }
    public int Season { get; set; }
    public bool IsWorkingDay { get; set; }
    public int Weather { get; set; }
    public float Temp { get; set; }
}

public class BikeSharing
{
    public int Day { get; set; }
    public int Casual { get; set; }
    public int Registred { get; set; }
}