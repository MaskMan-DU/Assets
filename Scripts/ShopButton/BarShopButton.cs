using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Xml;
using TGS;

public class BarShopButton : MonoBehaviour
{
    private ShopInformation shopInformation;
    TerrainGridSystem tgs;
    private GameManager gameManager;

    // 按钮对应选项的详细信息
    // 职业
    public TMP_Text Profession;
    private List<string> professionNames = new List<string>()
    {
        "Cowboy",
        "Mercenary"
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

    // 装备
    public TMP_Text Equipment;
    private List<string> equipmentNames;
    public string pieceEquipment;

    // 装备等级
    public TMP_Text EquipmentLevel;
    public int equipmentLevel;

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
        shopInformation = GameObject.Find("GameManager").GetComponent<ShopInformation>();

        weaponNames = new List<string>(shopInformation.WeaponList.Keys); // 获取所有武器名
        equipmentNames = new List<string>(shopInformation.EquipmentList.Keys); // 获取所有装备名
        abilityNames= new List<string>(shopInformation.AbilityList.Keys); // 获取所有能力名

        RefreshInformation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshInformation()
    {
        // 生成随机人物
        pieceProfession = professionNames[Random.Range(0, professionNames.Count)]; // 获取随机产生的职业
        Profession.text = pieceProfession; // 依据获取到的职业填入职业名

        pieceLevel = Random.Range(1, 4); // 获取棋子职业等级
        ProfessionLevel.text = pieceLevel.ToString(); // 填入棋子等级

        pieceWeapon = weaponNames[Random.Range(0, weaponNames.Count)]; // 获取随机选定的棋子武器
        Weapon.text = pieceWeapon; // 填入棋子武器

        weaponLevel = Random.Range(1, 4); // 获取棋子武器等级
        WeaponLevel.text = weaponLevel.ToString(); // 填入棋子武器等级

        pieceEquipment = equipmentNames[Random.Range(0, equipmentNames.Count)]; // 获取棋子装备
        Equipment.text = pieceEquipment; // 填入棋子装备

        equipmentLevel = Random.Range(1, 4); // 获取棋子装备等级
        EquipmentLevel.text = equipmentLevel.ToString();// 填入棋子装备等级

        pieceAbility = abilityNames[Random.Range(0, abilityNames.Count)]; // 获取棋子额外能力
        Ability.text = pieceAbility; // 填入棋子额外能力

        abilityDescription = shopInformation.AbilityList[pieceAbility].ablilityDescription; // 获取能力描述
        AbilityDescription.text = abilityDescription; // 填入能力描述
    }

    public void GeneratePiece()
    {

    }
}
