using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torta : MonoBehaviour
{
    string orientacion;
    public float velocidad;
    bool flagAnimacion = true;

    void Update()
    {
        if (orientacion.Equals("Front"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - velocidad * Time.deltaTime);
            if (flagAnimacion)
            {
                flagAnimacion = false;
                GetComponent<Animator>().SetTrigger("TortaFront");
            }
        }
        else if (orientacion.Equals("Back"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + velocidad * Time.deltaTime);
            if (flagAnimacion)
            {
                flagAnimacion = false;
                GetComponent<Animator>().SetTrigger("TortaBack");
            }
        }
        else if (orientacion.Equals("SideDerecha"))
        {
            transform.position = new Vector3(transform.position.x + velocidad * Time.deltaTime, transform.position.y);
            if (flagAnimacion)
            {
                flagAnimacion = false;
                GetComponent<Animator>().SetTrigger("TortaSide");
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else if (orientacion.Equals("SideIzquierda"))
        {
            transform.position = new Vector3(transform.position.x - velocidad * Time.deltaTime, transform.position.y);
            if (flagAnimacion)
            {
                flagAnimacion = false;
                GetComponent<Animator>().SetTrigger("TortaSide");
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }

        if (transform.position.x < -120 || transform.position.x > 120 || transform.position.y < -110 || transform.position.y > 110)
        {
            Destroy(gameObject);
        }
    }
    
    public void SetOrientacion(string orientacion)
    {
        this.orientacion = orientacion;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Pared"))
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Payaso"))
        {
            col.GetComponent<Payaso>().AturdirPayaso();
            Destroy(gameObject);
        }
    }
}
