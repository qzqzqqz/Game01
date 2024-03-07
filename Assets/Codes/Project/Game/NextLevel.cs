using UnityEngine;
using QFramework;

namespace PlatformShoot
{
    public class NextLevel : MonoBehaviour, IController
    {
        // Start is called before the first frame update
        void Start()
        {
            GameObject o;
            (o = gameObject).SetActive(false);
            this.RegisterEvent<ShowPassDoorEvent>(OnCanGamePass)
                .UnRegisterWhenGameObjectDestroyed(o);
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return PlatformShootGame.Interface;
        }

        private void OnCanGamePass(ShowPassDoorEvent obj)
        {
            gameObject.SetActive(true);
        }
    }
}
