using System.Collections;
using System.Collections.Generic;
using TGS;
using UnityEngine;

public class TGSSetting : MonoBehaviour
{
    TerrainGridSystem tgs;
    public GameManager gameManager;

    public const int CELL_DEFAULT = 1;
    public const int CELL_PLAYER = 2;
    public const int CELL_ENEMY = 4;
    public const int CELL_OBSTACLE = 8;
    public const int CELLS_ALL_NAVIGATABLE = ~(CELL_OBSTACLE | CELL_PLAYER | CELL_ENEMY);

    public List<int> GoldMinerCells = new List<int>() { 543, 577, 578, 545, 512, 511 };

    public List<int> Bar = new List<int>() { };
    public List<int> MilitaryAcademy = new List<int>() { };
    public List<int> WeaponStore = new List<int>() { };
    public List<int> EquipmentShop = new List<int>() { };

    private PlayerContoller activePieceController;

    // Start is called before the first frame update
    void Start()
    {
        tgs = TerrainGridSystem.instance;
    }

    // Update is called once per frame
    void Update()
    {
        activePieceController = gameManager.activePiece.GetComponent<PlayerContoller>();

        if (activePieceController != null )
        {
            if (BarCheck())
            {
                BarEvent();
            }

            if (MilitaryAcademyCheck())
            {
                MilitaryAcademyEvent();
            }

            if (WeaponStoreCheck())
            {
                WeaponStoreEvent();
            }

            if (EquipmentShopCheck())
            {
                EquipmentShopEvent();
            }
        }
    }

    private bool BarCheck()
    {
        foreach(var cell in Bar)
        {
            if (cell == activePieceController.currentCellIndex)
            {
                return true;
            }
        }
        return false;
    }

    public void BarEvent()
    {

    }

    private bool MilitaryAcademyCheck()
    {
        foreach (var cell in MilitaryAcademy)
        {
            if (cell == activePieceController.currentCellIndex)
            {
                return true;
            }
        }
        return false;
    }

    public void MilitaryAcademyEvent()
    {

    }

    private bool WeaponStoreCheck()
    {
        foreach (var cell in WeaponStore)
        {
            if (cell == activePieceController.currentCellIndex)
            {
                return true;
            }
        }
        return false;
    }

    public void WeaponStoreEvent()
    {

    }

    private bool EquipmentShopCheck()
    {
        foreach (var cell in EquipmentShop)
        {
            if (cell == activePieceController.currentCellIndex)
            {
                return true;
            }
        }
        return false;
    }

    public void EquipmentShopEvent()
    {

    }
}
