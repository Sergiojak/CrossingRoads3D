using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsEndgame : MonoBehaviour
{
    //Reiniciar juego, botón iniciar juego menú principal
    public void RestartGameButton()
    {
        SceneManager.LoadScene(1);
    }
    //Salir y cerrar juego
    public void ExitGameButton()
    {
        Application.Quit();
    }
}
