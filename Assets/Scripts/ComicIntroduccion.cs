using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ComicIntroduccion : MonoBehaviour
{
    public Sprite imagen2, imagen3, imagen4;
    Image image;
    int contador = 1;
    bool cambiar = false;

    void Start()
    {
        image = transform.GetChild(0).GetComponent<Image>();
        StartCoroutine(CambiarImagen());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            cambiar = true;
        }
    }

    IEnumerator CambiarImagen()
    {
        while (true)
        {
            yield return new WaitUntil(() => cambiar);
            cambiar = false;
            contador++;
            if (contador == 2)
            {
                image.sprite = imagen2;
            }
            if (contador == 3)
            {
                image.sprite = imagen3;
            }
            if (contador == 4)
            {
                image.sprite = imagen4;
            }
            if (contador == 5)
            {
                SceneManager.LoadScene("Nivel 1");
            }            
        }
    }
}
