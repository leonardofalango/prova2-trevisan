using static System.Console;

var pt = new Point(5, 5);
var circ = ConstrutorGeometrico.GetCirculo(pt, 5);
var squa = ConstrutorGeometrico.GetRetangulo(pt, 5, 5);
var tria = ConstrutorGeometrico.GetTriangulo(pt, 5, 5);

WriteLine(circ);
WriteLine(squa);
WriteLine(tria);


public class Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}

public abstract class FiguraGeometrica
{
    public abstract float Area { get; }
    public abstract float Perimetro { get; }
}

public static class ConstrutorGeometrico
{
    public static FiguraGeometrica GetCirculo(
        Point centro, float raio)
    {
        return new Circulo(centro, raio);
    }
    
    public static FiguraGeometrica GetRetangulo(
        Point cantoSuperiorEsquerdo, float altura, float largura)
    {
        return new Retangulo(cantoSuperiorEsquerdo, altura, largura);
    }
    
    public static FiguraGeometrica GetTriangulo(
        Point cantoEsquerdo, float baseTriangulo, float altura)
    {
        return new Triangulo(cantoEsquerdo, baseTriangulo, altura);
    }
}



public class Circulo : FiguraGeometrica
{
    public Point Centro { get; set; }
    public float Raio { get; set; }

    public Circulo(Point centro, float raio)
    {
        this.Centro = centro;
        this.Raio = raio;
    }

    public override float Area => 2 * (float) Math.PI * Raio;
    public override float Perimetro => (float) Math.PI * Raio * Raio;

    public override string ToString()
    {
        return $"Círculo:\nArea: {this.Area}\nPerimetro: {this.Perimetro}\n";
    }

}

public class Retangulo : FiguraGeometrica
{
    public Point CantoSuperiorEsquerdo { get; set; }
    public float Altura { get; set; }
    public float Largura { get; set; }
    public Retangulo(Point cantoSuperiorEsquerdo, float altura, float largura)
    {
        this.CantoSuperiorEsquerdo = cantoSuperiorEsquerdo;
        this.Altura = altura;
        this.Largura = largura;
    }

    public override float Area => Altura * Largura;
    public override float Perimetro => 2 * (Altura + Largura);

    public override string ToString()
    {
        return $"Retangulo:\nArea: {this.Area}\nPerimetro: {this.Perimetro}\nCanto: {this.CantoSuperiorEsquerdo}\n";
    }
}

public class Triangulo : FiguraGeometrica
{
    public Point CantoEsquerdo { get; set; }
    public float BaseTriangulo { get; set; }
    public float Altura { get; set; }

    public Triangulo(Point cantoEsquerdo, float baseTriangulo, float altura)
    {
        this.CantoEsquerdo = cantoEsquerdo;
        this.BaseTriangulo = baseTriangulo;
        this.Altura = altura;
    }


    public override float Area => (BaseTriangulo * Altura) / 2;
    public override float Perimetro => BaseTriangulo + Altura + (float) Math.Sqrt(BaseTriangulo * BaseTriangulo + Altura * Altura);

    public override string ToString()
    {
        return $"Triângulo:\nArea: {this.Area}\nPerimetro: {this.Perimetro}\nCanto Esq: {this.CantoEsquerdo}\n";
    }
}