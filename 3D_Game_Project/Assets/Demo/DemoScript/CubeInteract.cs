using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInteract : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Debug.Log("Interacting with Cube");
    }
}
