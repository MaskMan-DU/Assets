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
    private PlayerContoller.Camp lastCamp;
    private PlayerContoller.Camp currentCamp;

    public int level1price = 15;
    public int level2price = 30;
    public int level3price = 60;

    // 装备
    public TMP_Text Equipment;
    private List<string> equipmentNames;
    public string pieceEquipment;

    public Image equipmentImage;

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

        pieceEquipment = equipmentNames[Random.Range(0, equipmentNames.Count)]; // 获取棋子装备
        Equipment.text = pieceEquipment; // 填入棋子装备

        equipmentLevel = Random.Range(1, 4); // 获取棋子装备等级
        EquipmentLevel.text = equipmentLevel.ToString();// 填入棋子装备等级

        if (equipmentLevel == 1)
        {
            equipmentFunction = shopInformation.EquipmentList[pieceEquipment].level1EquipmentDescription; // 获取装备能力描述
        }else if (equipmentLevel == 2)
        {
            equipmentFunction = shopInformation.EquipmentList[pieceEquipment].level2EquipmentDescription; // 获取装备能力描述
        }else if (equipmentLevel == 3)
        {
            equipmentFunction = shopInformation.EquipmentList[pieceEquipment].level3EquipmentDescription; // 获取装备能力描述
        }
        
        EquipmentFunction.text = equipmentFunction; // 填入装备描述

        equipmentImage.sprite = Resources.Load<Sprite>("Images/shop/" + pieceEquipment + "" + equipmentLevel);
    }

    public void BuyEquipment()
    {
        var canBuy = false;
        // 花钱
        if (equipmentLevel == 1)
        {
            if (gameManager.activeCamp == PlayerContoller.Camp.Group1)
            {
                if (gameManager.group1Coin >= level1price)
                {
                    canBuy = true;
                    gameManager.group1Coin -= level1price;
                }
                
            }
            else if (gameManager.activeCamp == PlayerContoller.Camp.Group2)
            {
                if (gameManager.group2Coin >= level1price)
                {
                    canBuy = true;
                    gameManager.group2Coin -= level1price;
                }
            }
        }
        else if (equipmentLevel == 2)
        {
            if (gameManager.activeCamp == PlayerContoller.Camp.Group1)
            {
                if (gameManager.group1Coin >= level2price)
                {
                    canBuy = true;
                    gameManager.group1Coin -= level2price;
                }

            }
            else if (gameManager.activeCamp == PlayerContoller.Camp.Group2)
            {
                if (gameManager.group2Coin >= level2price)
                {
                    canBuy = true;
                    gameManager.group2Coin -= level2price;
                }
            }
        }
        else if (equipmentLevel == 3)
        {
            if (gameManager.activeCamp == PlayerContoller.Camp.Group1)
            {
                if (gameManager.group1Coin >= level3price)
                {
                    canBuy = true;
                    gameManager.group1Coin -= level3price;
                }

            }
            else if (gameManager.activeCamp == PlayerContoller.Camp.Group2)
            {
                if (gameManager.group2Coin >= level3price)
                {
                    canBuy = true;
                    gameManager.group2Coin -= level3price;
                }
            }
        }

        if (canBuy) 
        {
            gameManager.activePiece.GetComponent<PieceProperties>().equipment = shopInformation.EquipmentList[pieceEquipment].equipmentValue;
            gameManager.activePiece.GetComponent<PieceProperties>().equipmentLevel = equipmentLevel;

            gameManager.activePiece.GetComponent<PieceProperties>().UpdateEquipmentProperties();

            this.GetComponent<Button>().interactable = false;
        }       
    }
}
