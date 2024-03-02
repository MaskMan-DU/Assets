using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Profession
{
    public string professionName;
    public string professionLifeValueDescription;
    public string professionSkillDescription;
    public PieceProperties.Profession professionValue;

    public Profession(string professionName, string professionLifeValueDescription, string professionSkillDescription, PieceProperties.Profession professionValue)
    {
        this.professionName = professionName;
        this.professionLifeValueDescription = professionLifeValueDescription;
        this.professionSkillDescription = professionSkillDescription;
        this.professionValue = professionValue;
    }
}

public class Weapon
{
    public string weaponName;
    public string movementEffe;
    public string weaponAttackRange;
    public PieceProperties.Weapon weaponValue;

    public Weapon (string weaponName, string movementEffe, string weaponAttackRange, PieceProperties.Weapon weaponValue)
    {
        this.weaponName = weaponName;
        this.movementEffe = movementEffe;
        this.weaponAttackRange = weaponAttackRange;
        this.weaponValue = weaponValue;
    }
}

public class Ability
{
    public string ablilityName;
    public string ablilityDescription;
    public PieceProperties.Ability ablilityValue;

    public Ability(string ablilityName, string ablilityDescription, PieceProperties.Ability ablilityValue)
    {
        this.ablilityName = ablilityName;
        this.ablilityDescription = ablilityDescription;
        this.ablilityValue = ablilityValue;
    }
}

public class Equipment
{
    public string equipmentName;
    public string equipmentDescription;
    public PieceProperties.Equipment equipmentValue;

    public Equipment(string equipmentName, string equipmentDescription, PieceProperties.Equipment equipmentValue)
    {
        this.equipmentName = equipmentName;
        this.equipmentDescription = equipmentDescription;
        this.equipmentValue = equipmentValue;
    }
}

public class ShopInformation :MonoBehaviour
{
    public Dictionary<string, Profession> ProfessionList = new Dictionary<string, Profession>();

    public Dictionary<string, Weapon> WeaponList = new Dictionary<string, Weapon>();

    public Dictionary<string, Ability> AbilityList = new Dictionary<string, Ability>();

    public Dictionary<string, Equipment> EquipmentList = new Dictionary<string, Equipment>();

    private void Awake()
    {
        AddProfession();
        AddWeapon();
        AddAbility();
        AddEquipment();
    }

    private void AddProfession()
    {
        ProfessionList.Add(
            "Cowboy", // Key
            new Profession
            ("Cowboy", // 职业名
            "", // 职业生命值描述，依据等级提升，生命值也随之变化，或者始终保持不变
            "", // 职业固有能力描述，依据等级提升数值
            PieceProperties.Profession.Cowboy) // 职业对应的变量
            );
        ProfessionList.Add(
            "Mercenary", // Key
            new Profession
            ("Mercenary", // 职业名
            "", // 职业生命值描述，依据等级提升，生命值也随之变化，或者始终保持不变
            "", // 职业固有能力描述，依据等级提升数值
            PieceProperties.Profession.Mercenary) // 职业对应的变量
            );
        ProfessionList.Add(
            "Engineer", // Key
            new Profession
            ("Engineer", // 职业名
            "", // 职业生命值描述，依据等级提升，生命值也随之变化，或者始终保持不变
            "", // 职业固有能力描述，依据等级提升数值
            PieceProperties.Profession.Engineer) // 职业对应的变量
            );
        ProfessionList.Add(
            "Sniper", // Key
            new Profession
            ("Sniper", // 职业名
            "", // 职业生命值描述，依据等级提升，生命值也随之变化，或者始终保持不变
            "", // 职业固有能力描述，依据等级提升数值
            PieceProperties.Profession.Sniper) // 职业对应的变量
            );
    }

