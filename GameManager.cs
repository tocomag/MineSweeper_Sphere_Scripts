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
    void Awake()
    {
        BoardFactory bFactory = new BoardFactory(stgs);
        board = bFactory.CreateBoard();
        bService = new BoardService(stgs, board);
        ViewFactory vFactory = new ViewFactory(stgs, prefab, origin);
        boardView = vFactory.CreatBoardViewAndSphere();
        vService = new ViewService(stgs, boardView);
    }
    void Update()
    {
        
    }
}