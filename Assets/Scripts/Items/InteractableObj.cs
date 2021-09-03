using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObj : MonoBehaviour
{
    private bool isColliding = false;
    public GameObject player;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<PlayerController>() != null)
        {
            isColliding = true;
            onOverlap();
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.GetComponent<PlayerController>() != null)
        {
            isColliding = false;
            onExit();
        }
    }

    private void Update()
    {
        if (isColliding && player.GetComponent<PlayerController>().interact)
        {
            onInteract();
        }
    }

    public virtual void onInteract() {}
    public virtual void onOverlap() {}
    public virtual void onExit() {}
}
