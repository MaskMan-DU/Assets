using System.Collections;
using System.Collections.Generic;
using TGS;
using UnityEngine;

public class TrenchProperties : MonoBehaviour
{
    private GameManager gameManager;
    TerrainGridSystem tgs;

    [Header("基本属性")]
    public int lifeValue = 1;
    public int currentCellIndex;

    [Header("实时数据")]
    public int currentLifeValue;
    // Start is called before the first frame update
    void Start()
    {
        currentLifeValue = lifeValue;

        tgs = TerrainGridSystem.instance;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentCellIndex = tgs.CellGetIndex(transform.position, true);

        gameManager.TrenchsList.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Trench();

        if (currentLifeValue == 0)
        {
            gameManager.TrenchsList.Remove(this.gameObject);

            foreach (var i in gameManager.Group1Piece)
            {
                if (currentCellIndex == i.GetComponent<PlayerContoller>().currentCellIndex) // 如果Trench现在的位置有group1的棋子
                {
                    i.GetComponent<PieceProperties>().isTrenchActive = false; 
                    break;
                }
            }


            foreach (var i in gameManager.Group2Piece)
            {
                if (currentCellIndex == i.GetComponent<PlayerContoller>().currentCellIndex) // 如果Trench现在的位置有group2的棋子
                {
                    i.GetComponent<PieceProperties>().isTrenchActive = false; 
                    break;
                }
            }

            Destroy(gameObject);
        }

        
    }

    public void TrenchGetDamage()
    {
        currentLifeValue--;
    }

    public void Trench()
    {
        foreach(var i in gameManager.Group1Piece)
        {
            if (currentCellIndex == i.GetComponent<PlayerContoller>().currentCellIndex) // 如果Trench现在的位置有group1的棋子
            {
                i.GetComponent<PieceProperties>().isTrenchActive = true; // 该棋子处于被Trench保护的状态
            }
            else
            {
                i.GetComponent<PieceProperties>().isTrenchActive = false; // 如果该棋子清除被Trench保护的状态
            }
        }


        foreach (var i in gameManager.Group2Piece)
        {
            if (currentCellIndex == i.GetComponent<PlayerContoller>().currentCellIndex) // 如果Trench现在的位置有group2的棋子
            {
                i.GetComponent<PieceProperties>().isTrenchActive = true; // 该棋子处于被Trench保护的状态
            }
            else
            {
                i.GetComponent<PieceProperties>().isTrenchActive = false; // 如果该棋子清除被Trench保护的状态
            }
        }
    }
}
