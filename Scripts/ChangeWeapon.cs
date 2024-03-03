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
    public RigBuilder RigBuilder;
    public Animator animator;
    int currentWeapon = 0;
    int lastWeapon;
    // Start is called before the first frame update
    void Start()
    {
        RefreshWeaponBone();
        currentWeapon = pieceProperties.WeaponLevel;
        lastWeapon = currentWeapon;
    }

    // Update is called once per frame
    void Update()
    {
        currentWeapon = pieceProperties.WeaponLevel;
        if (currentWeapon != lastWeapon)
        {
            animator.enabled = false;
            RigBuilder.enabled = false;
            RefreshWeaponBone();
            lastWeapon = currentWeapon;
            RigBuilder.Build();
            RigBuilder.enabled = true;
            animator.enabled = true;
        }
    }
        
    public void RefreshWeaponBone()
    {
        
        //var rightData = Right.data;
        //var leftData = Left.data;

        pistolList[pieceProperties.WeaponLevel - 1].SetActive(true);


        Right.data.target = pistolList[pieceProperties.WeaponLevel - 1].transform.Find("ref_right_hand_grip");
        Left.data.target = pistolList[pieceProperties.WeaponLevel - 1].transform.Find("ref_left_hand_grip");



        for (int i = 0; i < pistolList.Length; i++)
        {
            if (i != pieceProperties.WeaponLevel - 1)
            {
                pistolList[i].SetActive(false);
            }
        }
    }
}
