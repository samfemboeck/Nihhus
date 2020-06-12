using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Zen;
using Zen.Components;

public class Squid : Component, IUpdatable, ITransformObserver
{
    public const float Speed = 50;
    public Mover Mover;
    public SpriteAnimator SpriteAnimator;
    public Transform Transform;

    StateMachine<Squid> _state;
    SquidIdleState _idleState = new SquidIdleState();
    SquidTeleState _teleState = new SquidTeleState();
    SquidLaserState _laserState = new SquidLaserState();

    Vector2 _localHeadPosition = new Vector2(145, 75);
    Vector2 _globalHeadPosition;

    public Squid()
    {
        // cache textures
        ContentLoader.Load<Texture2D>("Mobs/Squid/circle");
        ContentLoader.Load<Texture2D>("Mobs/Squid/beam");
        ContentLoader.Load<Texture2D>("Mobs/Squid/glow");
    }

    public override void Awake()
    {
        Transform = GetComponent<Transform>();
        Mover = GetComponent<Mover>();
        SpriteAnimator = GetComponent<SpriteAnimator>();

        _state = new StateMachine<Squid>(this, _teleState);
        _state.AddState(_idleState);
        _state.AddState(_laserState);
    }
    
    public Entity CreateCircle()
    {
        Entity entity = new Entity("circle");
        entity.AddComponent(new Circle(_globalHeadPosition));
        return entity;
    }

    public Color GetColor()
    {
        return Color.Red;
    }

    public void Update()
    {
        _state.Update(Time.DeltaTime);
    }

    public void TransformChanged(Transform transform)
    {
    }

    public void TransformInitialized(Transform transform) 
    {
        _globalHeadPosition = Vector2.Transform(_localHeadPosition, transform.TransformMatrix);
    }
}