using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlenderMixButton : MonoBehaviour, IMouseInteractable
{
    [SerializeField]
    private Blender blender;

    public void Interact()
    {
        if (blender.isInteractable)
        {
            Events.OnStartMixing?.Invoke();
            blender.Mix();
        }
    }
}
