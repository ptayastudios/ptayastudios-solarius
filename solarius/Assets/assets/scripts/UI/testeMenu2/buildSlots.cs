using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class buildSlots : MonoBehaviour, IDropHandler
{
    public int slot;
    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        item habilidade = dropped.GetComponent<item>();

        if (habilidade != null)
        {
            habilidade.parentAfterDrag = transform;


            lumi_golpes.Instance.HabilitySet(slot, habilidade.name_);
        }
    }
}
