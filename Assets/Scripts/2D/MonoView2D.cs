using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MonoView2D : MonoBehaviour
{
    public RectTransform wheelRectTransform;
    public Text sumWin;
    public Text lastWin;
    public Button spinButton;
    delegate void UpdateEvents();
    UpdateEvents Events;
    public List<Text> prizeValuesTexts;
    CoreManager cm = new CoreManager();
    float angVelocity = 0f;
    const float spinTime = 5f;
    float spinTimer = 0f;
    float maxVelocity;

    private void Start()
    {
        cm.NumGenerator(10, 40);
        cm.SetNumsToWheelSectors(prizeValuesTexts);
        cm.Start();
        spinButton.onClick.AddListener(OnClickSpinButton);
        
    }

    private void OnApplicationQuit()
    {
        cm.Quit();
    }

    private void Update()
    {
        if (Events != null)
        {
            Events();
        }
    }

    void OnClickSpinButton()
    {
        if (angVelocity == 0)
        {
            spinTimer = 0f;
            Events += StartSpinEvent;
            Events += RotateWheelEvent;
            maxVelocity = Random.Range(5f, 8.2f);
        }
    }

    void EndSpinEvent()
    {
        angVelocity -= Time.deltaTime * 4;
        if (angVelocity < 0)
        {
            angVelocity = 0;
            cm.SetNumsToScoreBoard(prizeValuesTexts[GetCurrentWinSectorIndex()].text, lastWin, sumWin);
            cm.SpinResult(prizeValuesTexts[GetCurrentWinSectorIndex()].text);
            Events -= EndSpinEvent;
            Events -= RotateWheelEvent;
        }
    }

    void StartSpinEvent()
    {
        if (angVelocity < maxVelocity)
        {
            angVelocity += Time.deltaTime * 4;
        }
        spinTimer += Time.deltaTime;
        if (spinTimer > spinTime)
        {
            Events += EndSpinEvent;
            Events -= StartSpinEvent;
        }
    }

    void RotateWheelEvent()
    {
        wheelRectTransform.Rotate(new Vector3(0, 0, angVelocity));
    }

    int GetCurrentWinSectorIndex()
    {
        return (int)(wheelRectTransform.eulerAngles.z / 22.5f);
    }
}
