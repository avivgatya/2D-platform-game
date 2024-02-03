using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dissappear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Des", 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (transform.up) * Time.deltaTime * 1.2f;
    }
    private void Des()
    {
        Destroy(gameObject);
    }
}
