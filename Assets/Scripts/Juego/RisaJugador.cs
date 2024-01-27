using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisaJugador : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            risaJugador();
        }
    }

    void risaJugador()
    {
        GameObject.Find("Payaso(Clone)").GetComponent<Payaso>().AhuyentarPayaso();
    }
}
