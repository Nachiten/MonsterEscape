using UnityEngine;

public class MovimientoEnemigoRotatorio : MonoBehaviour
{
    GameObject jugador;
    GameObject proyectil;

    bool perdio = false, estoyEnAnimacion = false;

    float timePassed = 0;

    void Awake()
    {
        jugador = GameObject.Find("Jugador");
        proyectil = GameObject.Find("Proyectil");

        proyectil.SetActive(false);
    }

    void Update()
    {
        if (perdio || estoyEnAnimacion)
            return;

        transform.right = jugador.transform.position - transform.position;
        transform.Rotate(new Vector3(0, 0, -90));

        timePassed += Time.deltaTime;

        if (timePassed >= 6f) 
        {
            timePassed = 0;

            proyectil.SetActive(true);
        }
    }

    public void perdioJuego()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        perdio = true;
    }
}
