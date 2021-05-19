using UnityEngine;

public class ProyectilRotatorioManager : MonoBehaviour, IPersonajeMapa
{
    GameObject jugador;
    GameObject sprite;

    Rigidbody2D rigidBodyObjetoActual;

    float velocidad = 8f;

    bool pausa = false;

    void Awake()
    {
        rigidBodyObjetoActual = GetComponent<Rigidbody2D>();

        jugador = GameObject.Find("Jugador");

        foreach (Transform hijoDeMiPadre in transform.parent.transform)
        {
            if (hijoDeMiPadre.gameObject.name.Equals("Sprite")) 
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

    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    public void perderJuego()
    {
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ParedMapa"))
            gameObject.SetActive(false);
    }
}
