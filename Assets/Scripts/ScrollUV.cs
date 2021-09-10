using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollUV : MonoBehaviour
{

    [SerializeField] private float moveFactor;
    private MeshRenderer meshrenderer;

    private void Start()
    {
        meshrenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {

        // Update main trxture offset based on player movemeent
        Material mat = meshrenderer.material;
        Vector2 offset = mat.mainTextureOffset;
        offset.x = transform.position.x * moveFactor;
        offset.y = transform.position.y * moveFactor;


        mat.mainTextureOffset = offset;
    }
}
