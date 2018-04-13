using UnityEngine;

public class EnemyCheckSurroundings : MonoBehaviour
{

    [Header("GameObjects")]
    private GameObject _target;

    private EnemyMovement _move;

    void Start()
    {
        _move = GetComponent<EnemyMovement>();
    }


    public bool CheckDistance(float distance, GameObject target)
    {
        return (Vector2.Distance(transform.position, target.transform.position) <= distance);
    }

    public int GetDirection()
    {
        if (_target.transform.position.x > transform.position.x)
            return 1;
        else
            return -1;
    }


    public bool CheckPos(bool ray, Vector2 offset, float length)
    {
        offset = new Vector2(transform.position.x + offset.x, transform.position.y + offset.y);
        RaycastHit2D hit = Physics2D.Raycast(offset, Vector2.down, length);
        Debug.DrawRay(offset, Vector2.down * length, Color.red);
        if (hit && hit.collider.tag == "Platform")
        {
            if (hit.point.y > transform.position.y - 0.5f)
                _move.Climb();
            else
                ray = true; 
        }
        else
            ray = false;
        return ray;
    }
}