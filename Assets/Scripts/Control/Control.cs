using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    private const float maxRayDistance = 1000;

    [SerializeField]
    private LayerMask layerMask;

    private void CheckForMouseInteract()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxRayDistance, layerMask))
            {
                IMouseInteractable interactable = hit.collider.GetComponent<IMouseInteractable>();
                
                if (interactable != null)
                {
                    interactable.Interact();
                }
            }

        }
    }

    void Update()
    {
        CheckForMouseInteract();
    }
}
