using System.Collections.Generic;

public class BoardService
{
    private BoardSettings stgs;
    private Board board;
    public BoardService(BoardSettings stgs, Board board)
    {
        this.stgs = stgs;
        this.board = board;
    }
    // 盤面の内部データに対する処理を書いていく
}