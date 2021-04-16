﻿using System.Diagnostics;

namespace Abyss_Call
{
    public class SceneSwitcher : System
    {
        public override bool Requirements(Entity e) => e.HasComponent<Mouseover>() && e.HasComponent<SceneSwitch>();

        protected override void UpdateEntity(Entity entity, double deltaTime)
        {
            if (entity.GetComponent<Mouseover>().Hovered && Game.MouseManager.IsButtonPressed(MouseButton.Left))
            {
                Game.ScenesManager.SwitchToScene(entity.GetComponent<SceneSwitch>().NextScene);
                entity.GetComponent<Mouseover>().Hovered = false;
                Game.AudioManager.PlayEffect("mouseover", 1);
            }
        }
    }
}