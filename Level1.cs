using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Nihhus.Components;
using Zen;

namespace Nihhus
{
    public class Level1 : Machine
    {
        protected override void FireUp()
        {
            LoadBackground();
            //LoadMarineSnow();
            LoadPlayer();
            LoadJellyfish();
        }

        void LoadBackground()
        {
            var texture = Content.Load<Texture2D>("Levels/Jellyfish/background");
            var sprite = new Sprite(texture, new Rectangle(0, 0, Screen.Width + 2, Screen.Height + 2), Vector2.Zero);
            var entity = new Entity("static-background");
            entity.AddComponent(new Zen.Components.SpriteRenderer(sprite));
            AddEntity(entity);
        }

        void LoadPlayer()
        {
            var player = new Entity("player");

            var atlas = Zen.SpriteAtlasLoader.ParseSpriteAtlas("Content/Character/atlas/character.atlas", true);
            var animator = new Zen.Components.SpriteAnimator(atlas);
            animator.Play("idle");
            player.AddComponent(animator);

            player.AddComponent<Mover>();
            player.AddComponent<Player>();

            player.Transform.Position = Screen.Center;

            AddEntity(player);
        }

        void LoadJellyfish()
        {
            Entity entity = new Entity("jellyfish");
            SpriteAtlas atlas = Zen.SpriteAtlasLoader.ParseSpriteAtlas("Content/Mobs/Jellyfish/atlas/jellyfish.atlas", true);
            entity.AddComponent(new Zen.Components.SpriteAnimator(atlas));
            entity.AddComponent<Mover>();
            entity.AddComponent<Jellyfish>();
            
            AddEntity(entity);
        }

        /*void LoadMarineSnow()
        {
            var texture = Content.Load<Texture2D>("Levels/Jellyfish/marine-snow");
            var sprite = new Sprite(texture);

            // Front Background
            var front = new Entity("marine-snow-front");
            var background = new ScrollingDownBackground(
                sprite,
                velocity: 25,
                opacity: 1
            );
            front.AddComponent(background);
            AddEntity(front);

            // Middle Background
            var middle = new Entity("marine-snow-middle");
            var background2 = new ScrollingDownBackground(
                sprite,
                velocity: 18f,
                textureScrollX: (int)(texture.Width * 1 / 3f),
                textureScrollY: (int)(texture.Height * 1 / 3f),
                opacity: 0.8f
            );
            middle.AddComponent(background2);
            AddEntity(middle);

            // Back Background
            var back = new Entity("marine-snow-back");
            var background3 = new ScrollingDownBackground(
                sprite,
                velocity: 12.5f,
                textureScrollX: (int)(texture.Width * 2 / 3f),
                textureScrollY: (int)(texture.Height * 2 / 3f),
                opacity: 0.5f
            );
            back.AddComponent(background3);
            AddEntity(back);
        }
        */
    }
}