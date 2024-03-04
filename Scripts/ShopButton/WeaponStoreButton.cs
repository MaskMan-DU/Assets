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
    }

    public void RefreshInformation()
    {
        pieceWeapon = weaponNames[Random.Range(0, weaponNames.Count)]; // ��ȡ���ѡ������������
        Weapon.text = pieceWeapon; // ������������

        weaponLevel = Random.Range(1, 4); // ��ȡ���������ȼ�
        WeaponLevel.text = weaponLevel.ToString(); // �������������ȼ�

        weaponAttackRange = shopInformation.WeaponList[pieceWeapon].weaponAttackRange; // ��ȡ����������Χ
        WeaponAttackRange.text = weaponAttackRange.ToString(); // ��������������Χ

        weaponPlueMovement = shopInformation.WeaponList[pieceWeapon].movementEffect; // ��ȡ�����ƶ�Ӱ��
        WeaponPlueMovement.text = weaponPlueMovement.ToString(); // ���������ƶ�Ӱ��

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
        // ��Ǯ

        gameManager.activePiece.GetComponent<PieceProperties>().pieceWeapon = shopInformation.WeaponList[pieceWeapon].weaponValue;
        gameManager.activePiece.GetComponent<PieceProperties>().WeaponLevel = weaponLevel;

        gameManager.activePiece.GetComponent<PieceProperties>().UpdateWeaponProperties();

        this.GetComponent<Button>().interactable = false;
    }
}
