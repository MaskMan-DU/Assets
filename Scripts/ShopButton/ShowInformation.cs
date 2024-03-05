using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShowInformation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler

{
    public GameObject DetailInformation;

    public void OnPointerEnter(PointerEventData eventData)
    {
        DetailInformation.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DetailInformation.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        DetailInformation.SetActive(false);
    }
}
