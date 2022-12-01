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
var averageByMonth = bikes.GroupBy(b => b.Day / 30).Select(g => new {Mounth = g.Key, Mean = g.Average(day => day.Casual + day.Registred)});
foreach (var item in averageByMonth)
{
    Console.WriteLine($"Média de aluguéis no mês {item.Mounth}: {item.Mean}");
}
Console.WriteLine();

Console.WriteLine("3. Como a estação, condições de tempo e temperatura impactam nos resultados?");
var joinBikeDay = bikes.Join(days, bike => bike.Day, day => day.Day, (bike, day) => new { BikesObj = bike, DayObj = day});

var averageByTemp = joinBikeDay.GroupBy(j => j.DayObj.Temp)
    .Select(g => new { Temp = g.Key, Quant = g.Sum(day => day.BikesObj.Casual + day.BikesObj.Registred)});
var averageBySeason = joinBikeDay.GroupBy(j => j.DayObj.Season)
    .Select(g => new { Season = g.Key, Quant = g.Sum(day => day.BikesObj.Casual + day.BikesObj.Registred)});
var averageByWeather = joinBikeDay.GroupBy(j => j.DayObj.Weather)
    .Select(g => new { Weather = g.Key, Quant = g.Sum(day => day.BikesObj.Casual + day.BikesObj.Registred)});

foreach (var item in averageByTemp)
    Console.WriteLine($"Temperatura: {item.Temp} Quantidade de aluguéis: {item.Quant}");
Console.WriteLine();

foreach (var item in averageBySeason)
    Console.WriteLine($"Estação: {item.Season} Quantidade de aluguéis: {item.Quant}");
Console.WriteLine();

foreach (var item in averageByWeather)
    Console.WriteLine($"Clima: {item.Weather} Quantidade de aluguéis: {item.Quant}");
Console.WriteLine();


Console.WriteLine("4. Qual a média de aluguel de bicicletas nos dias de trabalho? E nos dias que não se trabalha?");
var averageByWorkingDay = joinBikeDay.GroupBy(g => g.DayObj.IsWorkingDay)
    .Select(s => new { IsWorkingDay = s.Key, Average = s.Sum(x => x.BikesObj.Casual + x.BikesObj.Registred)});
foreach (var item in averageByWorkingDay)
{
    Console.WriteLine($"Dia de Trabalho: {item.IsWorkingDay}, Aluguéis: {item.Average}");
}
Console.WriteLine();


Console.WriteLine("5. Quais são os picos, tanto de alta quanto de baixa para o aluguel de bicicletas e quais eram as condições (dia de trabalho, condições do tempo, etc) nesses dias.");
var max = bikes.Max(b => b.Casual + b.Registred);
var min = bikes.Min(b => b.Casual + b.Registred);
var maxBike = bikes.First(b => b.Casual + b.Registred == max);
var minBike = bikes.First(b => b.Casual + b.Registred == min);
var maxCondition = days.First(x => x.Day == maxBike.Day);
var minCondition = days.First(x => x.Day == minBike.Day);

Console.WriteLine($"Maior aluguel: {max} Condições climáticas do dia ({maxCondition.Day}): {maxCondition.Season}, {maxCondition.Weather}, {maxCondition.Temp}");
Console.WriteLine();
Console.WriteLine($"Maior aluguel: {min} Condições climáticas do dia ({minCondition.Day}): {minCondition.Season}, {minCondition.Weather}, {minCondition.Temp}");



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