using UnityEngine;

public class Ui_manager : MonoBehaviour
{
    public GameObject menu;

    void start(){
        menu.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            menu.SetActive(!menu.activeSelf);
        }
    }
}
