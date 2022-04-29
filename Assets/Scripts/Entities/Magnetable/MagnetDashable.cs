using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetDashable : Magnetable
{
    [SerializeField] GameObject waypoint;
    [SerializeField] float distance = 0.2f;

    float magnetForce = 75;

    public override void OnMagnetism(PlayerController pc = null)
    {
        if (pc != null && Vector3.Distance(transform.position, pc.transform.position) >= 1f)
        { 
            pc.transform.position = Vector3.MoveTowards(pc.transform.position, waypoint.transform.position, magnetForce * Time.deltaTime);
            pc.isDashing = true;

            distance = Vector3.Distance(pc.transform.position, waypoint.transform.position);

            if (distance <= 0.1f) _interactable = false;
        }
    }

    public override void OnExit()
    {
        distance = 0.2f;
        StartCoroutine(Cooldown());        
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.35f);
        _interactable = true;
        StopCoroutine(Cooldown());
    }
}
