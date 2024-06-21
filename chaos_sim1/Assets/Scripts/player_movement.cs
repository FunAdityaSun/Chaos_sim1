using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class player_movement : MonoBehaviour
{
    public Rigidbody rb;
    private Vector3 initialHit;
    private bool isDragging = false;
    private bool isMoving = false;
    private float startTime = 0f;

    public Slider forceSlider;
    public Slider stopTimeSlider;
    public Text forceText;
    public Text stopTimeText;
    public float stopTime = 5.0f;
    public float force = 10f;
    //public Text coordsText;
    private Vector3 endCoords;
    private Vector3 startCoords;
    private Vector3 initialPosition;

    public Button resetButton;
    public GameObject coordsPrefab;
    public Transform scrollTransform;

    // public LineRenderer lineRenderer;
    // private List<Vector3> linePositions = new List<Vector3>();

    //private List<Vector3> startCoordsList = new List<Vector3>();
    //private List<Vector3> endCoordsList = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = transform.position;
        resetButton.onClick.AddListener(ResetBall);

        forceSlider.value = force;
        stopTimeSlider.value = stopTime;
        UpdateForce(force);
        UpdateStopTime(stopTime);

        forceSlider.onValueChanged.AddListener(UpdateForce);
        stopTimeSlider.onValueChanged.AddListener(UpdateStopTime);

        // linePositions.Add(transform.position);
        // lineRenderer.positionCount = linePositions.Count;
        // lineRenderer.SetPositions(linePositions.ToArray());
    }

    public void UpdateForce(float force)
    {
        this.force = force;
        forceText.text = force.ToString();
    }

    public void UpdateStopTime(float stopTime)
    {
        this.stopTime = stopTime;
        stopTimeText.text = stopTime.ToString();
    }

    public void ResetBall()
    {
        transform.position = initialPosition;
        rb.velocity = Vector3.zero;
        startTime = 0f;
        isMoving = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == gameObject)
            {
                isDragging = true;
                initialHit = hit.point;
            }
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            Ray ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray1, out hit))
            {
                Vector3 endHit = ray1.GetPoint(hit.distance);
                Vector3 direction = (endHit - initialHit).normalized;
                rb.AddForce(direction * force, ForceMode.Impulse);
            }
            startTime = 0f;
            startCoords = transform.position;
            isMoving = true;
            isDragging = false;
        }

        if (isMoving)
        {
            startTime += Time.deltaTime;
            if (startTime >= stopTime)
            {
                rb.velocity = Vector3.zero;
                endCoords = transform.position;
                UpdateCoords(startCoords, endCoords);

                // linePositions.Add(transform.position);
                // lineRenderer.positionCount = linePositions.Count;
                // lineRenderer.SetPositions(linePositions.ToArray());

                //transform.position = initialPosition;
                startTime = 0f;
                isMoving = false;
            }
        }

    }

    void UpdateCoords(Vector3 startCoords, Vector3 endCoords)
    {
        /**
        if (coordsText != null)
        {
            string coordsStr = string.Format("({0:F2}, {1:F2}) | ({2:F2}, {3:F2})",
                                           startCoords.x, startCoords.z, 
                                           endCoords.x, endCoords.z);
            Debug.Log(coordsStr);
            coordsText.text = coordsStr + "\n";
            Debug.Log(coordsText.text);
            Canvas.ForceUpdateCanvases(); 
        }
        else
        {
            Debug.Log("Coords text not set");
        }**/
        GameObject coordsText = Instantiate(coordsPrefab, scrollTransform);
        Text coords = coordsText.GetComponent<Text>();
        coords.text = string.Format("({0:F2}, {1:F2}) | ({2:F2}, {3:F2})",
                                    startCoords.x, startCoords.z,
                                    endCoords.x, endCoords.z);
        
    }
}
