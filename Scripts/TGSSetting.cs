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

    public List<int> Bar = new List<int>() { 551, 585, 586, 553, 520, 519, 537, 504, 503, 535, 569, 570};
    public List<int> MilitaryAcademy = new List<int>() { 181, 147, 115, 116, 149, 182, 907, 908, 941, 974, 973, 939 };
    public List<int> WeaponStore = new List<int>() { 320, 286, 254, 255, 288, 321, 835, 834, 800, 768, 769, 802 };
    public List<int> EquipmentShop = new List<int>() { 306, 272, 240, 241, 274, 307, 782, 814, 848, 849, 816, 783 };

    public List<int> NormalEnemyCells = new List<int> { 556, 422, 686, 532, 402, 666, 979, 968, 679, 647, 646, 442, 408, 441, 640, 639, 672, 415, 449, 448, 121, 110 };
    public List<int> ElitleEnemyCells = new List<int> { 321, 255, 286, 783, 849, 814, 240, 306, 274, 768, 834, 802, 941, 907, 973, 116, 182, 147, 577, 543, 511, 512, 545, 578 };

    public List<int> StartCell= new List<int>() { 528, 560};

    public List<int> Group1StartCell = new List<int>() { 528 , 562, 496 };
    public List<int> Group2StartCell = new List<int>() { 560 , 593, 527 };

    private PlayerContoller activePieceController;

    public GameObject ShopButton;

    public GameObject BarShopUI;

    public GameObject MilitaryAcademyShopUI;

    public GameObject WeaponShopUI;

    public GameObject EquipmentShopUI;

    // private int professionLevel; // 1 = À¶£¬ 2 = ×Ï£¬ 3 = »Æ

    // private int weaponLevel; // 1 = À¶£¬ 2 = ×Ï£¬ 3 = »Æ

    private void Awake()
    {
        tgs = TerrainGridSystem.instance;
        GenerateElitleEnemy();
        GenerateNormalEnemy();
    }

    // Start is called before the first frame update
    void Start()
    {
        
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


    public void GenerateNormalEnemy()
    {
        var NormalEnemyList = new List<string>()
        {
            "NormalEnemyWithPistol",
            "NormalEnemyWithAR",
            "NormalEnemyWithSR",
            "NormalEnemyWithRPG",
        };

        foreach (var i in NormalEnemyCells)
        {
            var enemyName = NormalEnemyList[Random.Range(0, NormalEnemyList.Count)];

            var enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy/Normal/" + enemyName);

            var enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

            enemy.transform.SetParent(GameObject.Find("Enemy").transform);

            var enemyController = enemy.GetComponent<EnemyController>();

            enemyController.currentCellIndex = i;
        }
    }

    public void GenerateElitleEnemy()
    {
        var ElitleEnemyList = new List<string>()
        {
            "EliteEnemyWithPistol",
            "EliteEnemyWithAR",
            "EliteEnemyWithSR",
            "EliteEnemyWithRPG",
        };

        foreach (var i in ElitleEnemyCells)
        {
            var enemyName = ElitleEnemyList[Random.Range(0, ElitleEnemyList.Count)];

            var enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy/Elite/" + enemyName);

            var enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

            enemy.transform.SetParent(GameObject.Find("Enemy").transform);

            var enemyController = enemy.GetComponent<EnemyController>();

            enemyController.currentCellIndex = i;
        }
    }
}
