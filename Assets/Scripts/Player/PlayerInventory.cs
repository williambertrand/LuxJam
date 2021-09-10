using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{

    // Currently using unlimited ammo
    public int bombCount;

    [SerializeField] private TMP_Text bombCountText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool ExpendBomb()
    {
        if(bombCount > 0)
        {
            bombCount -= 1;
            bombCountText.text = "Bombs: " + bombCount;
            return true;
        }
        else
        {
            return false;
        }
    }
}
