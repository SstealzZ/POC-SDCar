using UnityEngine;

public class RouteScroller : MonoBehaviour
{
    public float baseSpeed = 2f;
    public Transform car;
    private CarIA carIA;

    public float resetY = -10f;
    public float startY = 10f;

    void Start()
    {
        carIA = car.GetComponent<CarIA>();
    }

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
