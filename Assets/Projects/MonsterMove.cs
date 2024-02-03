using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MonsterMove : MonoBehaviour
{
    
    private float movementSpeed=0.5f;
    private float health=15f;

    public GameObject cam; // for text rotation
    public GameObject floatingTextDamage;
    private GameObject text;  // hold damage after Instantiating
    public GameObject manager; // for exp update
    private Vector3 knockBackDirection;
    private void Start()
    {
       // transform.position = new Vector3(Random.Range(-40, -10), 0.64f, Random.Range(-15, 15));
    }
    void Update()
    {
        transform.position += (transform.forward) * Time.deltaTime * movementSpeed;
    }
    public void ReduceHealth(int hit)
    {
        bool check = false;
        if (Random.Range(1, 5) == 2)
        {
            check = true;
            hit *= 2;
        }
        health -= hit;
        // float dist = Vector3.Distance(other.position, transform.position);
        text = Instantiate(floatingTextDamage, transform.position + new Vector3(0, 5f, 0), cam.transform.rotation);
        text.GetComponent<TMPro.TextMeshPro>().text= hit.ToString();
       if(check==true) text.GetComponent<TMPro.TextMeshPro>().color = Color.red;
        
        if (health <= 0)
        {
           // manager.GetComponent<CalculatingEXPandLEVEL>().AddExp(12);
           TemporaryDie();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (collision.gameObject.GetComponent<CheckCol>().collidedYet == false)
            {
                transform.position += new Vector3(0,0,0.5f);
                collision.gameObject.GetComponent<CheckCol>().collidedYet = true;
               // knockBackDirection = collision.gameObject.GetComponent<DestroyMe>().KnockBackDirection();
               // MinimizeKnockBack(ref knockBackDirection);
               // this.transform.position += knockBackDirection;
              //  Invoke("ReverseKnockBack", 0.1f);
                ReduceHealth(Random.Range(4, 10));
            }
        }
    }
    
    private void TemporaryDie()
    {
        Debug.Log("die");
        Invoke("ReBearth", 5f);
        this.gameObject.SetActive(false);
    }
    private void ReBearth()
    {
        Debug.Log("rebearth");
        health = 15f;
        transform.position = new Vector3(Random.Range(-40, -10), 0.64f, Random.Range(-15, 15));
        this.gameObject.SetActive(true);


    }
}
