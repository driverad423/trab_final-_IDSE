using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mensajeInicio : MonoBehaviour
{
    bool active;
    Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled =  true;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            //active=  !active;
        
            active=false;
            canvas.enabled =  active;
            Time.timeScale =  (active) ?  0 :1f;
        }
        
    }
}
