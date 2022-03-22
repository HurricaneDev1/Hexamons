using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCreation : MonoBehaviour
{
    [SerializeField]private GameObject linePrefab;
    [SerializeField]private int numLines;
    [SerializeField]private int minLines;
    [SerializeField]private float lineLength;
    [SerializeField]private float maxWidth;
    [SerializeField]private float minWidth;
    private GameObject newLineGen;
    // Start is called before the first frame update
    void Start()
    {
        SetUpLines();
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.J)){
            SetUpLines();
        }
    }

    void SetUpLines(){
        if(newLineGen != null){
            Destroy(newLineGen);
        }
        newLineGen = Instantiate(linePrefab);
        LineRenderer lRend = newLineGen.GetComponent<LineRenderer>();
        lRend.positionCount = Random.Range(minLines,numLines);
        lRend.colorGradient = MakeGradient();
        for(int i = 0; i < lRend.positionCount; i++){
            float disX = Random.Range(i * -lineLength, i * lineLength);
            float disY = Random.Range(i * -lineLength, i * lineLength);
            lRend.startWidth = Random.Range(minWidth,maxWidth);
            lRend.SetPosition(i, new Vector3(disX,disY,0));
        }
    }

    Gradient MakeGradient(){
        Gradient gradient = new Gradient();
        float alpha = 1.0f;
        Color32 cow = MakeColor();
        Color32 cow1 = MakeColor();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(cow, 0.0f), new GradientColorKey(cow1, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        return gradient;
    }

    Color32 MakeColor(){
        return new Color32(
        (byte)UnityEngine.Random.Range(0, 255), //Red
        (byte)UnityEngine.Random.Range(0, 255), //Green
        (byte)UnityEngine.Random.Range(0, 255), //Blue
        255 //Alpha (transparency)
        );
    }
}
