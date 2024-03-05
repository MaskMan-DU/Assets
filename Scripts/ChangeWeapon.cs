using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;

public class ChangeWeapon : MonoBehaviour
{
    public TwoBoneIKConstraint Right;
    public TwoBoneIKConstraint Left;
    public PieceProperties pieceProperties;
    public GameObject[] pistolList;
    public GameObject[] ARList;
    public GameObject[] SRList;
    public GameObject[] RPGList;
    public RigBuilder RigBuilder;
    public Animator animator;
    int currentWeaponLevel = 0;
    int lastWeaponLevel;
    PieceProperties.Weapon currentWeapon;
    PieceProperties.Weapon lastWeapon;
    public ParticleSystem findShootFire;
    // Start is called before the first frame update
    void Start()
    {
        currentWeapon = pieceProperties.pieceWeapon;
        currentWeaponLevel = pieceProperties.WeaponLevel;
        lastWeaponLevel = currentWeaponLevel;
        lastWeapon = currentWeapon;
        if (pieceProperties.pieceWeapon == PieceProperties.Weapon.Pistol)
        {
            RefreshWeaponBone(pistolList);
        }
        else if (pieceProperties.pieceWeapon == PieceProperties.Weapon.Assault_Rifle)
        {
            RefreshWeaponBone(ARList);
        }
        else if (pieceProperties.pieceWeapon == PieceProperties.Weapon.Sniper_Rifle)
        {
            RefreshWeaponBone(SRList);
        }
        else
        {
            RefreshWeaponBone(RPGList);
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentWeapon = pieceProperties.pieceWeapon;
        currentWeaponLevel = pieceProperties.WeaponLevel;
        if (currentWeaponLevel != lastWeaponLevel || currentWeapon != lastWeapon)
        {
            animator.enabled = false;
            RigBuilder.enabled = false;
            if(currentWeapon == PieceProperties.Weapon.Pistol)
            {
                RefreshWeaponBone(pistolList);
                InvisibalWeapon();
            }
            else if(currentWeapon == PieceProperties.Weapon.Assault_Rifle)
            {
                RefreshWeaponBone(ARList);
                InvisibalWeapon();
            }
            else if(currentWeapon == PieceProperties.Weapon.Sniper_Rifle)
            {
                RefreshWeaponBone(SRList);
                InvisibalWeapon();
            }
            else
            {
                RefreshWeaponBone(RPGList);
                InvisibalWeapon();
            }
            lastWeapon = currentWeapon;
            lastWeaponLevel = currentWeaponLevel;
            RigBuilder.Build();
            RigBuilder.enabled = true;
            animator.enabled = true;
        }
    }
        
    public void RefreshWeaponBone(GameObject[] weaponList)
    {

        weaponList[currentWeaponLevel - 1].SetActive(true);

        Right.data.target = weaponList[currentWeaponLevel - 1].transform.Find("ref_right_hand_grip");
        Left.data.target = weaponList[currentWeaponLevel - 1].transform.Find("ref_left_hand_grip");
        findShootFire = weaponList[currentWeaponLevel - 1].GetComponentInChildren<ParticleSystem>();


    }

    public void InvisibalWeapon()
    {
        if(lastWeapon == PieceProperties.Weapon.Pistol)
        {
            pistolList[lastWeaponLevel-1].SetActive(false);
        }else if(lastWeapon == PieceProperties.Weapon.Assault_Rifle)
        {
            ARList[lastWeaponLevel-1].SetActive(false) ;
        }else if(lastWeapon == PieceProperties.Weapon.Sniper_Rifle)
        {
            SRList[lastWeaponLevel-1].SetActive(false) ;
        }
        else
        {
            RPGList[lastWeaponLevel-1].SetActive(false) ;
        }
    }
}
