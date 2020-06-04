using Microsoft.Xna.Framework;
using Zen;
using Zen.Components;

public class ScrollingDownBackground : Component, IUpdatable
{
    private readonly float _velocity;  
    private readonly int _textureScrollX;
    private readonly int _textureScrollY;
    private readonly Sprite _sprite;
    private readonly float _opacity;

    public ScrollingDownBackground(Sprite sprite, float velocity = 25, float opacity = 1, int textureScrollX = 0, int textureScrollY = 0)
    {
        _sprite = sprite;
        _textureScrollX = textureScrollX;
        _textureScrollY = textureScrollY;
        _opacity = opacity;
        _velocity = velocity;
    }

    public override void Mount()
    {
        var rendererTop = new TiledSpriteRenderer(_sprite, new Rectangle(_textureScrollX, _textureScrollY, Screen.Width, Screen.Height), new Vector2(0, -Screen.Height));
        rendererTop.Origin = Screen.Center;
        rendererTop.Color = Color.White * _opacity;
        Entity.AddComponent(rendererTop);

        var rendererBottom = new TiledSpriteRenderer(_sprite, new Rectangle(_textureScrollX, _textureScrollY, Screen.Width, Screen.Height), Vector2.Zero);
        rendererBottom.Origin = Screen.Center;
        rendererBottom.Color = Color.White * _opacity;
        Entity.AddComponent(rendererBottom);

        Transform.Position = Screen.Center;
    }

    public void Update()
    {
        var pos = Transform.Position;
        pos.Y += _velocity * Time.DeltaTime;
        
        if (pos.Y > 1.5f * Screen.Height)
            pos.Y = 0.5f * Screen.Height;

        Transform.Position = pos;
    }
}