using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keybinds
{
    public KeyCode forward, backward, right, left;
    public Keybinds()
    {
        forward = KeyCode.W;
        backward = KeyCode.S;
        right = KeyCode.D;
        left = KeyCode.A;
    }
}
