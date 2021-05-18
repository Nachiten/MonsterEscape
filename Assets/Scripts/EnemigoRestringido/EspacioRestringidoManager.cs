using UnityEngine;

public class EspacioRestringidoManager : MonoBehaviour
{
    GameObject jugador;
    SpriteRestringidoManager spriteRestringidoManager;

    private void Awake()
    {
        jugador = GameObject.Find("Jugador");

        foreach (Transform hijoDeMiPadre in transform.parent.transform)
        {
            if (hijoDeMiPadre.gameObject.name == "Sprite")
            {
                spriteRestringidoManager = hijoDeMiPadre.gameObject.GetComponent<SpriteRestringidoManager>();
                break;
            }
        }

        if (spriteRestringidoManager == null)
        {
            Debug.LogError("No encuentro el espacio.");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Jugador")) 
        {
            Debug.Log("Jugador entro a espacio enemigo");
            spriteRestringidoManager.seguirA(jugador);
        }
    }
}
