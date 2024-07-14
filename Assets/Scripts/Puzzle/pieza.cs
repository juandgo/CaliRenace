using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class pieza : MonoBehaviour
{
    private Vector3 PosicionCorrecta;
    public bool Encajada;
    public bool Seleccionada;

    void Start()
    {
        PosicionCorrecta = transform.position;

        float xOffset = 5f;
        float yOffset = 2.5f;
        float yOffsetDebajo = 7f; // Ajusta este valor según sea necesario
        Vector3 nuevaPosicion;

        do
        {
            nuevaPosicion = new Vector3(Random.Range(5f, 11f), Random.Range(2.5f, -7));
        }
        while (EsSobreLaImagen(nuevaPosicion, xOffset, yOffset) || EsDebajoDeLaImagen(nuevaPosicion, yOffsetDebajo));

        transform.position = nuevaPosicion;
    }
    
    void Update()
    {
        if (Vector3.Distance(transform.position, PosicionCorrecta) < 0.5f)
        {
            if (!Seleccionada)
            {
                if (Encajada == false)
                {
                    transform.position = PosicionCorrecta;
                    Encajada = true;
                    GetComponent<SortingGroup>().sortingOrder = 0;
                    Camera.main.GetComponent<juego>().PiezasEncajadas++;
                }
            }
        }
    }

    bool EsSobreLaImagen(Vector3 posicion, float xOffset, float yOffset)
    {
        // Ajustar estos valores según el tamaño y la posición de la imagen en tu escena
        float imageLeft = Camera.main.transform.position.x - xOffset;
        float imageRight = Camera.main.transform.position.x + xOffset;
        float imageTop = Camera.main.transform.position.y + yOffset;
        float imageBottom = Camera.main.transform.position.y - yOffset;

        if (posicion.x > imageLeft && posicion.x < imageRight && posicion.y < imageTop && posicion.y > imageBottom)
        {
            return true;
        }
        return false;
    }

    bool EsDebajoDeLaImagen(Vector3 posicion, float yOffsetDebajo)
    {
        // Ajustar este valor según la posición de la imagen en tu escena
        float imageBottom = Camera.main.transform.position.y - yOffsetDebajo;

        if (posicion.y < imageBottom)
        {
            return true;
        }

        return false;
    }
}
