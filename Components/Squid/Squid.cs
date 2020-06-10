using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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
    public Transform Transform;
    StateMachine<Squid> _state;
    SquidIdleState _idleState = new SquidIdleState();
    SquidTeleState _teleState = new SquidTeleState();
    SquidLaserState _laserState = new SquidLaserState();
    ContentManager _content;

    public Squid(ContentManager content)
    {
        _content = content;
    }

    public override void LoadComponents()
    {
        Transform = AddComponent<Transform>();

        SpriteAtlas atlas = SpriteAtlasLoader.ParseSpriteAtlas("Content/Mobs/Squid/atlas/squid.atlas", true);
        Animator = new SpriteAnimator(atlas);
        AddComponent(Animator);

        Mover = AddComponent<Mover>();

        Texture2D texture = _content.Load<Texture2D>("Mobs/Squid/circle");
        Ring = new Sprite(texture);
        texture = _content.Load<Texture2D>("Mobs/Squid/glow");
        Glow = new Sprite(texture);
        texture = _content.Load<Texture2D>("Mobs/Squid/beam");
        Beam = new Sprite(texture);
    }

    public override void Mount()
    {
        float height = Animator.CurrentAnimation.Sprites[0].UvRect.Height;
        Transform.Position = new Vector2(Screen.Width * 0.5f, Screen.Height - 0.5f * height);

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