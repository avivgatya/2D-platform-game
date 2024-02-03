using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class searchForPalSC : MonoBehaviour
{
    public Transform target;
    private float dist = 2f;
    void Update()
    {
        var dir = transform.position - target.transform.position;
        dir = Vector3.ClampMagnitude(dir, dist);
        // transform.position = target.transform.position + dir;
        transform.position =  target.transform.position + dir;

        
        transform.RotateAround(target.position, new Vector3(0, 0, 1), 360 * Time.deltaTime);
        //var newPos = (transform.position - target.transform.position).normalized * dist;

        // newPos += target.transform.position;
        // transform.position = newPos;


    }
}
