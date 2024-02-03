using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmSelfDestory : MonoBehaviour
{
    void Start()
    {
        Invoke("Des", 0.05f);
    }

    private void Des()
    {
        Destroy(gameObject);
    }
}
