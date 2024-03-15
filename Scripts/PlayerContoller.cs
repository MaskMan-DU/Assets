using System.Collections;
using System.Collections.Generic;
using TGS;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    public enum State
    {
        IDLE,
        ATTACKING,
        ATTACKSELECT,
        USEEQUIPMENT,
        EQUIPMENTPOSITONSELECT,
        MOVING,
        MOVESELECT,
        ENDTURN,
        WAITFORNEXTTURN,
        REFRESH
    }

    public enum Camp
    {
        Group1,
        Group2,
    }

    public Camp camp;

    public State state;

    TerrainGridSystem tgs;
    private GameManager gameManager;
    private PieceProperties pieceProperties;
    private TGSSetting tgsSetting;
    private ChangeWeapon changeWeapon;

    public List<Color> rangeOriginalColor;

    public int steps;
    public List<int> moveRange;
    private List<int> moveList;
    private short moveCounter;

    public int attackTargetIndex;
    public int attackRange;
    public List<int> attackRangeCellList;
    public bool isAttacking = false;
    public bool hasAttack = false;

    public int equipmentTargetIndex;
    public int equipmentRange;
    public List<int> equipmentRangeCellList;
    public bool usedEquipment = false;

    public int initialCellIndex;
    public int currentCellIndex;

    public bool isInGoldMine = false;
    public Animator animator;
    private ParticleSystem shootFire;


    // Use this for initialization
    private void Start()
    {
        tgs = TerrainGridSystem.instance;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        pieceProperties = this.gameObject.GetComponent<PieceProperties>();
        changeWeapon = this.gameObject.GetComponent<ChangeWeapon>();
        tgsSetting = GameObject.Find("GameManager").GetComponent<TGSSetting>();
        shootFire = changeWeapon.findShootFire;

        state = State.IDLE;

        var targetPos = tgs.CellGetPosition(initialCellIndex);

        transform.position = targetPos;

        

        // 绑定棋子阵营
        if (camp == Camp.Group1)
        {
            gameManager.Group1Piece.Add(this.gameObject);
        }
        else
        {
            gameManager.Group2Piece.Add(this.gameObject);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        shootFire = changeWeapon.findShootFire;
        // 加载武器攻击范围
        attackRange = pieceProperties.attackRange;
        equipmentRange = pieceProperties.equipmentRange;

        currentCellIndex = tgs.CellGetIndex(transform.position,true);

        

        switch (state)
        {
            case State.IDLE:
                tgs.CellSetGroup(currentCellIndex, TGSSetting.CELL_PLAYER);
                tgs.CellSetCanCross(currentCellIndex, false);
                gameManager.GoldMineCheck();
                break;

            case State.MOVING:
                animator.SetBool("isRunning", true);
                gameManager.ActionCancelButton.SetActive(false);

                if (moveCounter < moveList.Count)
                {
                    Move(tgs.CellGetPosition(moveList[moveCounter]));
                }
                else
                {
                    moveCounter = 0;
                    animator.SetBool("isRunning", false);
                    state = State.IDLE;

                    
                    
                    gameManager.PieceActionMenu.SetActive(true);
                }

                if (moveRange != null)
                {
                    CleanRange(moveRange);
                }

                // 如果该棋子步数为0，并且已经攻击过或者使用过道具，则该棋子回合结束
                if (steps == 0 && hasAttack)
                {
                    animator.SetBool("isRunning", false);
                    state = State.ENDTURN;
                }
                break;

            case State.MOVESELECT:
                if (Input.GetMouseButtonUp(0))
                {
                    //gets path when left mouse is released and over terrain
                    int t_cell = tgs.cellHighlightedIndex;

                    tgs.CellFadeOut(t_cell, Color.green);

                    if (t_cell != -1)
                    {
                        if (moveRange.Contains(t_cell))
                        {
                            //checks if we selected a cell
                            int startCell = tgs.CellGetIndex(tgs.CellGetAtPosition(transform.position, true));

                            float totalCost;

                            moveList = tgs.FindPath(startCell, t_cell, out totalCost, maxSteps: steps, includeInvisibleCells: false);

                            if (moveList == null)
                                return;

                            moveCounter = 0;

                            state = State.MOVING;
                        }
                    }
                    else
                    {
                        Debug.Log("No Cell");
                    }
                }
                break;
            case State.ATTACKING:
                if (attackRangeCellList != null)
                {
                    CleanRange(attackRangeCellList);
                }

                // 面朝攻击对象
                transform.LookAt(tgs.CellGetPosition(attackTargetIndex));

                PlayEffect();
                // 播放攻击动画
                // Animator.setbool("isAttacking", true) 然后播放动画（此处代码为例子，当具备动画时需要重新编写）

                Attack();

                state = State.IDLE; // 在动画完成后应该加载到动画关键帧中。那时需要删除这行代码

                gameManager.ActionCancelButton.SetActive(false);
                gameManager.PieceActionMenu.SetActive(true);

                // 如果该棋子步数为0，并且已经攻击过或者使用过道具，则该棋子回合结束
                if (steps == 0 && hasAttack)
                {
                    state = State.ENDTURN;
                }
                break;
            case State.ATTACKSELECT: 
                if (Input.GetMouseButtonUp(0))
                {
                    int t_cell = tgs.cellHighlightedIndex;

                    if (attackRangeCellList.Contains(t_cell))
                    {
                        if (pieceProperties.pieceWeapon == PieceProperties.Weapon.Rocket_Launcher)
                        {
                            attackTargetIndex= t_cell;
                            state = State.ATTACKING;
                        }
                        else
                        {
                            if (tgs.CellGetGroup(t_cell) == TGSSetting.CELL_ENEMY)
                            {
                                attackTargetIndex = t_cell;
                                state = State.ATTACKING;
                            }
                            else if (tgs.CellGetGroup(t_cell) == TGSSetting.CELL_PLAYER)
                            {
                                switch (camp)
                                {
                                    case Camp.Group1:
                                        foreach (var i in gameManager.Group2Piece)
                                        {
                                            var pieceController = i.GetComponent<PlayerContoller>();
                                            if (pieceController.currentCellIndex == t_cell)
                                            {
                                                attackTargetIndex = t_cell;
                                                state = State.ATTACKING;
                                                break;
                                            }
                                        }
                                        break;
                                    case Camp.Group2:
                                        foreach (var i in gameManager.Group1Piece)
                                        {
                                            var pieceController = i.GetComponent<PlayerContoller>();
                                            if (pieceController.currentCellIndex == t_cell)
                                            {
                                                attackTargetIndex = t_cell;
                                                state = State.ATTACKING;
                                                break;
                                            }
                                        }
                                        break;
                                }
                            }else 
                            {
                                foreach(var i in gameManager.TrenchsList)
                                {
                                    if (t_cell == i.GetComponent<TrenchProperties>().currentCellIndex)
                                    {
                                        attackTargetIndex= t_cell;
                                        state = State.ATTACKING;
                                        break;
                                    }
                                }
                            }
                        }
                                    
                    }
                }
                break;
            case State.USEEQUIPMENT:
                gameManager.ActionCancelButton.SetActive(false);

                if (equipmentRangeCellList != null)
                {
                    CleanRange(equipmentRangeCellList);
                }

                // 在equipmentTargetIndex上复制一个Wire
                Vector3 targetPos = tgs.CellGetPosition(equipmentTargetIndex, true);
                Instantiate(gameManager.Wire, targetPos, Quaternion.identity);

                pieceProperties.equipmentDurability--;

                usedEquipment = true;

                gameManager.PieceActionMenu.SetActive(true);
                state = State.IDLE;

                break;
            case State.EQUIPMENTPOSITONSELECT:
                if (pieceProperties.equipment == PieceProperties.Equipment.First_Aid_Kit)
                {
                    pieceProperties.First_Aid_Kit();
                    usedEquipment = true;
                    state = State.IDLE;
                }else if (pieceProperties.equipment == PieceProperties.Equipment.Trench) 
                {
                    pieceProperties.Trench();
                    usedEquipment = true;
                    state = State.IDLE;
                }else if (pieceProperties.equipment == PieceProperties.Equipment.Wire)
                {
                    if (Input.GetMouseButtonUp(0))
                    {
                        int t_cell = tgs.cellHighlightedIndex;
                        if (equipmentRangeCellList.Contains(t_cell))
                        {
                            equipmentTargetIndex = t_cell;
                            state = State.USEEQUIPMENT;
                            break;
                        }
                    }
                }
                break;

            case State.ENDTURN:
                gameManager.activePiece = null;
                state = State.WAITFORNEXTTURN;

                gameManager.state = GameManager.State.ChangeToOtherSide;
                break;
                

            case State.WAITFORNEXTTURN:
                tgs.CellSetGroup(currentCellIndex, TGSSetting.CELL_PLAYER);
                tgs.CellSetCanCross(currentCellIndex, false);

                break;
            case State.REFRESH:
                hasAttack = false;
                usedEquipment = false;

                state = State.IDLE;
                break;
        }

        if (pieceProperties.currentLifeValue <= 0) 
        {
            state = State.ENDTURN;
            tgs.CellSetGroup(currentCellIndex, TGSSetting.CELL_DEFAULT);
            tgs.CellSetCanCross(currentCellIndex, true);
            this.transform.position = tgs.CellGetPosition(gameManager.PiecePosCheck(camp));
            pieceProperties.currentLifeValue = pieceProperties.finalLifeValue;
        } 
    }

    void Move(Vector3 in_vec)
    {
        float speed = 50f;
        float step = speed * Time.deltaTime;

        // target position must account the sphere height since the cellGetPosition will return the center of the cell which is at floor.
        transform.position = Vector3.MoveTowards(transform.position, in_vec, step);
        transform.LookAt(in_vec);

        // Check if character has reached next cell (we use a small threshold to avoid floating point comparison; also we check only xz plane since the character y position could be adjusted or limited
        // by the slope of the terrain).
        float dist = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(in_vec.x, in_vec.z));
        if (dist <= 0.1f)
        {
            moveCounter++;
            steps--;
        }
    }

    public void ShowRange(int range)
    {
        rangeOriginalColor = new List<Color>();
        moveRange = new List<int>();
        attackRangeCellList = new List<int>();
        equipmentRangeCellList = new List<int>();

        Cell cell = tgs.CellGetAtPosition(transform.position, true);

        List<int> indices = new List<int>();

        if (state == State.MOVESELECT) 
        {
            if (!isInGoldMine && gameManager.GoldMineHasPiece)
            {
                indices = tgs.CellGetNeighbours(cell, range, TGSSetting.CELLS_ALL_NAVIGATABLE);
                foreach(var i in tgsSetting.GoldMinerCells)
                {
                    if (indices.Contains(i)) 
                    { 
                        indices.Remove(i);
                    }
                }
            }
            else
            {
                indices = tgs.CellGetNeighbours(cell, range, TGSSetting.CELLS_ALL_NAVIGATABLE);
            }
            

            for (int i = 0; i < indices.Count; i++)
            {
                rangeOriginalColor.Add(tgs.CellGetColor(indices[i]));
            }

            moveRange = indices;

            tgs.CellSetColor(moveRange, new Color(0, 0, 1, 0.5f));          
        }
        else if (state == State.ATTACKSELECT)
        {
            tgs.CellGetNeighbours(currentCellIndex, range, indices, -1, 0,  CanCrossCheckType.IgnoreCanCrossCheckOnAllCells, int.MaxValue, true, false);

            for (int i = 0; i < indices.Count;i++)
            {
                if (tgs.CellGetGroup(indices[i]) == TGSSetting.CELL_PLAYER)
                {
                    switch (camp)
                    {
                        case Camp.Group1:
                            foreach (var p in gameManager.Group1Piece)
                            {
                                var pieceController = p.GetComponent<PlayerContoller>();
                                if (pieceController.currentCellIndex == indices[i])
                                {
                                    indices.RemoveAt(i);
                                    break;
                                }
                            }
                            break;
                        case Camp.Group2:
                            foreach (var p in gameManager.Group2Piece)
                            {
                                var pieceController = p.GetComponent<PlayerContoller>();

                                if (pieceController.currentCellIndex == indices[i])
                                {
                                    indices.RemoveAt(i);
                                    break;
                                }
                            }
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
        else if (pieceProperties.equipment == PieceProperties.Equipment.Wire && state == State.EQUIPMENTPOSITONSELECT)
        {
            indices = tgs.CellGetNeighbours(cell, equipmentRange, TGSSetting.CELLS_ALL_NAVIGATABLE);

            for (int i = 0; i < indices.Count; i++)
            {
                rangeOriginalColor.Add(tgs.CellGetColor(indices[i]));
            }

            equipmentRangeCellList = indices;

            tgs.CellSetColor(equipmentRangeCellList, new Color(0, 1, 0, 0.5f));
        }    
    }

    public void CleanRange(List<int> targetRange)
    {
        tgs.CellSetColor(targetRange, Color.clear);

        for (int i = 0; i < targetRange.Count; i++)
        {
            tgs.CellSetColor(targetRange[i], rangeOriginalColor[i]);
        }

    }

    public void ResetCellCross()
    {
        tgs.CellSetGroup(currentCellIndex, TGSSetting.CELL_DEFAULT);
        tgs.CellSetCanCross(currentCellIndex, true);
    }

    public void Attack()
    {
        if (pieceProperties.pieceWeapon == PieceProperties.Weapon.Rocket_Launcher)
        {
            var damage = Random.Range(pieceProperties.finalminWeaponDamage, pieceProperties.finalmaxWeaponDamage + 1);

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
                            o.GetComponent<PieceProperties>().currentLifeValue -= damage;

                            if (o.GetComponent<PieceProperties>().currentLifeValue <= 0)
                            {
                                if (camp == Camp.Group1)
                                {
                                    gameManager.group1Coin += (o.GetComponent<PieceProperties>().enemyCoin + pieceProperties.extraEnemyCoin);
                                }

                                if (camp == Camp.Group2)
                                {
                                    gameManager.group2Coin += (o.GetComponent<PieceProperties>().enemyCoin + pieceProperties.extraEnemyCoin);
                                }
                            }
                        }
                    }
                }
                else if (tgs.CellGetGroup(p) == TGSSetting.CELL_PLAYER)
                {
                    switch (camp)
                    {
                        case Camp.Group1:
                            foreach (var o in gameManager.Group2Piece)
                            {
                                if (o.GetComponent<PlayerContoller>().currentCellIndex == p)
                                {
                                    var rocketTarget = o.GetComponent<PieceProperties>();

                                    if (rocketTarget.isTrenchActive) // 首先判断是否无敌
                                    {
                                        foreach (var i in gameManager.TrenchsList) // 找到位于该Cell的Trench
                                        {
                                            if (i.GetComponent<TrenchProperties>().currentCellIndex == p)
                                            {
                                                i.GetComponent<TrenchProperties>().TrenchGetDamage();
                                                break;
                                            }
                                        }

                                        rocketTarget.isTrenchActive = false;
                                    }
                                    else // 如果不无敌，是否有
                                    {
                                        if (rocketTarget.equipment == PieceProperties.Equipment.Bulletproof_Vest)
                                        {
                                            rocketTarget.equipmentDurability -= damage;
                                            rocketTarget.currentLifeValue -= (damage - rocketTarget.damageReduce);

                                        }
                                        else
                                        {
                                            rocketTarget.currentLifeValue -= damage;
                                        }                               
                                    }
                                    
                                }

                            }
                            break;

                        case Camp.Group2:
                            foreach (var o in gameManager.Group1Piece)
                            {
                                if (o.GetComponent<PlayerContoller>().currentCellIndex == p)
                                {
                                    var rocketTarget = o.GetComponent<PieceProperties>();
                                    if (rocketTarget.isTrenchActive) // 首先判断是否无敌
                                    {
                                        foreach (var i in gameManager.TrenchsList) // 找到位于该Cell的Trench
                                        {
                                            if (i.GetComponent<TrenchProperties>().currentCellIndex == p)
                                            {
                                                i.GetComponent<TrenchProperties>().TrenchGetDamage();
                                                break;
                                            }
                                        }

                                        rocketTarget.isTrenchActive = false;
                                    }
                                    else // 如果不无敌，是否有
                                    {
                                        if (rocketTarget.equipment == PieceProperties.Equipment.Bulletproof_Vest)
                                        {
                                            rocketTarget.equipmentDurability -= damage;
                                            rocketTarget.currentLifeValue -= (damage - rocketTarget.damageReduce);

                                        }
                                        else
                                        {
                                            rocketTarget.currentLifeValue -= damage;
                                        }
                                    }
                                }

                            }
                            break;
                    }
                         
                    
                }
                else if (tgs.CellGetGroup(p) == TGSSetting.CELL_WIRE)
                {
                    foreach (var o in gameManager.Obstacles)
                    {
                        if (o.GetComponent<ObstacleProperties>().currentCellIndex == p)
                        {
                            o.GetComponent<ObstacleProperties>().ObstacleGetDamage();
                        }
                    }
                }else
                {
                    foreach (var i in gameManager.TrenchsList)
                    {
                        if (p == i.GetComponent<TrenchProperties>().currentCellIndex)
                        {
                            i.GetComponent<TrenchProperties>().TrenchGetDamage();
                            break;
                        }
                    }
                }
            }

            // Animator.setbool("isAttacking", false)
        }
        else
        {
            if (tgs.CellGetGroup(attackTargetIndex) == TGSSetting.CELL_ENEMY)
            {
                foreach (var i in gameManager.EnemyPiece)
                {
                    var enemyController = i.GetComponent<EnemyController>();
                    if (enemyController.currentCellIndex == attackTargetIndex)
                    {
                        var targetPieceProperties = i.GetComponent<PieceProperties>();

                        var damage = Random.Range(pieceProperties.finalminWeaponDamage, pieceProperties.finalmaxWeaponDamage + 1);

                        print(damage);

                        targetPieceProperties.currentLifeValue -= damage;

                        print(targetPieceProperties.currentLifeValue);
                        if (targetPieceProperties.currentLifeValue <= 0)
                        {
                            if (camp == Camp.Group1)
                            {
                                gameManager.group1Coin += (targetPieceProperties.enemyCoin + pieceProperties.extraEnemyCoin);
                            }

                            if (camp == Camp.Group2)
                            {
                                gameManager.group2Coin += (targetPieceProperties.enemyCoin + pieceProperties.extraEnemyCoin);
                            }
                        }

                        // Animator.setbool("isAttacking", false)

                        break;
                    }
                }
            }
            else if (tgs.CellGetGroup(attackTargetIndex) == TGSSetting.CELL_PLAYER)
            {
                switch (camp)
                {
                    case Camp.Group1:
                        foreach (var i in gameManager.Group2Piece)
                        {
                            var pieceController = i.GetComponent<PlayerContoller>();
                            if (pieceController.currentCellIndex == attackTargetIndex)
                            {
                                var targetPieceProperties = i.GetComponent<PieceProperties>();
                                var damage = Random.Range(pieceProperties.finalminWeaponDamage, pieceProperties.finalmaxWeaponDamage + 1);

                                if (targetPieceProperties.isTrenchActive) // 首先判断是否无敌
                                {
                                    foreach (var o in gameManager.TrenchsList) // 找到位于该Cell的Trench
                                    {
                                        if (o.GetComponent<TrenchProperties>().currentCellIndex == attackTargetIndex)
                                        {
                                            o.GetComponent<TrenchProperties>().TrenchGetDamage();
                                            break;
                                        }
                                    }

                                    targetPieceProperties.isTrenchActive = false;
                                }
                                else // 如果不无敌，是否有
                                {
                                    if (targetPieceProperties.equipment == PieceProperties.Equipment.Bulletproof_Vest)
                                    {
                                        targetPieceProperties.equipmentDurability -= damage;
                                        targetPieceProperties.currentLifeValue -= (damage - targetPieceProperties.damageReduce);

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
                        break;
                    case Camp.Group2:
                        foreach (var i in gameManager.Group1Piece)
                        {
                            var pieceController = i.GetComponent<PlayerContoller>();
                            if (pieceController.currentCellIndex == attackTargetIndex)
                            {
                                var targetPieceProperties = i.GetComponent<PieceProperties>();

                                var damage = Random.Range(pieceProperties.finalminWeaponDamage, pieceProperties.finalmaxWeaponDamage + 1);

                                if (targetPieceProperties.isTrenchActive) // 首先判断是否无敌
                                {
                                    foreach (var o in gameManager.TrenchsList) // 找到位于该Cell的Trench
                                    {
                                        if (o.GetComponent<TrenchProperties>().currentCellIndex == attackTargetIndex)
                                        {
                                            o.GetComponent<TrenchProperties>().TrenchGetDamage();
                                            break;
                                        }
                                    }

                                    targetPieceProperties.isTrenchActive = false;
                                }
                                else // 如果不无敌，是否有
                                {
                                    if (targetPieceProperties.equipment == PieceProperties.Equipment.Bulletproof_Vest)
                                    {
                                        targetPieceProperties.equipmentDurability -= damage;
                                        targetPieceProperties.currentLifeValue -= (damage - targetPieceProperties.damageReduce);

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
                        break;
                }



            }
            else 
            {
                foreach (var i in gameManager.TrenchsList)
                {
                    if (attackTargetIndex == i.GetComponent<TrenchProperties>().currentCellIndex)
                    {
                        i.GetComponent<TrenchProperties>().TrenchGetDamage();
                        break;
                    }
                }

            }
        }
        

        hasAttack = true;
    }

    public void PlayEffect()
    {
        if (shootFire != null && !shootFire.isPlaying)
        {
            shootFire.Play();
        }
    }
}
