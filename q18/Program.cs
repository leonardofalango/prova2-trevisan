using System;
using System.Drawing;
using System.Collections.Generic;

Universe universe = new Universe();
Earth earth = new Earth();
universe.Add(earth);

// universe.Add(new Mon(new PointF(0f, -385)));
// universe.Add(new Mon(new PointF(0f, 385)));

universe.Add(new Mon(30));
universe.Add(new Mon(-30));

universe.Add(new Rocket(-45, 0.0020f, earth));
universe.Add(new Rocket(45f, 0.0005f, earth));

// universe.Add(new Satellite(-30, earth));


App.Run(universe, 1000);

public abstract partial class Body
{
    public PointF Position { get; set; }
    public float VelocityX { get; set; }
    public float VelocityY { get; set; }
    public Color Color { get; set; }
    public float Size { get; set; }
    public float Mass { get; set; }

    public void Update(float dt)
    {
        Position = new PointF(
            Position.X + VelocityX * dt,
            Position.Y + VelocityY * dt
        );
    }

    public void ApplyForce(Body other, float dt)
    {
        const float G = 6.6743E-11f;
        float r = 1000 * 1000 * Distance(other);
        float force = G * this.Mass * other.Mass / (r * r);
        float acceleration = force / Mass;
        float dx = 1000 * 1000 * (other.Position.X - this.Position.X);
        float dy = 1000 * 1000 * (other.Position.Y - this.Position.Y);
        this.VelocityX += (dt * acceleration * dx / r) / 1000 / 1000;
        this.VelocityY += (dt * acceleration * dy / r) / 1000 / 1000;
    }

    public float Distance(Body other)
    {
        float dx = other.Position.X - this.Position.X;
        float dy = other.Position.Y - this.Position.Y;
        return (float)Math.Sqrt(dx * dx + dy * dy);
    }
}

public class Earth : Body
{
    public Earth()
    {
        Position = PointF.Empty;
        VelocityX = 0f;
        VelocityY = 0f;
        Color = Color.Blue;
        Mass = 5.9742E24f;
        Size = 127.562f; // 12756,2 km
    }
}

public class Mon : Body
{
    public Mon(float angle)
    {
        Position = new PointF(385 * (float) Math.Cos(angle), 385 * (float) Math.Sin(angle)); // distância Terra-Lua
        VelocityX = 0.001f; // 1 km/s
        VelocityY = 0;
        Color = Color.White;
        Mass = 7.36E22f;
        Size = 3.4748f; // 3474,8 km
    }
}

public class Rocket : Body
{
    public Rocket(float angle, float velocity, Earth earth)
    {
        Position = new PointF(earth.Size * (float) Math.Cos(angle), earth.Size * (float) Math.Sin(angle));
        VelocityX = velocity;
        VelocityY = velocity;
        
        Color = Color.Red;
        Mass = 0.05f;
        Size = 10f;
    }
}

public class Satellite : Body
{
    public Satellite(float angle, Earth earth)
    {
        Position = new PointF(385 * (float) Math.Cos(angle), 385 * (float) Math.Sin(angle));

        float r = 0.12756f / 2 - 0.1f;
        float g = 6.67E-11f;
        float m = earth.Mass;
        float perpendicularVel = g * m / r;

        VelocityX = perpendicularVel * (float) Math.Sin(angle);
        VelocityY = perpendicularVel * (float) Math.Cos(angle);
        
        Color = Color.Red;
        Mass = 0.05f;
        Size = 10f;
    }
}

public class Universe
{
    public List<Body> Bodies { get; private set; }
        = new List<Body>();
    
    public void Add(Body body)
        => Bodies.Add(body);

    public void Update(float dt)
    {
        foreach (var x in Bodies)
        {
            foreach (var y in Bodies)
            {
                if (x == y)
                    continue;
                
                x.ApplyForce(y, dt);
            }
        }

        foreach (var x in Bodies)
            x.Update(dt);
    }
}