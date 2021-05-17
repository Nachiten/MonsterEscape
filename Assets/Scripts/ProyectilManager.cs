using UnityEngine;

public class ProyectilManager : MonoBehaviour
{
    GameObject jugador;
    GameObject enemigo;

    float velocidad = 5f;

    void Awake()
    {
        jugador = GameObject.Find("Jugador");
        enemigo = GameObject.Find("EnemigoRotatorio");
    }

    void OnEnable()
    {
        transform.position = enemigo.transform.position;

        transform.right = jugador.transform.position - transform.position;

        Debug.Log("Transform right: " + transform.right);

        GetComponent<Rigidbody2D>().velocity = transform.right * velocidad;
    }

    void Update()
    {
        float distancia = Vector3.Distance(transform.position, Vector3.zero);

        if (distancia > 10f) 
        {
            gameObject.SetActive(false);
        }
    }
}
