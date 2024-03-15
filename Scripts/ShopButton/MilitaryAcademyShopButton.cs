using System.Collections;
using System.Collections.Generic;
using TGS;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MilitaryAcademyShopButton : MonoBehaviour
{
    private ShopInformation shopInformation;
    TerrainGridSystem tgs;
    private GameManager gameManager;
    private TGSSetting tGSSetting;
    private PlayerContoller.Camp lastCamp;
    private PlayerContoller.Camp currentCamp;

    // 按钮对应选项的详细信息
    // 职业
    public Image professionImage;
    public TMP_Text Profession;
    private List<string> professionNames = new List<string>()
    {
        "Engineer",
        "Sniper"
    };
    public string pieceProfession;

    // 职业等级
    public TMP_Text ProfessionLevel;
    public int pieceLevel;

    // 武器
    public TMP_Text Weapon;
    private List<string> weaponNames;
    public string pieceWeapon;

    // 武器等级
    public TMP_Text WeaponLevel;
    public int weaponLevel;

    // 能力
    public TMP_Text Ability;
    private List<string> abilityNames;
    public string pieceAbility;

    // 能力描述
    public TMP_Text AbilityDescription;
    public string abilityDescription;


    // Start is called before the first frame update
    void Start()
    {
        tgs = TerrainGridSystem.instance;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        tGSSetting = GameObject.Find("GameManager").GetComponent<TGSSetting>();
        shopInformation = GameObject.Find("GameManager").GetComponent<ShopInformation>();

        weaponNames = new List<string>(shopInformation.WeaponList.Keys); // 获取所有武器名
        abilityNames = new List<string>(shopInformation.AbilityList.Keys); // 获取所有能力名

        RefreshInformation();
    }

    // Update is called once per frame
    void Update()
    {
        currentCamp = gameManager.activeCamp;

        if (currentCamp != lastCamp)
        {
            lastCamp = currentCamp;
            RefreshInformation();
        }
    }

    public void RefreshInformation()
    {
        this.GetComponent<Button>().interactable = true;

        // 生成随机人物
        pieceProfession = professionNames[Random.Range(0, professionNames.Count)]; // 获取随机产生的职业
        Profession.text = pieceProfession; // 依据获取到的职业填入职业名

        pieceLevel = Random.Range(1, 4); // 获取棋子职业等级
        ProfessionLevel.text = "Level " + pieceLevel.ToString(); // 填入棋子等级

        professionImage.sprite = Resources.Load<Sprite>("Images/shop/" + pieceProfession.ToLower() + "" + pieceLevel);

        if (pieceProfession == "Sniper")
        {
            List<string> weaponList = weaponNames;
            weaponList.Remove("Rocket Launcher");
            
            pieceWeapon = weaponList[Random.Range(0, weaponList.Count)]; // 获取随机选定的棋子武器
            Weapon.text = pieceWeapon; // 填入棋子武器

        }
        else
        {
            pieceWeapon = weaponNames[Random.Range(0, weaponNames.Count)]; // 获取随机选定的棋子武器
            Weapon.text = pieceWeapon; // 填入棋子武器
        }
       

        weaponLevel = Random.Range(1, 4); // 获取棋子武器等级
        WeaponLevel.text = weaponLevel.ToString(); // 填入棋子武器等级

        pieceAbility = abilityNames[Random.Range(0, abilityNames.Count)]; // 获取棋子额外能力
        Ability.text = pieceAbility; // 填入棋子额外能力

        abilityDescription = shopInformation.AbilityList[pieceAbility].ablilityDescription; // 获取能力描述
        AbilityDescription.text = abilityDescription; // 填入能力描述
    }

    public void GeneratePiece()
    {
        var canBuy = false;
        // 花钱
        if (pieceLevel == 1)
        {
            if (gameManager.activeCamp == PlayerContoller.Camp.Group1)
            {
                if (gameManager.group1Coin >= 20)
                {
                    canBuy = true;
                    gameManager.group1Coin -= 20;
                }

            }
            else if (gameManager.activeCamp == PlayerContoller.Camp.Group2)
            {
                if (gameManager.group2Coin >= 20)
                {
                    canBuy = true;
                    gameManager.group2Coin -= 20;
                }
            }
        }
        else if (pieceLevel == 2)
        {
            if (gameManager.activeCamp == PlayerContoller.Camp.Group1)
            {
                if (gameManager.group1Coin >= 40)
                {
                    canBuy = true;
                    gameManager.group1Coin -= 40;
                }

            }
            else if (gameManager.activeCamp == PlayerContoller.Camp.Group2)
            {
                if (gameManager.group2Coin >= 40)
                {
                    canBuy = true;
                    gameManager.group2Coin -= 40;
                }
            }
        }
        else if (pieceLevel == 3)
        {
            if (gameManager.activeCamp == PlayerContoller.Camp.Group1)
            {
                if (gameManager.group1Coin >= 60)
                {
                    canBuy = true;
                    gameManager.group1Coin -= 60;
                }

            }
            else if (gameManager.activeCamp == PlayerContoller.Camp.Group2)
            {
                if (gameManager.group2Coin >= 60)
                {
                    canBuy = true;
                    gameManager.group2Coin -= 60;
                }
            }
        }

        if (canBuy)
        {
            switch (gameManager.activeCamp)
            {
                case PlayerContoller.Camp.Group1:
                    if (gameManager.Group1Piece.Count < 3)
                    {
                        var piecePrefab = Resources.Load<GameObject>("Prefabs/Characters/" + pieceProfession + "/" + pieceProfession);

                        var piece = Instantiate(piecePrefab, transform.position, Quaternion.identity);

                        piece.transform.SetParent(GameObject.Find("Group1").transform);
                        piece.GetComponent<PlayerContoller>().camp = PlayerContoller.Camp.Group1;
                        piece.GetComponent<PlayerContoller>().initialCellIndex = gameManager.PiecePosCheck(PlayerContoller.Camp.Group1);
                        piece.GetComponent<PieceProperties>().PieceLevel = pieceLevel;
                        piece.GetComponent<PieceProperties>().WeaponLevel = weaponLevel;
                        piece.GetComponent<PieceProperties>().equipment = PieceProperties.Equipment.None;
                        piece.GetComponent<PieceProperties>().ability = shopInformation.AbilityList[pieceAbility].ablilityValue;

                    }
                    break;
                case PlayerContoller.Camp.Group2:
                    if (gameManager.Group2Piece.Count < 3)
                    {
                        var piecePrefab = Resources.Load<GameObject>("Prefabs/Characters/" + pieceProfession + "/" + pieceProfession);

                        var piece = Instantiate(piecePrefab, transform.position, Quaternion.identity);

                        piece.transform.SetParent(GameObject.Find("Group2").transform);
                        piece.GetComponent<PlayerContoller>().camp = PlayerContoller.Camp.Group2;
                        piece.GetComponent<PlayerContoller>().initialCellIndex = gameManager.PiecePosCheck(PlayerContoller.Camp.Group2);
                        piece.GetComponent<PieceProperties>().PieceLevel = pieceLevel;
                        piece.GetComponent<PieceProperties>().WeaponLevel = weaponLevel;
                        piece.GetComponent<PieceProperties>().equipment = PieceProperties.Equipment.None;
                        piece.GetComponent<PieceProperties>().ability = shopInformation.AbilityList[pieceAbility].ablilityValue;

                    }
                    break;
            }

            this.GetComponent<Button>().interactable = false;
        }   
    }
}
