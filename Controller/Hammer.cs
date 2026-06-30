using UnityEngine;

public class Hammer : MonoBehaviour
{
    private BoardService bService;
    public void Init(BoardService bService)
    {
        this.bService = bService;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (bService == null)
        {
            Debug.Log("HammerクラスにBoardServiceが設定されていません");
            return;
        }
        if (!collision.gameObject.TryGetComponent<CellView>(out CellView view)) return;
        bService.Open(view.id);
    }
}