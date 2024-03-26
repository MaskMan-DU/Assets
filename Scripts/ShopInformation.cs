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
    public string level1MovementEffect;
    public string level2MovementEffect;
    public string level3MovementEffect;
    public string weaponAttackRange;
    public PieceProperties.Weapon weaponValue;

    public Weapon (string weaponName, string level1MovementEffect, string level2MovementEffect, string level3MovementEffect, string weaponAttackRange, PieceProperties.Weapon weaponValue)
    {
        this.weaponName = weaponName;
        this.level1MovementEffect = level1MovementEffect;
        this.level2MovementEffect = level2MovementEffect;
        this.level3MovementEffect = level3MovementEffect;
        this.weaponAttackRange = weaponAttackRange;
        this.weaponValue = weaponValue;
    }
}

public class Ability
{
    public string ablilityName;
    public string level1AbilityDescription;
    public string level2AbilityDescription;
    public string level3AbilityDescription;
    public PieceProperties.Ability ablilityValue;

    public Ability(string ablilityName, string level1AbilityDescription, string level2AbilityDescription, string level3AbilityDescription, PieceProperties.Ability ablilityValue)
    {
        this.ablilityName = ablilityName;
        this.level1AbilityDescription = level1AbilityDescription;
        this.level2AbilityDescription = level2AbilityDescription;
        this.level3AbilityDescription = level3AbilityDescription;
        this.ablilityValue = ablilityValue;
    }
}

public class Equipment
{
    public string equipmentName;
    public string level1EquipmentDescription;
    public string level2EquipmentDescription;
    public string level3EquipmentDescription;
    public PieceProperties.Equipment equipmentValue;

