using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rock : MonoBehaviour
{
    [SerializeField] private Collider gogglesCol;
    [SerializeField] private InputActionProperty indexTriggerAction;
    [SerializeField] private InputActionProperty gripTriggerAction;
    public event Action<string> OnGogglesaAccessd;
    private bool isInsideGoggles;
    void OnEnable()
    {
        indexTriggerAction.action.Enable();
        gripTriggerAction.action.Enable();
    }
    void OnDisable()
    {
        indexTriggerAction.action.Disable();
        gripTriggerAction.action.Disable();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other == gogglesCol) isInsideGoggles = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other == gogglesCol) isInsideGoggles = false;
    }

    public void MoveLayer()
    {
        if (isInsideGoggles && indexTriggerAction.action.WasPressedThisFrame())
        {
            OnGogglesaAccessd?.Invoke("Go Up");
            return;
        }
        if (isInsideGoggles && gripTriggerAction.action.WasPressedThisFrame())
        {
            OnGogglesaAccessd?.Invoke("Go Down");
            return;
        }
    }
}