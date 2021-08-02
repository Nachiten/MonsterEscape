using UnityEngine;

public class SpriteRotatorioManager : MonoBehaviour, IPersonajeMapa
{
    GameObject jugador;
    GameObject proyectil;

    bool perdio = false, estoyEnAnimacion = false, pausa = false;

    float timePassed = 0;
    float intervaloAparicion = 2.5f;

    void Awake()
    {
        jugador = GameObject.Find("Jugador");
        
        // Busco al proyectil dentro de los hijos de mi padre
        foreach (Transform hijoDeMiPadre in transform.parent.transform)
        {
            if (hijoDeMiPadre.gameObject.name.Equals("Proyectil"))
            {
                proyectil = hijoDeMiPadre.gameObject;
                break;
            }
        }

        if (proyectil == null)
        {
            Debug.LogError("No encuentro el proyectil.");
        }
    }

    private void Start()
    {
        proyectil.SetActive(false);
    }

    void Update()
    {
        if (perdio || estoyEnAnimacion || pausa)
            return;

        // Apunto hacia el jugador
        transform.right = jugador.transform.position - transform.position;
        transform.Rotate(new Vector3(0, 0, -90));

        timePassed += Time.deltaTime;

        // Aparezco el proyectil si paso el tiempo fijado
        if (timePassed >= intervaloAparicion) 
        {
            timePassed = 0;
            proyectil.SetActive(true);
        }
    }

    public void perderJuego()
    {
        perdio = true;
    }

    public void manejarPausa()
    {
        pausa = !pausa;
    }
}
