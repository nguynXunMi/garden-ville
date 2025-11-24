using UnityEngine;
using UnityEngine.SceneManagement;

namespace Demo
{
    public class Menu : MonoBehaviour
    {
        public void OnClickPlay()
        {
            SceneManager.LoadSceneAsync(1);
        }

        public void OnClickQuit()
        {
            Application.Quit();
        }
    }
}