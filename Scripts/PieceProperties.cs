using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceProperties : MonoBehaviour
{
    public enum Profession
    {
        Cowboy,
        Mercenary,
        Engineer,
        Sniper,
    }

    public enum Weapon
    {
        Pistol,
        Assault_Rifle,
        Sniper_Rifle,
        Rocket_Launcher,
    }

    public enum Equipment
    {
        Bulletproof_Vest,
        First_Aid_Kit,
        Wire,
        Trench,
    }

    public enum Ability
    {

    }


    [Header("基本属性")]
    public int lifeValue = 100;

    public int plusMovement;
    public int attackRange;

    public Profession pieceProfession = Profession.Cowboy;
    public int PieceLevel = 1;

    public Weapon pieceWeapon = Weapon.Pistol;
    public int WeaponLevel = 1;
    public int minWeaponDamage = 1;
    public int maxWeaponDamage = 6;

    public Equipment equipment = Equipment.Wire;


    [Header("实时数据")]
    public int currentLifeValue;

    // Start is called before the first frame update
    void Start()
    {
        currentLifeValue = lifeValue;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateProfessionProperties();
        UpdateWeaponProperties();
        UpdateEquipmentProperties();
    }

    private void UpdateProfessionProperties()
    {
        switch (pieceProfession)
        {
            case Profession.Cowboy:
                if (PieceLevel == 1)
                {
                    lifeValue = 130;
                }
                else if (PieceLevel == 2)
                {
                    lifeValue = 160;
                }
                else if (PieceLevel == 3)
                {
                    lifeValue = 220;
                }


                if (pieceWeapon == Weapon.Pistol)
                {
                    if (WeaponLevel == 1)
                    {
                        minWeaponDamage += 2;
                        maxWeaponDamage += 2;
                    }else if (WeaponLevel == 2)
                    {
                        minWeaponDamage += 4;
                        maxWeaponDamage += 4;
                    }else if (WeaponLevel == 3)
                    {
                        minWeaponDamage += 8;
                        maxWeaponDamage += 8;
                    }
                }
                break;
            case Profession.Mercenary: 
                break;
            case Profession.Engineer: 
                break;
            case Profession.Sniper: 
                break;
        }
    }

    private void UpdateWeaponProperties()
    {

    }

    private void UpdateEquipmentProperties()
    {

    }
}
