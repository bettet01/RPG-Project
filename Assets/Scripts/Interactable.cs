﻿using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 3f;

    bool isFocus = false;
    bool hasInteracted = false;
    Transform player;
    public Transform interactionTransform;

    public virtual void Interact()
    {
        // to be overwritten
        Debug.Log("Interacting with " + transform.name);
    }

    private void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
                if (distance <= radius)
                {
                    Interact();
                    hasInteracted = true;
                }
            }
        }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }


    private void OnDrawGizmosSelected()
    {

        if(interactionTransform == null)
        {
            interactionTransform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

}
