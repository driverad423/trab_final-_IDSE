using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 4f;
    public float speed = 1f;
    public bool grounded;
    public float jumpPower = 6.5f;
    

    private Rigidbody2D rb2d;
    //private Animator anim;
    private bool jump;
    private bool doubleJump;
    private SpriteRenderer spr;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Animaciones del personaje
        //Verifica si el personaje se mueve
        //anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        //Verifica si el personaje esta sobre algo
        //anim.SetBool("Grounded", grounded);
        //Con la direccional hacia arriba salta
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            if (grounded)
            {
                jump = true;
                doubleJump = true;
            }else if (doubleJump)
            {
                jump = true;
                doubleJump = false;
            }
            
        }
    }

    void FixedUpdate()
    {

        float h = Input.GetAxisRaw("Horizontal");

        rb2d.AddForce(Vector2.right * speed * h);

        //limita la velocidad
        float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        
        rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);
        
        //Cambiar de dirección al personaje
        if(h > 0.1f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if (h < -0.1f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        //Salto
        if (jump)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            
            jump = false;
        }

    }

    //Posicion inicial si el personaje desaparece
    void OnBecameInvisible()
    {
        transform.position = new Vector3(0, 1f, 0);
    }
    public void EnemyJump()
    {
        jump = true;
    }
    public void EnemyKnockBack(float enemyPos)
    {
        jump = true;
        float side = Mathf.Sign(enemyPos - transform.position.x);
        rb2d.AddForce(Vector2.left* side * jumpPower, ForceMode2D.Impulse);
        Invoke("EnableMovement", 0.7f);
        spr.color = new Color(255/255f, 106/255f,0/255f);
    }
    void EnableMovement()
    {
        spr.color = Color.white;
    }


}
