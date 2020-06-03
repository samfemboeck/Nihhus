using Nihhus.Components;
using Zen;
using Zen.Util;

internal class PlayerCatchState : State<Player>
{
    private const float CatchSpeed = 400;

    public override void Begin()
    {
        _context.Mover.Velocity = _context.FacingDirection * CatchSpeed;
        _context.Animator.Play("catch");
        _context.Transform.LookAt(_context.Transform.Position + _context.FacingDirection);
        var timer = new Timer(0.4f, false, () => _machine.ChangeState<PlayerIdleState>());
        //_context.VesselCatchCollider.Enabled = true;
        _context.ControlsEnabled = false;
    }

    public override void Update(float deltaTime)
    {
        /*
        if (_context.Animator.FlipX)
            _context.VesselCatchCollider.LocalOffset = new Vector2(-_context.TextureBounds.Width / 2f + 22 + _context.VesselColliderBounds.X / 2f, -_context.TextureBounds.Height / 2f + 6);
        else
            _context.VesselCatchCollider.LocalOffset = new Vector2(-_context.TextureBounds.Width / 2f + 38 + _context.VesselColliderBounds.X / 2f, -_context.TextureBounds.Height / 2f + 6);
        */
    }

    public override void End() 
    {
        //_context.VesselCatchCollider.Enabled = false;
        _context.ControlsEnabled = true;
    }
}