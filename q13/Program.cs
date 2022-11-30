using System;

double[] array = new double[]
{
    8.4, 8.6, 8.4, 7.0, 7.2, 10.0, 7.2, 9.4, 9.8
};
Console.WriteLine(mediaEspecial(array));

double mediaEspecial(double[] array)
{
    double media = 0;
    if (array.Length >= 4)
        return (array[0] + array[1] + array[array.Length - 1] + array[array.Length-2]) / 4;
        
    for (int i = 0; i < array.Length; i++)
        media += array[i];
    return media / array.Length;
}