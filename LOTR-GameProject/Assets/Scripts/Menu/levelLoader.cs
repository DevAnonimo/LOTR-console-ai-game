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
            if (SceneManager.GetActiveScene().buildIndex == 3 || SceneManager.GetActiveScene().buildIndex == 4)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                SceneManager.LoadScene(0);
            }
            StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        }

        public void Dead()
        {
            StartCoroutine(LoadLevel(0));
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
