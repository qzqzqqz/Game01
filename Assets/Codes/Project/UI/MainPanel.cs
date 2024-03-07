using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace PlatformShoot
{
    public class MainPanel : MonoBehaviour,IController
    {
        private Text mScoreText;

        private int _score = 1;
        
        // Start is called before the first frame update
        void Start()
        {
            mScoreText = transform.Find("ScoreText").GetComponent<Text>();
            this.GetModel<IGameModel>().Score.RegisterWithInitValue(OnScoreChanged)
                .UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void OnScoreChanged(int score)
        {
            mScoreText.text = score.ToString();
        }

        public void UpdateScoreText()
        {
            int score = int.Parse(mScoreText.text);
            mScoreText.text = (score + _score).ToString();
        }

        IArchitecture IBelongToArchitecture.GetArchitecture()
        {
            return PlatformShootGame.Interface;
        }
    }
}
