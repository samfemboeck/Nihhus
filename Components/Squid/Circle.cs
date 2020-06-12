using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zen;
using Zen.Components;
using Zen.Util;

public class Circle : Component, IUpdatable
{
    Transform _transform;
    float _scaleMultiplier = 1.01f;
    Vector2 _headPosition;

    CircleCollider _outerCircleCollider;
    CircleCollider _innerCircleCollider;
    HashSet<Collider> _tmpOuterCollisions;
    HashSet<Collider> _tmpInnerCollisions;

    public Circle(Vector2 headPosition)
    {
        _headPosition = headPosition;
    }

    public override void AddComponents()
    {
        Texture2D texture = ContentLoader.Load<Texture2D>("Mobs/Squid/circle");
        SpriteRenderer spriteRenderer = new SpriteRenderer(new Sprite(texture));
        spriteRenderer.Color = Color.HotPink;
        AddComponent(spriteRenderer);

        RectangleF targetRectangle = spriteRenderer.TargetRectangle;
        _outerCircleCollider = new CircleCollider(
            new Vector2(targetRectangle.Left, targetRectangle.Top), 
            new Vector2(targetRectangle.Right, targetRectangle.Top),
            new Vector2(targetRectangle.Right, targetRectangle.Bottom),
            new Vector2(targetRectangle.Left, targetRectangle.Bottom)
        );
        AddComponent(_outerCircleCollider);

        float localCircleBorderRadius = 6;
        _innerCircleCollider = new CircleCollider(
            new Vector2(targetRectangle.Left + localCircleBorderRadius, targetRectangle.Top + localCircleBorderRadius), 
            new Vector2(targetRectangle.Right - localCircleBorderRadius, targetRectangle.Top + localCircleBorderRadius),
            new Vector2(targetRectangle.Right - localCircleBorderRadius, targetRectangle.Bottom - localCircleBorderRadius),
            new Vector2(targetRectangle.Left + localCircleBorderRadius, targetRectangle.Bottom - localCircleBorderRadius)
        );
        AddComponent(_innerCircleCollider);

        _transform = AddComponent<Transform>();
        _transform.Position = _headPosition;
        _transform.Origin = spriteRenderer.TargetRectangle.Size * 0.5f;
        _transform.Scale = 0.01f;
    }
    
    public void Update()
    {
        _transform.Scale *= _scaleMultiplier;
        
        if (_transform.Scale >= 2.5f)
            EntityManager.RemoveEntity(Entity);
        
        // TODO
        if (Physics.BroadphaseCast(_outerCircleCollider, out _tmpOuterCollisions, (int)CollisionLayer.Player))
        {
            foreach (Collider collider in _tmpOuterCollisions)
            {
                PolygonCollider playerCollider = (PolygonCollider)collider;
                if (playerCollider.Intersects(_outerCircleCollider) && !playerCollider.Intersects(_innerCircleCollider))
                    EntityManager.RemoveEntity(playerCollider.Entity);
            }
        }
    }
}