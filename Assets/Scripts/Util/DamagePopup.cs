using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    private float moveYSpeed;
    private float moveXSpeed;

    private float duration;
    private float spawnAt;

    float _r, _g, _b;

    // Update is called once per frame
    float c;
    void Update()
    {
        transform.position += new Vector3(moveXSpeed * Time.deltaTime, moveYSpeed * Time.deltaTime, 0);
        text.fontMaterial.SetColor("_FaceColor", Color.Lerp(Color.white, Color.clear, (Time.time - spawnAt) / duration));
        if (Time.time - spawnAt >= duration)
        {
            gameObject.SetActive(false);
        }
    }

    public void Setup(float damage, float moveY, float moveX, float d, int size)
    {
        moveXSpeed = moveX;
        moveYSpeed = moveY;
        duration = d;
        text.text = damage.ToString();
        text.fontSize = size;

        _r = text.color.r;
        _g = text.color.g;
        _b = text.color.b;

        c = 1;

        spawnAt = Time.time;
    }

    public void Reset()
    {
        text.fontMaterial.SetColor("_FaceColor", Color.white);
    }
}
