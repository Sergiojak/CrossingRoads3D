using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsEndgame : MonoBehaviour
{
    public void RestartGameButton()
    {
        SceneManager.LoadScene(0);
    }
    public void ExitGameButton()
    {
        Application.Quit();
    }
}
