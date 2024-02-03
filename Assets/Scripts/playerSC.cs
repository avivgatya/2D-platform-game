using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class playerSC : MonoBehaviour
{
    public Animator animator; //ref to animator of this object off course 
    public float moveSpeed=7f;
    public float forceJump=30f;
    public GameObject Bullet;
    private GameObject BulletInst;
    public Transform transformShotPosition;
    Rigidbody2D rb;
    public ParticleSystem smokeParticles;
    public ParticleSystem jumpParticles;
    public GameObject muzzleFlashPre;
    private int moveByTouchDirection = 0; // -1=left  0= no move  1=right
    private int facingRight = -1;
    private float timeOfCoolDownShooting = 1f; //2sec
    private bool canShoot = true;
    private bool grounded=false;
    private int points=0;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI manaText;
    private int mana;

    // audio
    public AudioSource audioSource;
    public AudioClip rechargeAudio;
    public AudioClip shotAudio;
    

    private void Start()
    {
        mana = 29;
        AddMana();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
       var movement = Input.GetAxis("Horizontal");
        if (movement != 0)
        {
            transform.position += new Vector3(movement * moveSpeed * Time.deltaTime, 0, 0);
            if(grounded)animator.SetBool("runAnimeBool", true);
            
        }
        else if (movement == 0 && moveByTouchDirection != 0)
        {
            transform.position += new Vector3(moveByTouchDirection * moveSpeed * Time.deltaTime, 0, 0);
            if (grounded) animator.SetBool("runAnimeBool", true);
        }
        else animator.SetBool("runAnimeBool", false);  // this else is only for the animation.


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(grounded)
            {
                rb.AddForce(new Vector2(0, forceJump), ForceMode2D.Impulse);
                animator.SetTrigger("jumpTrigger");
            }    
            else
            {
                if (mana > 25)
                {
                    rb.AddForce(new Vector2(0, forceJump), ForceMode2D.Impulse);
                    //animator.SetTrigger("jumpTrigger");
                    Instantiate(jumpParticles, transform.position + new Vector3(0.03f * facingRight, 0.2f, 0), Quaternion.Euler(0, 0, -90));
                    SpendMana();
                }
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canShoot)
            {
                animator.SetTrigger("shotTrigger");
                Instantiate(smokeParticles,transform.position+new Vector3(-0.15f*facingRight,-0.4f,0),transform.localRotation);
                int spinOnY;
                if (facingRight == 1) spinOnY = 180;
                else spinOnY = 0;
                Instantiate(muzzleFlashPre,transform.position+new Vector3(0.7f*facingRight,-0.3f,-1f),Quaternion.Euler(0,spinOnY,0));
                
                BulletInst = Instantiate(Bullet, transformShotPosition.position, transformShotPosition.rotation);
                BulletInst.GetComponent<BulletMove>().ChangeDirection(facingRight);
                canShoot = false;
                Invoke("CoolDownShooting", timeOfCoolDownShooting);
                audioSource.PlayOneShot(shotAudio);
                Invoke("SoundRecharge", 0.3f);

            }
        }
        //change facing
        if ((movement < 0 || moveByTouchDirection < 0) && (facingRight == 1)) Flip();
        if ((movement > 0 || moveByTouchDirection > 0) && (facingRight == -1)) Flip();
    }
    public void SpecialFunc(int direction)
    {
        moveByTouchDirection = direction;
    }
    public void SpecialJump()
    {
        
        rb.AddForce(new Vector2(0, forceJump), ForceMode2D.Impulse);
        animator.SetTrigger("jumpTrigger");


    }
    private void Flip()
    {
        facingRight *= -1;
        Vector2 currentScale = transform.localScale; //player
        currentScale.x *= -1;
        transform.localScale = currentScale; //player
 
    }
    private void CoolDownShooting()
    {
        canShoot = true;
    }


    public void AddOnePoint()
    {
        points += 1;
        scoreText.text = "Killed: "+points.ToString();
    }
    public void AddMana()
    {
        mana += 1;
        manaText.text = "Energy: "+mana.ToString();
    }
    private void SpendMana()
    {
        mana -= 25;
        manaText.text = "Energy: " + mana.ToString();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
            Debug.Log("Grounded!");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
            Debug.Log("Not Grounded!");
        }
    }

    private void SoundRecharge()
    {
        audioSource.PlayOneShot(rechargeAudio);
    }
}
