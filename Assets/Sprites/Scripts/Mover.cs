using UnityEngine;
using UnityEngine.InputSystem;

public class Mover : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    void Update()
    {
        var kb = Keyboard.current;
        if (kb == null) return;

        Vector2 dir = Vector2.zero;
        if (kb.wKey.isPressed) dir.y += 1;
        if (kb.sKey.isPressed) dir.y -= 1;
        if (kb.aKey.isPressed) dir.x -= 1;
        if (kb.dKey.isPressed) dir.x += 1;

        transform.Translate(dir.normalized * speed * Time.deltaTime);
    }
}