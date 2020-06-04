using Microsoft.Xna.Framework;
using Zen;

public class SquidTeleState : State<Squid>
{
    bool _actionStarted;
    float centerTolerance = 1;

    public override void Begin()
    {
        _actionStarted = false;
        _context.Mover.Velocity *= -1;
    }
    
    public override void Update(float deltaTime)
    {
        if (!_actionStarted)
		{
			if (_context.Transform.Position.X >= (Screen.Width * 0.5f) - centerTolerance && _context.Transform.Position.X <= (Screen.Width * 0.5f) + centerTolerance)
				StartAction();
		}
    }

    void StartAction()
    {
        _actionStarted = true;
        _context.Animator.Color = Color.HotPink;
        _context.Transform.Position = new Vector2(Screen.Width * 0.5f, _context.Transform.Position.Y);
        _context.Animator.Play("telekinesis");
        _context.Animator.OnAnimationFinish += ChangeState;
        _context.Mover.Velocity = Vector2.Zero;
    }

    void ChangeState()
    {
        _machine.ChangeState<SquidIdleState>();
    }

    public override void End()
    {
        _context.Animator.OnAnimationFinish -= ChangeState;
    }
}