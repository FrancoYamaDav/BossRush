using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerAssign : MonoBehaviour
{
    [SerializeField]Slider _hpSlider, _stSlider, _proyectileSlider;

    public Slider hpSlider { get { return _hpSlider; } }

    public Slider stSlider { get { return _stSlider; } }

    public Slider proyectileSlider { get { return _proyectileSlider; } }

    [SerializeField]Image _magnet;
    public Image magnet { get { return _magnet; } }

}
