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

    // װ��
    public TMP_Text Equipment;
    private List<string> equipmentNames;
    public string pieceEquipment;

    // װ���ȼ�
    public TMP_Text EquipmentLevel;
    public int equipmentLevel;

    // װ������
    public TMP_Text EquipmentFunction;
    public string equipmentFunction;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        shopInformation = GameObject.Find("GameManager").GetComponent<ShopInformation>();

        equipmentNames = new List<string>(shopInformation.EquipmentList.Keys); // ��ȡ����װ����

        RefreshInformation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshInformation()
    {
        this.GetComponent<Button>().interactable = true;

        pieceEquipment = equipmentNames[Random.Range(0, equipmentNames.Count)]; // ��ȡ����װ��
        Equipment.text = pieceEquipment; // ��������װ��

        equipmentLevel = Random.Range(1, 4); // ��ȡ����װ���ȼ�
        EquipmentLevel.text = equipmentLevel.ToString();// ��������װ���ȼ�

        equipmentFunction = shopInformation.EquipmentList[pieceEquipment].equipmentDescription; // ��ȡװ����������
        EquipmentFunction.text = equipmentFunction; // ����װ������
    }

    public void BuyEquipment()
    {
        // ��Ǯ

        gameManager.activePiece.GetComponent<PieceProperties>().equipment = shopInformation.EquipmentList[pieceEquipment].equipmentValue;
        gameManager.activePiece.GetComponent<PieceProperties>().equipmentLevel = equipmentLevel;

        gameManager.activePiece.GetComponent<PieceProperties>().UpdateEquipmentProperties();

        this.GetComponent<Button>().interactable = false;
    }
}
