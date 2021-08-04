using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingBG : MonoBehaviour
{
    public Rocks rocks;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > -26)
        {
            transform.position += Vector3.left * rocks.speed / 100;

        }
        else
        {
            transform.position = new Vector3(26, 0f, 0);
        }
    }
}
