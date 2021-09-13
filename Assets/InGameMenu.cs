using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{

    public bool isOpen;
    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isOpen = !isOpen;

            if (isOpen)
            {
                panel.SetActive(true);
            } else
            {
                panel.SetActive(false);
            }
        }
    }

    public void Close()
    {
        panel.SetActive(false);
    }

    public void Quit()
    {
        GameManager.Instance.OnGameOver();
    }
}
