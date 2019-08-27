using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonoView3D : MonoBehaviour
{
    Rigidbody wheelRigidbody;
    float timerForRandomValue = 0;
    float timer = 0;
    public Rigidbody arrowRigidbody;
    float prevAnglVelZ;
    bool isSpinning = false;
    public Text sumWin;
    public Text lastWin;
    public List<TextMesh> prizeValuesTexts;
    public ButtonScript spinButton;
    CoreManager cm = new CoreManager();

    void Start()
    {
        arrowRigidbody.centerOfMass = new Vector3(0.5f, 0f, 0f);
        wheelRigidbody = GetComponent<Rigidbody>();
        cm.NumGenerator(10, 40);
        cm.SetNumsToWheelSectors(prizeValuesTexts);
        cm.Start();
    }

    private void OnApplicationQuit()
    {
        cm.Quit();
    }

    private void Update()
    {
        if (spinButton.spin && !isSpinning)
        {
            timer = 0;
            timerForRandomValue = UnityEngine.Random.Range(6f, 7.6f);
            isSpinning = true;
            spinButton.spin = false;
        }
        if (timer < timerForRandomValue)
        {
            timer += Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if (timer < timerForRandomValue)
        {
            SpinWheel();
        }
        else if (isSpinning)
        {
            CheckArrowPosition();
        }

        ArrowGravity();
    }

    void SpinWheel()
    {
        wheelRigidbody.AddTorque(Vector3.back * 5f);
    }

    void ArrowGravity()
    {
        arrowRigidbody.AddTorque(Vector3.back * 1f);
    }

    void CheckArrowPosition()
    {
        if (prevAnglVelZ == arrowRigidbody.angularVelocity.z)
        {
            RayFromArrowForCheck();
        }
        prevAnglVelZ = arrowRigidbody.angularVelocity.z;
    }

    void RayFromArrowForCheck()
    {
        RaycastHit hit;

        if (Physics.Raycast(arrowRigidbody.transform.position, arrowRigidbody.transform.TransformDirection(Vector3.left), out hit, 1f))
        {

            string a = hit.collider.transform.GetChild(0).GetComponent<TextMesh>().text;

            cm.SetNumsToScoreBoard(a, lastWin, sumWin);
            cm.SpinResult(a);
            isSpinning = false;
            spinButton.spin = false;
        }
    }

}
