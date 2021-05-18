using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool pausa = false;

    GameObject textoPerdio;

    IEnumerable<IPersonajeMapa> listaInterfaces;

    private void Awake()
    {
        textoPerdio = GameObject.Find("TextoPerdio");
        listaInterfaces = FindObjectsOfType<MonoBehaviour>().OfType<IPersonajeMapa>();
    }

    private void Start()
    {
        textoPerdio.SetActive(false);
    }

    public void perderJuego() 
    {
        textoPerdio.SetActive(true);

        // Le digo a todas las inferfaces que perdi
        foreach (IPersonajeMapa unaInterfaz in listaInterfaces) 
        {
            unaInterfaz.perderJuego();
        }
    }

    public void manejarPausa() 
    {
        // Le digo a todas las inferfaces que manejen su pausa
        foreach (IPersonajeMapa unaInterfaz in listaInterfaces)
        {
            unaInterfaz.manejarPausa();
        }
    }
}
