using Microsoft.Xna.Framework;
using Zen;
using Zen.Components;

public class Mover : Component, IUpdatable
{       
    public Vector2 Velocity = Vector2.Zero;

    public void Update()
    {
        var move = Transform.Position + Velocity * Time.DeltaTime;

        if (move.X < 0 || move.X > Screen.Width || move.Y < 0 || move.Y > Screen.Height)
            return;

        Transform.Position = move;
    }
}