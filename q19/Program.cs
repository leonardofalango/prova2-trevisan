using System;

Vetor v = (1, 2);
Vetor u = (4, 3);
Vetor r = (5, 5);

Console.WriteLine($"{v} + {u} = {r}?");

if (v + u == r)
    Console.WriteLine("Sim!");
else Console.WriteLine("Não! (mas devia)");


public class Vetor
{
    public int[] Dados { get; private set; }
    public Vetor(int[] dados)
    {
        this.Dados = dados;
    }

    public bool EIgual(Vetor vetor)
    {
        for (int i = 0; i < this.Dados.Length; i++)
            if (this.Dados[i] != vetor.Dados[i])
                return false;
        return true;
    }

    public Vetor Soma(Vetor vetor)
    {
        int[] soma = new int[this.Dados.Length];
        for (int i = 0; i < this.Dados.Length; i++)
            soma[i] = this.Dados[i] + vetor.Dados[i];
        return new Vetor(soma);
    }

    public override string ToString()
    {
        string s = "(";
        for (int i = 0; i < this.Dados.Length; i++)
            s += this.Dados[i] + (i == this.Dados.Length - 1 ? ")" : ", ");
        return s;
    }

    public static implicit operator Vetor((int, int) tupla)
        => new Vetor(new int[] {
            tupla.Item1, 
            tupla.Item2
        });

    public static implicit operator Vetor((int, int, int) tupla)
        => new Vetor(new int[] {
            tupla.Item1, 
            tupla.Item2,
            tupla.Item3
        });

    public static Vetor operator +(Vetor v, Vetor u)
        => v.Soma(u);

    public static bool operator ==(Vetor v, Vetor u)
        => v.EIgual(u);

    public static bool operator !=(Vetor v, Vetor u)
        => !v.EIgual(u);
}
