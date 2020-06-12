using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zen;
using Zen.Components;
using Zen.Util;

public class MarineSnow : Component, ITransformObserver
{
    Texture2D _texture;
    RectangleF _uvRectangle;
    Vector2 _velocity;

    public MarineSnow(Texture2D texture, RectangleF uvRectangle, Vector2 velocity)
    {
        _texture = texture;
        _uvRectangle = uvRectangle;
        _velocity = velocity;
    }

    public override void AddComponents()
    {
        AddComponent<Transform>();

        AddComponent<Mover>().Velocity = _velocity;

        Sprite sprite = new Sprite(_texture, _uvRectangle);
        SpriteRenderer rendererTop = new SpriteRenderer(sprite, Screen.Width, Screen.Height);
        rendererTop.Material = Material.LinearWrap;
        rendererTop.PositionOffset = new Vector2(0, -Screen.Height);
        AddComponent(rendererTop);

        SpriteRenderer rendererBottom = new SpriteRenderer(sprite, Screen.Width, Screen.Height);
        rendererBottom.Material = Material.LinearWrap;
        AddComponent(rendererBottom);
    }

    public void TransformChanged(Transform transform)
    {
        if (transform.Position.Y >= Screen.Height)
            transform.Position = Vector2.Zero;
    }

    public void TransformInitialized(Transform transform) {}
}