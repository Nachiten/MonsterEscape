using UnityEngine;

public class MovimientoJugador : MonoBehaviour, IPersonajeMapa
{
    float rotacion = 0, velocidad = 7f;

    bool perdio = false, pausa = false;

    Rigidbody2D rigidBodyObjetoActual;

    private void Awake()
    {
        rigidBodyObjetoActual = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (perdio || pausa)
            return;

        ejecutarMovimientoJugador();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo") || collision.CompareTag("ParedMapa"))
        {
            perderJuegoLocal();   
        }
    }

    void perderJuegoLocal() 
    {
        perdio = true;
        rigidBodyObjetoActual.velocity = Vector2.zero;
        GameObject.Find("GameManager").GetComponent<GameManager>().perderJuego();
    }

    void ejecutarMovimientoJugador() 
    {
        float movimientoX = 0;
        float movimientoY = 0;

        bool apreteArriba = false;
        bool apreteAbajo = false;

        // Arriba - +Y
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            movimientoY += velocidad;
            rotacion = 0;
            apreteArriba = true;
        }

        // Abajo - -Y
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            movimientoY += -velocidad;
            rotacion = 180;
            apreteAbajo = true;
        }

        // Derecha - +X
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            movimientoX += velocidad;
            if (apreteArriba)
                rotacion = -45;
            else if (apreteAbajo)
                rotacion = 225;
            else
                rotacion = -90;
        }

        // Izquierda - -X
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            movimientoX += -velocidad;
            if (apreteArriba)
                rotacion = 45;
            else if (apreteAbajo)
                rotacion = 135;
            else
                rotacion = 90;
        }

        rigidBodyObjetoActual.velocity = new Vector2(movimientoX, movimientoY);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotacion));
    }

    public void perderJuego() { }

    public void manejarPausa() 
    { 
        pausa = !pausa; 

        if (pausa)
            rigidBodyObjetoActual.velocity = Vector2.zero;
    }
}
