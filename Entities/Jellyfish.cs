using Microsoft.Xna.Framework;
using Zen;
using Zen.Util;
using Zen.Components;

class Jellyfish : Zen.Components.Component
{
    private Mover _mover;
    private SpriteAnimator _animator;
    private const float Speed = 200;
    private const float MaxMoveInterval = 5;
    private Timer _timer;

    public override void Register(Zen.Entity entity)
    {
        base.Register(entity);

        _mover = GetComponent<Mover>();
        _animator = GetComponent<SpriteAnimator>();
        _timer = new Timer(Random.NextFloat() * MaxMoveInterval, true, Move);
    }

    private void Move()
    {
        _animator.Play("swim", SpriteAnimator.LoopMode.FreezeAtLastFrame);
        Vector2 randomDirection = Random.Range(new Vector2(-1, -1), new Vector2(1, 1));
        randomDirection.Normalize();
        Transform.LookAt(Transform.Position + randomDirection);
        _mover.Velocity = randomDirection * Speed;
    }

    public override void Destroy()
    {
        _timer.Destroy();
    }
}