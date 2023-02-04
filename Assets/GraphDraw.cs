using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphDraw : MonoBehaviour
{
    [SerializeField] private GameObject GraphPrefab;
    [SerializeField] private float lineWidth;
    [SerializeField] private Gradient previewLineColor;
    [SerializeField] private Gradient staticLineColor;
    [SerializeField] private Gradient dynamicLineColor;
    private float inputA = 0;
    private float centerX = 0;
    private float centerY = 0;
    private GameObject currentLine;
    public GraphSetting currentLineScript;

    void Start()
    {
        // centerX = GameObject.Find("GridController").GetComponent<GridDraw>().centerX;
        // centerY = GameObject.Find("GridController").GetComponent<GridDraw>().centerY;

    }

    public void UpdateInputA(string a)
    {
        if (float.TryParse(a, out inputA))
        {
            Debug.Log(inputA);
            DrawGraph();
        }
        else
        {
            Debug.Log("UpdateInputA couldn't parse a string...");
        }
    }

    void InstantiateGraph()
    {
        // rangeErrorText.text = "";
        currentLine = Instantiate(GraphPrefab, this.transform); //インスタンス化
        currentLineScript = currentLine.GetComponent<GraphSetting>(); //GraphスクリプトをGraphプレハブから取得
        currentLineScript.SetLineColor(previewLineColor);
        currentLineScript.SetLineWidth(lineWidth);
    }

    void DrawGraph()
    {
        InstantiateGraph();
        float minX = -5;
        float range = 10;
        float y;

        float x = minX;
        for (int i = 0; i < range * 10 + 1; i++)//ここで頂点の数を調整して、処理の重さを変える
        {
            try
            {
                //開始点に、計算したx,yのベクトルを足すと、新しい点がプロットできる
                y = inputA * x + 1;
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
            }
            // calcFuncString = poweredString.Replace("x", x.ToString()); //計算用の文字列に代入
            // try
            // {
            //     y = System.Convert.ToSingle(new DataTable().Compute(calcFuncString, null)); //計算用の文字列を計算⇒float型に変換
            //                                                                                 // Debug.Log($"{x}でyは{y}");
            //     if (-50 < y && y < 50)
            //     {
            //         currentLineScript.AddPoint(new Vector3(x + centerX, y + centerY, 0) + startPoint); //開始点に、計算したx,yのベクトルを足すと、新しい点がプロットできる
            //     }
            //     x += 0.1f; //xを次のポイントへ
            // }
            // catch
            // {
            //     // Debug.Log("例外発生中！");
            //     rangeErrorText.text = "式を見直してみよう！";
            //     currentLineScript.deleteThisGraph();
            // }
        }
    }

}
