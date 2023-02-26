using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ship_moveController : MonoBehaviour
{
    Vector2 m_Position;
    Rigidbody2D rb;
    public float speed;
    bool clicked = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider!=null && hit.collider.gameObject.tag == "Holder")
            {
                clicked = true;
            }
        }
        if (clicked && Input.GetMouseButton(0))
        {
            m_Position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if(Mathf.Abs(m_Position.x - rb.position.x) > 0.05 || Mathf.Abs(m_Position.y - rb.position.y) > 0.05)
            {
                rb.velocity = new Vector2(m_Position.x - rb.position.x, m_Position.y - rb.position.y).normalized * speed;
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            clicked = false;
            rb.velocity = new Vector2(0, 0);
        }
    }
}
