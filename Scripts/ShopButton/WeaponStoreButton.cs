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

    // 武器
    public TMP_Text Weapon;
    private List<string> weaponNames;
    public string pieceWeapon;

    // 武器等级
    public TMP_Text WeaponLevel;
    public int weaponLevel;

    // 武器攻击范围
    public TMP_Text WeaponAttackRange;
    public string weaponAttackRange;

    // 武器额外移动范围
    public TMP_Text WeaponPlueMovement;
    public string weaponPlueMovement;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        shopInformation = GameObject.Find("GameManager").GetComponent<ShopInformation>();

        weaponNames = new List<string>(shopInformation.WeaponList.Keys); // 获取所有装备名

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
        pieceWeapon = weaponNames[Random.Range(0, weaponNames.Count)]; // 获取随机选定的棋子武器
        Weapon.text = pieceWeapon; // 填入棋子武器

        weaponLevel = Random.Range(1, 4); // 获取棋子武器等级
        WeaponLevel.text = weaponLevel.ToString(); // 填入棋子武器等级

        weaponAttackRange = shopInformation.WeaponList[pieceWeapon].weaponAttackRange; // 获取武器攻击范围
        WeaponAttackRange.text = weaponAttackRange.ToString(); // 填入武器攻击范围

        weaponPlueMovement = shopInformation.WeaponList[pieceWeapon].movementEffect; // 获取武器移动影响
        WeaponPlueMovement.text = weaponPlueMovement.ToString(); // 填入武器移动影响

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

    public void BuyWeapon()
    {
        // 花钱

        gameManager.activePiece.GetComponent<PieceProperties>().pieceWeapon = shopInformation.WeaponList[pieceWeapon].weaponValue;
        gameManager.activePiece.GetComponent<PieceProperties>().WeaponLevel = weaponLevel;

        gameManager.activePiece.GetComponent<PieceProperties>().UpdateWeaponProperties();

        this.GetComponent<Button>().interactable = false;
    }
}
