using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Nihhus.Components;
using Zen;
using Zen.Components;

namespace Nihhus
{
    public class Level1 : Machine
    {
        protected override void FireUp()
        {
            LoadBackground();
            //LoadMarineSnow();
            //LoadJellyfish();
            //LoadSquid();
            //LoadPlayer();
        }

        void LoadBackground()
        {
            var texture = Content.Load<Texture2D>("Levels/Jellyfish/background");
            var sprite = new Sprite(texture, new Rectangle(200, 200, 500, 500), Vector2.Zero);
            var entity = new Entity("static-background");

            entity.AddComponent(new SpriteRenderer(sprite));

            AddEntity(entity);
        }

        void LoadPlayer()
        {
            var player = new Entity("player");

            var atlas = Zen.SpriteAtlasLoader.ParseSpriteAtlas("Content/Character/atlas/character.atlas", true);
            var animator = new SpriteAnimator(atlas);
            animator.Play("idle");
            player.AddComponent(animator);

            player.AddComponent<Mover>();

            player.AddComponent<Player>();

            player.AddComponent<Collider>();

            player.Transform.Position = Screen.Center;

            AddEntity(player);
        }

        void LoadJellyfish()
        {
            Entity entity = new Entity("jellyfish");
            SpriteAtlas atlas = Zen.SpriteAtlasLoader.ParseSpriteAtlas("Content/Mobs/Jellyfish/atlas/jellyfish.atlas", true);
            entity.AddComponent(new SpriteAnimator(atlas));

            entity.AddComponent<Mover>();

            entity.AddComponent<Jellyfish>();
    
            AddEntity(entity);
        }

        void LoadSquid()
        {
            Entity entity = new Entity("squid");
            SpriteAtlas atlas = SpriteAtlasLoader.ParseSpriteAtlas("Content/Mobs/Squid/atlas/squid.atlas", true);
            entity.AddComponent(new SpriteAnimator(atlas));

            entity.AddComponent<Mover>();
            
            Texture2D texture = Content.Load<Texture2D>("Mobs/Squid/ring");
            Sprite ring = new Sprite(texture);
            texture = Content.Load<Texture2D>("Mobs/Squid/glow");
            Sprite glow = new Sprite(texture);
            texture = Content.Load<Texture2D>("Mobs/Squid/beam");
            Sprite beam = new Sprite(texture);
            Squid squid = new Squid(ring, glow, beam);
            entity.AddComponent(squid);
            
            AddEntity(entity);
        }

        void LoadMarineSnow()
        {
            var texture = Content.Load<Texture2D>("Levels/Jellyfish/marine-snow");
            var sprite = new Sprite(texture);

            // Front Background
            var front = new Entity("marine-snow-front");

            var background = new ScrollingDownBackground(
                sprite,
                velocity: 30,
                opacity: 0.8f
            );
            front.AddComponent(background);

            AddEntity(front);

            // Middle Background
            var middle = new Entity("marine-snow-middle");

            var background2 = new ScrollingDownBackground(
                sprite,
                velocity: 15f,
                textureScrollX: (int)(texture.Width * 1 / 3f),
                textureScrollY: (int)(texture.Height * 1 / 3f),
                opacity: 0.7f
            );
            middle.AddComponent(background2);

            AddEntity(middle);

            // Back Background
            var back = new Entity("marine-snow-back");

            var background3 = new ScrollingDownBackground(
                sprite,
                velocity: 7.5f,
                textureScrollX: (int)(texture.Width * 2 / 3f),
                textureScrollY: (int)(texture.Height * 2 / 3f),
                opacity: 0.6f
            );
            back.AddComponent(background3);
            
            AddEntity(back);
        }
    }
}