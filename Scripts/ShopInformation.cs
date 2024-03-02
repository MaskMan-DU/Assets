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
            ("Cowboy", // ְҵ��
            "", // ְҵ����ֵ���������ݵȼ�����������ֵҲ��֮�仯������ʼ�ձ��ֲ���
            "", // ְҵ�����������������ݵȼ�������ֵ
            PieceProperties.Profession.Cowboy) // ְҵ��Ӧ�ı���
            );
        ProfessionList.Add(
            "Mercenary", // Key
            new Profession
            ("Mercenary", // ְҵ��
            "", // ְҵ����ֵ���������ݵȼ�����������ֵҲ��֮�仯������ʼ�ձ��ֲ���
            "", // ְҵ�����������������ݵȼ�������ֵ
            PieceProperties.Profession.Mercenary) // ְҵ��Ӧ�ı���
            );
        ProfessionList.Add(
            "Engineer", // Key
            new Profession
            ("Engineer", // ְҵ��
            "", // ְҵ����ֵ���������ݵȼ�����������ֵҲ��֮�仯������ʼ�ձ��ֲ���
            "", // ְҵ�����������������ݵȼ�������ֵ
            PieceProperties.Profession.Engineer) // ְҵ��Ӧ�ı���
            );
        ProfessionList.Add(
            "Sniper", // Key
            new Profession
            ("Sniper", // ְҵ��
            "", // ְҵ����ֵ���������ݵȼ�����������ֵҲ��֮�仯������ʼ�ձ��ֲ���
            "", // ְҵ�����������������ݵȼ�������ֵ
            PieceProperties.Profession.Sniper) // ְҵ��Ӧ�ı���
            );
    }

    private void AddWeapon()
    {
        WeaponList.Add(
            "Pistol", // Key
            new Weapon
            ("Cowboy", // ������
            "", // �������ƶ������Ӱ��
            "", // ����������Χ
            PieceProperties.Weapon.Pistol) // ������Ӧ�ı���
            );
        WeaponList.Add(
            "Assault Rifle", // Key
            new Weapon
            ("Assault Rifle", // ������
            "", // �������ƶ������Ӱ��
            "", // ����������Χ
            PieceProperties.Weapon.Assault_Rifle) // ������Ӧ�ı���
            );
        WeaponList.Add(
            "Sniper Rifle", // Key
            new Weapon
            ("Sniper Rifle", // ������
            "", // �������ƶ������Ӱ��
            "", // ����������Χ
            PieceProperties.Weapon.Sniper_Rifle) // ������Ӧ�ı���
            );
        WeaponList.Add(
            "Rocket Launcher", // Key
            new Weapon
            ("Rocket Launcher", // ������
            "", // �������ƶ������Ӱ��
            "", // ����������Χ
            PieceProperties.Weapon.Rocket_Launcher) // ������Ӧ�ı���
            );
    }

    private void AddAbility()
    {
        AbilityList.Add(
            "Spiritual Leader", // Key
            new Ability
            ("Spiritual Leader", // ������
            "", // ������������������ְҵ�ȼ���������ֵҲ��֮�仯
            PieceProperties.Ability.Spiritual_Leader) // ְҵ��Ӧ�ı���
            );
        AbilityList.Add(
            "Nutritionist", // Key
            new Ability
            ("Nutritionist", // ������
            "", // ������������������ְҵ�ȼ���������ֵҲ��֮�仯
            PieceProperties.Ability.Nutritionist) // ְҵ��Ӧ�ı���
            );
        AbilityList.Add(
            "Fitness Trainer", // Key
            new Ability
            ("Fitness Trainer", // ������
            "", // ������������������ְҵ�ȼ���������ֵҲ��֮�仯
            PieceProperties.Ability.Fitness_Trainer) // ְҵ��Ӧ�ı���
            );
        AbilityList.Add(
            "Plunderer", // Key
            new Ability
            ("Plunderer", // ������
            "", // ������������������ְҵ�ȼ���������ֵҲ��֮�仯
            PieceProperties.Ability.Plunderer) // ְҵ��Ӧ�ı���
            );
        AbilityList.Add(
            "Gold Miner", // Key
            new Ability
            ("Gold Miner", // ������
            "", // ������������������ְҵ�ȼ���������ֵҲ��֮�仯
            PieceProperties.Ability.Gold_Miner) // ְҵ��Ӧ�ı���
            );
        AbilityList.Add(
            "Shooting Instructor", // Key
            new Ability
            ("Shooting Instructor", // ������
            "", // ������������������ְҵ�ȼ���������ֵҲ��֮�仯
            PieceProperties.Ability.Shooting_Instructor) // ְҵ��Ӧ�ı���
            );
        AbilityList.Add(
            "Veteran", // Key
            new Ability
            ("Veteran", // ������
            "", // ������������������ְҵ�ȼ���������ֵҲ��֮�仯
            PieceProperties.Ability.Veteran) // ְҵ��Ӧ�ı���
            );
        AbilityList.Add(
            "Assualt Captain", // Key
            new Ability
            ("Assualt Captain", // ������
            "", // ������������������ְҵ�ȼ���������ֵҲ��֮�仯
            PieceProperties.Ability.Assualt_Captain) // ְҵ��Ӧ�ı���
            );
    }

    private void AddEquipment()
    {
        EquipmentList.Add(
            "Bulletproof Vest", // Key
            new Equipment
            ("Bulletproof Vest", // װ����
            "", // װ������������װ���ȼ���������ֵҲ��֮�仯
            PieceProperties.Equipment.Bulletproof_Vest)
            );
        EquipmentList.Add(
            "First Aid Kit", // Key
            new Equipment
            ("First Aid Kit", // װ����
            "", // װ������������װ���ȼ���������ֵҲ��֮�仯
            PieceProperties.Equipment.First_Aid_Kit)
            );
        EquipmentList.Add(
            "Wire", // Key
            new Equipment
            ("Wire", // װ����
            "", // װ������������װ���ȼ���������ֵҲ��֮�仯
            PieceProperties.Equipment.Wire)
            );
        EquipmentList.Add(
            "Trench", // Key
            new Equipment
            ("Trench", // װ����
            "", // װ������������װ���ȼ���������ֵҲ��֮�仯
            PieceProperties.Equipment.Trench)
            );
    }

}
