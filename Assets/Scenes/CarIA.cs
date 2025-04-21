using UnityEngine;

public class CarIA : MonoBehaviour
{
    [Header("Détection")]
    public float detectionDistance = 8f;
    public float distanceChangementVoie = 5f;
    public LayerMask enemyLayer;

    [Header("Vitesse")]
    public float vitesseMax = 5f;
    public float vitesseMin = 1f;
    public float vitesseActuelle = 2f;
    public float ralentissementForce = 2f;
    
    public float[] lanesX = new float[] { -1.5f, -0.5f, 0.5f };
    private int currentLane = 1;
    private int targetLane = 1;
    private float changementVoieTimer = 0f;
    private float delaiMinChangementVoie = 1f;

    public float laneChangeSpeed = 5f;
    private bool isChangingLane = false;

    void Awake()
    {
        Vector3 pos = transform.position;
        pos.x = lanesX[currentLane];
        transform.position = pos;
    }

    void Update()
    {
        changementVoieTimer -= Time.deltaTime;
        float targetSpeed = vitesseMax;

        // 🔍 Détection d'obstacle sur la voie actuelle
        Vector2 origin = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.up, detectionDistance, enemyLayer);

        if (hit.collider != null)
        {
            float distance = hit.distance;
            targetSpeed = Mathf.Lerp(vitesseMin, vitesseMax, distance / detectionDistance);

            // Changement de voie uniquement si on est assez proche et après le délai minimum
            if (distance < distanceChangementVoie && !isChangingLane && changementVoieTimer <= 0)
            {
                // Analyse de toutes les voies pour trouver la meilleure option
                int bestLane = FindBestLane();
                if (bestLane != currentLane)
                {
                    targetLane = bestLane;
                    currentLane = bestLane;
                    isChangingLane = true;
                    changementVoieTimer = delaiMinChangementVoie;
                }
            }
        }
        else
        {
            isChangingLane = false;
        }

        // Transition de vitesse
        vitesseActuelle = Mathf.Lerp(vitesseActuelle, targetSpeed, Time.deltaTime * ralentissementForce);

        // Mouvement fluide vers voie cible
        Vector3 currentPos = transform.position;
        float targetX = lanesX[targetLane];
        currentPos.x = Mathf.Lerp(currentPos.x, targetX, Time.deltaTime * laneChangeSpeed);
        transform.position = currentPos;

        // Reset du changement de voie si on est arrivé à destination
        if (Mathf.Abs(currentPos.x - targetX) < 0.01f)
        {
            isChangingLane = false;
        }
    }

    int FindBestLane()
    {
        float[] distances = new float[lanesX.Length];
        bool[] laneBlocked = new bool[lanesX.Length];

        // Vérifier chaque voie
        for (int i = 0; i < lanesX.Length; i++)
        {
            Vector3 checkPos = new Vector3(lanesX[i], transform.position.y, transform.position.z);
            RaycastHit2D laneCheck = Physics2D.Raycast(checkPos, Vector2.up, detectionDistance, enemyLayer);

            if (laneCheck.collider != null)
            {
                distances[i] = laneCheck.distance;
                laneBlocked[i] = true;
            }
            else
            {
                distances[i] = detectionDistance;
                laneBlocked[i] = false;
            }
        }

        // Trouver la meilleure voie
        int bestLane = currentLane;
        float bestScore = -1f;

        for (int i = 0; i < lanesX.Length; i++)
        {
            if (!laneBlocked[i])
            {
                // Favoriser les voies libres
                float score = detectionDistance;
                
                // Pénaliser les changements de voie trop grands
                score -= Mathf.Abs(i - currentLane) * 2;
                
                if (score > bestScore)
                {
                    bestScore = score;
                    bestLane = i;
                }
            }
            else if (distances[i] > distances[currentLane])
            {
                // Si toutes les voies sont bloquées, choisir celle avec l'obstacle le plus loin
                float score = distances[i] - Mathf.Abs(i - currentLane) * 2;
                if (score > bestScore)
                {
                    bestScore = score;
                    bestLane = i;
                }
            }
        }

        return bestLane;
    }
}
