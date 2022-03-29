using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    PlayerModel target;
    public Vector3 positionOffset;
    //public Vector3 rotationOffset = new Vector3(0,5,0);

    public float sensitivity;

    private void Awake()
    {
        positionOffset = new Vector3(0, 2, -6);

        target = FindObjectOfType<PlayerModel>();        
    }

    void FixedUpdate()
    {
        if (target != null)
            transform.position = target.transform.position + positionOffset;

        Rotate();
    }

    void Rotate()
    {
        float rotateHorizontal = Input.GetAxis("Mouse X");

        float limitHorizontal = rotateHorizontal * -sensitivity;
        transform.RotateAround(target.transform.position, -Vector3.up, limitHorizontal);
        //Acá poner que rote el pj en x
        //player.transform.localRotation = Quaternion.AngleAxis(currentLooking.x, target.transform.up);


        float rotateVertical = Input.GetAxis("Mouse Y");
        float limitVertical = rotateVertical * -sensitivity;
        //transform.RotateAround(target.transform.position, Vector3.right, limitVertical);
        //no es rotate around, solo pa arriba y pa abajo
        //Acá rotar SOLO el raycast en y
    }
}
