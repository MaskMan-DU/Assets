using System.Collections;
using System.Collections.Generic;
using TGS;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentShopButton : MonoBehaviour
{
    private ShopInformation shopInformation;
    private GameManager gameManager;

    // 装备
    public TMP_Text Equipment;
    private List<string> equipmentNames;
    public string pieceEquipment;

    // 装备等级
    public TMP_Text EquipmentLevel;
    public int equipmentLevel;

    // 装备作用
    public TMP_Text EquipmentFunction;
    public string equipmentFunction;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        shopInformation = GameObject.Find("GameManager").GetComponent<ShopInformation>();

        equipmentNames = new List<string>(shopInformation.EquipmentList.Keys); // 获取所有装备名

        RefreshInformation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshInformation()
    {
        this.GetComponent<Button>().interactable = true;

        pieceEquipment = equipmentNames[Random.Range(0, equipmentNames.Count)]; // 获取棋子装备
        Equipment.text = pieceEquipment; // 填入棋子装备

        equipmentLevel = Random.Range(1, 4); // 获取棋子装备等级
        EquipmentLevel.text = equipmentLevel.ToString();// 填入棋子装备等级

        equipmentFunction = shopInformation.EquipmentList[pieceEquipment].equipmentDescription; // 获取装备能力描述
        EquipmentFunction.text = equipmentFunction; // 填入装备描述
    }

    public void BuyEquipment()
    {
        // 花钱

        gameManager.activePiece.GetComponent<PieceProperties>().equipment = shopInformation.EquipmentList[pieceEquipment].equipmentValue;
        gameManager.activePiece.GetComponent<PieceProperties>().equipmentLevel = equipmentLevel;

        gameManager.activePiece.GetComponent<PieceProperties>().UpdateEquipmentProperties();

        this.GetComponent<Button>().interactable = false;
    }
}
