using UnityEngine;
using UnityEngine.EventSystems;

public class TesteClick : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Botão clicado");
    }
}