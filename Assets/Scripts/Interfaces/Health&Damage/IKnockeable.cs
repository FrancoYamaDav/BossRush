using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKnockeable
{
    public void ReceiveKnockback(float knockbackIntensity);
}
