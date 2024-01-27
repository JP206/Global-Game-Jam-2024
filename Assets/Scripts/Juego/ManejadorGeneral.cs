using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejadorGeneral : MonoBehaviour
{
    public GameObject canvasJuegoTerminado; 
        
    public void TerminarJuego()
    {
        canvasJuegoTerminado.SetActive(true);
        GameObject.Find("Player").GetComponent<MovimientoJugador>().puedeMover = false;
        GameObject.Find("Clown(Clone)").GetComponent<Payaso>().puedeMover = false;
    }
}