    public Equipment(string equipmentName, string level1EquipmentDescription, string level2EquipmentDescription, string level3EquipmentDescription, PieceProperties.Equipment equipmentValue)
    {
        this.equipmentName = equipmentName;
        this.level1EquipmentDescription = level1EquipmentDescription;
        this.level2EquipmentDescription = level2EquipmentDescription;
        this.level3EquipmentDescription = level3EquipmentDescription;
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
            ("Pistol", // ������
            "+1", // �������ƶ������Ӱ��
            "+2", // �������ƶ������Ӱ��
            "+3", // �������ƶ������Ӱ��
            "1", // ����������Χ
            PieceProperties.Weapon.Pistol) // ������Ӧ�ı���
            );
        WeaponList.Add(
            "Assault Rifle", // Key
            new Weapon
            ("Assault Rifle", // ������
            "0", // �������ƶ������Ӱ��
            "0", // �������ƶ������Ӱ��
            "0", // �������ƶ������Ӱ��
            "2", // ����������Χ
            PieceProperties.Weapon.Assault_Rifle) // ������Ӧ�ı���
            );
        WeaponList.Add(
            "Sniper Rifle", // Key
            new Weapon
            ("Sniper Rifle", // ������
            "-2", // �������ƶ������Ӱ��
            "-1", // �������ƶ������Ӱ��
            "0", // �������ƶ������Ӱ��
            "3", // ����������Χ
            PieceProperties.Weapon.Sniper_Rifle) // ������Ӧ�ı���
            );
        WeaponList.Add(
            "Rocket Launcher", // Key
            new Weapon
            ("Rocket Launcher", // ������
            "-2", // �������ƶ������Ӱ��
            "-1", // �������ƶ������Ӱ��
            "0", // �������ƶ������Ӱ��
            "2", // ����������Χ
            PieceProperties.Weapon.Rocket_Launcher) // ������Ӧ�ı���
            );
    }

    private void AddAbility()
    {
        AbilityList.Add(
            "Spiritual Leader", // Key
            new Ability
            ("Spiritual Leader", // ������
            "All team members' weapon damage +1", // ������������������ְҵ�ȼ���������ֵҲ��֮�仯
            "All team members' weapon damage +2", // ������������������ְҵ�ȼ���������ֵҲ��֮�仯
            "All team members' weapon damage +4", // ������������������ְҵ�ȼ���������ֵҲ��֮�仯
            PieceProperties.Ability.Spiritual_Leader) // ְҵ��Ӧ�ı���
            );
        AbilityList.Add(
            "Nutritionist", // Key
            new Ability
            ("Nutritionist", // ������
            "All team members' maximum health limit +5 ", // ������������������ְҵ�ȼ���������ֵҲ��֮�仯
            "All team members' maximum health limit +10 ", // ������������������ְҵ�ȼ���������ֵҲ��֮�仯
            "All team members' maximum health limit +20 ", // ������������������ְҵ�ȼ���������ֵҲ��֮�仯
            PieceProperties.Ability.Nutritionist) // ְҵ��Ӧ�ı���
            );
        AbilityList.Add(
            "Fitness Trainer", // Key
            new Ability
            ("Fitness Trainer", // ������
            "All team members' movement speed +1", // ������������������ְҵ�ȼ���������ֵҲ��֮�仯
            "All team members' movement speed +1", // ������������������ְҵ�ȼ���������ֵҲ��֮�仯
            "All team members' movement speed +1", // ������������������ְҵ�ȼ���������ֵҲ��֮�仯
            PieceProperties.Ability.Fitness_Trainer) // ְҵ��Ӧ�ı���
            );
        AbilityList.Add(
            "Plunderer", // Key
            new Ability
            ("Plunderer", // ������
            "Gain +5 coins per extra kill", // ������������������ְҵ�ȼ���������ֵҲ��֮�仯
            "Gain +10 coins per extra kill", // ������������������ְҵ�ȼ���������ֵҲ��֮�仯
            "Gain +20 coins per extra kill", // ������������������ְҵ�ȼ���������ֵҲ��֮�仯
            PieceProperties.Ability.Plunderer) // ְҵ��Ӧ�ı���
            );
        AbilityList.Add(
            "Gold Miner", // Key
            new Ability
            ("Gold Miner", // ������
            "Gold Output +10/turn ", // ������������������ְҵ�ȼ���������ֵҲ��֮�仯
            "Gold Output +20/turn ", // ������������������ְҵ�ȼ���������ֵҲ��֮�仯
            "Gold Output +40/turn ", // ������������������ְҵ�ȼ���������ֵҲ��֮�仯
            PieceProperties.Ability.Gold_Miner) // ְҵ��Ӧ�ı���
            );
        AbilityList.Add(
            "Shooting Instructor", // Key
            new Ability
            ("Shooting Instructor", // ������
            "All team members' sniper rifle damage +2", // ������������������ְҵ�ȼ���������ֵҲ��֮�仯
            "All team members' sniper rifle damage +4", // ������������������ְҵ�ȼ���������ֵҲ��֮�仯
            "All team members' sniper rifle damage +8", // ������������������ְҵ�ȼ���������ֵҲ��֮�仯
            PieceProperties.Ability.Shooting_Instructor) // ְҵ��Ӧ�ı���
            );
        AbilityList.Add(
            "Veteran", // Key
            new Ability
            ("Veteran", // ������
            "All team members' AR Damage +2", // ������������������ְҵ�ȼ���������ֵҲ��֮�仯
            "All team members' AR Damage +4", // ������������������ְҵ�ȼ���������ֵҲ��֮�仯
            "All team members' AR Damage +8", // ������������������ְҵ�ȼ���������ֵҲ��֮�仯
            PieceProperties.Ability.Veteran) // ְҵ��Ӧ�ı���
            );
        AbilityList.Add(
            "Assualt Captain", // Key
            new Ability
            ("Assualt Captain", // ������
            "All team members' pistol damage +2\r\nSelf-health +10", // ������������������ְҵ�ȼ���������ֵҲ��֮�仯
            "All team members' pistol damage +4\r\nSelf-health +15", // ������������������ְҵ�ȼ���������ֵҲ��֮�仯
            "All team members' pistol damage +8\r\nSelf-health +30", // ������������������ְҵ�ȼ���������ֵҲ��֮�仯
            PieceProperties.Ability.Assualt_Captain) // ְҵ��Ӧ�ı���
            );
    }

    private void AddEquipment()
    {
        EquipmentList.Add(
            "Bulletproof Vest", // Key
            new Equipment
            ("Bulletproof Vest", // װ����
            "Damage reduction -1\r\nDurability: 100", // װ������������װ���ȼ���������ֵҲ��֮�仯
            "Damage reduction -2\r\nDurability: 100", // װ������������װ���ȼ���������ֵҲ��֮�仯
            "Damage reduction -3\r\nDurability: 100", // װ������������װ���ȼ���������ֵҲ��֮�仯
            PieceProperties.Equipment.Bulletproof_Vest)
            );
        EquipmentList.Add(
            "First Aid Kit", // Key
            new Equipment
            ("First Aid Kit", // װ����
            "Restores 50 health points\r\nUsable times: 1 \r\n", // װ������������װ���ȼ���������ֵҲ��֮�仯
            "Restores 50 health points\r\nUsable times: 2 \r\n", // װ������������װ���ȼ���������ֵҲ��֮�仯
            "Restores 50 health points\r\nUsable times: 3 \r\n", // װ������������װ���ȼ���������ֵҲ��֮�仯
            PieceProperties.Equipment.First_Aid_Kit)
            );
        EquipmentList.Add(
            "Wire", // Key
            new Equipment
            ("Wire", // װ����
            "Blocks the grid \r\nRange: 1\r\nUsable times: 1 \r\n", // װ������������װ���ȼ���������ֵҲ��֮�仯
            "Blocks the grid \r\nRange: 1\r\nUsable times: 2 \r\n", // װ������������װ���ȼ���������ֵҲ��֮�仯
            "Blocks the grid \r\nRange: 1\r\nUsable times: 3 \r\n", // װ������������װ���ȼ���������ֵҲ��֮�仯
            PieceProperties.Equipment.Wire)
            );
        EquipmentList.Add(
            "Trench", // Key
            new Equipment
            ("Trench", // װ����
            "Prevents the next damage received\r\nRange: 0\r\nUsable times: 1 \r\n", // װ������������װ���ȼ���������ֵҲ��֮�仯
            "Prevents the next damage received\r\nRange: 0\r\nUsable times: 2 \r\n", // װ������������װ���ȼ���������ֵҲ��֮�仯
            "Prevents the next damage received\r\nRange: 0\r\nUsable times: 3 \r\n", // װ������������װ���ȼ���������ֵҲ��֮�仯
            PieceProperties.Equipment.Trench)
            );
    }

}