    private void AddWeapon()
    {
        WeaponList.Add(
            "Pistol", // Key
            new Weapon
            ("Cowboy", // 武器名
            "", // 武器对移动距离的影响
            "", // 武器攻击范围
            PieceProperties.Weapon.Pistol) // 武器对应的变量
            );
        WeaponList.Add(
            "Assault Rifle", // Key
            new Weapon
            ("Assault Rifle", // 武器名
            "", // 武器对移动距离的影响
            "", // 武器攻击范围
            PieceProperties.Weapon.Assault_Rifle) // 武器对应的变量
            );
        WeaponList.Add(
            "Sniper Rifle", // Key
            new Weapon
            ("Sniper Rifle", // 武器名
            "", // 武器对移动距离的影响
            "", // 武器攻击范围
            PieceProperties.Weapon.Sniper_Rifle) // 武器对应的变量
            );
        WeaponList.Add(
            "Rocket Launcher", // Key
            new Weapon
            ("Rocket Launcher", // 武器名
            "", // 武器对移动距离的影响
            "", // 武器攻击范围
            PieceProperties.Weapon.Rocket_Launcher) // 武器对应的变量
            );
    }

    private void AddAbility()
    {
        AbilityList.Add(
            "Spiritual Leader", // Key
            new Ability
            ("Spiritual Leader", // 技能名
            "", // 技能描述，依据棋子职业等级提升，数值也随之变化
            PieceProperties.Ability.Spiritual_Leader) // 职业对应的变量
            );
        AbilityList.Add(
            "Nutritionist", // Key
            new Ability
            ("Nutritionist", // 技能名
            "", // 技能描述，依据棋子职业等级提升，数值也随之变化
            PieceProperties.Ability.Nutritionist) // 职业对应的变量
            );
        AbilityList.Add(
            "Fitness Trainer", // Key
            new Ability
            ("Fitness Trainer", // 技能名
            "", // 技能描述，依据棋子职业等级提升，数值也随之变化
            PieceProperties.Ability.Fitness_Trainer) // 职业对应的变量
            );
        AbilityList.Add(
            "Plunderer", // Key
            new Ability
            ("Plunderer", // 技能名
            "", // 技能描述，依据棋子职业等级提升，数值也随之变化
            PieceProperties.Ability.Plunderer) // 职业对应的变量
            );
        AbilityList.Add(
            "Gold Miner", // Key
            new Ability
            ("Gold Miner", // 技能名
            "", // 技能描述，依据棋子职业等级提升，数值也随之变化
            PieceProperties.Ability.Gold_Miner) // 职业对应的变量
            );
        AbilityList.Add(
            "Shooting Instructor", // Key
            new Ability
            ("Shooting Instructor", // 技能名
            "", // 技能描述，依据棋子职业等级提升，数值也随之变化
            PieceProperties.Ability.Shooting_Instructor) // 职业对应的变量
            );
        AbilityList.Add(
            "Veteran", // Key
            new Ability
            ("Veteran", // 技能名
            "", // 技能描述，依据棋子职业等级提升，数值也随之变化
            PieceProperties.Ability.Veteran) // 职业对应的变量
            );
        AbilityList.Add(
            "Assualt Captain", // Key
            new Ability
            ("Assualt Captain", // 技能名
            "", // 技能描述，依据棋子职业等级提升，数值也随之变化
            PieceProperties.Ability.Assualt_Captain) // 职业对应的变量
            );
    }

    private void AddEquipment()
    {
        EquipmentList.Add(
            "Bulletproof Vest", // Key
            new Equipment
            ("Bulletproof Vest", // 装备名
            "", // 装备描述，依据装备等级提升，数值也随之变化
            PieceProperties.Equipment.Bulletproof_Vest)
            );
        EquipmentList.Add(
            "First Aid Kit", // Key
            new Equipment
            ("First Aid Kit", // 装备名
            "", // 装备描述，依据装备等级提升，数值也随之变化
            PieceProperties.Equipment.First_Aid_Kit)
            );
        EquipmentList.Add(
            "Wire", // Key
            new Equipment
            ("Wire", // 装备名
            "", // 装备描述，依据装备等级提升，数值也随之变化
            PieceProperties.Equipment.Wire)
            );
        EquipmentList.Add(
            "Trench", // Key
            new Equipment
            ("Trench", // 装备名
            "", // 装备描述，依据装备等级提升，数值也随之变化
            PieceProperties.Equipment.Trench)
            );
    }

}
