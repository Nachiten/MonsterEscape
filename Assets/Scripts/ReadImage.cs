using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct BloqueMapa 
{
    public float rotacion;
    public Vector3 posicion;
    public Vector3 tama�o;

    public BloqueMapa(int rotacion, Vector3 posicion, Vector3 tama�o) : this()
    {
        this.rotacion = rotacion;
        this.posicion = posicion;
        this.tama�o = tama�o;
    }
}

public class ReadImage : MonoBehaviour
{
    public Texture2D imagenMapa;
    public Texture2D image;

    public GameObject wallObject;

    List<BloqueMapa> bloquesASpawnear;

    int filas, columnas;

    float grosorPared = 1f;

    int[,] matriz;
    private void Start()
    {
        bloquesASpawnear = new List<BloqueMapa>();

        escanearImagenHaciaMatriz();

        buscarYMarcarLineas(0);

        buscarYMarcarLineas(1);

        instanciarBloques();
    }

    void instanciarBloques() 
    {
        foreach (BloqueMapa unBloque in bloquesASpawnear)
        {
            GameObject bloqueInstanciado = Instantiate(wallObject, unBloque.posicion, Quaternion.identity);

            bloqueInstanciado.transform.localScale = unBloque.tama�o;
        }
    }

    // 0 = X, 1 = Y
    void buscarYMarcarLineas(int coordenada)
    {
        int repeticiones = 0;

        while (buscarCandidato(coordenada)) 
        {
            if (repeticiones > 300000) 
            {
                Debug.LogError("[buscarYMarcarLineasX] Demasiadas repeticiones!!!");
                break;
            }
    
            repeticiones++;
        }
    }

    // 0 = X, 1 = Y
    bool buscarCandidato(int coordenada) 
    {
        for (int unaFila = 0; unaFila < filas; unaFila++)
        {
            for (int unaColumna = 0; unaColumna < columnas; unaColumna++)
            {
                // Es un candidato a fila (x)
                if (matriz[unaFila, unaColumna] == 1 && coordenada == 0)
                {
                    buscarFilaEnPos(unaFila, unaColumna);
                    return true;
                }

                // Es un candidato a columna (y)
                if (matriz[unaFila, unaColumna] == 2 && coordenada == 1) 
                {
                    buscarColumnaEnPos(unaFila, unaColumna);
                    return true;
                }
            }
        }

        return false;
    }

    void buscarColumnaEnPos(int fila, int columna) 
    {
        int limiteMin = fila;
        int limiteMax = fila;

        int filaActual = fila;

        int repeticiones = 0;

        while (tieneUnVecinoY(filaActual, columna))
        {
            matriz[filaActual, columna] = 0;
            matriz[filaActual + 1, columna] = 0;
            filaActual++;
            limiteMax = filaActual;

            if (repeticiones > 300000)
            {
                Debug.LogError("[buscarFilaEnPos] Demasiadas repeticiones!!!");
                break;
            }

            repeticiones++;
        }

        float tama�oY = limiteMax - limiteMin + 1;
        float posY = limiteMin + tama�oY / 2;

        BloqueMapa bloqueActual = new BloqueMapa(90, new Vector3(columna + 0.5f, filas - 1 - posY + 0.5f, 0), new Vector3(grosorPared, tama�oY, 1));

        bloquesASpawnear.Add(bloqueActual);
    }

    void buscarFilaEnPos(int fila, int columna) 
    {
        int limiteMin = columna;
        int limiteMax = columna;

        int columnaActual = columna;

        int repeticiones = 0;

        while (tieneUnVecinoX(fila, columnaActual)) 
        {
            matriz[fila, columnaActual] = 0;
            matriz[fila, columnaActual + 1] = 0;
            columnaActual ++;
            limiteMax = columnaActual;

            if (repeticiones > 300000)
            {
                Debug.LogError("[buscarFilaEnPos] Demasiadas repeticiones!!!");
                break;
            }

            repeticiones++;
        }

        if (limiteMax == limiteMin)
        {
            matriz[fila, columnaActual] = 2;
            return;
        }
            
        float tama�oX = limiteMax - limiteMin + 1;
        float posX = limiteMin + tama�oX / 2;

        BloqueMapa bloqueActual = new BloqueMapa(0, new Vector3(posX, filas - 1 - fila,0), new Vector3(tama�oX, grosorPared, 1));

        bloquesASpawnear.Add(bloqueActual);
    }

    bool tieneUnVecinoY(int fila, int columna) 
    {
        return (fila + 1 < filas && matriz[fila + 1, columna] == 2);
    }

    bool tieneUnVecinoX(int fila, int columna)
    {
        return (columna + 1 < columnas && matriz[fila, columna + 1] == 1);
    }

    void escanearImagenHaciaMatriz() 
    {
        image = imagenMapa;
        Color[] pix = image.GetPixels();

        filas = image.height;
        columnas = image.width;

        matriz = new int[filas, columnas];

        int contador = 0;

        for (int unaFila = filas - 1; unaFila >= 0; unaFila--) 
        {
            for (int unaColumna = 0; unaColumna < columnas; unaColumna++)
            {
                Color colorActual = pix[contador];

                int valor = 0;

                if (colorActual.Equals(Color.white))
                    valor = 1;

                matriz[unaFila, unaColumna] = valor;

                contador++;
            }
        }
    }

    // 1 - Escanear imagen hacia matriz
    // 2 - Leer matriz hasta encontrar primera coincidencia (x)
    // 3 - Buscar final de linea (x)
    // 4 - Guardar linea (x)
    // 5 - Repetir desde paso dos hasta no encontrar mas lineas

    // 6 - Leer matriz hasta encontrar primera coincidencia (y)
    // 7 - Buscar final de linea (7)
    // 8 - Guardar linea (y)
    // 9 - Repetir desde paso dos hasta no encontrar mas lineas

    // 10 - Instanciar todas las lineas guardadas como objetos

    void printearMatriz()
    {
        string stringMatriz = "";

        for (int unafila = 0; unafila < filas; unafila++)
        {
            for (int unacolumna = 0; unacolumna < columnas; unacolumna++)
            {
                stringMatriz += string.Format("{0} ", matriz[unafila, unacolumna]);
            }

            stringMatriz += System.Environment.NewLine + System.Environment.NewLine;
        }

        Debug.Log(stringMatriz);
    }

    void printearBloques()
    {
        if (bloquesASpawnear.Count == 0)
        {
            Debug.LogError("No hay bloques");
            return;
        }

        int contador = 0;

        foreach (BloqueMapa unBloque in bloquesASpawnear)
        {
            Debug.Log("Bloque numero: " + contador);
            Debug.Log("Rotacion: " + unBloque.rotacion);
            Debug.Log("Posicion: " + unBloque.posicion.ToString());
            Debug.Log("Tama�o: " + unBloque.tama�o.ToString());

            contador++;
        }
    }
}
