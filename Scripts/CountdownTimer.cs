using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public Text TextTime;
    public float currentTime = 0f;
    public float startingTime = 100f;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;

    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;

        if(currentTime < 20)
        {
            TextTime.color = Color.red;
        } else if(currentTime < 60)
        {
            TextTime.color = Color.yellow;
        } else
        {
            TextTime.color = Color.black;
        }

        TextTime.text = currentTime.ToString().Substring(0, currentTime.ToString().Length -1);

        if (currentTime <= 0.1)
        {
            print(currentTime.ToString());
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}
