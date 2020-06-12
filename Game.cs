using Microsoft.Xna.Framework;
using Zen;

namespace Nihhus
{
    public class Game : Core
    {
        Level1 _level1;

        protected override void Initialize()
        {
            Screen.SetSize(Screen.MonitorWidth, Screen.MonitorHeight);
            Screen.IsFullscreen = false;
            Screen.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _level1 = new Level1();
        }
    }
}
