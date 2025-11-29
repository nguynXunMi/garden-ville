using UnityEngine.SceneManagement;
using UnityEngine;

namespace Main
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