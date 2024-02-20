using System.Collections;
using System.Collections.Generic;
using TGS;
using Unity.VisualScripting;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    public enum State
    {
        IDLE,
        ATTACKING,
        ATTACKSELECT,
        ENDTURN,
        REFRESH
    }

    public State state;

    TerrainGridSystem tgs;
    private GameManager gameManager;
    private PieceProperties pieceProperties;

    public List<Color> rangeOriginalColor;

    public int attackTargetIndex;
    public int attackRange;
    public List<int> attackRangeCellList;
    public bool isAttacking = false;
    public bool hasAttack = false;


    public int currentCellIndex;


    // Start is called before the first frame update
    void Start()
    {
        tgs = TerrainGridSystem.instance;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        pieceProperties = this.gameObject.GetComponent<PieceProperties>();
        // tgsSetting = GameObject.Find("GameManager").GetComponent<TGSSetting>();

        state = State.IDLE;

        var targetPos = tgs.CellGetPosition(currentCellIndex);

        transform.position = targetPos;

        gameManager.EnemyPiece.Add(this.gameObject);


        
    }

    // Update is called once per frame
    void Update()
    {
        // 初始化敌人棋子数据（需要补充 敌人武器类型，武器级别，敌人生命值）

        // 加载武器攻击范围
        attackRange = pieceProperties.attackRange;


        switch (state)
        {
            case State.IDLE:
                tgs.CellSetGroup(currentCellIndex, TGSSetting.CELL_ENEMY);
                tgs.CellSetCanCross(currentCellIndex, false);
                break;
            case State.ATTACKING:
                if (attackRangeCellList != null)
                {
                    // CleanRange(attackRangeCellList);
                }

                // 面朝攻击对象
                transform.LookAt(tgs.CellGetPosition(attackTargetIndex));

                // 播放攻击动画
                // Animator.setbool("isAttacking", true) 然后播放动画（此处代码为例子，当具备动画时需要重新编写）

                Attack();

                state = State.ENDTURN; // 在动画完成后应该加载到动画关键帧中。那时需要删除这行代码

                gameManager.ActionCancelButton.SetActive(false);
                gameManager.PieceActionMenu.SetActive(true);
                break;
            case State.ATTACKSELECT:
                // ShowRange(attackRange);

                List<int> protentialTargetPiecesIndex = new List<int>();
                foreach(var i in attackRangeCellList)
                {
                    if (tgs.CellGetGroup(i) == TGSSetting.CELL_PLAYER)
                    {
                        protentialTargetPiecesIndex.Add(i);
                    }
                }

                int targetPieceIndex = -1;
                float lastDistance = -1f;

                foreach(var i in protentialTargetPiecesIndex)
                {
                    float distance;
                    distance = (tgs.CellGetPosition(i) - transform.position).magnitude;

                    if (lastDistance == -1)
                    {
                        lastDistance = distance;
                        targetPieceIndex = i;
                    }
                    else if (distance < lastDistance)
                    {
                        lastDistance = distance;
                        targetPieceIndex = i;
                    }
                }



                if (targetPieceIndex != -1)
                {
                    attackTargetIndex = targetPieceIndex;
                    state = State.ATTACKING;
                    break;
                }
                else
                {
                    state = State.ENDTURN;
                    CleanRange(attackRangeCellList);
                    print("Enemy find no target!");
                    break;
                }
            case State.ENDTURN:
                tgs.CellSetGroup(currentCellIndex, TGSSetting.CELL_ENEMY);
                tgs.CellSetCanCross(currentCellIndex, false);
                break;
            case State.REFRESH:
                hasAttack = false;
                state = State.IDLE;
                break;
        }

        if (pieceProperties.currentLifeValue <= 0)
        {
            tgs.CellSetGroup(currentCellIndex, TGSSetting.CELL_DEFAULT);
            tgs.CellSetCanCross(currentCellIndex, true);
            gameManager.EnemyPiece.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void OnMouseEnter()
    {
        if (gameManager.activePiece == null)
        {
            ShowRange(attackRange);
        }
        else if(gameManager.activePiece.GetComponent<PlayerContoller>().state == PlayerContoller.State.IDLE)
        {
            ShowRange(attackRange);
        }


    }

    private void OnMouseExit()
    {
        if (gameManager.activePiece == null)
        {
            CleanRange(attackRangeCellList);
        }
        else if (gameManager.activePiece.GetComponent<PlayerContoller>().state == PlayerContoller.State.IDLE)
        {
            CleanRange(attackRangeCellList);
        }
        

    }


    private void ShowRange(int range)
    {
        rangeOriginalColor = new List<Color>();
        attackRangeCellList = new List<int>();

        // Cell cell = tgs.CellGetAtPosition(transform.position, true);

        List<int> indices = new List<int>();

        tgs.CellGetNeighbours(currentCellIndex, range, indices, -1, 0, CanCrossCheckType.IgnoreCanCrossCheckOnAllCells, int.MaxValue, false, false);

        for (int i = 0; i < indices.Count; i++)
        {
            foreach (var p in gameManager.EnemyPiece)
            {
                var pieceController = p.GetComponent<EnemyController>();
                if (pieceController.currentCellIndex == indices[i])
                {
                    indices.RemoveAt(i);
                    break;
                }
            }
        }

        for (int i = 0; i < indices.Count; i++)
        {
            rangeOriginalColor.Add(tgs.CellGetColor(indices[i]));
        }

        attackRangeCellList = indices;

        tgs.CellSetColor(attackRangeCellList, new Color(1, 0, 0, 0.5f));
    }

    private void CleanRange(List<int> targetRange)
    {
        tgs.CellSetColor(targetRange, Color.clear);

        for (int i = 0; i < targetRange.Count; i++)
        {
            tgs.CellSetColor(targetRange[i], rangeOriginalColor[i]);
        }

    }

    public void Attack()
    {
        if (tgs.CellGetGroup(attackTargetIndex) == TGSSetting.CELL_PLAYER)
        {
            foreach (var i in gameManager.Group1Piece)
            {
                var pieceController = i.GetComponent<PlayerContoller>();
                if (pieceController.currentCellIndex == attackTargetIndex)
                {
                    var targetPieceProperties = i.GetComponent<PieceProperties>();

                    var damage = Random.Range(pieceProperties.minWeaponDamage, pieceProperties.maxWeaponDamage + 1);

                    if (pieceProperties.pieceWeapon == PieceProperties.Weapon.Rocket_Launcher)
                    {
                        List<int> targets = new List<int>();
                        tgs.CellGetNeighbours(attackTargetIndex, 1, targets, -1, 0, CanCrossCheckType.IgnoreCanCrossCheckOnAllCells, int.MaxValue, true, false);
                        targets.Add(attackTargetIndex);

                        foreach (var p in targets)
                        {
                            if (tgs.CellGetGroup(p) == TGSSetting.CELL_ENEMY)
                            {
                                foreach (var o in gameManager.EnemyPiece)
                                {
                                    if (o.GetComponent<EnemyController>().currentCellIndex == p)
                                    {
                                        targetPieceProperties.currentLifeValue -= damage;
                                    }
                                }
                            }
                            else if (tgs.CellGetGroup(p) == TGSSetting.CELL_PLAYER)
                            {
                                foreach (var o in gameManager.Group2Piece)
                                {
                                    if (o.GetComponent<PlayerContoller>().currentCellIndex == p)
                                    {
                                        var rocketTarget = o.GetComponent<PieceProperties>();
                                        if (rocketTarget.equipment == PieceProperties.Equipment.Bulletproof_Vest)
                                        {
                                            rocketTarget.equipmentDurability -= damage;
                                            rocketTarget.currentLifeValue -= (damage - targetPieceProperties.damageReduce);

                                        }
                                        else if (rocketTarget.equipment == PieceProperties.Equipment.Trench)
                                        {
                                            if (rocketTarget.isTrenchActive)
                                            {
                                                rocketTarget.equipmentDurability--;
                                                rocketTarget.isTrenchActive = false;
                                            }
                                            else
                                            {
                                                rocketTarget.currentLifeValue -= damage;
                                            }
                                        }
                                        else
                                        {
                                            rocketTarget.currentLifeValue -= damage;
                                        }
                                    }

                                }
                            }
                            else if (tgs.CellGetGroup(p) == TGSSetting.CELL_OBSTACLE)
                            {
                                foreach (var o in gameManager.Obstacles)
                                {
                                    if (o.GetComponent<ObstacleProperties>().currentCellIndex == p)
                                    {
                                        o.GetComponent<ObstacleProperties>().ObstacleGetDamage();
                                    }
                                }
                            }
                        }

                        // Animator.setbool("isAttacking", false)

                    }
                    else
                    {
                        if (targetPieceProperties.equipment == PieceProperties.Equipment.Bulletproof_Vest)
                        {
                            targetPieceProperties.equipmentDurability -= damage;
                            targetPieceProperties.currentLifeValue -= (damage - targetPieceProperties.damageReduce);

                        }
                        else if (targetPieceProperties.equipment == PieceProperties.Equipment.Trench)
                        {
                            if (targetPieceProperties.isTrenchActive)
                            {
                                targetPieceProperties.equipmentDurability--;
                                targetPieceProperties.isTrenchActive = false;
                            }
                            else
                            {
                                targetPieceProperties.currentLifeValue -= damage;
                            }
                        }
                        else
                        {
                            targetPieceProperties.currentLifeValue -= damage;
                        }
                    }

                    // Animator.setbool("isAttacking", false)

                    break;
                }
            }

            foreach (var i in gameManager.Group2Piece)
            {
                var pieceController = i.GetComponent<PlayerContoller>();
                if (pieceController.currentCellIndex == attackTargetIndex)
                {
                    var targetPieceProperties = i.GetComponent<PieceProperties>();

                    var damage = Random.Range(pieceProperties.minWeaponDamage, pieceProperties.maxWeaponDamage + 1);

                    if (pieceProperties.pieceWeapon == PieceProperties.Weapon.Rocket_Launcher)
                    {
                        List<int> targets = new List<int>();
                        tgs.CellGetNeighbours(attackTargetIndex, 1, targets, -1, 0, CanCrossCheckType.IgnoreCanCrossCheckOnAllCells, int.MaxValue, true, false);
                        targets.Add(attackTargetIndex);

                        foreach (var p in targets)
                        {
                            if (tgs.CellGetGroup(p) == TGSSetting.CELL_ENEMY)
                            {
                                foreach (var o in gameManager.EnemyPiece)
                                {
                                    if (o.GetComponent<EnemyController>().currentCellIndex == p)
                                    {
                                        targetPieceProperties.currentLifeValue -= damage;
                                    }
                                }
                            }
                            else if (tgs.CellGetGroup(p) == TGSSetting.CELL_PLAYER)
                            {
                                foreach (var o in gameManager.Group1Piece)
                                {
                                    if (o.GetComponent<PlayerContoller>().currentCellIndex == p)
                                    {
                                        var rocketTarget = o.GetComponent<PieceProperties>();
                                        if (rocketTarget.equipment == PieceProperties.Equipment.Bulletproof_Vest)
                                        {
                                            rocketTarget.equipmentDurability -= damage;
                                            rocketTarget.currentLifeValue -= (damage - targetPieceProperties.damageReduce);

                                        }
                                        else if (rocketTarget.equipment == PieceProperties.Equipment.Trench)
                                        {
                                            if (rocketTarget.isTrenchActive)
                                            {
                                                rocketTarget.equipmentDurability--;
                                                rocketTarget.isTrenchActive = false;
                                            }
                                            else
                                            {
                                                rocketTarget.currentLifeValue -= damage;
                                            }
                                        }
                                        else
                                        {
                                            rocketTarget.currentLifeValue -= damage;
                                        }
                                    }

                                }
                            }
                            else if (tgs.CellGetGroup(p) == TGSSetting.CELL_OBSTACLE)
                            {
                                foreach (var o in gameManager.Obstacles)
                                {
                                    if (o.GetComponent<ObstacleProperties>().currentCellIndex == p)
                                    {
                                        o.GetComponent<ObstacleProperties>().ObstacleGetDamage();
                                    }
                                }
                            }
                        }

                        // Animator.setbool("isAttacking", false)

                    }
                    else
                    {
                        if (targetPieceProperties.equipment == PieceProperties.Equipment.Bulletproof_Vest)
                        {
                            targetPieceProperties.equipmentDurability -= damage;
                            targetPieceProperties.currentLifeValue -= (damage - targetPieceProperties.damageReduce);

                        }
                        else if (targetPieceProperties.equipment == PieceProperties.Equipment.Trench)
                        {
                            if (targetPieceProperties.isTrenchActive)
                            {
                                targetPieceProperties.equipmentDurability--;
                                targetPieceProperties.isTrenchActive = false;
                            }
                            else
                            {
                                targetPieceProperties.currentLifeValue -= damage;
                            }
                        }
                        else
                        {
                            targetPieceProperties.currentLifeValue -= damage;
                        }
                    }

                    // Animator.setbool("isAttacking", false)

                    break;
                }
            }
        }

        hasAttack = true;
    }
}
