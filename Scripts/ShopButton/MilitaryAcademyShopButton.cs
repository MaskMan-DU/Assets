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

    // ��ť��Ӧѡ�����ϸ��Ϣ
    // ְҵ
    public Image professionImage;
    public TMP_Text Profession;
    private List<string> professionNames = new List<string>()
    {
        "Engineer",
        "Sniper"
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
        abilityNames = new List<string>(shopInformation.AbilityList.Keys); // ��ȡ����������

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

        // �����������
        pieceProfession = professionNames[Random.Range(0, professionNames.Count)]; // ��ȡ���������ְҵ
        Profession.text = pieceProfession; // ���ݻ�ȡ����ְҵ����ְҵ��

        pieceLevel = Random.Range(1, 4); // ��ȡ����ְҵ�ȼ�
        ProfessionLevel.text = "Level " + pieceLevel.ToString(); // �������ӵȼ�

        professionImage.sprite = Resources.Load<Sprite>("Images/shop/" + pieceProfession.ToLower() + "" + pieceLevel);

        if (pieceProfession == "Sniper")
        {
            List<string> weaponList = weaponNames;
            weaponList.Remove("Rocket Launcher");
            
            pieceWeapon = weaponList[Random.Range(0, weaponList.Count)]; // ��ȡ���ѡ������������
            Weapon.text = pieceWeapon; // ������������

        }
        else
        {
            pieceWeapon = weaponNames[Random.Range(0, weaponNames.Count)]; // ��ȡ���ѡ������������
            Weapon.text = pieceWeapon; // ������������
        }
       

        weaponLevel = Random.Range(1, 4); // ��ȡ���������ȼ�
        WeaponLevel.text = weaponLevel.ToString(); // �������������ȼ�

        pieceAbility = abilityNames[Random.Range(0, abilityNames.Count)]; // ��ȡ���Ӷ�������
        Ability.text = pieceAbility; // �������Ӷ�������

        abilityDescription = shopInformation.AbilityList[pieceAbility].ablilityDescription; // ��ȡ��������
        AbilityDescription.text = abilityDescription; // ������������
    }

    public void GeneratePiece()
    {
        var canBuy = false;
        // ��Ǯ
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
