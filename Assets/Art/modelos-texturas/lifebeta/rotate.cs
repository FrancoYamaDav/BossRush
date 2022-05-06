using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class rotate : MonoBehaviour
{
    public float rotationVel;
    // Start is called before the first frame update

    void Update()
    {
        transform.Rotate(0, rotationVel * Time.deltaTime,0);
    }
}
