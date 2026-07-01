using UnityEngine;
[CreateAssetMenu(fileName = "BoardSettings", menuName = "Game/BoardSettings")]
public class BoardSettings : ScriptableObject
{
    public int layer;
    public int face = 6;
    public int size;
    public float radius;
    public float reductionRatioPerLayer;
    public int mines;
    public int cellHealth;
    public int playerDamage;
    public int mineDamage;
    public int availableFlags;
    public float otherAlpha = 0.1f; // 現在の層以外のマスの不透明度
    public float currentAlpha = 1.0f; // 現在の層の不透明度
    public float requiredImpactForce; // ハンマーでマスをたたくのに必要な衝撃
    public bool isRightHanded; // 右利きかどうか
}