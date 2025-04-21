using UnityEngine;

/// <summary>
/// Controls the scrolling of the road/background elements to create the illusion of movement.
/// Synchronizes scrolling speed with the player car's current velocity.
/// </summary>
public class RouteScroller : MonoBehaviour
{
    /// <summary>
    /// Default scrolling speed when not synchronized with the car
    /// </summary>
    public float baseSpeed = 2f;
    
    /// <summary>
    /// Reference to the player car transform
    /// </summary>
    public Transform car;
    
    /// <summary>
    /// Reference to the car's AI component for speed synchronization
    /// </summary>
    private CarIA carIA;

    /// <summary>
    /// Y position at which the route element resets to the starting position
    /// </summary>
    public float resetY = -10f;
    
    /// <summary>
    /// Starting Y position for the route element after reset
    /// </summary>
    public float startY = 10f;

    /// <summary>
    /// Initializes the component by obtaining a reference to the car's AI controller.
    /// </summary>
    void Start()
    {
        carIA = car.GetComponent<CarIA>();
    }

    /// <summary>
    /// Updates the route element position each frame based on the car's speed.
    /// Handles the looping behavior to create infinite scrolling effect.
    /// </summary>
    void Update()
    {
        float speed = baseSpeed;

        if (carIA != null)
        {
            speed = carIA.vitesseActuelle;
        }

        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y <= resetY)
        {
            transform.position = new Vector3(transform.position.x, startY, transform.position.z);
        }
    }
}
