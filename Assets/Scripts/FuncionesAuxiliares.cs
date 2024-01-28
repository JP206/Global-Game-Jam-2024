using UnityEngine.SceneManagement;
using UnityEngine;

public class FuncionesAuxiliares : MonoBehaviour
{
    public GameObject imagenHowToPlay;
    public void CambiarEscena(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }

    public void SalirDelJuego()
    {
        Application.Quit();
    }

    public void BotonHowToPlay()
    {
        imagenHowToPlay.SetActive(true);
    }

    public void CerrarHowToPlay()
    {
        imagenHowToPlay.SetActive(false);
    }
}
