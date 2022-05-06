using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{
    private void Awake()
    {
        GameManager temp = GetComponent<GameManager>();

        if (temp == null) new GameManager();
    }
}
