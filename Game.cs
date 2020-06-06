using Zen;

namespace Nihhus
{
    public class Game : Core
    {
        public override void Start()
        {
            Machine = new Level1();
        }
    }
}
