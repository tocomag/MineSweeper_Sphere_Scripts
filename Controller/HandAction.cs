using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAction : MonoBehaviour // 手の動作を管理するクラス
{
    [SerializeField] private Collider gogglesRightCol;
    [SerializeField] private Collider gogglesLeftCol;
    [SerializeField] private InputActionProperty indexTriggerAction;
    [SerializeField] private InputActionProperty gripTriggerAction;
    [SerializeField] private BoardSettings stgs;
    private bool isInsideGoggles;
    public event Action<String> OnGogglesAccessed;
    void Start()
    {
        if (stgs.isRightHanded) gogglesLeftCol.enabled = false;
        if (!stgs.isRightHanded) gogglesRightCol.enabled = false;
    }
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
        if (other == gogglesRightCol) isInsideGoggles = true;
        if (other == gogglesLeftCol) isInsideGoggles = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other == gogglesRightCol) isInsideGoggles = false;
        if (other == gogglesLeftCol) isInsideGoggles = false;
    }

    public void MoveLayer()
    {
        if (isInsideGoggles && indexTriggerAction.action.WasPressedThisFrame())
        {
            OnGogglesAccessed?.Invoke("Go Up"); // GameManagerに現在の層を上げるように通知
            return;
        }
        if (isInsideGoggles && gripTriggerAction.action.WasPressedThisFrame())
        {
            OnGogglesAccessed?.Invoke("Go Down"); // GameManagerに現在の層を下げるように通知
            return;
        }
    }
}