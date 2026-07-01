using TMPro;
using UnityEngine;
public class ViewService
{
    private BoardSettings stgs;
    private BoardView boardView;
    private BoardService bService;
    private GameManager gm;
    public ViewService(BoardSettings stgs, BoardView boardView, BoardService bService, GameManager gm)
    {
        this.stgs = stgs;
        this.boardView = boardView;
        this.bService = bService;
        this.gm = gm;
        bService.OnCellProccessed += UpdateView;
        gm.OnLayerChanged += EmphasizeCurrentLayer;
    }
    // Viewに対する処理を書いていく
    private void UpdateView(Cell cell)
    {
        CellView view = boardView.views[cell.id];
        if (cell.health == 1)
        {
            // Textを壊れる直前であることを示す！マークに変える
            return;
        }
        if (cell.isMine && cell.isRevealed)
        {
            view.vMRen.enabled = false;
            view.vCol.enabled = false;
            // Textを地雷のマークに変える
            return;
        }
        if (cell.isRevealed)
        {
            view.vMRen.enabled = false;
            view.vCol.enabled = false;
            // Textを周囲の地雷数に変える
            return;
        }
        if (cell.isFlagged)
        {
            view.vMRen.enabled = false;
            view.vCol.enabled = false;
            // Textを旗のマークに変える
            return;
        }
    }
    private void EmphasizeCurrentLayer()
    {
        foreach (CellView view in boardView.views)
        {
            int layer = view.id / (stgs.face * stgs.size * stgs.size);
            if (layer != gm.currentLayer)
            {
                // 現在の層でないマスの周囲の地雷数のTextの不透明度を下げる
                Color otherColor = view.vText.color;
                otherColor.a = stgs.otherAlpha;
                view.vText.color = otherColor;
                continue;
            }
            // 現在の層のマスの不透明度をもとに戻す
            Color currentColor = view.vText.color;
            currentColor.a = stgs.currentAlpha;
            view.vText.color = currentColor;
        }
    }
}