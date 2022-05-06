using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetDashable : Magnetable, IUpdate
{
    [SerializeField] GameObject waypoint;
    float distance = 0.2f, distanceLimit = 0.1f;
    float cooldownTime = 0.5f;

    float magnetForce = 75;

    PlayerController _pc;

    public override void OnMagnetism(PlayerController pc = null)
    {
        if (pc == null) return;

        _pc = pc;
        isBeingUsed = true;
        distance = Vector3.Distance(_pc.transform.position, waypoint.transform.position);
        _interactable = false;
        UpdateManager.Instance.AddToUpdate(this);
    }

    public void OnUpdate()
    {
        if (isBeingUsed)
        {
            _pc.transform.position = Vector3.MoveTowards(_pc.transform.position, waypoint.transform.position, magnetForce * Time.deltaTime);
            _pc.isDashing = true;

            distance = Vector3.Distance(_pc.transform.position, waypoint.transform.position);

            if (distance <= distanceLimit)
            {
                isBeingUsed = false;
                StartCoroutine(Cooldown());
                UpdateManager.Instance.RemoveFromUpdate(this);
            }
        }
    }

    public override void OnExit(){}

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        _interactable = true;
        distance = 0.2f;
        StopCoroutine(Cooldown());
    } 
}
