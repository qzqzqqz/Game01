using UnityEngine;
using UnityEngine.UI;

namespace PlatformShoot
{
    public class MainPanel : MonoBehaviour
    {
        private Text mScoreText;

        private int _score = 1;
        
        // Start is called before the first frame update
        void Start()
        {
            mScoreText = transform.Find("ScoreText").GetComponent<Text>();
            
        }

        public void UpdateScoreText()
        {
            int score = int.Parse(mScoreText.text);
            mScoreText.text = (score + _score).ToString();
        }
    }
}
