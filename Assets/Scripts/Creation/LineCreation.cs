using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCreation : MonoBehaviour
{
    [SerializeField]private GameObject circle;
    [SerializeField]private GameObject linePrefab;
    public LineRenderer lRend;
    [SerializeField]private int numLines;
    [SerializeField]private int minLines;
    [SerializeField]private float lineLength;
    [SerializeField]private float maxWidth;
    [SerializeField]private float minWidth;
    [SerializeField]private int eyeDis;
    [SerializeField]private int maxEyes;
    private GameObject newLineGen;
    // Start is called before the first frame update
    // void Start()
    // {
    //     SetUpLines();
    // }

    void Update(){
        if(Input.GetKeyDown(KeyCode.J)){
            SetUpLines();
        }
    }

    //Makes random lines going random directions with a random gradient
    void SetUpLines(){
        if(newLineGen != null){
            Destroy(newLineGen);
        }
        newLineGen = Instantiate(linePrefab);
        lRend = newLineGen.GetComponent<LineRenderer>();
        lRend.positionCount = Random.Range(minLines,numLines);
        lRend.colorGradient = MakeGradient();
        for(int i = 0; i < lRend.positionCount; i++){
            float disX = Random.Range(i * -lineLength, i * lineLength);
            float disY = Random.Range(i * -lineLength, i * lineLength);
            lRend.startWidth = Random.Range(minWidth,maxWidth);
            lRend.SetPosition(i, new Vector3(disX,disY,0));
        }

        PlaceEyes();
    }

    //Places eyes on the hexamon in similarish places
    void PlaceEyes(){
        int numEyes = Random.Range(1,maxEyes);
        for(int j = 0; j < numEyes; j++){
            float x = Random.Range(-lineLength * eyeDis,lineLength * eyeDis);
            float y = Random.Range(-lineLength * eyeDis,lineLength * eyeDis);
            Vector3 vec = new Vector3(transform.position.x + x,transform.position.y + y,0);
            var circ = Instantiate(circle, vec, Quaternion.identity);
            circ.transform.parent = lRend.GetComponent<Transform>();
        }
    }

    //Makes a gradient
    public Gradient MakeGradient(){
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

    //Makes a color
    Color32 MakeColor(){
        return new Color32(
        (byte)UnityEngine.Random.Range(0, 255), //Red
        (byte)UnityEngine.Random.Range(0, 255), //Green
        (byte)UnityEngine.Random.Range(0, 255), //Blue
        255 //Alpha (transparency)
        );
    }
}
