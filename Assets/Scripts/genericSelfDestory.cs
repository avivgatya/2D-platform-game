using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genericSelfDestory : MonoBehaviour
{
    void Start()
    {
        Invoke("Des", 5f);
    }

    private void Des()
    {
        Destroy(gameObject);
    }
}
