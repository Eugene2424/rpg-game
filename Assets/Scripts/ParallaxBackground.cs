using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private float _moveModifier = 0.34f;
    private Vector3 _startPos;

    private void Start()
    {
         _startPos = transform.position;
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float posX = Mathf.Lerp(transform.position.x, _startPos.x + (mousePos.x * _moveModifier), 2f * Time.deltaTime);
        float posY = Mathf.Lerp(transform.position.y, _startPos.y + (mousePos.y * _moveModifier), 2f * Time.deltaTime);
        
        transform.position = new Vector3(posX, posY, 0);
    }
}
