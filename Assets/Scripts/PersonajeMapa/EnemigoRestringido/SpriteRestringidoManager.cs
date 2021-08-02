using UnityEngine;

public class SpriteRestringidoManager : MonoBehaviour, IPersonajeMapa
{
    GameObject espacio;
    GameObject objetoASeguir = null;

    float velocidad = 5f;

    bool perdio = false, estoyEnAnimacion = false, pausa = false;

    float tiempoVolverCentro = 0.5f;

    Rigidbody2D rigidBodyObjetoActual;

    void Awake()
    {
        rigidBodyObjetoActual = GetComponent<Rigidbody2D>();

        foreach (Transform hijoDeMiPadre in transform.parent.transform)
        {
            if (hijoDeMiPadre.gameObject.name == "Espacio")
            {
                espacio = hijoDeMiPadre.gameObject;
                break;
            }
        }

        if (espacio == null)
        {
            Debug.LogError("No encuentro el espacio.");
        }
    }

    void Update()
    {
        if (perdio || objetoASeguir == null || estoyEnAnimacion || pausa)
            return;

        transform.right = objetoASeguir.transform.position - transform.position;

        rigidBodyObjetoActual.velocity = transform.right * velocidad;

        transform.Rotate(new Vector3(0, 0, -90));
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("EspacioEnemigo"))
        {
            volverACentro();
        }
    }

    public void seguirA(GameObject unObjeto) 
    {
        if (unObjeto == null)
            rigidBodyObjetoActual.velocity = Vector2.zero;

        objetoASeguir = unObjeto;
    }

    void volverACentro() 
    {
        seguirA(null);

        // Apuntar hacia el centro
        transform.right = espacio.transform.position - transform.position;
        transform.Rotate(new Vector3(0, 0, -90));

        estoyEnAnimacion = true;

        LeanTween.move(gameObject, espacio.transform.position, tiempoVolverCentro).setOnComplete(_ => estoyEnAnimacion = false);
    }

    public void perderJuego() 
    {
        rigidBodyObjetoActual.velocity = Vector2.zero;
        perdio = true;
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
