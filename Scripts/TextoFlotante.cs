using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextoFlotante : MonoBehaviour
{
    public float tiempoDeVida = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tiempoDeVida -=Time.deltaTime;
        if (tiempoDeVida<=0)
        {
            Destroy(this.gameObject);
        }


    }
}
