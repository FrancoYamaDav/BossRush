using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour, IFixedUpdate
{
    PlayerController target;
    public Vector3 positionOffset;
    //public Vector3 rotationOffset = new Vector3(0,5,0);

    public float sensitivity;

    bool ready = false;
    float startUpDelay = 1.2f;

    private void Awake()
    {
        positionOffset = new Vector3(0, 2, -6);

        target = FindObjectOfType<PlayerController>();

        StartCoroutine(StartupDelay());
    }
    private void Start()
    {
        UpdateManager.Instance.AddToFixedUpdate(this);
    }

    public void OnFixedUpdate()
    {
        if (target != null)
            transform.position = target.transform.position + positionOffset;
        
        //if (ready) Rotate();
    }

    float minHorizontal, maxHorizontal;
    float minVertical = -45, maxVertical = 20000;

    void Rotate()
    {
        //Horizontal
        float rotateHorizontal = Input.GetAxis("Mouse X") * sensitivity;
        Vector3 horizontalAngle = new Vector3(rotateHorizontal, 0, 0);
        target.transform.localRotation = target.transform.localRotation * Quaternion.AngleAxis(horizontalAngle.x, target.transform.up);
        //transform.position = new Vector3(transform.position.x + rotateHorizontal, transform.position.y, transform.position.z);

        //Vertical
        float rotateVertical = Input.GetAxis("Mouse Y");

        float limitVertical = rotateVertical * -sensitivity;
        //limitVertical = Mathf.Clamp(rotateVertical * -sensitivity, minVertical, maxVertical);
        transform.Rotate(new Vector3(limitVertical, 0, 0));

        target.ChangeRaycastAngle(new Vector3(0, rotateVertical*sensitivity, 0));
    }

    IEnumerator StartupDelay()
    {
        yield return new WaitForSeconds(startUpDelay);
        ready = true;
        StopCoroutine(StartupDelay());
    }
}
