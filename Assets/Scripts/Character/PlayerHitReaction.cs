using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitReaction : PlayerHit
{
    GameObject toolbar;
    ToolbarController toolbarController;
    public override void Hit()
    {
        toolbar = GameObject.FindWithTag("toolbar");
        toolbarController = GetComponent<ToolbarController>();
        
        switch (toolbarController.GetItem.Name)
        {
            case "Food_Corn":
                Debug.Log("");
                AddHunger(20);
                break;

            case "Food_Parsley":
                Debug.Log(" ");
                AddHealth(50);
                AddHunger(80);
                AddHunger(80);
                break;

            case "Food_Potato":
                Debug.Log(" ");
                AddHealth(100);
                AddHunger(250);
                break;

            case "Food_Strawberry":
                Debug.Log(" ");
                AddHealth(30);
                AddHunger(100);
                break;

            case "Food_Tomato":
                Debug.Log(" ");
                AddHunger(50);
                break;
                
        }
                 
        
        GameManager.instance.inventoryContainer.RemoveItem(GameManager.instance.toolbarControllerGlobal.GetItem, 1);
        toolbar.SetActive(!toolbar.activeInHierarchy);
        toolbar.SetActive(true);
    }

    void AddHunger(int add)
    {
        if(HungerController.currentHunger + add < 500)
            HungerController.currentHunger += add;
        else
            HungerController.currentHunger = 500;
    }

    void AddHealth(int add)
    {
        if (HealthController.currentHealth + add < 100)
            HealthController.currentHealth += add;
        else
            HealthController.currentHealth = 100;
    }
    
}