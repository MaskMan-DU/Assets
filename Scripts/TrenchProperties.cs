using System.Collections;
using System.Collections.Generic;
using TGS;
using UnityEngine;

public class TrenchProperties : MonoBehaviour
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

        gameManager.TrenchsList.Add(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //Trench();  
    }

    public void TrenchGetDamage()
    {
        currentLifeValue--;

        gameManager.TrenchsList.Remove(this.gameObject);

        foreach (var i in gameManager.Group1Piece)
        {
            if (currentCellIndex == i.GetComponent<PlayerContoller>().currentCellIndex) // ���Trench���ڵ�λ����group1������
            {
                i.GetComponent<PieceProperties>().isTrenchActive = false;
                break;
            }
        }


        foreach (var i in gameManager.Group2Piece)
        {
            if (currentCellIndex == i.GetComponent<PlayerContoller>().currentCellIndex) // ���Trench���ڵ�λ����group2������
            {
                i.GetComponent<PieceProperties>().isTrenchActive = false;
                break;
            }
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerContoller>() != null)
        {
            other.GetComponent<PieceProperties>().isTrenchActive = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerContoller>() != null)
        {
            other.GetComponent<PieceProperties>().isTrenchActive = false;
        }
    }

}
