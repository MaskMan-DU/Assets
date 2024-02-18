using System.Collections;
using System.Collections.Generic;
using TGS;
using UnityEngine;

public class PieceProperties : MonoBehaviour
{
    private GameManager gameManager;
    private TGSSetting tgsSetting;
    TerrainGridSystem tgs;

    public enum PieceType
    {
        Player,
        NormalEnemy,
        EliteEnemy
    }
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
        None,
        Bulletproof_Vest,
        First_Aid_Kit,
        Wire,
        Trench,
    }

    public enum Ability
    {

    }


    [Header("基本属性")]
    public PieceType pieceType; 
    public int lifeValue = 100;

    public int plusMovement;
    public int attackRange;

    public Profession pieceProfession = Profession.Cowboy;
    public int PieceLevel = 1;

    public Weapon pieceWeapon = Weapon.Pistol;
    public int WeaponLevel = 1;
    public int minWeaponDamage = 1;
    public int maxWeaponDamage = 6;

    public Equipment equipment = Equipment.None;
    public int equipmentLevel = 1;
    public int equipmentDurability;
    public int equipmentRange = 0;  
    public int damageReduce = 0;
    public bool isTrenchActive = false;

    [Header("实时数据")]
    public int currentLifeValue;

    // Start is called before the first frame update
    void Start()
    {
        tgs = TerrainGridSystem.instance;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        tgsSetting = GameObject.Find("GameManager").GetComponent<TGSSetting>();

        if (pieceType == PieceType.Player)
        {
            UpdateWeaponProperties();
            UpdateProfessionProperties();
            UpdateEquipmentProperties();
        }
        else
        {
            pieceWeapon = (Weapon)Random.Range(0, 4);
            if (pieceType == PieceType.NormalEnemy) // 是普通敌人
            {
                lifeValue = 100;
                WeaponLevel = 1;
            }
            else
            {
                lifeValue = 150;
                WeaponLevel = Random.Range(2, 4);
            }
            UpdateWeaponProperties();
        }
        

        currentLifeValue = lifeValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (equipmentDurability <= 0)
        {
            equipment = Equipment.None;
            equipmentDurability = 0;
            damageReduce = 0;
            equipmentRange = 0;
            isTrenchActive = false;
        }
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
                if (PieceLevel == 1)
                {
                    lifeValue = 120;
                }
                else if (PieceLevel == 2)
                {
                    lifeValue = 140;
                }
                else if (PieceLevel == 3)
                {
                    lifeValue = 180;
                }

                if (pieceWeapon == Weapon.Assault_Rifle)
                {
                    if (WeaponLevel == 1)
                    {
                        minWeaponDamage += 2;
                        maxWeaponDamage += 2;
                    }
                    else if (WeaponLevel == 2)
                    {
                        minWeaponDamage += 4;
                        maxWeaponDamage += 4;
                    }
                    else if (WeaponLevel == 3)
                    {
                        minWeaponDamage += 8;
                        maxWeaponDamage += 8;
                    }
                }
                break;
            case Profession.Engineer:
                if (PieceLevel == 1)
                {
                    lifeValue = 110;
                }
                else if (PieceLevel == 2)
                {
                    lifeValue = 120;
                }
                else if (PieceLevel == 3)
                {
                    lifeValue = 140;
                }

                foreach (var i in tgsSetting.GoldMinerCells)
                {
                    if (tgs.CellGetIndex(transform.position) == i)
                    {
                        if (PieceLevel == 1)
                        {
                            // 黄金数量增加 20 
                        }
                        else if (PieceLevel == 2)
                        {
                            // 黄金数量增加 40 
                        }
                        else if (PieceLevel == 3)
                        {
                            // 黄金数量增加 80
                        }
                    }
                }
                break;
            case Profession.Sniper:
                lifeValue = 100;

                if (pieceWeapon == Weapon.Sniper_Rifle)
                {
                    if (WeaponLevel == 1)
                    {
                        minWeaponDamage += 2;
                        maxWeaponDamage += 2;
                    }
                    else if (WeaponLevel == 2)
                    {
                        minWeaponDamage += 6;
                        maxWeaponDamage += 6;
                    }
                    else if (WeaponLevel == 3)
                    {
                        minWeaponDamage += 12;
                        maxWeaponDamage += 12;
                    }
                }
                break;
        }
    }

    public void UpdateWeaponProperties()
    {
        switch(pieceWeapon)
        {
            case Weapon.Pistol:
                attackRange = 1;

                if (WeaponLevel == 1)
                {
                    plusMovement = 1;
                    minWeaponDamage = 1;
                    maxWeaponDamage = 6;
                }else if (WeaponLevel == 2)
                {
                    plusMovement = 2;
                    minWeaponDamage = 2;
                    maxWeaponDamage = 12;
                }
                else if (WeaponLevel == 3)
                {
                    plusMovement = 3;
                    minWeaponDamage = 3;
                    maxWeaponDamage = 18;
                }
                break;
            case Weapon.Assault_Rifle:
                attackRange = 2;
                plusMovement = 0;

                if (WeaponLevel == 1)
                {
                    minWeaponDamage = 1;
                    maxWeaponDamage = 6;
                }
                else if (WeaponLevel == 2)
                {
                    minWeaponDamage = 2;
                    maxWeaponDamage = 12;
                }
                else if (WeaponLevel == 3)
                {
                    minWeaponDamage = 3;
                    maxWeaponDamage = 18;
                }
                break;
            case Weapon.Sniper_Rifle:
                attackRange = 3;

                if (WeaponLevel == 1)
                {
                    plusMovement = -2;
                    minWeaponDamage = 1;
                    maxWeaponDamage = 6;
                }
                else if (WeaponLevel == 2)
                {
                    plusMovement = -1;
                    minWeaponDamage = 2;
                    maxWeaponDamage = 12;
                }
                else if (WeaponLevel == 3)
                {
                    plusMovement = 0;
                    minWeaponDamage = 3;
                    maxWeaponDamage = 18;
                }
                break;
            case Weapon.Rocket_Launcher:
                attackRange = 2;

                if (WeaponLevel == 1)
                {
                    plusMovement = -2;
                    minWeaponDamage = 1;
                    maxWeaponDamage = 6;
                }
                else if (WeaponLevel == 2)
                {
                    plusMovement = -1;
                    minWeaponDamage = 2;
                    maxWeaponDamage = 12;
                }
                else if (WeaponLevel == 3)
                {
                    plusMovement = 0;
                    minWeaponDamage = 3;
                    maxWeaponDamage = 18;
                }
                break;
        }
    }

    public void UpdateEquipmentProperties()
    {
        switch (equipment)
        {
            case Equipment.None:
                break;
            case Equipment.Wire:
                equipmentRange = 1;
                if (equipmentLevel == 1)
                {
                    equipmentDurability = 1;
                }
                else if (equipmentLevel == 2)
                {
                    equipmentDurability = 2;
                }
                else if (equipmentLevel == 3)
                {
                    equipmentDurability = 3;
                }
                break;
            case Equipment.First_Aid_Kit:
                if (equipmentLevel == 1)
                {
                    equipmentDurability = 1;
                }
                else if (equipmentLevel == 2)
                {
                    equipmentDurability = 2;
                }
                else if (equipmentLevel == 3)
                {
                    equipmentDurability = 3;
                }
                break;
            case Equipment.Bulletproof_Vest:
                equipmentDurability = 100;
                if (equipmentLevel== 1)
                {
                    damageReduce = 1;
                }else if (equipmentLevel== 2)
                {
                    damageReduce= 2;
                }else if (equipmentLevel== 3)
                {
                    damageReduce= 3;
                }
                break;
            case Equipment.Trench:
                if (equipmentLevel == 1)
                {
                    equipmentDurability = 1;
                }
                else if (equipmentLevel == 2)
                {
                    equipmentDurability = 2;
                }
                else if (equipmentLevel == 3)
                {
                    equipmentDurability = 3;
                }
                break;
        }
    }

    public void First_Aid_Kit()
    {
        currentLifeValue += 50;
        if (currentLifeValue > lifeValue)
        {
            currentLifeValue = lifeValue;
        }
        equipmentDurability--;
    }

    public void Trench()
    {
        isTrenchActive = true;
    }

}
