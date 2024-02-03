using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    private int direction=0; // -1 left 1 right
    private float velocity=40f;
    void Update()
    {
        transform.position += new Vector3(direction * velocity * Time.deltaTime, 0, 0);
    }

    public void ChangeDirection(int newDirection)
    {
        direction = newDirection;
    }
    
}
