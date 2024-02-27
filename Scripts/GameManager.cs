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
    public GameObject Trench;
    public GameObject PieceActionMenu;
    public GameObject ActionCancelButton;

    public List<GameObject> Group1Piece = null;
    public List<GameObject> Group2Piece = null;
    public List<GameObject> EnemyPiece = null;
    public List<GameObject> Obstacles = null;
    public List<GameObject> TrenchsList = null;

    // Start is called before the first frame update
    void Start()
    {
        tgs = TerrainGridSystem.instance;
        tgsSetting = gameObject.GetComponent<TGSSetting>();
        state = State.RollMoveDice;
        PieceActionMenu.SetActive(false);
        ActionCancelButton.SetActive(false);

        GameStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (Group1Piece!= null || Group2Piece!= null) 
        {
            UpdatePieceAbility();
        }

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
        var group1ExtraWeaponDamage = 0;
        var group1ExtraMovement = 0;
        var group1ExtraLifeValue = 0;
        var group1ExtraPistolDamage = 0;
        var group1ExtraARDamage = 0;
        var group1ExtraSRDamage = 0;

        foreach (var i in Group1Piece)
        {
            var pieceProperties = i.GetComponent<PieceProperties>();

            group1ExtraPistolDamage += pieceProperties.abilityExtraPistolWeaponDamage;
            group1ExtraSRDamage += pieceProperties.abilityExtraSRWeaponDamage;
            group1ExtraARDamage += pieceProperties.abilityExtraARWeaponDamage;

            group1ExtraLifeValue += pieceProperties.abilityPlusLifeValue;
            group1ExtraMovement += pieceProperties.abilityPlusMovment;
            group1ExtraWeaponDamage += pieceProperties.abilityPlusWeaponDamage;
        }

        foreach (var i in Group1Piece)
        {
            var pieceProperties = i.GetComponent<PieceProperties>();

            pieceProperties.finalLifeValue = pieceProperties.lifeValue + group1ExtraLifeValue;
            /*if (pieceProperties.currentLifeValue < pieceProperties.finalLifeValue && pieceProperties.currentLifeValue != 0)
            {
                pieceProperties.currentLifeValue += group1ExtraLifeValue;

                if (pieceProperties.currentLifeValue > pieceProperties.finalLifeValue)
                {
                    pieceProperties.currentLifeValue = pieceProperties.finalLifeValue;
                }
            }*/


            pieceProperties.finalMovement = pieceProperties.plusMovement + group1ExtraMovement;
            pieceProperties.plusWeaponDamage = group1ExtraWeaponDamage;

            pieceProperties.plusPistolDamage = group1ExtraPistolDamage;
            pieceProperties.plusARDamage = group1ExtraARDamage;
            pieceProperties.plusSRDamage = group1ExtraSRDamage;
        }

        var group2ExtraWeaponDamage = 0;
        var group2ExtraMovement = 0;
        var group2ExtraLifeValue = 0;
        var group2ExtraPistolDamage = 0;
        var group2ExtraARDamage = 0;
        var group2ExtraSRDamage = 0;

        foreach (var i in Group2Piece)
        {
            var pieceProperties = i.GetComponent<PieceProperties>();

            group2ExtraPistolDamage += pieceProperties.abilityExtraPistolWeaponDamage;
            group2ExtraSRDamage += pieceProperties.abilityExtraSRWeaponDamage;
            group2ExtraARDamage += pieceProperties.abilityExtraARWeaponDamage;

            group2ExtraLifeValue += pieceProperties.abilityPlusLifeValue;
            group2ExtraMovement += pieceProperties.abilityPlusMovment;
            group2ExtraWeaponDamage += pieceProperties.abilityPlusWeaponDamage;
        }

        foreach (var i in Group2Piece)
        {
            var pieceProperties = i.GetComponent<PieceProperties>();

            pieceProperties.finalLifeValue = pieceProperties.lifeValue + group2ExtraLifeValue;
            /*if (pieceProperties.currentLifeValue < pieceProperties.finalLifeValue && pieceProperties.currentLifeValue != 0)
            {
                pieceProperties.currentLifeValue += group2ExtraLifeValue;

                if (pieceProperties.currentLifeValue > pieceProperties.finalLifeValue)
                {
                    pieceProperties.currentLifeValue = pieceProperties.finalLifeValue;
                }
            }*/

            pieceProperties.finalMovement = pieceProperties.plusMovement + group2ExtraMovement;
            pieceProperties.plusWeaponDamage = group2ExtraWeaponDamage;

            pieceProperties.plusPistolDamage = group2ExtraPistolDamage;
            pieceProperties.plusARDamage = group2ExtraARDamage;
            pieceProperties.plusSRDamage = group2ExtraSRDamage;
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


    public void GameStart()
    {
        PieceProperties.Profession group1PieceProfession = (PieceProperties.Profession) PlayerPrefs.GetInt("group1Profession");
        PieceProperties.Ability group1PieceAbility = (PieceProperties.Ability)PlayerPrefs.GetInt("group1Ability");

        PieceProperties.Profession group2PieceProfession = (PieceProperties.Profession)PlayerPrefs.GetInt("group2Profession");
        PieceProperties.Ability group2PieceAbility = (PieceProperties.Ability)PlayerPrefs.GetInt("group2Ability");

        GameObject group1Piece;
        GameObject group2Piece;

        GameObject group1Prefab;
        GameObject group2Prefab;

        switch (group1PieceProfession)
        {
            case PieceProperties.Profession.Cowboy:
                group1Prefab = Resources.Load<GameObject>("Prefabs/Characters/Cowboy/CowboyWithPistol");
                group1Piece = Instantiate(group1Prefab, transform.position, Quaternion.identity);

                group1Piece.transform.SetParent(GameObject.Find("Group1").transform);

                group1Piece.GetComponent<PlayerContoller>().camp = PlayerContoller.Camp.Group1;
                group1Piece.GetComponent<PieceProperties>().pieceProfession = PieceProperties.Profession.Cowboy;
                group1Piece.GetComponent<PieceProperties>().ability = group1PieceAbility;

                group1Piece.GetComponent<PlayerContoller>().initialCellIndex = tgsSetting.StartCell[0];



                break;
            case PieceProperties.Profession.Mercenary:
                group1Prefab = Resources.Load<GameObject>("Prefabs/Characters/Mercenary/MercenaryWithPistol");
                group1Piece = Instantiate(group1Prefab, transform.position, Quaternion.identity);

                group1Piece.GetComponent<PlayerContoller>().camp = PlayerContoller.Camp.Group1;
                group1Piece.GetComponent<PieceProperties>().pieceProfession = PieceProperties.Profession.Mercenary;
                group1Piece.GetComponent<PieceProperties>().ability = group1PieceAbility;

                group1Piece.transform.SetParent(GameObject.Find("Group1").transform);

                group1Piece.GetComponent<PlayerContoller>().initialCellIndex = tgsSetting.StartCell[0];
                break;
            case PieceProperties.Profession.Sniper:
                group1Prefab = Resources.Load<GameObject>("Prefabs/Characters/Sniper/SniperWithPistol");
                group1Piece = Instantiate(group1Prefab, transform.position, Quaternion.identity);

                group1Piece.GetComponent<PlayerContoller>().camp = PlayerContoller.Camp.Group1;
                group1Piece.GetComponent<PieceProperties>().pieceProfession = PieceProperties.Profession.Sniper;
                group1Piece.GetComponent<PieceProperties>().ability = group1PieceAbility;

                group1Piece.transform.SetParent(GameObject.Find("Group1").transform);

                group1Piece.GetComponent<PlayerContoller>().initialCellIndex = tgsSetting.StartCell[0];
                break;
            case PieceProperties.Profession.Engineer:
                group1Prefab = Resources.Load<GameObject>("Prefabs/Characters/Engineer/EngineerWithPistol");
                group1Piece = Instantiate(group1Prefab, transform.position, Quaternion.identity);

                group1Piece.GetComponent<PlayerContoller>().camp = PlayerContoller.Camp.Group1;
                group1Piece.GetComponent<PieceProperties>().pieceProfession = PieceProperties.Profession.Engineer;
                group1Piece.GetComponent<PieceProperties>().ability = group1PieceAbility;

                group1Piece.transform.SetParent(GameObject.Find("Group1").transform);

                group1Piece.GetComponent<PlayerContoller>().initialCellIndex = tgsSetting.StartCell[0];
                break;
        }


        switch (group2PieceProfession)
        {
            case PieceProperties.Profession.Cowboy:
                group2Prefab = Resources.Load<GameObject>("Prefabs/Characters/Cowboy/CowboyWithPistol");
                group2Piece = Instantiate(group2Prefab, transform.position, Quaternion.identity);

                group2Piece.GetComponent<PlayerContoller>().camp = PlayerContoller.Camp.Group2;
                group2Piece.GetComponent<PieceProperties>().pieceProfession = PieceProperties.Profession.Cowboy;
                group2Piece.GetComponent<PieceProperties>().ability = group2PieceAbility;

                group2Piece.transform.SetParent(GameObject.Find("Group2").transform);

                group2Piece.GetComponent<PlayerContoller>().initialCellIndex = tgsSetting.StartCell[1];
                break;
            case PieceProperties.Profession.Mercenary:
                group2Prefab = Resources.Load<GameObject>("Prefabs/Characters/Mercenary/MercenaryWithPistol");
                group2Piece = Instantiate(group2Prefab, transform.position, Quaternion.identity);

                group2Piece.GetComponent<PlayerContoller>().camp = PlayerContoller.Camp.Group2;
                group2Piece.GetComponent<PieceProperties>().pieceProfession = PieceProperties.Profession.Mercenary;
                group2Piece.GetComponent<PieceProperties>().ability = group2PieceAbility;

                group2Piece.transform.SetParent(GameObject.Find("Group2").transform);
                group2Piece.GetComponent<PlayerContoller>().initialCellIndex = tgsSetting.StartCell[1];
                break;
            case PieceProperties.Profession.Sniper:
                group2Prefab = Resources.Load<GameObject>("Prefabs/Characters/Sniper/SniperWithPistol");
                group2Piece = Instantiate(group2Prefab, transform.position, Quaternion.identity);

                group2Piece.GetComponent<PlayerContoller>().camp = PlayerContoller.Camp.Group2;
                group2Piece.GetComponent<PieceProperties>().pieceProfession = PieceProperties.Profession.Sniper;
                group2Piece.GetComponent<PieceProperties>().ability = group2PieceAbility;

                group2Piece.transform.SetParent(GameObject.Find("Group2").transform);
                group2Piece.GetComponent<PlayerContoller>().initialCellIndex = tgsSetting.StartCell[1];
                break;
            case PieceProperties.Profession.Engineer:
                group2Prefab = Resources.Load<GameObject>("Prefabs/Characters/Engineer/EngineerWithPistol");
                group2Piece = Instantiate(group2Prefab, transform.position, Quaternion.identity);

                group2Piece.GetComponent<PlayerContoller>().camp = PlayerContoller.Camp.Group2;
                group2Piece.GetComponent<PieceProperties>().pieceProfession = PieceProperties.Profession.Engineer;
                group2Piece.GetComponent<PieceProperties>().ability = group2PieceAbility;

                group2Piece.transform.SetParent(GameObject.Find("Group2").transform);

                group2Piece.GetComponent<PlayerContoller>().initialCellIndex = tgsSetting.StartCell[1];
                break;
        }
    }

}
