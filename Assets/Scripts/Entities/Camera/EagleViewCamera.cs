using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleViewCamera : MonoBehaviour
{
    PlayerModel target;
    Vector3 offset;

    private void Awake()
    {
        offset = new Vector3(0, 10, 0);

        target = FindObjectOfType<PlayerModel>();
    }

    private void Update()
    {
        transform.position = target.transform.position + offset;
    }
}
