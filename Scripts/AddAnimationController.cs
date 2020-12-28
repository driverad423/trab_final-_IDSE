using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddAnimationController : MonoBehaviour
{
    public Text textAnimation;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.active = false;
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void launchAnimation(string value)
    {

        anim.enabled = true;
        gameObject.active = true;
        textAnimation.text = value;

        //launch animation
        anim.Play("Base Layer.AddAnimation", 0, 0.25f);

    }
}
