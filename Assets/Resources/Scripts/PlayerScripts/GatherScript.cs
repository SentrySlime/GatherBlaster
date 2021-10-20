using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherScript : MonoBehaviour
{

    //public GameObject pickedUpIngredient;
    public List<GameObject> pickedUpIngredient = new List<GameObject>();
    public GameObject tempObj;

    void Start()
    {
        
    }

    
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && tempObj != null)
            PickUpIngredient(tempObj);
    }


    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ingredient"))
        {
            print("Huull");
            tempObj = other.gameObject;
        }
        else
            tempObj = null;
    }


    public void PickUpIngredient(GameObject ingredient)
    {
        
        pickedUpIngredient.Add(ingredient);
        ingredient.SetActive(false);
        tempObj = null;
    }

}
