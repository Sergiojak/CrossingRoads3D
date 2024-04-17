using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsEndgame : MonoBehaviour
{
    //Reiniciar juego, bot�n iniciar juego men� principal
    public void RestartGameButton()
    {
        SceneManager.LoadScene(0);
    }
    //Salir y cerrar juego
    public void ExitGameButton()
    {
        Application.Quit();
    }
}
