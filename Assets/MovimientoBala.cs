using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoBala : MonoBehaviour
{
    public float velocidad = 15;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);//se mueve hacia adelante 
        Destroy(gameObject, 5);//si no coliciona con nada en 5 segundos se destruye
    }
}
