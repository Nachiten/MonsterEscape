using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            unaInterfaz.perderJuego();
    }

    public void manejarPausa() 
    {
        pausa = !pausa;

        // Le digo a todas las inferfaces que manejen su pausa
        foreach (IPersonajeMapa unaInterfaz in listaInterfaces)
            unaInterfaz.manejarPausa();
        
        if (pausa)
            LeanTween.pauseAll();
        else
            LeanTween.resumeAll();
    }

    public void reiniciar() 
    {
        SceneManager.LoadScene(0);
    }
}
