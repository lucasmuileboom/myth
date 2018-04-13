using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public Vector2 Move(float speed)
    {
        return new Vector2(transform.position.x + (speed * Time.deltaTime), transform.position.y);
    }

    public void Climb()
    {
        _rb.velocity += new Vector2(0f, 20) * Time.deltaTime;
    }
}
