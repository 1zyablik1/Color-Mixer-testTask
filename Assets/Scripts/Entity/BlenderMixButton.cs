using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlenderMixButton : MonoBehaviour, IMouseInteractable
{
    [SerializeField]
    private Blender blender;

    public void Interact()
    {
        Events.OnStartMixing?.Invoke();
        blender.Mix();
    }
}
