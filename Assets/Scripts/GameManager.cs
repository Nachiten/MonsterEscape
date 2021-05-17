using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject textoPerdio;

    private void Awake()
    {
        textoPerdio = GameObject.Find("TextoPerdio");
    }

    private void Start()
    {
        textoPerdio.SetActive(false);
    }

    public void perdioJuego() 
    {
        textoPerdio.SetActive(true);

        GameObject.Find("EnemigoRestringido").GetComponent<MovimientoEnemigoRestringido>().perdioJuego();
    }
}
