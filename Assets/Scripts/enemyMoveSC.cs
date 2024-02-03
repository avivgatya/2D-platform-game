using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class enemyMoveSC : MonoBehaviour
{
    private int direction = 1; //1 right -1 left
    public GameObject playerScript;
    private float velocity = 0.7f;
 //   Vector3 look1;
//    private float radius = 3.5f;
   // private Transform tr;
  //  private SpriteRenderer sr;
  //  Collider2D platformIStandOn;
    //float maxVerticalForce = 25f;
   // float maxHorizontalForce = 25f;
    private Rigidbody2D rb;

    private Vector3 startPosition;

    // die
    public ParticleSystem particles;
    public BoxCollider2D boxCollider;
    public SpriteRenderer spriteRenderer;
    private bool dieCoroutine = false;


    [SerializeField]
   // private bool canJump = true;
    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        ChangeDirection(true);

    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 look = transform.position + new Vector3(1, 1, 0);
        // look1 = transform.position + new Vector3(-0.6f, -0.4f, 0);
        //Debug.DrawLine(transform.position, look1, Color.red);
        transform.position += new Vector3(velocity * direction * Time.deltaTime, 0, 0);
    }
/*
    private void FixedUpdate()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius);

        // Debug.Log("size of array of colliders:" + hitColliders.Length);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.tag == "Ground")
            {
                tr = hitCollider.gameObject.GetComponent<Transform>();
                sr = hitCollider.gameObject.GetComponent<SpriteRenderer>();
                bool situation = tr.position.y < transform.position.y && transform.position.y - tr.position.y < 0.5f;
                bool facingPlatform = ((tr.position.x > transform.position.x) && direction == 1) || ((tr.position.x < transform.position.x) && direction == -1);
                if (hitCollider.gameObject != this.gameObject && !situation && canJump && facingPlatform)
                {
                    sr.color = Color.red;
                    //  if(Random.Range(1,100)==1)
                    executeJump(tr.position.x - transform.position.x, tr.position.y - transform.position.y);
                }
            }
        }
    }
*/
    public void ChangeDirection(bool random)
    {
        int temp = this.direction;
        if (random)
        { 
            if (Random.Range(0, 2) == 1) this.direction = 1; // 0 to 1
            else this.direction = -1;
        }
        else this.direction *= -1; //changing direction
        if(direction!=temp) FlipSprite();

    }

    void FlipSprite()
    {
        Vector2 currentScale = transform.localScale; //player
        currentScale.x *= -1;
        transform.localScale = currentScale; //player
    }
    /*
    private void executeJump(float x,float y) //x and y are the distances between obj and target platform
    {
        float forceX, forceY;
        forceX = x;
        forceY = y+(9.8f/2f);
        if (forceX <= maxHorizontalForce && forceY <= maxVerticalForce)
        {
            
            rb.AddForce(new Vector2(forceX , forceY), ForceMode2D.Impulse);
            canJump = false;
        }
    }
    */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Border")
            ChangeDirection(false);
    }

    public void Die() //start die
    {
        particles.Play();
        playerScript.GetComponent<playerSC>().AddOnePoint();
        StartCoroutine(DissolveDie());

    }
    private void ReSpawn()
    {
        transform.position = startPosition;
        spriteRenderer.enabled = true;
        boxCollider.enabled = true;
        StartCoroutine(DissolveWake());
    }
    IEnumerator DissolveWake()
    {
        while (!dieCoroutine&& spriteRenderer.material.GetFloat("_Fade") < 0.96f)
        {
            float value = Mathf.Lerp(spriteRenderer.material.GetFloat("_Fade"), 1f, 1 * Time.deltaTime);
            spriteRenderer.material.SetFloat("_Fade", value);
            yield return null;
        }
        spriteRenderer.material.SetFloat("_Fade", 1f);
        //end
    }
    IEnumerator DissolveDie()
    {
        dieCoroutine = true;
        while (spriteRenderer.material.GetFloat("_Fade")>0.04f)
        {
            float value = Mathf.Lerp(spriteRenderer.material.GetFloat("_Fade"), 0f, 8*Time.deltaTime);
            spriteRenderer.material.SetFloat("_Fade", value);
            yield return null;
        }
        spriteRenderer.material.SetFloat("_Fade", 0f);
        //end
        spriteRenderer.enabled = false;
        boxCollider.enabled = false;
        dieCoroutine = false;
        Invoke("ReSpawn", 6f);
    }


}