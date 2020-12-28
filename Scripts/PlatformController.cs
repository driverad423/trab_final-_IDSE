using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformController : MonoBehaviour
{

    public int valuePlatform;
    public int status;
    public int id;

    private Text TextPlatform;
    public AudioSource audio;
    public AddAnimationController animationController;
    
    public float onWaitTime;
    public float waitTime;
    

    //0 ready
    //1 wait

    // Start is called before the first frame update
    void Start()
    {
        waitTime = 7f;
        onWaitTime = waitTime;
        TextPlatform = GetComponentInChildren<Text>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (status == 1)
        {
            onWaitTime -= 1 * Time.deltaTime;
            TextPlatform.text = "";

            if (onWaitTime <= 0)
            {
                status = 0;
                onWaitTime = waitTime;
            }
        }else
        {
            if (valuePlatform > 0)
            {
                TextPlatform.text = "+" + valuePlatform;
            } else if (valuePlatform == 0) {
                TextPlatform.text = "";
            } else
            {
                TextPlatform.text = "" + valuePlatform;
            }
        }

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (status == 0)
        {
            audio.Play();
            animationController.launchAnimation(TextPlatform.text);
            status = 1;
            EventLauncher.addStep(id);   
        }
        
    }
}
