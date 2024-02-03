using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drag6nFollow : MonoBehaviour
{
 
    public Transform target; //player
    public float smoothedSpeed;
    public float zValue;

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position;
        Vector3 smoothedPosition = Vector2.Lerp(transform.position, desiredPosition, smoothedSpeed * Time.deltaTime);
        transform.position = smoothedPosition + new Vector3(0, 0, -zValue);
        if(transform.position.x-target.position.x>0)
        {
            Vector2 currentScale = transform.localScale; 
            currentScale.x = Mathf.Abs(currentScale.x);
            transform.localScale = currentScale;
        }
        else
        {
            Vector2 currentScale = transform.localScale;
            currentScale.x = -1*Mathf.Abs(currentScale.x);
            transform.localScale = currentScale;
        }
    }
}
