using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target; //player
    public float smoothedSpeed ;
    public float zValue;
    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position ;
        Vector3 smoothedPosition = Vector2.Lerp(transform.position, desiredPosition, smoothedSpeed*Time.deltaTime);
        transform.position = smoothedPosition + new Vector3(0, 0, -zValue);
    }
}
