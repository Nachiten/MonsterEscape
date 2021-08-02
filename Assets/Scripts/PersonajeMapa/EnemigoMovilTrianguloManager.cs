using UnityEngine;

public class EnemigoMovilTrianguloManager : MonoBehaviour, IPersonajeMapa
{
    float tiempoMover = 1f, ladoTriangulo = 5f;

    void Start()
    {
        moverPosicion1a2();
    }

    void moverPosicion1a2() 
    {
        Vector3 posicionActual = transform.position;

        LeanTween.move(gameObject, new Vector3(posicionActual.x + ladoTriangulo, posicionActual.y, posicionActual.z), tiempoMover).setOnComplete(moverPosicion2a3);
    }

    void moverPosicion2a3()
    {
        Vector3 posicionActual = transform.position;

        LeanTween.move(gameObject, new Vector3(posicionActual.x - ladoTriangulo / 2, posicionActual.y + 0.866f * ladoTriangulo, posicionActual.z), tiempoMover).setOnComplete(moverPosicion3a1);
    }

    void moverPosicion3a1()
    {
        Vector3 posicionActual = transform.position;

        LeanTween.move(gameObject, new Vector3(posicionActual.x - ladoTriangulo / 2, posicionActual.y - 0.866f * ladoTriangulo, posicionActual.z), tiempoMover).setOnComplete(moverPosicion1a2);
    }

    public void perderJuego() { LeanTween.pause(gameObject); }

    public void manejarPausa() { }
}
