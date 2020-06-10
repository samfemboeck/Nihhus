using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Nihhus.Components;
using Zen;
using Zen.Components;
using Zen.Util;

namespace Nihhus
{
    public class Level1 : Machine
    {
        protected override void FireUp()
        {
            LoadBackground();
            LoadMarineSnow();
            LoadJellyfish();
            LoadSquid();
            LoadPlayer();
        }

        void LoadBackground()
        {
            var entity = new Entity("static-background");
            var texture = Content.Load<Texture2D>("Levels/Jellyfish/background");
            var sprite = new Sprite(texture, new RectangleF(0, 0, texture.Width, texture.Height), Vector2.Zero, Screen.Width, Screen.Height);
            

            entity.AddComponent<Transform>();
            entity.AddComponent(new SpriteRenderer(sprite));

            AddEntity(entity);
        }

        void LoadPlayer()
        {
            var player = new Entity("player");

            Transform transform = new Transform();
            transform.Position = Screen.Center;
            player.AddComponent(transform);

            var atlas = Zen.SpriteAtlasLoader.ParseSpriteAtlas("Content/Character/atlas/character.atlas", true);
            var animator = new SpriteAnimator(atlas);
            animator.Play("idle");
            player.AddComponent(animator);

            player.AddComponent<Mover>();

            player.AddComponent<Player>();

            player.AddComponent<Vertex4Collider>();

            AddEntity(player);
        }

        void LoadJellyfish()
        {
            Entity entity = new Entity("jellyfish");

            entity.AddComponent<Transform>().Position = Screen.Center;

            SpriteAtlas atlas = Zen.SpriteAtlasLoader.ParseSpriteAtlas("Content/Mobs/Jellyfish/atlas/jellyfish.atlas", true);
            entity.AddComponent(new SpriteAnimator(atlas));

            entity.AddComponent<Mover>();

            entity.AddComponent<Jellyfish>();
    
            AddEntity(entity);
        }

        void LoadSquid()
        {
            Entity entity = AddEntity("squid");
            entity.AddComponent(new Squid(Content));
        }

        void LoadMarineSnow()
        {
            var texture = Content.Load<Texture2D>("Levels/Jellyfish/marine-snow");

            Entity front = AddEntity("marine-snow-front");
            front.AddComponent(new MarineSnow(texture, new RectangleF(0, 0, Screen.Width, Screen.Height), new Vector2(0, 50)));

            Entity middle = AddEntity("marine-snow-middle");
            middle.AddComponent(new MarineSnow(texture, new RectangleF(texture.Width * 1/3f, texture.Height * 1/3f, Screen.Width, Screen.Height), new Vector2(0, 25)));

            Entity back = AddEntity("marine-snow-back");
            back.AddComponent(new MarineSnow(texture, new RectangleF(texture.Width * 2/3f, texture.Height * 2/3f, Screen.Width, Screen.Height), new Vector2(0, 12.5f)));
        }
    }
}