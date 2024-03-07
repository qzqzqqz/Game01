using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QFramework;

namespace PlatformShoot
{
    public class PlatformShootGame : Architecture<PlatformShootGame>
    {
        protected override void Init()
        {
            RegisterModel<IGameModel>(new GameModel());
            RegisterSystem<ICameraSystem>(new CameraSystem());
        }
    }
}