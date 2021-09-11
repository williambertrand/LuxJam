using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopupManager : MonoBehaviour
{

    public static DamagePopupManager Instance;

    [SerializeField] DamagePopup DamagePopupPrefab;
    [SerializeField] int capacity;

    Queue<DamagePopup> popups = new Queue<DamagePopup>();

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < capacity; i++)
        {
            DamagePopup obj = Instantiate(DamagePopupPrefab);
            obj.transform.SetParent(transform);
            obj.gameObject.SetActive(false);
            popups.Enqueue(obj);
        }

    }

    public DamagePopup SpawnPopup(Vector3 pos, float damage)
    {

        int size = (int)Mathf.Lerp(10, 24, damage / 100);

        DamagePopup pop = popups.Dequeue();
        pop.gameObject.SetActive(true);
        pop.Setup(damage, 0.3f, 0.6f, 1.0f, size);
        pop.transform.position = pos;
        popups.Enqueue(pop);
        return pop;
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
}
