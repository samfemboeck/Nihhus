using Microsoft.Xna.Framework;
using Zen;
using Zen.Util;
using Zen.Components;

class Jellyfish : Component
{
    Mover _mover;
    SpriteAnimator _animator;
    const float Speed = 200;
    const float MaxMoveInterval = 5;
    Timer _timer;

    public override void Mount()
    {
        _mover = GetComponent<Mover>();
        _animator = GetComponent<SpriteAnimator>();
        _timer = new Timer(Random.NextFloat() * MaxMoveInterval, false, Move);
    }

    private void Move()
    {
        _timer = new Timer(Random.NextFloat() * MaxMoveInterval, false, Move);
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