using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndSceneManager : MonoBehaviour
{
    public Vector3 Pos1;
    public Vector3 Pos2;
    public Vector3 Pos3;

    public TMP_Text EndText;

    // Start is called before the first frame update
    void Start()
    {
        EndText.text = PlayerPrefs.GetString("Winner") + " win the game!\r\nThanks for playing!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
