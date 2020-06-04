using Microsoft.Xna.Framework;
using Zen;
using Zen.Components;

public class Squid : Component, IUpdatable
{
    public const float Speed = 50;
    public Sprite Ring;
    public Sprite Glow;
    public Sprite Beam;
    public Mover Mover;
    public SpriteAnimator Animator;
    StateMachine<Squid> _state;
    SquidIdleState _idleState = new SquidIdleState();
    SquidTeleState _teleState = new SquidTeleState();
    SquidLaserState _laserState = new SquidLaserState();


    public Squid(Sprite ring, Sprite glow, Sprite beam)
    {
        Ring = ring;
        Glow = glow;
        Beam = beam;
    }

    public override void Mount()
    {
        Animator = GetComponent<SpriteAnimator>();

        float textureHeight = Animator.CurrentAnimation.Sprites[0].SourceRect.Height;
        Transform.Position = new Vector2(Screen.Width * 0.5f, Screen.Height - 0.5f * textureHeight);

        Mover = GetComponent<Mover>();
        Mover.Velocity = new Vector2(-1 * Speed, 0);

        _state = new StateMachine<Squid>(this, _idleState);
        _state.AddState(_teleState);
        _state.AddState(_laserState);
    }

    public Color GetColor()
    {
        return Color.Red;
    }

    public void Update()
    {
        _state.Update(Time.DeltaTime);
    }
}