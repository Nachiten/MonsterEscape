using UnityEngine;

public class EnemigoLaserManager : MonoBehaviour, IPersonajeMapa
{
    Rigidbody2D rigidBodyObjeto;

    GameObject laser;

    bool laserActivo = true, perdio = false, pausa = false;

    float velocidad = 0.4f, distanciaRecorrida = 6f, distanciaMin, distanciaMax,
          tiempoMostradoLaser = 3.7f, intervaloAparicionLaser = 1.3f, tiempoPasado = 0;

    Vector2 vectorOriginal;

    // Start is called before the first frame update
    void Awake()
    {
        rigidBodyObjeto = GetComponent<Rigidbody2D>();

        distanciaMin = transform.position.y;
        distanciaMax = distanciaMin + distanciaRecorrida;

        // Busco al proyectil dentro de mis hijos
        foreach (Transform hijoMio in transform)
        {
            if (hijoMio.gameObject.name.Equals("Laser"))
            {
                laser = hijoMio.gameObject;
                break;
            }
        }

        vectorOriginal = -transform.right;
    }

    private void Start()
    {
        rigidBodyObjeto.velocity = vectorOriginal * velocidad;
        laser.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (perdio || pausa)
            return;

        moverSprite();

        triggerLaser();
    }

    void moverSprite() 
    {
        float posY = transform.position.y;

        if (posY > distanciaMax)
        {
            rigidBodyObjeto.velocity = -vectorOriginal * velocidad;
        }

        if (posY < distanciaMin)
        {
            rigidBodyObjeto.velocity = vectorOriginal * velocidad;
        }
    }

    void triggerLaser() 
    {
        tiempoPasado += Time.deltaTime;

        bool debeAparecerLaserYNoActivo = tiempoPasado >= intervaloAparicionLaser && !laserActivo;
        bool debeDesaparecerLaserYActivo = tiempoPasado >= tiempoMostradoLaser && laserActivo;

        if (debeAparecerLaserYNoActivo || debeDesaparecerLaserYActivo) 
        {
            tiempoPasado = 0;

            laserActivo = !laserActivo;

            laser.SetActive(laserActivo);
        }
    }

    public void perderJuego()
    {
        perdio = true;
        rigidBodyObjeto.velocity = Vector2.zero;
    }

    Vector2 ultimaVelocidad;

    public void manejarPausa()
    {
        pausa = !pausa;

        if (pausa)
        {
            ultimaVelocidad = rigidBodyObjeto.velocity;
            rigidBodyObjeto.velocity = Vector2.zero;
        }
        else
        {
            rigidBodyObjeto.velocity = ultimaVelocidad;
        }
    }
}
