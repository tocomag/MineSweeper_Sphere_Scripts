using UnityEngine;
public class BoardService
{
    private BoardSettings stgs;
    private Board board;
    private int usedFlags;
    public BoardService(BoardSettings stgs, Board board)
    {
        this.stgs = stgs;
        this.board = board;
    }
    public void Open(int id)
    {
        var cell = board.cells[id];
        if (cell.isFlagged) return;
        if (cell.isRevealed) return;
        // 開けようとするマスの上のマスが開いていないと開けられない
        if (id / stgs.face * stgs.size * stgs.size != 0)
        {
            int upperId = id + stgs.face * stgs.size * stgs.size;
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
        }
    }
    public void SetFlag(int id)
    {
        var cell = board.cells[id];
        if (cell.isFlagged) return;
        if (cell.isRevealed) return;
        if (usedFlags >= stgs.availableFlags) return;
        cell.isFlagged = true;
        usedFlags++;
    }
    public void GetDamaged(Cell cell, int damage)
    {
        if (cell.health <= 0) return;
        cell.health -= damage;
        if (cell.health <= 0) cell.isRevealed = true;
    }
    // 盤面の内部データに対する処理を書いていく
}