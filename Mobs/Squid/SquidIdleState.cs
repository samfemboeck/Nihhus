using Microsoft.Xna.Framework;
using Zen;
using Zen.Util;

public class SquidIdleState : State<Squid>
{
    public override void Begin()
    {
        _context.Animator.Play("idle");
        _context.Mover.Velocity = new Vector2(-1 * Squid.Speed, 0);
        _context.Animator.Color = _context.GetColor();
        
        if (Random.Chance(50))
            new Timer(5, false, () => _machine.ChangeState<SquidTeleState>());
        else
            new Timer(5, false, () => _machine.ChangeState<SquidLaserState>());
    }

    public override void Update(float deltaTime)
    {
    }
}