using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GraphDraw : MonoBehaviour
{
    [SerializeField] private GameObject GraphPrefab;
    [SerializeField] private float lineWidth;
    [SerializeField] private Gradient previewLineColor;
    [SerializeField] private Gradient staticLineColor;
    [SerializeField] private Gradient dynamicLineColor;
    [HideInInspector] public TMP_InputField minField;
    [HideInInspector] public TMP_InputField maxField;
    public bool isDynamic = false;
    public GameObject MinFieldObject;
    public GameObject MaxFieldObject;
    private float minX;
    private float maxX;
    public TMP_Text ErrorField;
    private float inputA = 0;
    private float inputB = 0;
    private float centerX = 0;
    private float centerY = 0;

    private GameObject currentLine;
    [HideInInspector] public GraphSetting currentLineScript;
    Dictionary<string, string> errorMessages = new Dictionary<string, string>(){
        {"hide",""},
        {"range","xの範囲を確認してみよう！"},
        {"valid","数字を確認してみよう！"}
    };
    void Awake()
    {
        minField = MinFieldObject.gameObject.GetComponent<TMP_InputField>();
        maxField = MaxFieldObject.gameObject.GetComponent<TMP_InputField>();
        minX = float.Parse(minField.text);
        maxX = float.Parse(maxField.text);
    }
    void Start()
    {
    }
    void InstantiateGraph()
    {
        // rangeErrorText.text = "";
        currentLine = Instantiate(GraphPrefab, this.transform); //インスタンス化
        currentLineScript = currentLine.GetComponent<GraphSetting>(); //GraphスクリプトをGraphプレハブから取得
        currentLineScript.SetLineColor(previewLineColor);
        currentLineScript.SetLineWidth(lineWidth);
    }
    void Draw()
    {
        DeletePreview();
        InstantiateGraph();
        // float minX = -5;
        float range = maxX - minX;
        float y;
        float x = minX;

        for (int i = 0; i < range * 10 + 1; i++)//ここで頂点の数を調整して、処理の重さを変える
        {
            try
            {
                //開始点に、計算したx,yのベクトルを足すと、新しい点がプロットできる
                y = inputA * x + inputB;
                if (-50 < y && y < 50)
                {
                    //開始点に、計算したx,yのベクトルを足すと、新しい点がプロットできる
                    currentLineScript.AddPoint(new Vector3(x + centerX, y + centerY, 0));
                }
                x += 0.1f; //xを次のポイントへ
            }
            catch
            {
                Debug.Log("点打てなかった");
                currentLineScript.DeleteThisGraph();
            }
        }
        currentLine.tag = "Preview";
    }
    public void DeletePreview()
    {
        GameObject[] previews = GameObject.FindGameObjectsWithTag("Preview");
        foreach (GameObject preview in previews)
        {
            Destroy(preview);
        }
    }

    public void CompleteGraph()
    {
        if (isDynamic)
        {
            currentLine.tag = "DynamicGraph";
            currentLineScript.SetLineColor(dynamicLineColor);
        }
        else
        {
            currentLine.tag = "StaticGraph";
            currentLineScript.SetLineColor(staticLineColor);
        }
    }
    // 変数が変更された時の処理
    public void UpdateInputA(string a)
    {
        if (float.TryParse(a, out inputA))
        {
            Debug.Log(inputA);
            Draw();
        }
        else
        {
            Debug.Log("UpdateInputA couldn't parse a string...");
        }
    }
    public void UpdateInputB(string b)
    {
        if (float.TryParse(b, out inputB))
        {
            Debug.Log(inputB);
            Draw();
        }
        else
        {
            Debug.Log("UpdateInputB couldn't parse a string...");
        }
    }
    public void UpdateRange()
    {
        if (float.TryParse(minField.text, out minX) && float.TryParse(maxField.text, out maxX))
        {
            if (minX < maxX)
            {
                UpdateErrorMessage("hide");
                Draw();
            }
            else
            {
                UpdateErrorMessage("range");
            }
        }
        else
        {
            UpdateErrorMessage("valid");
        }
    }
    private void UpdateErrorMessage(string s)
    {
        ErrorField.text = errorMessages[s];
    }
    public void ToggleIsDynamic()
    {
        isDynamic = !isDynamic;
    }
}
