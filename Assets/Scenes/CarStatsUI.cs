using UnityEngine;
using TMPro;

public class CarStatsUI : MonoBehaviour
{
    public TextMeshProUGUI txtKm;
    public TextMeshProUGUI txtSpeed;
    public TextMeshProUGUI txtCollision;

    public CarIA carIA; // ✅ on référence directement le script IA
    public float kmSimules = 0f;
    public float kmMax = 50f;

    void Update()
    {
        if (carIA == null) return;

        // ✅ Avancement basé sur la vraie vitesse de la voiture
        kmSimules += carIA.vitesseActuelle * Time.deltaTime;

        txtKm.text = "KM : " + Mathf.FloorToInt(kmSimules) + " / " + kmMax;
        txtSpeed.text = "Vitesse : " + carIA.vitesseActuelle.ToString("F1") + " u/s";

        // ✅ Raycast depuis la position du véhicule
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
