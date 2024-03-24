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
        Spiritual_Leader,
        Nutritionist,
        Fitness_Trainer,
        Plunderer,
        Gold_Miner,
        Shooting_Instructor,
        Veteran,
        Assualt_Captain
    }


    [Header("基本属性")]
    public PieceType pieceType; 

    public float lifeValue = 100;
    public float finalLifeValue;

    public int plusMovement;
    public int finalMovement;

    public int attackRange;

    public Profession pieceProfession = Profession.Cowboy;
    public int PieceLevel = 1;

    public Weapon pieceWeapon = Weapon.Pistol;
    public int WeaponLevel = 1;

    public int minWeaponDamage = 1;
    public int maxWeaponDamage = 6;

    public int plusWeaponDamage;

    public int finalminWeaponDamage;
    public int finalmaxWeaponDamage;

    public int plusPistolDamage = 0;
    public int plusARDamage = 0;
    public int plusSRDamage = 0;

    public Equipment equipment = Equipment.None;
    public int equipmentLevel = 1;
    public int equipmentDurability;
    public int equipmentRange = 0;  
    public int damageReduce = 0;
    public bool isTrenchActive = false;

    public Ability ability;
    public int abilityPlusWeaponDamage = 0;
    public int abilityExtraPistolWeaponDamage = 0;
    public int abilityExtraARWeaponDamage = 0;
    public int abilityExtraSRWeaponDamage = 0;
    public int abilityPlusMovment = 0;
    public int abilityPlusLifeValue = 0;
    public int extraEnemyCoin = 0;

    public int goldOutPutSpeed = 100;


    public int enemyCoin;
    

    [Header("实时数据")]
    public float currentLifeValue;

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
            UpdateAbility();
            UpdateEquipmentProperties();
            currentLifeValue = lifeValue;
        }
        else
        {
            if (pieceType == PieceType.NormalEnemy) // 是普通敌人
            {
                lifeValue = 6f;
                finalLifeValue = lifeValue;
                WeaponLevel = 1;
                currentLifeValue = finalLifeValue;
                enemyCoin = 15;
            }
            else
            {
                lifeValue = 20f;
                finalLifeValue = lifeValue;
                WeaponLevel = Random.Range(2, 4);
                currentLifeValue = finalLifeValue;
                enemyCoin = 30;
            }
            UpdateWeaponProperties();
        }
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
        }

        if (pieceType== PieceType.Player)
        {
            switch (pieceWeapon)
            {
                case Weapon.Pistol:
                    finalminWeaponDamage = minWeaponDamage + plusPistolDamage + plusWeaponDamage;
                    finalmaxWeaponDamage = maxWeaponDamage + plusPistolDamage + plusWeaponDamage;
                    break;
                case Weapon.Assault_Rifle:
                    finalminWeaponDamage = minWeaponDamage + plusARDamage + plusWeaponDamage;
                    finalmaxWeaponDamage = maxWeaponDamage + plusARDamage + plusWeaponDamage;
                    break;
                case Weapon.Sniper_Rifle:
                    finalminWeaponDamage = minWeaponDamage + plusSRDamage + plusWeaponDamage;
                    finalmaxWeaponDamage = maxWeaponDamage + plusSRDamage + plusWeaponDamage;
                    break;
                case Weapon.Rocket_Launcher:
                    finalminWeaponDamage = minWeaponDamage + plusWeaponDamage;
                    finalmaxWeaponDamage = maxWeaponDamage + plusWeaponDamage;
                    break;
            }
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
                    // 黄金数量增加 20 
                    goldOutPutSpeed += 20;
                }
                else if (PieceLevel == 2)
                {
                    lifeValue = 120;
                    // 黄金数量增加 40 
                    goldOutPutSpeed += 40;
                }
                else if (PieceLevel == 3)
                {
                    lifeValue = 140;
                    // 黄金数量增加 80
                    goldOutPutSpeed += 80;
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
        var canInstantiate = true;
        // 生成一个堡垒,堡垒生成消耗耐久度1，
        foreach(var i in gameManager.TrenchsList)
        {
            var playerController = this.gameObject.GetComponent<PlayerContoller>();
            if (playerController.currentCellIndex == i.GetComponent<TrenchProperties>().currentCellIndex)
            {
                canInstantiate = false;
                break;
            }
        }
        
        if (canInstantiate)
        {
            Instantiate(gameManager.Trench, transform.position, Quaternion.identity);
            equipmentDurability--;
        }
    }
    
    private void UpdateAbility()
    {
        switch (ability)
        {
            case Ability.Spiritual_Leader:
                if (PieceLevel == 1)
                {
                    abilityPlusWeaponDamage = 1;
                }else if (PieceLevel == 2)
                {
                    abilityPlusWeaponDamage = 2;
                }
                else if (PieceLevel == 3)
                {
                    abilityPlusWeaponDamage = 4;
                }
                break;
            case Ability.Nutritionist:
                if (PieceLevel == 1)
                {
                    abilityPlusLifeValue = 10;
                }
                else if (PieceLevel == 2)
                {
                    abilityPlusLifeValue = 20;
                }
                else if (PieceLevel == 3)
                {
                    abilityPlusLifeValue = 40;
                }
                break;
            case Ability.Fitness_Trainer:
                abilityPlusMovment = 1;
                break;
            case Ability.Shooting_Instructor:
                if (PieceLevel == 1)
                {
                    abilityExtraSRWeaponDamage = 2;
                }
                else if (PieceLevel == 2)
                {
                    abilityExtraSRWeaponDamage = 4;
                }
                else if (PieceLevel == 3)
                {
                    abilityExtraSRWeaponDamage = 8;
                }
                break;
            case Ability.Veteran:
                if (PieceLevel == 1)
                {
                    abilityExtraARWeaponDamage = 2;
                }
                else if (PieceLevel == 2)
                {
                    abilityExtraARWeaponDamage = 4;
                }
                else if (PieceLevel == 3)
                {
                    abilityExtraARWeaponDamage = 8;
                }
                break;
            case Ability.Assualt_Captain:
                if (PieceLevel == 1)
                {
                    abilityExtraPistolWeaponDamage = 2;
                    lifeValue += 20;
                }
                else if (PieceLevel == 2)
                {
                    abilityExtraPistolWeaponDamage = 4;
                    lifeValue += 40;
                }
                else if (PieceLevel == 3)
                {
                    abilityExtraPistolWeaponDamage = 8;
                    lifeValue += 80;
                }
                break;
            case Ability.Gold_Miner:
                if (PieceLevel == 1)
                {
                    goldOutPutSpeed += 10;
                }else if (PieceLevel == 2)
                {
                    goldOutPutSpeed += 20;
                }else if (PieceLevel == 3)
                {
                    goldOutPutSpeed += 40;
                }
                break;
        }
    }

}
