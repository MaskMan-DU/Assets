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

    // ��ť��Ӧѡ�����ϸ��Ϣ
    // ְҵ
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

    // װ��
    public TMP_Text Equipment;
    private List<string> equipmentNames;
    public string pieceEquipment;

    // װ���ȼ�
    public TMP_Text EquipmentLevel;
    public int equipmentLevel;

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
        shopInformation = GameObject.Find("GameManager").GetComponent<ShopInformation>();

        weaponNames = new List<string>(shopInformation.WeaponList.Keys); // ��ȡ����������
        equipmentNames = new List<string>(shopInformation.EquipmentList.Keys); // ��ȡ����װ����
        abilityNames= new List<string>(shopInformation.AbilityList.Keys); // ��ȡ����������

        RefreshInformation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshInformation()
    {
        // �����������
        pieceProfession = professionNames[Random.Range(0, professionNames.Count)]; // ��ȡ���������ְҵ
        Profession.text = pieceProfession; // ���ݻ�ȡ����ְҵ����ְҵ��

        pieceLevel = Random.Range(1, 4); // ��ȡ����ְҵ�ȼ�
        ProfessionLevel.text = pieceLevel.ToString(); // �������ӵȼ�

        pieceWeapon = weaponNames[Random.Range(0, weaponNames.Count)]; // ��ȡ���ѡ������������
        Weapon.text = pieceWeapon; // ������������

        weaponLevel = Random.Range(1, 4); // ��ȡ���������ȼ�
        WeaponLevel.text = weaponLevel.ToString(); // �������������ȼ�

        pieceEquipment = equipmentNames[Random.Range(0, equipmentNames.Count)]; // ��ȡ����װ��
        Equipment.text = pieceEquipment; // ��������װ��

        equipmentLevel = Random.Range(1, 4); // ��ȡ����װ���ȼ�
        EquipmentLevel.text = equipmentLevel.ToString();// ��������װ���ȼ�

        pieceAbility = abilityNames[Random.Range(0, abilityNames.Count)]; // ��ȡ���Ӷ�������
        Ability.text = pieceAbility; // �������Ӷ�������

        abilityDescription = shopInformation.AbilityList[pieceAbility].ablilityDescription; // ��ȡ��������
        AbilityDescription.text = abilityDescription; // ������������
    }

    public void GeneratePiece()
    {

    }
}
