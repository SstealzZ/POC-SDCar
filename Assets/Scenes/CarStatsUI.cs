using UnityEngine;
using TMPro;

/// <summary>
/// Manages the user interface displaying real-time statistics about the self-driving car.
/// Monitors and displays distance traveled, current speed, and obstacle proximity information.
/// </summary>
public class CarStatsUI : MonoBehaviour
{
    /// <summary>
    /// Text element displaying distance traveled
    /// </summary>
    public TextMeshProUGUI txtKm;
    
    /// <summary>
    /// Text element displaying current speed
    /// </summary>
    public TextMeshProUGUI txtSpeed;
    
    /// <summary>
    /// Text element displaying collision risk information
    /// </summary>
    public TextMeshProUGUI txtCollision;

    /// <summary>
    /// Reference to the self-driving car AI component
    /// </summary>
    public CarIA carIA;
    
    /// <summary>
    /// Simulated distance traveled in kilometers
    /// </summary>
    public float kmSimules = 0f;
    
    /// <summary>
    /// Maximum distance goal for the simulation
    /// </summary>
    public float kmMax = 50f;

    /// <summary>
    /// Updates the UI elements with current car statistics each frame.
    /// Calculates distance traveled based on car speed and displays obstacle detection information.
    /// </summary>
    void Update()
    {
        if (carIA == null) return;

        kmSimules += carIA.vitesseActuelle * Time.deltaTime;

        txtKm.text = "KM : " + Mathf.FloorToInt(kmSimules) + " / " + kmMax;
        txtSpeed.text = "Vitesse : " + carIA.vitesseActuelle.ToString("F1") + " u/s";

        RaycastHit2D hit = Physics2D.Raycast(carIA.transform.position, Vector2.up, 10f, LayerMask.GetMask("Enemy"));

        if (hit.collider != null)
        {
            float dist = hit.distance;
            txtCollision.text = "Obstacle à : " + dist.ToString("F1") + " u";
        }
        else
        {
            txtCollision.text = "Obstacle : Aucun";
        }
    }
}
