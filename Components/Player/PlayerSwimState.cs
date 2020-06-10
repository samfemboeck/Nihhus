using Microsoft.Xna.Framework;
using Nihhus.Components;
using Zen;

internal class PlayerSwimState : State<Player>
{
    private const float Speed = 170;

    public override void Begin()
    {
        _context.Animator.Play("swim");
    }

    public override void Reason()
    {
        if (_context.Move == Vector2.Zero)
            _machine.ChangeState<PlayerIdleState>();
    }

    public override void Update(float deltaTime)
    {
        if (_context.Move == Vector2.Zero) return;
        
        _context.Mover.Velocity = _context.Move * Speed;
        _context.Transform.FlipX = _context.FacingDirection.X == -1;
        _context.Transform.LookAt(_context.Transform.Position + _context.Move);
    }
}