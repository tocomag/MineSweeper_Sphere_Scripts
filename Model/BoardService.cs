using System;
using UnityEngine;
public class BoardService
{
    private BoardSettings stgs;
    private Board board;
    private int usedFlags;
    private Hammer hammer;
    public event Action<Cell> OnCellProccessed;
    public BoardService(BoardSettings stgs, Board board, Hammer hammer)
    {
        this.stgs = stgs;
        this.board = board;
        this.hammer = hammer;
        hammer.OnHammerHit += Open;
    }
    public void Open(GameObject obj)
    {
        CellView view = obj.GetComponent<CellView>();
        if (view == null) return;
        int id = view.id;
        var cell = board.cells[id];
        if (cell.isFlagged) return;
        if (cell.isRevealed) return;
        if (id / (stgs.face * stgs.size * stgs.size) != 0)
        {
            // 開けようとするマスの上のマスが開いていないと開けられない
            int upperId = id - stgs.face * stgs.size * stgs.size;
            var upperCell = board.cells[upperId];
            if (!upperCell.isRevealed) return;
        }
        GetDamaged(cell, stgs.playerDamage);

        if (cell.isRevealed && cell.isMine)
        {
            Debug.Log("地雷が開けられました");
            // 地雷を開いたときの処理
            /*
               地雷を開いたときは、周囲８マスと下層９マスにダメージを与える
               地雷は上の層が開いていないマスにはダメージを与えられない
               「周囲のマスの取得」「周囲の８マスの上のマスの判定」
            */
            int lowerID = id + stgs.face * stgs.size * stgs.size;
            Cell lowerCell = board.cells[lowerID];
            GetDamaged(lowerCell, stgs.mineDamage);
            foreach (GameObject aroundObj in view.neighbors)
            {
                CellView aroundView = aroundObj.GetComponent<CellView>();
                if (aroundView == null) continue;
                int aroundID = aroundView.id;
                Cell aroundCell = board.cells[aroundID];
                if (aroundID / (stgs.face * stgs.size * stgs.size) != 0)
                {
                    // 開けようとするマスの上のマスが開いていないと開けられない
                    int upperID = aroundID - stgs.face * stgs.size * stgs.size;
                    Cell upperCell = board.cells[upperID];
                    if (!upperCell.isRevealed) continue;
                }
                GetDamaged(aroundCell, stgs.mineDamage);
                int aroundLowerID = aroundID + stgs.face * stgs.size * stgs.size;
                if (aroundLowerID > stgs.face * stgs.size * stgs.size * stgs.layer - 1)
                {
                    // 下層が存在しないマスで地雷を開けると、スコアを減らす処理を追加する
                    continue;
                }
                Cell aroundLowerCell = board.cells[aroundLowerID];
                GetDamaged(aroundLowerCell, stgs.mineDamage);
            }
        }
        OnCellProccessed?.Invoke(cell); // Viewに処理したマスの状態を通知
    }
    public void SetFlag(GameObject obj)
    {
        CellView view = obj.GetComponent<CellView>();
        if (view == null) return;
        int id = view.id;
        var cell = board.cells[id];
        if (cell.isFlagged) return;
        if (cell.isRevealed) return;
        if (usedFlags >= stgs.availableFlags) return;
        cell.isFlagged = true;
        usedFlags++;
        OnCellProccessed?.Invoke(cell); // Viewに処理したマスの状態を通知
    }
    public void GetDamaged(Cell cell, int damage)
    {
        if (cell.health <= 0) return;
        cell.health -= damage;
        if (cell.health <= 0) cell.isRevealed = true;
        OnCellProccessed?.Invoke(cell); // Viewに処理したマスの状態を通知
    }
}