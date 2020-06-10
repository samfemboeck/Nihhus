using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nihhus.Components;
using Zen;

internal class PlayerIdleState : State<Player>
{
    public override void Begin()
    {
        _context.Animator.Play("idle");
        _context.Mover.Velocity = Vector2.Zero;
        _context.Transform.Rotation = 0;
    }

    public override void Reason()
    {
        KeyboardState kb = Keyboard.GetState();
        
        if (kb.IsKeyDown((Keys)Player.Controls.Catch))
            _machine.ChangeState<PlayerCatchState>();

        if (kb.IsKeyDown((Keys)Player.Controls.Up) ||
            kb.IsKeyDown((Keys)Player.Controls.Down) ||
            kb.IsKeyDown((Keys)Player.Controls.Left) ||
            kb.IsKeyDown((Keys)Player.Controls.Right))
        {
            _machine.ChangeState<PlayerSwimState>();
        }
    }

    public override void Update(float deltaTime)
    {
    }
}