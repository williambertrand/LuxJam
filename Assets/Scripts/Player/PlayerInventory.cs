using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{


    public static PlayerInventory Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Currently using unlimited ammo
    public int bombCount;
    public long credits;

    public TMP_Text bombCountText;
    public TMP_Text creditsText;

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
            bombCountText.text = "" + bombCount;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AddCredits(int val)
    {
        credits += val;
        creditsText.text = credits.ToString();
    }

    public void OnBombPickup(int val)
    {
        bombCount += val;
        bombCountText.text = "" + bombCount;
    }
}
