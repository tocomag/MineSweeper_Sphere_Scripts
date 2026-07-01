using System;
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
    public HandAction handAction;
    public Hammer hammer;
    public int currentLayer { get; private set; }
    public event Action OnLayerChanged;
    void Awake()
    {
        currentLayer = 0;
        handAction.OnGogglesAccessed += ChangeCurrentLayer;
        BoardFactory bFactory = new BoardFactory(stgs);
        board = bFactory.CreateBoard();
        bService = new BoardService(stgs, board, hammer);
        ViewFactory vFactory = new ViewFactory(stgs, prefab, origin);
        boardView = vFactory.CreatBoardViewAndSphere();
        vService = new ViewService(stgs, boardView, bService, this);
    }
    void Update()
    {
        handAction.MoveLayer();
    }
    private void ChangeCurrentLayer(string direction)
    {
        if (direction == "Go Up") currentLayer++;
        if (direction == "Go Down") currentLayer--;
        currentLayer = Mathf.Max(0, currentLayer);
        currentLayer = Mathf.Min(currentLayer, stgs.layer - 1);
        OnLayerChanged?.Invoke(); // Viewに現在の層が変わったことを通知
    }
}