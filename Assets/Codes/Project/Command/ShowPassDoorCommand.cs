
using QFramework;

namespace PlatformShoot
{
    public class ShowPassDoorCommand: AbstractCommand
    {
        protected override void OnExecute()
        {
            this.SendEvent<ShowPassDoorEvent>();
        }
    }
}