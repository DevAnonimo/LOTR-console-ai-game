using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Menu
{
    public class levelLoader : MonoBehaviour
    {
        public Animator transition;

        public float transitionTime = 1f;


        public void LoadNextLevel()
        {
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }

        private IEnumerator LoadLevel(int levelIndex)
        {
            //Play animation
            transition.SetTrigger("Start");

            //Wait
            yield return new WaitForSeconds(transitionTime);

            //Load scene
            SceneManager.LoadScene(levelIndex);
        }
    }
}
