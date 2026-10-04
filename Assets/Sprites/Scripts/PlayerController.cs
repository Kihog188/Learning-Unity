using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var kb = Keyboard.current;
        if (kb == null) return;

        Vector2 dir = Vector2.zero;
        if (kb.wKey.isPressed) dir.y += 1;
        if (kb.sKey.isPressed) dir.y -= 1;
        if (kb.aKey.isPressed) dir.x -= 1;
        if (kb.dKey.isPressed) dir.x += 1;

        moveInput = dir.normalized;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * speed;
    }
}
