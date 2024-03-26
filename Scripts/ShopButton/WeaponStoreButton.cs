using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponStoreButton : MonoBehaviour
{
    private ShopInformation shopInformation;
    private GameManager gameManager;
    private PlayerContoller.Camp lastCamp;
    private PlayerContoller.Camp currentCamp;

    private int lastTurn;

    public int level1price = 10;
    public int level2price = 20;
    public int level3price = 40;

    public Image weaponImage;

    // ����
    public TMP_Text Weapon;
    private List<string> weaponNames;
    public string pieceWeapon;

    // �����ȼ�
    public TMP_Text WeaponLevel;
    public int weaponLevel;

    // ����������Χ
    public TMP_Text WeaponAttackRange;
    public string weaponAttackRange;

    // ���������ƶ���Χ
    public TMP_Text WeaponPlueMovement;
    public string weaponPlueMovement;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        shopInformation = GameObject.Find("GameManager").GetComponent<ShopInformation>();

        weaponNames = new List<string>(shopInformation.WeaponList.Keys); // ��ȡ����װ����

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

        if (gameManager.activePiece != null)
        {
            if (gameManager.activePiece.GetComponent<PieceProperties>().pieceProfession != PieceProperties.Profession.Engineer)
            {
                if (pieceWeapon == "Rocket Launcher")
                {
                    this.GetComponent<Button>().interactable = false;
                }
                else
                {
                    this.GetComponent<Button>().interactable = true;
                }
            }
            else
            {
                this.GetComponent<Button>().interactable = true;
            }
        }
    }

    public void RefreshInformation()
    {
        pieceWeapon = weaponNames[Random.Range(0, weaponNames.Count)]; // ��ȡ���ѡ������������
        Weapon.text = pieceWeapon; // ������������

        weaponLevel = Random.Range(1, 4); // ��ȡ���������ȼ�
        WeaponLevel.text = weaponLevel.ToString(); // �������������ȼ�

        weaponAttackRange = shopInformation.WeaponList[pieceWeapon].weaponAttackRange; // ��ȡ����������Χ
        WeaponAttackRange.text = weaponAttackRange.ToString(); // ��������������Χ

        if (weaponLevel == 1)
        {
            weaponPlueMovement = shopInformation.WeaponList[pieceWeapon].level1MovementEffect; // ��ȡ�����ƶ�Ӱ��
        }
        else if (weaponLevel == 2)
        {
            weaponPlueMovement = shopInformation.WeaponList[pieceWeapon].level2MovementEffect; // ��ȡ�����ƶ�Ӱ��
        }
        else if (weaponLevel == 3)
        {
            weaponPlueMovement = shopInformation.WeaponList[pieceWeapon].level3MovementEffect; // ��ȡ�����ƶ�Ӱ��
        }
        
        WeaponPlueMovement.text = weaponPlueMovement.ToString(); // ���������ƶ�Ӱ��

        weaponImage.sprite = Resources.Load<Sprite>("Images/shop/" + pieceWeapon + "" + weaponLevel);
    }

    public void BuyWeapon()
    {
        var canBuy = false;
        // ��Ǯ
        if (weaponLevel == 1)
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
        else if (weaponLevel == 2)
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
        else if (weaponLevel == 3)
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
            gameManager.activePiece.GetComponent<PieceProperties>().pieceWeapon = shopInformation.WeaponList[pieceWeapon].weaponValue;
            gameManager.activePiece.GetComponent<PieceProperties>().WeaponLevel = weaponLevel;

            gameManager.activePiece.GetComponent<PieceProperties>().UpdateWeaponProperties();

            this.GetComponent<Button>().interactable = false;
        }  
    }
}
