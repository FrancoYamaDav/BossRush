using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    PlayerModel target;
    public Vector3 positionOffset;
    //public Vector3 rotationOffset = new Vector3(0,5,0);

    private void Awake()
    {
        positionOffset = new Vector3(-6, 2, 0);

        target = FindObjectOfType<PlayerModel>();
    }

    private void Update()
    {
        transform.position = target.transform.position + positionOffset;
        //transform.rotation = target.transform.rotation;
    }
}
