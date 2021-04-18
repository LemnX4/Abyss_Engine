﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

namespace Abyss_Call
{
    public class Renderer : System
    {
        public GraphicsDeviceManager DeviceManager;
        public SpriteBatch SpriteBatch;
        public override bool Requirements(Entity e) => e.HasComponent<Transform>() && e.HasComponent<Drawable>();

        public Renderer(Game game)
        {
            DeviceManager = new GraphicsDeviceManager(game);
        }

        public void CreateSpriteBatch()
        {
            SpriteBatch = new SpriteBatch(DeviceManager.GraphicsDevice);
        }

        public void Draw(double deltaTime)
        {
            SpriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, null);

            foreach (Entity e in EntityBucket)
                if (e.IsRenderable)
                    DrawEntity(e);

            SpriteBatch.End();
        }

        public void DrawEntity(Entity entity)
        {
            Transform t = entity.GetComponent<Transform>();
            Drawable d = entity.GetComponent<Drawable>();

            var texture = Game.TextureManager.GetTexture(d.TextureTag);
            Point TexturePosition = d.TexturePosition;

            if (texture == null)
                return;

            TexturePosition.Y += d.Direction * d.TextureSize.Y;

            SpriteBatch.Draw(texture,
                new Rectangle((int)t.Position.X - d.Offset.X * Game.SPS, (int)t.Position.Y - d.Offset.Y * Game.SPS, 
                (int)(d.TextureSize.X * Game.SPS * t.Scale), (int)(d.TextureSize.Y * Game.SPS * t.Scale)),
                new Rectangle(TexturePosition, d.TextureSize), d.Color * d.Alpha, t.Rotation, new Vector2(0, 0),
                SpriteEffects.None, d.LayerDepth);
        }
    }
}
