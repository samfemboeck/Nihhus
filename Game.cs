using Zen;

namespace Nihhus
{
    public class Game : Core
    {
        public override void Start()
        {
            Screen.SetSize(Screen.MonitorWidth, Screen.MonitorHeight);
            Screen.IsFullscreen = false;
            Screen.ApplyChanges();

            Machine = new Level1();
        }
    }
}
