using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMagnetable
{
    public void OnMagnetism(PlayerController pc = null);
    public void OnExit();
}
