using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keybinds
{
    public KeyCode forward, backward, right, left, roll;
    public Keybinds()
    {
        //Quizas hacer un json con "saved config" y "default config", cargarlos segun setting

        //default config
        forward = KeyCode.W;
        backward = KeyCode.S;
        right = KeyCode.D;
        left = KeyCode.A;
        roll = KeyCode.Space;
    }
}
