using QFramework;
using UnityEngine.SceneManagement;

namespace PlatformShoot
{
    public class NextLevelCommand : AbstractCommand
    {
        private readonly string _mSceneName;

        public NextLevelCommand(string name)
        {
            _mSceneName = name;
        }
        protected override void OnExecute()
        {
            SceneManager.LoadScene(_mSceneName);
        }
    }
}