using UnityEngine.SceneManagement;
using UnityEngine;

public class FuncionesAuxiliares : MonoBehaviour
{
    public void CambiarEscena(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }

    public void SalirDelJuego()
    {
        Application.Quit();
    }
}
