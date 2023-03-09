using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Button_Manager : MonoBehaviour , IPointerClickHandler , IPointerExitHandler, IPointerEnterHandler
{
    public TextMeshProUGUI Text_Mesh_Pro;
    public Color Highlight_Color;

    void Awake() {
        Text_Mesh_Pro = gameObject.GetComponent<TextMeshProUGUI>();      
    }
    public void OnPointerClick(PointerEventData eventData){
        Button_function();
    }
    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        Text_Mesh_Pro.color = Highlight_Color;
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        Text_Mesh_Pro.color = Color.white;
    }

    public virtual void Button_function(){
        Application.Quit();
    }
}
