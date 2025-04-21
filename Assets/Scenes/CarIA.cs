using UnityEngine;

/// <summary>
/// Self-driving car AI that manages obstacle detection, lane changing behavior,
/// and adaptive speed control based on environmental conditions.
/// </summary>
public class CarIA : MonoBehaviour
{
    [Header("Detection")]
    public float detectionDistance = 8f;
    public float distanceChangementVoie = 5f;
    public LayerMask enemyLayer;

    [Header("Speed")]
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

    /// <summary>
    /// Initializes the car position to the default lane on component awakening.
    /// </summary>
    void Awake()
    {
        Vector3 pos = transform.position;
        pos.x = lanesX[currentLane];
        transform.position = pos;
    }

    /// <summary>
    /// Updates the car's behavior each frame, handling obstacle detection,
    /// lane changing decisions, and speed adjustments.
    /// </summary>
    void Update()
    {
        changementVoieTimer -= Time.deltaTime;
        float targetSpeed = vitesseMax;

        Vector2 origin = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.up, detectionDistance, enemyLayer);

        if (hit.collider != null)
        {
            float distance = hit.distance;
            targetSpeed = Mathf.Lerp(vitesseMin, vitesseMax, distance / detectionDistance);

            if (distance < distanceChangementVoie && !isChangingLane && changementVoieTimer <= 0)
            {
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

        vitesseActuelle = Mathf.Lerp(vitesseActuelle, targetSpeed, Time.deltaTime * ralentissementForce);

        Vector3 currentPos = transform.position;
        float targetX = lanesX[targetLane];
        currentPos.x = Mathf.Lerp(currentPos.x, targetX, Time.deltaTime * laneChangeSpeed);
        transform.position = currentPos;

        if (Mathf.Abs(currentPos.x - targetX) < 0.01f)
        {
            isChangingLane = false;
        }
    }

    /// <summary>
    /// Determines the optimal lane for the car based on obstacle detection and safety analysis.
    /// 
    /// <returns>The index of the best lane to move to</returns>
    /// </summary>
    int FindBestLane()
    {
        float[] distances = new float[lanesX.Length];
        bool[] laneBlocked = new bool[lanesX.Length];

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

        int bestLane = currentLane;
        float bestScore = -1f;

        for (int i = 0; i < lanesX.Length; i++)
        {
            if (!laneBlocked[i])
            {
                float score = detectionDistance;
                
                score -= Mathf.Abs(i - currentLane) * 2;
                
                if (score > bestScore)
                {
                    bestScore = score;
                    bestLane = i;
                }
            }
            else if (distances[i] > distances[currentLane])
            {
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
