using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BoardSettings stgs;
    public GameObject prefab;
    public GameObject origin;
    public Board board;
    public BoardService bService;
    public BoardView boardView;
    public ViewService vService;
    public Rock rock;
    public int currentLayer;
    void Awake()
    {
        rock.OnGogglesaAccessd += ChangeCurrentLayer;
        currentLayer = 0;
        BoardFactory bFactory = new BoardFactory(stgs);
        board = bFactory.CreateBoard();
        bService = new BoardService(stgs, board);
        ViewFactory vFactory = new ViewFactory(stgs, prefab, origin);
        boardView = vFactory.CreatBoardViewAndSphere();
        vService = new ViewService(stgs, boardView, bService, this);
    }
    void Update()
    {
        
    }
    private void ChangeCurrentLayer(string direction)
    {
        if (direction == "Go Up")
        {
            currentLayer++;
            return;
        }
        if (direction == "Go Down")
        {
            currentLayer--;
            return;
        }
    }
}