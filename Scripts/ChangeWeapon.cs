using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ChangeWeapon : MonoBehaviour
{
    public TwoBoneIKConstraint Right;
    public TwoBoneIKConstraint Left;
    public PieceProperties pieceProperties;
    public GameObject[] pistolList;
    // Start is called before the first frame update
    void Start()
    {
        RefreshWeaponBone();
    }

    // Update is called once per frame
    void Update()
    {
        RefreshWeaponBone();
    }
        
    public void RefreshWeaponBone()
    {
        var rightData = Right.data;
        var leftData = Left.data;

        pistolList[pieceProperties.WeaponLevel - 1].SetActive(true);

        rightData.target = pistolList[pieceProperties.WeaponLevel - 1].transform.Find("ref_right_hand_grip");
        leftData.target = pistolList[pieceProperties.WeaponLevel - 1].transform.Find("ref_left_hand_grip");

        // print(rightData.target.transform.position);
        // print(leftData.target.transform.position);

        Right.data = rightData;
        Left.data = leftData;

        for(int i = 0; i < pistolList.Length; i++)
        {
            if (i != pieceProperties.WeaponLevel - 1)
            {
                pistolList[i].SetActive(false);
            }
        }
    }
}
