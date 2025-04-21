using UnityEngine;

/// <summary>
/// Controls the behavior of enemy vehicles in the simulation.
/// Manages movement speed and automatic cleanup when vehicles leave the visible area.
/// </summary>
public class EnemyCar : MonoBehaviour
{
    /// <summary>
    /// Movement speed of the enemy car in units per second
    /// </summary>
    public float speed = 2f;

    /// <summary>
    /// Updates the enemy car position each frame and destroys it when it moves out of bounds.
    /// </summary>
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }
}
