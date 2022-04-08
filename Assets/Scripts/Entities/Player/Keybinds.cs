using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keybinds
{
    public KeyCode forward, backward, right, left;
    public KeyCode roll, dash;
    public KeyCode hitMelee, hitDistance;
    public KeyCode magnetism;
    public Keybinds()
    {
        //Quizas hacer un json con "saved config" y "default config", cargarlos segun setting

        //default config
        forward = KeyCode.W;
        backward = KeyCode.S;
        right = KeyCode.D;
        left = KeyCode.A;

        roll = KeyCode.LeftControl;
        dash = KeyCode.Space;

        hitMelee = KeyCode.Mouse0;
        hitDistance = KeyCode.Mouse1;

        magnetism = KeyCode.G;
    }
}
