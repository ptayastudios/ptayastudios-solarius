using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class slots : MonoBehaviour
{
    public int index;
    public GameObject actHability;
    
    public void OnDrop(PointerEventData eventData){
        var drag = eventData.pointerDrag.GetComponent<habilityItem>();

        if(drag != null){
            Destroy(actHability);

            actHability = eventData.pointerDrag;
            actHability.transform.SetParent(transform);
            actHability.transform.position = transform.position;

            lumi_golpes.Instance.habilities[index] = drag.name_;
        }
    }



}
