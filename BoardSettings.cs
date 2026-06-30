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
}