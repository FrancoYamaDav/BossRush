using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerModel _m;
    Keybinds _keybinds;

    #region Commands
    ICommand forward, backward, right, left;
    #endregion
    
    private void Awake()
    {
        _m = GetComponent<PlayerModel>();
        _keybinds = new Keybinds();

        #region Commands
        forward = new Forward(transform);
        backward = new Backward(transform);
        right = new Right(transform);
        left = new Left(transform);
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        Brain();
    }

    void Brain()
    {
        if (Input.GetKey(_keybinds.forward))    forward.Execute(0.1f);       
        if (Input.GetKey(_keybinds.backward))   backward.Execute(0.1f);       
        if (Input.GetKey(_keybinds.right))      right.Execute(0.1f);       
        if (Input.GetKey(_keybinds.left))       left.Execute(0.1f);  
    }
}
