using System.Collections;
using System.Collections.Generic;
using TGS;
using UnityEngine;
using UnityEngine.UI;

public class TGSSetting : MonoBehaviour
{
    TerrainGridSystem tgs;
    public GameManager gameManager;

    public const int CELL_DEFAULT = 1;
    public const int CELL_PLAYER = 2;
    public const int CELL_ENEMY = 4;
    public const int CELL_WIRE = 8;
    public const int CELLS_ALL_NAVIGATABLE = ~(CELL_WIRE | CELL_PLAYER | CELL_ENEMY);

    public List<int> GoldMinerCells = new List<int>() { 543, 577, 578, 545, 512, 511 };

    public List<int> Bar = new List<int>() { 535, 569, 570, 537, 504, 503, 551, 585, 586, 553, 520, 519 };
    public List<int> MilitaryAcademy = new List<int>() { };
    public List<int> WeaponStore = new List<int>() { };
    public List<int> EquipmentShop = new List<int>() { };

    public List<int> StartCell= new List<int>() { 528, 560};

    private PlayerContoller activePieceController;

    public GameObject ShopButton;

    public GameObject BarShopUI;

    public GameObject MilitaryAcademyShopUI;

    public GameObject WeaponShopUI;

    public GameObject EquipmentShopUI;

    // private int professionLevel; // 1 = À¶£¬ 2 = ×Ï£¬ 3 = »Æ

    // private int weaponLevel; // 1 = À¶£¬ 2 = ×Ï£¬ 3 = »Æ

    // Start is called before the first frame update
    void Start()
    {
        tgs = TerrainGridSystem.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.activePiece != null )
        {
            activePieceController = gameManager.activePiece.GetComponent<PlayerContoller>();
            if (BarCheck())
            {
                BarEvent();
                MilitaryAcademyShopUI.SetActive(false);
                WeaponShopUI.SetActive(false);
                EquipmentShopUI.SetActive(false);
            }

            else if (MilitaryAcademyCheck())
            {
                MilitaryAcademyEvent();
                BarShopUI.SetActive(false);
                WeaponShopUI.SetActive(false);
                EquipmentShopUI.SetActive(false);
            }

            else if (WeaponStoreCheck())
            {
                WeaponStoreEvent();
                BarShopUI.SetActive(false);
                MilitaryAcademyShopUI.SetActive(false);
                EquipmentShopUI.SetActive(false);
            }


            else if (EquipmentShopCheck())
            {
                EquipmentShopEvent();
                BarShopUI.SetActive(false);
                MilitaryAcademyShopUI.SetActive(false);
                WeaponShopUI.SetActive(false);
            }
            else
            {
                ShopButton.SetActive(false);
            }
        }
        else
        {
            ShopButton.SetActive(false);
        }
    }

    private bool BarCheck()
    {
        foreach(var cell in Bar)
        {
            if (cell == activePieceController.currentCellIndex && activePieceController.state == PlayerContoller.State.IDLE)
            {
                ShopButton.SetActive(true);
                return true;
            }
        }
        return false;
    }

    public void BarEvent()
    {
        ShopButton.GetComponent<ShopButton>().ActiveShop = BarShopUI;
    }

    private bool MilitaryAcademyCheck()
    {
        foreach (var cell in MilitaryAcademy)
        {
            if (cell == activePieceController.currentCellIndex && activePieceController.state == PlayerContoller.State.IDLE)
            {
                ShopButton.SetActive(true);
                return true;
            }
        }
        return false;
    }

    public void MilitaryAcademyEvent()
    {
        ShopButton.GetComponent<ShopButton>().ActiveShop = MilitaryAcademyShopUI;
    }

    private bool WeaponStoreCheck()
    {
        foreach (var cell in WeaponStore)
        {
            if (cell == activePieceController.currentCellIndex && activePieceController.state == PlayerContoller.State.IDLE)
            {
                ShopButton.SetActive(true);
                return true;
            }
        }
        return false;
    }

    public void WeaponStoreEvent()
    {
        ShopButton.GetComponent<ShopButton>().ActiveShop = WeaponShopUI;
    }

    private bool EquipmentShopCheck()
    {
        foreach (var cell in EquipmentShop)
        {
            if (cell == activePieceController.currentCellIndex && activePieceController.state == PlayerContoller.State.IDLE)
            {
                ShopButton.SetActive(true);
                return true;
            }
        }
        return false;
    }

    public void EquipmentShopEvent()
    {
        ShopButton.GetComponent<ShopButton>().ActiveShop = EquipmentShopUI;
    }
}
