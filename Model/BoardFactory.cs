using System.Collections.Generic;
public class BoardFactory
{
    private BoardSettings stgs;
    private int[,] neighbors =
    {
        {-1,1},{0,1},{1,1},
        {-1,0},{0,0},{1,0},
        {-1,-1},{0,-1},{1,-1}
    };
    public BoardFactory(BoardSettings stgs)
    {
        this.stgs = stgs;
    }
    public Board CreateBoard()
    {
        int id = 0;
        Cell[] cells = new Cell[stgs.layer * stgs.face * stgs.size * stgs.size];
        for (int l = 0; l < stgs.layer; l++)
        {
            for (int f = 0; f < stgs.face; f++)
            {
                for (int s1 = 0; s1 < stgs.size; s1++)
                {
                    for (int s2 = 0; s2 < stgs.size; s2++)
                    {
                        // 識別番号の生成
                        id = l * stgs.face * stgs.size * stgs.size +
                             f * stgs.size * stgs.size +
                             s1 * stgs.size + s2;
                        cells[id] = new Cell(id, false, false, false, stgs.cellHealth, 0);
                    }
                }
            }
        }

        // 指定数の地雷の設置処理
        List<Cell> mines = new List<Cell>();
        while (mines.Count < stgs.mines)
        {
            int layer = UnityEngine.Random.Range(0, stgs.layer - 1);
            int face = UnityEngine.Random.Range(0, stgs.face - 1);
            int x = UnityEngine.Random.Range(0, stgs.size - 1);
            int y = UnityEngine.Random.Range(0, stgs.size - 1);
            id = ReturnID(layer, face, x, y);
            var cell = cells[id];
            if (cell.isMine) continue;
            cell.isMine = true;
            mines.Add(cell);
            // 周囲のマスの「周囲の地雷数」を+1
            // ※境界線及び角のマスの周囲のマスの取得は実装は不具合が生じる
            for (int i = 0; i < neighbors.GetLength(0); i++)
            {
                int nx = x + neighbors[i, 0];
                int ny = y + neighbors[i, 1];
                if (nx < 0 || nx >= stgs.size) continue;
                if (ny < 0 || ny >= stgs.size) continue;
                int nID = ReturnID(layer, face, nx, ny);
                var nCell = cells[nID];
                nCell.aroundMineCount++;
            }
        }

        return new Board(cells);
    }
    public List<int> TranslateID(int id)
    {
        List<int> translatedID = new List<int>(); 
        int layer = id / (stgs.face * stgs.size * stgs.size);
        int face = id % (stgs.face * stgs.size * stgs.size) / (stgs.size * stgs.size);
        int x = id % (stgs.face * stgs.size * stgs.size) % (stgs.size * stgs.size) / stgs.size;
        int y = id % (stgs.face * stgs.size * stgs.size) % (stgs.size * stgs.size) % stgs.size;
        translatedID.Add(layer);
        translatedID.Add(face);
        translatedID.Add(x);
        translatedID.Add(y);
        return translatedID;
    }
    public int ReturnID(int layerID, int faceID, int xID, int yID)
    {
        int id = layerID * stgs.face * stgs.size * stgs.size +
                 faceID * stgs.size * stgs.size +
                 xID * stgs.size + yID;
        return id;
    }
}