using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour
{
    BaseProyectileSpawner _bps;

    // Start is called before the first frame update
    void Start()
    {
        _bps = GetComponent<BaseProyectileSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
            _bps.Shoot();
    }
}
