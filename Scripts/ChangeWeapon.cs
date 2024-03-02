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
    }

    // Update is called once per frame
    void Update()
    {
        var rightData = Right.data;
        var leftData = Left.data;

        rightData.target = pistolList[pieceProperties.WeaponLevel - 1].transform.Find("ref_right_hand_grip");
        leftData.target = pistolList[pieceProperties.WeaponLevel - 1].transform.Find("ref_left_hand_grip");
    }
        //if (twoBoneIKConstraint != null && newTarget != null)
        //{

        //    var data = twoBoneIKConstraint.data;
        //    data.target = newTarget;
        //    twoBoneIKConstraint.data = data;
        //}
}
