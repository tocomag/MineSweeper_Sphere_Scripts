public class Cell
{
    public int id;
    public bool isMine;
    public bool isRevealed;
    public bool isFlagged;
    public int health;
    public int aroundMineCount;
    public Cell(int id, bool isMine, bool isRevealed, bool isFlagged, int health, int aroundMineCount)
    {
        this.id = id;
        this.isMine = isMine;
        this.isRevealed = isRevealed;
        this.isFlagged = isFlagged;
        this.health = health;
        this.aroundMineCount = aroundMineCount;
    }
}