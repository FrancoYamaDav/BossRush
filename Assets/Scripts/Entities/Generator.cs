using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour, IMagnetable
{
    MeshRenderer _meshRenderer;
    Material neutralMat, unchargedMat;

    bool _isCharged = false;
    public bool isCharged { get { return _isCharged; } }

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();

        unchargedMat = Resources.Load<Material>("Materials/Discharged");
        neutralMat = Resources.Load<Material>("Materials/Neutral");
    }

    public void OnMagnetism(PlayerController pc = null)
    {
        _isCharged = true;
        ChangeMat(neutralMat);
    }

    void ChangeMat(Material mat)
    {
        _meshRenderer.material = mat;
    }
}

