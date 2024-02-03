using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfDestory : MonoBehaviour
{
    void Start()
    {
        Invoke("Des", 3f);
    }

    private void Des()
    {
        Destroy(gameObject);
    }
}
