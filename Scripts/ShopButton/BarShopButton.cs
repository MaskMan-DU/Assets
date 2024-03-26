using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Xml;
using TGS;
using Unity.VisualScripting;

public class BarShopButton : MonoBehaviour
{
    private ShopInformation shopInformation;
    TerrainGridSystem tgs;
    private GameManager gameManager;
    private TGSSetting tGSSetting;
    private PlayerContoller.Camp lastCamp;
    private PlayerContoller.Camp currentCamp;

    private int lastTurn = 0;

    public int level1price = 15;
    public int level2price = 30;
    public int level3price = 60;


    // ��ť��Ӧѡ�����ϸ��Ϣ
    // ְҵ
    public Image professionImage;
    public TMP_Text Profession;
    private List<string> professionNames = new List<string>()
    {
        "Cowboy",
        "Mercenary"
    };
    public string pieceProfession;

    // ְҵ�ȼ�
    public TMP_Text ProfessionLevel;
    public int pieceLevel;

    // ����
    public TMP_Text Weapon;
    private List<string> weaponNames;
    public string pieceWeapon;

    // �����ȼ�
    public TMP_Text WeaponLevel;
    public int weaponLevel;

    // ����
    public TMP_Text Ability;
    private List<string> abilityNames;
    public string pieceAbility;

    // ��������
    public TMP_Text AbilityDescription;
    public string abilityDescription;


    // Start is called before the first frame update
    void Start()
    {
        tgs = TerrainGridSystem.instance;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        tGSSetting = GameObject.Find("GameManager").GetComponent<TGSSetting>();
        shopInformation = GameObject.Find("GameManager").GetComponent<ShopInformation>();

        weaponNames = new List<string>(shopInformation.WeaponList.Keys); // ��ȡ����������
        abilityNames= new List<string>(shopInformation.AbilityList.Keys); // ��ȡ����������


        // �޻��
        weaponNames.Remove("Rocket Launcher");

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
        
        if (lastTurn != gameManager.TurnNumber)
        {
            lastTurn++;
            RefreshInformation();
        }
    }

    public void RefreshInformation()
    {
        this.GetComponent<Button>().interactable = true;

        // �����������
        pieceProfession = professionNames[Random.Range(0, professionNames.Count)]; // ��ȡ���������ְҵ
        Profession.text = pieceProfession; // ���ݻ�ȡ����ְҵ����ְҵ��

        pieceLevel = Random.Range(1, 4); // ��ȡ����ְҵ�ȼ�
        ProfessionLevel.text = "Level "+ pieceLevel.ToString(); // �������ӵȼ�

        professionImage.sprite = Resources.Load<Sprite>("Images/shop/" + pieceProfession.ToLower() + "" + pieceLevel);

        pieceWeapon = weaponNames[Random.Range(0, weaponNames.Count)]; // ��ȡ���ѡ������������
        Weapon.text = pieceWeapon; // ������������

        weaponLevel = Random.Range(1, 3); // ��ȡ���������ȼ�
        WeaponLevel.text = weaponLevel.ToString(); // �������������ȼ�

        pieceAbility = abilityNames[Random.Range(0, abilityNames.Count)]; // ��ȡ���Ӷ�������
        Ability.text = pieceAbility; // �������Ӷ�������

        if (pieceLevel == 1)
        {
            abilityDescription = shopInformation.AbilityList[pieceAbility].level1AbilityDescription; // ��ȡ��������
        }else if (pieceLevel == 2)
        {
            abilityDescription = shopInformation.AbilityList[pieceAbility].level2AbilityDescription; // ��ȡ��������
        }else if (pieceLevel == 3)
        {
            abilityDescription = shopInformation.AbilityList[pieceAbility].level3AbilityDescription; // ��ȡ��������
        }
        
        AbilityDescription.text = abilityDescription; // ������������
    }

    public void GeneratePiece()
    {

        var canBuy = false;
        var cost = 0;
        // ��Ǯ
        if (pieceLevel == 1)
        {
            if (gameManager.activeCamp == PlayerContoller.Camp.Group1)
            {
                if (gameManager.group1Coin >= level1price)
                {
                    canBuy = true;
                    cost = level1price;
                }

            }
            else if (gameManager.activeCamp == PlayerContoller.Camp.Group2)
            {
                if (gameManager.group2Coin >= level1price)
                {
                    canBuy = true;
                    cost = level1price;
                }
            }
        }
        else if (pieceLevel == 2)
        {
            if (gameManager.activeCamp == PlayerContoller.Camp.Group1)
            {
                if (gameManager.group1Coin >= level2price)
                {
                    canBuy = true;                    
                    cost = level2price;
                }

            }
            else if (gameManager.activeCamp == PlayerContoller.Camp.Group2)
            {
                if (gameManager.group2Coin >= level2price)
                {
                    canBuy = true;
                    cost = level2price;
                }
            }
        }
        else if (pieceLevel == 3)
        {
            if (gameManager.activeCamp == PlayerContoller.Camp.Group1)
            {
                if (gameManager.group1Coin >= level3price)
                {
                    canBuy = true;
                    cost = level3price;
                }

            }
            else if (gameManager.activeCamp == PlayerContoller.Camp.Group2)
            {
                if (gameManager.group2Coin >= level3price)
                {
                    canBuy = true;
                    cost = level3price;
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
                        piece.GetComponent<PieceProperties>().pieceWeapon = shopInformation.WeaponList[pieceWeapon].weaponValue;
                        piece.GetComponent<PieceProperties>().WeaponLevel = weaponLevel;
                        piece.GetComponent<PieceProperties>().equipment = PieceProperties.Equipment.None;
                        piece.GetComponent<PieceProperties>().ability = shopInformation.AbilityList[pieceAbility].ablilityValue;

                        gameManager.group1Coin -= cost;

                        this.GetComponent<Button>().interactable = false;
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
                        piece.GetComponent<PieceProperties>().pieceWeapon = shopInformation.WeaponList[pieceWeapon].weaponValue;
                        piece.GetComponent<PieceProperties>().WeaponLevel = weaponLevel;
                        piece.GetComponent<PieceProperties>().equipment = PieceProperties.Equipment.None;
                        piece.GetComponent<PieceProperties>().ability = shopInformation.AbilityList[pieceAbility].ablilityValue;

                        gameManager.group2Coin -= cost;

                        this.GetComponent<Button>().interactable = false;
                    }
                    break;
            }

            
        }
       
    }
}
