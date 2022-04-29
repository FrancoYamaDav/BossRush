using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIBossAssign : MonoBehaviour
{
    [SerializeField] Slider _hpSlider;
    public Slider hpSlider { get { return _hpSlider; } }

    [SerializeField] TMP_Text _bossName;
    public TMP_Text bossName { get { return _bossName; } }
}
