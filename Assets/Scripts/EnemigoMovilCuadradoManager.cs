using UnityEngine;

public class EnemigoMovilCuadradoManager : MonoBehaviour, IPersonajeMapa
{
    float tiempoMover = 1f, ladoCuadrado = 4f;

    void Start()
    {
        moverPosicion1a2();
    }

    void moverPosicion1a2()
    {
        Vector3 posicionActual = transform.position;

        LeanTween.move(gameObject, new Vector3(posicionActual.x + ladoCuadrado, posicionActual.y, posicionActual.z), tiempoMover).setOnComplete(moverPosicion2a3);
    }

    void moverPosicion2a3() 
    {
        Vector3 posicionActual = transform.position;

        LeanTween.move(gameObject, new Vector3(posicionActual.x, posicionActual.y + ladoCuadrado, posicionActual.z), tiempoMover).setOnComplete(moverPosicion3a4);
    }

    void moverPosicion3a4()
    {
        Vector3 posicionActual = transform.position;

        LeanTween.move(gameObject, new Vector3(posicionActual.x - ladoCuadrado, posicionActual.y, posicionActual.z), tiempoMover).setOnComplete(moverPosicion4a1);
    }

    void moverPosicion4a1()
    {
        Vector3 posicionActual = transform.position;

        LeanTween.move(gameObject, new Vector3(posicionActual.x, posicionActual.y - ladoCuadrado, posicionActual.z), tiempoMover).setOnComplete(moverPosicion1a2);
    }

    public void perderJuego() { LeanTween.pause(gameObject); }

    public void manejarPausa() { }
}
