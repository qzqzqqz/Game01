using UnityEngine;

namespace PlatformShoot
{
    public class Bullet : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            GameObject.Destroy(this.gameObject, 3f);
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(12 * Time.deltaTime, 0, 0);
        }
    }
}