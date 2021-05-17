using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspacioEnemigoManager : MonoBehaviour
{
    GameObject jugador;
    GameObject enemigoRestringido;

    private void Awake()
    {
        jugador = GameObject.Find("Jugador");
        enemigoRestringido = GameObject.Find("EnemigoRestringido");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Jugador")) 
        {
            Debug.Log("Jugador entro a espacio enemigo");
            enemigoRestringido.GetComponent<MovimientoEnemigoRestringido>().seguirA(jugador);
        }
            
    }
}
