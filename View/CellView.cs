using TMPro;
using UnityEngine;
using System.Collections.Generic;
public class CellView : MonoBehaviour
{
    public int id;
    public Collider vCol;
    public MeshRenderer vMRen;
    public TextMeshProUGUI vText;
    public List<GameObject> neighbors = new List<GameObject>(); // 周囲8マスを格納する配列
    public void SetNeighbor(GameObject obj)
    {
        if (neighbors.Count >= 8) return;
        neighbors.Add(obj);
    }
}