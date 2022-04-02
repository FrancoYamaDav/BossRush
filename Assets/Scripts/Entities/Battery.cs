using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour, IMagnetable
{
    MeshRenderer _meshRenderer;
    Material chargedMat, unchargedMat;

    bool _isCharged = true;
    public bool isCharged { get { return _isCharged; } }

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();

        chargedMat = Resources.Load<Material>("Materials/Charged");
        unchargedMat = Resources.Load<Material>("Materials/Discharged");
    }

    public void OnMagnetism(PlayerController pc = null)
    {
        _isCharged = false;
        ChangeMat(unchargedMat);
    }

    void ChangeMat(Material mat)
    {
        _meshRenderer.material = mat;
    }
}
