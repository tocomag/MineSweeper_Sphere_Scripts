using System;
using UnityEngine;

public class Hammer : MonoBehaviour
{    
    [SerializeField] private BoardSettings stgs;
    private bool isTouching;
    public event Action<GameObject> OnHammerHit;
    private void OnCollisionEnter(Collision collision)
    {
        // 衝撃が小さい場合は無視
        float impulse = collision.impulse.magnitude;
        if (impulse < stgs.requiredImpactForce) return;
        // 一度衝突したら、何度も衝突判定が呼ばれないようにする
        if (isTouching) return;
        isTouching = true;

        GameObject obj = collision.collider.gameObject;
        if (obj == null) return;
        OnHammerHit?.Invoke(obj); // Modelにハンマーと衝突したマスのGameObjectを通知
    }
    private void OnCollisionExit(Collision collision)
    {
        isTouching = false;
    }
}