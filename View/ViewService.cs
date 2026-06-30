public class ViewService
{
    private BoardView boardView;
    private BoardSettings stgs;
    private BoardService bService;
    public ViewService(BoardSettings stgs, BoardView boardView, BoardService bService)
    {
        this.stgs = stgs;
        this.boardView = boardView;
        this.bService = bService;
        bService.OnCellProccessed += UpdateView;
    }
    // Viewに対する処理を書いていく
    private void UpdateView(Cell cell)
    {
        CellView view = boardView.views[cell.id];
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
}