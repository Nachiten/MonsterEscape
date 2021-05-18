using UnityEngine;

public class ProyectilRotatorioManager : MonoBehaviour, IPersonajeMapa
{
    GameObject jugador;
    GameObject sprite;

    Rigidbody2D rigidBodyObjetoActual;

    float velocidad = 8f;

    bool perdio = false, pausa = false;

    void Awake()
    {
        rigidBodyObjetoActual = GetComponent<Rigidbody2D>();

        jugador = GameObject.Find("Jugador");

        foreach (Transform hijoDeMiPadre in transform.parent.transform)
        {
            if (hijoDeMiPadre.gameObject.name == "Sprite") 
            {
                sprite = hijoDeMiPadre.gameObject;
                break;
            } 
        }

        if (sprite == null) 
        {
            Debug.LogError("No encuentro el sprite.");
        }
    }

    void OnEnable()
    {
        transform.position = sprite.transform.position;
        transform.right = jugador.transform.position - transform.position;
        rigidBodyObjetoActual.velocity = transform.right * velocidad;
    }

    void Update()
    {
        if (perdio || pausa)
            return;

        float distancia = Vector3.Distance(transform.position, Vector3.zero);

        if (distancia > 10f) 
        {
            gameObject.SetActive(false);
        }
    }

    public void perderJuego()
    {
        perdio = true;
        rigidBodyObjetoActual.velocity = Vector2.zero;
    }

    Vector2 ultimaVelocidad;

    public void manejarPausa() 
    {
        pausa = !pausa;

        if (pausa)
        {
            ultimaVelocidad = rigidBodyObjetoActual.velocity;
            rigidBodyObjetoActual.velocity = Vector2.zero;
        }
        else
        {
            rigidBodyObjetoActual.velocity = ultimaVelocidad;
        }
    }
}
