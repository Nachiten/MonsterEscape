using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    public GameObject jugador;

    public float velocidad = 3f;

    void Awake()
    {
        //jugador = GameObject.Find("Jugador");
    }

    void Update()
    {
        transform.right = jugador.transform.position - transform.position;

        GetComponent<Rigidbody2D>().velocity = transform.right * velocidad;

        transform.Rotate(new Vector3(0, 0, -90));
    }
}
