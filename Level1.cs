using System.Collections.Generic;
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
    public class Level1
    {
        public Level1()
        {
            List<Entity> entities = new List<Entity>();

            entities.Add(LoadBackground());
            LoadMarineSnow(entities);
            entities.Add(LoadJellyfish());
            entities.Add(LoadSquid());
            entities.Add(LoadPlayer());

            foreach (Entity entity in entities)
                EntityManager.AddEntity(entity);
        }

        Entity LoadBackground()
        {
            var entity = new Entity("static-background");

            entity.AddComponent<Transform>();

            var texture = ContentLoader.Load<Texture2D>("Levels/Jellyfish/background");
            var sprite = new Sprite(texture);
            SpriteRenderer spriteRenderer = new SpriteRenderer(sprite, Screen.Width, Screen.Height);
            entity.AddComponent(spriteRenderer);

            return entity;
        }

        Entity LoadPlayer()
        {
            Entity player = new Entity("player");

            Transform transform = new Transform();
            transform.Position = Screen.Center;
            player.AddComponent(transform);

            SpriteAtlas atlas = SpriteAtlasLoader.ParseSpriteAtlas("Content/Character/atlas/character.atlas", true);
            SpriteAnimator animator = new SpriteAnimator(atlas);
            animator.Play("idle");
            player.AddComponent(animator);

            transform.Origin = animator.TargetRectangle.Size * 0.5f;

            player.AddComponent<Mover>();

            PolygonCollider polygonCollider = new PolygonCollider();
            polygonCollider.CollisionLayer = CollisionLayer.Player;
            player.AddComponent<PolygonCollider>();

            player.AddComponent<Player>();

            return player;
        }

        Entity LoadJellyfish()
        {
            Entity entity = new Entity("jellyfish");

            Transform transform = entity.AddComponent<Transform>();
            transform.Position = Screen.Center;

            SpriteAtlas atlas = SpriteAtlasLoader.ParseSpriteAtlas("Content/Mobs/Jellyfish/atlas/jellyfish.atlas", true);
            SpriteAnimator animator = new SpriteAnimator(atlas);
            entity.AddComponent(animator);

            transform.Origin = new Vector2(animator.TargetRectangle.Width * 0.5f, animator.TargetRectangle.Height * 0.5f);

            entity.AddComponent<Mover>();

            CircleCollider circleCollider = new CircleCollider();
            circleCollider.CollisionLayer = CollisionLayer.Jellyfish;
            entity.AddComponent(circleCollider);

            entity.AddComponent<Jellyfish>();

            return entity;
        }

        Entity LoadSquid()
        {
            Entity entity = new Entity("squid");

            SpriteAtlas spriteAtlas = SpriteAtlasLoader.ParseSpriteAtlas("Content/Mobs/Squid/atlas/squid.atlas", true);
            SpriteAnimator spriteAnimator = new SpriteAnimator(spriteAtlas);
            entity.AddComponent(spriteAnimator);

            Transform transform = entity.AddComponent<Transform>();
            transform.Origin = spriteAnimator.TargetRectangle.Size * 0.5f;
            transform.Position = new Vector2(Screen.Width * 0.5f, Screen.Height - 0.5f * spriteAnimator.TargetRectangle.Height);

            entity.AddComponent<Mover>();

            entity.AddComponent<Squid>();

            return entity;
        }

        void LoadMarineSnow(List<Entity> entities)
        {
            Texture2D texture = ContentLoader.Load<Texture2D>("Levels/Jellyfish/marine-snow");

            Entity front = new Entity("marine-snow-front");
            front.AddComponent(new MarineSnow(texture, new RectangleF(0, 0, Screen.Width, Screen.Height), new Vector2(0, 50)));
            entities.Add(front);

            Entity middle = new Entity("marine-snow-middle");
            middle.AddComponent(new MarineSnow(texture, new RectangleF(texture.Width * 1 / 3f, texture.Height * 1 / 3f, Screen.Width, Screen.Height), new Vector2(0, 25)));
            entities.Add(middle);

            Entity back = new Entity("marine-snow-back");
            back.AddComponent(new MarineSnow(texture, new RectangleF(texture.Width * 2 / 3f, texture.Height * 2 / 3f, Screen.Width, Screen.Height), new Vector2(0, 12.5f)));
            entities.Add(back);
        }
    }
}