using System.Collections;
using System.Collections.Generic;
using TGS;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    TerrainGridSystem tgs;
    public TGSSetting tgsSetting;
    public GameObject activePiece;
    public PlayerContoller.Camp activeCamp;

    public bool group1HasAct = false;
    public bool group2HasAct = false;

    public bool hasRollDice = false;

    public int group1MoveDice;
    public int group2MoveDice;

    public int attackDice;

    public int group1Gold;
    public int group2Gold;

    public int group1Coin;
    public int group2Coin;

    public enum State
    {
        RollMoveDice,
        SelectPiece,
        PieceAct,
        ChangeToOtherSide,
        EnemyAct,
        EnemyActCheck,
        NextTurn
    }

    public State state;
    public GameObject Wire;
    public GameObject PieceActionMenu;
    public GameObject ActionCancelButton;

    public List<GameObject> Group1Piece = null;
    public List<GameObject> Group2Piece = null;
    public List<GameObject> EnemyPiece = null;
    public List<GameObject> Obstacles = null;

    // Start is called before the first frame update
    void Start()
    {
        tgs = TerrainGridSystem.instance;
        tgsSetting = gameObject.GetComponent<TGSSetting>();
        state = State.RollMoveDice;
        PieceActionMenu.SetActive(false);
        ActionCancelButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            // 掷骰子阶段
            case State.RollMoveDice:
                PieceActionMenu.SetActive(false);

                RollMoveDice(); // 掷骰子来确定两组棋子的移动力
                WhoMoveFirst(); // 依据移动力高低来确定谁先移动

                state = State.SelectPiece;
                break;

            // 棋子选择阶段
            case State.SelectPiece:
                PieceActionMenu.SetActive(false);
                SelectPiece();
                break;
            
            // 选中棋子后，进行棋子行动
            case State.PieceAct:
                break;
            
            case State.ChangeToOtherSide:
                CheckGroupsEndTurnCondition();
                if (group1HasAct && group2HasAct)
                {
                    state = State.EnemyAct;
                }
                else
                {

                    if (group1HasAct)
                    {
                        activeCamp = PlayerContoller.Camp.Group2;
                    }
                    else if (group2HasAct)
                    {
                        activeCamp = PlayerContoller.Camp.Group1;
                    }

                    state = State.SelectPiece;
                }
                
                break;

            case State.EnemyAct:
                // PieceActionMenu.SetActive(false);
                foreach(var i in EnemyPiece)
                {
                    var enemyController = i.GetComponent<EnemyController>();
                    enemyController.state = EnemyController.State.ATTACKSELECT;
                }

                state = State.EnemyActCheck;
                break;
            case State.EnemyActCheck:

                bool allEnemyEndTurn = true;
                
                foreach (var i in EnemyPiece)
                {
                    var enemyController = i.GetComponent<EnemyController>();
                    if (enemyController.state != EnemyController.State.ENDTURN)
                    {
                        allEnemyEndTurn= false;
                        break;
                    }
                }

                if (allEnemyEndTurn)
                {
                    state = State.NextTurn;
                }

                break;

            // 下一回合，重置部分参数
            case State.NextTurn:
                hasRollDice = false;
                group1HasAct = false;
                group2HasAct = false;

                RefreshPiecesState();
                GetGold();

                state = State.RollMoveDice;
                break;
        }

        UpdatePieceAbility();
    }


    public void RollMoveDice()
    {
        if (!hasRollDice)
        {
            group1MoveDice = Random.Range(1, 7);
            group2MoveDice = Random.Range(1, 7);

            foreach (var i in Group1Piece)
            {
                i.GetComponent<PlayerContoller>().steps = group1MoveDice + i.GetComponent<PieceProperties>().finalMovement;

                if (i.GetComponent<PlayerContoller>().steps < 0)
                {
                    i.GetComponent<PlayerContoller>().steps = 0;
                }
            }

            foreach (var i in Group2Piece)
            {
                i.GetComponent<PlayerContoller>().steps = group2MoveDice + i.GetComponent<PieceProperties>().finalMovement;
                
                if (i.GetComponent<PlayerContoller>().steps < 0)
                {
                    i.GetComponent<PlayerContoller>().steps = 0;
                }
            }


            hasRollDice = true;
        }
    }

    /// <summary>
    /// 依据行动力判断那方先行动
    /// </summary>
    public void WhoMoveFirst()
    {
        if (group1MoveDice > group2MoveDice)
        {
            activeCamp = PlayerContoller.Camp.Group1;
        }
        else if (group2MoveDice > group1MoveDice)
        {
            activeCamp = PlayerContoller.Camp.Group2;
        }
        else if (group1MoveDice == group2MoveDice)
        {
            activeCamp = (PlayerContoller.Camp)Random.Range(0, 2);
        }
    }

    /// <summary>
    /// 鼠标选择activeCamp阵营中的棋子，使该棋子为activePiece
    /// </summary>
    public void SelectPiece()
    {
        if (Input.GetMouseButtonUp(0))
        {
            int t_cell = tgs.cellHighlightedIndex;

            // 判定玩家分组为玩家1或玩家2
            if (activeCamp == PlayerContoller.Camp.Group1)
            {
                foreach (var i in Group1Piece)
                {
                    var targetPieceController = i.GetComponent<PlayerContoller>();
                    if (t_cell == targetPieceController.currentCellIndex)
                    {
                        if (targetPieceController.state != PlayerContoller.State.WAITFORNEXTTURN)
                        {
                            state = State.PieceAct;
                            activePiece = i.gameObject;
                            PieceActionMenu.SetActive(true);
                            break;
                        }       
                    }

                }

            }
            else
            {
                foreach (var i in Group2Piece)
                {
                    var targetPieceController = i.GetComponent<PlayerContoller>();
                    if (t_cell == targetPieceController.currentCellIndex)
                    {
                        if (targetPieceController.state != PlayerContoller.State.WAITFORNEXTTURN)
                        {                         
                            state = State.PieceAct;
                            activePiece = i.gameObject;
                            PieceActionMenu.SetActive(true);
                            break;
                        }
                    }

                }
            }
        }
    }

    /// <summary>
    /// 检查两个组的回合结束情况
    /// </summary>
    public void CheckGroupsEndTurnCondition()
    {
        foreach (var i in Group1Piece)
        {
            if (i.GetComponent<PlayerContoller>().state != PlayerContoller.State.WAITFORNEXTTURN)
            {
                group1HasAct = false;
                break;
            }
            else
            {
                group1HasAct = true;
            }

        }

        foreach (var i in Group2Piece)
        {
            if (i.GetComponent<PlayerContoller>().state != PlayerContoller.State.WAITFORNEXTTURN)
            {
                group2HasAct = false;
                break;
            }
            else
            {
                group2HasAct = true;
            }
        }
    }

    public void RefreshPiecesState()
    {
        foreach (var i in Group1Piece)
        {
            i.GetComponent<PlayerContoller>().state = PlayerContoller.State.REFRESH;

        }

        foreach (var i in Group2Piece)
        {
            i.GetComponent<PlayerContoller>().state = PlayerContoller.State.REFRESH;
        }

        foreach(var i in EnemyPiece)
        {
            i.GetComponent<EnemyController>().state= EnemyController.State.REFRESH;
        }
    }


    public void UpdatePieceAbility()
    {
        if (this.gameObject.GetComponent<PlayerContoller>() != null)
        {
            var pieceCamp = this.gameObject.GetComponent<PlayerContoller>().camp;

            switch (pieceCamp)
            {
                case PlayerContoller.Camp.Group1:


                    break;
                case PlayerContoller.Camp.Group2:


                    break;

            }
        }
    }

    public void GetGold()
    {
        foreach (var i in Group1Piece)
        {
            var goldOutPut = i.GetComponent<PieceProperties>().goldOutPutSpeed;

            if (tgsSetting.GoldMinerCells.Contains(i.GetComponent<PlayerContoller>().currentCellIndex))
            {
                group1Gold += goldOutPut;
                i.GetComponent<PlayerContoller>().isInGoldMine = true;
            }
            else
            {
                i.GetComponent<PlayerContoller>().isInGoldMine = false;
            }  
        }

        foreach (var i in Group2Piece)
        {
            var goldOutPut = i.GetComponent<PieceProperties>().goldOutPutSpeed;
            if (tgsSetting.GoldMinerCells.Contains(i.GetComponent<PlayerContoller>().currentCellIndex))
            {
                group2Gold += goldOutPut; 
                i.GetComponent<PlayerContoller>().isInGoldMine = true;
            }
            else
            {
                i.GetComponent<PlayerContoller>().isInGoldMine = false;
            }
        }
    }

}
