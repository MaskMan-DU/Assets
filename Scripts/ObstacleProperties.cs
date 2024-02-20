using System.Collections;
using System.Collections.Generic;
using TGS;
using UnityEngine;

public class ObstacleProperties : MonoBehaviour
{
    private GameManager gameManager;
    TerrainGridSystem tgs;

    [Header("��������")]
    public int lifeValue = 1;
    public int currentCellIndex;

    [Header("ʵʱ����")]
    public int currentLifeValue;
    // Start is called before the first frame update
    void Start()
    {
        currentLifeValue = lifeValue;

        tgs = TerrainGridSystem.instance;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        currentCellIndex = tgs.CellGetIndex(transform.position, true);
        tgs.CellSetGroup(currentCellIndex, TGSSetting.CELL_WIRE);
        tgs.CellSetCanCross(currentCellIndex, false);
        gameManager.Obstacles.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLifeValue == 0)
        {
            tgs.CellSetGroup(currentCellIndex, TGSSetting.CELL_DEFAULT);
            tgs.CellSetCanCross(currentCellIndex, true);
            gameManager.Obstacles.Remove(this.gameObject);
            Destroy(gameObject);
        }
    }

    public void ObstacleGetDamage()
    {
        currentLifeValue--;
    }
}
