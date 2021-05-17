using UnityEngine;

public class MovimientoEnemigoRestringido : MonoBehaviour
{
    GameObject centroEspacio;
    GameObject objetoASeguir = null;

    float velocidad = 5f;

    bool perdio = false, estoyEnAnimacion = false;

    float tiempoVolverCentro = 0.5f;

    void Awake()
    {
        centroEspacio = GameObject.Find("CentroEspacio");
    }

    void Update()
    {
        if (perdio || objetoASeguir == null || estoyEnAnimacion)
            return;

        transform.right = objetoASeguir.transform.position - transform.position;

        GetComponent<Rigidbody2D>().velocity = transform.right * velocidad;

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
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        objetoASeguir = unObjeto;
    }

    void volverACentro() 
    {
        seguirA(null);

        // Apuntar hacia el centro
        transform.right = centroEspacio.transform.position - transform.position;
        transform.Rotate(new Vector3(0, 0, -90));

        estoyEnAnimacion = true;

        LeanTween.move(gameObject, centroEspacio.transform.position, tiempoVolverCentro).setOnComplete(_ => estoyEnAnimacion = false);
    }

    public void perdioJuego() 
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        perdio = true;
    }
}
