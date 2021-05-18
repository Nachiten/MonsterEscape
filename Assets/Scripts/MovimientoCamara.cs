using UnityEngine;

public class MovimientoCamara : MonoBehaviour
{
    GameObject jugador;

    void Awake()
    {
        jugador = GameObject.Find("Jugador");
    }

    private void Update()
    {
        transform.position = new Vector3(jugador.transform.position.x, jugador.transform.position.y, transform.position.z);
    }
}
