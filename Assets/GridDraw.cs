using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridDraw : MonoBehaviour
{
    public GameObject GridPrefab;
    public GameObject AxisCountPrefab;
    public int centerX = 0;
    public int centerY = 0;
    public int xStartRelative = -22;
    public int xEndRelative = 22;
    public int yStartRelative = -14;
    public int yEndRelative = 14;

    private int xStart;
    private int xEnd;
    private int yStart;
    private int yEnd;

    private float firstWidth = 0.12f;
    private float firstAlpha = 0.9f;
    private float secondWidth = 0.08f;
    private float secondAlpha = 0.6f;
    private float thirdWidth = 0.06f;
    private float thirdAlpha = 0.3f;
    private float axisCountScale = 0.18f;
    private float originalGap = 0.4f;
    private float LineWidth;

    // public Gradient LineColor;

    GridSetting currentGrid;

    void Start()
    {
        xStart = xStartRelative + centerX;
        xEnd = xEndRelative + centerX;
        yStart = yStartRelative + centerY;
        yEnd = yEndRelative + centerY;
        DrawLines('x', xStart, xEnd, yStart, yEnd, centerX, centerY);
        DrawLines('y', yStart, yEnd, xStart, xEnd, centerY, centerX);
    }

    // void DrawGrid()
    // {
    // //! x軸
    // for (int i = xStart; i < xEnd + 1; i++)
    // {
    //     //GridPrefabをインスタンス化して、GridSettingコンポーネントを取得
    //     currentGrid = Instantiate(GridPrefab, this.transform).GetComponent<GridSetting>();
    //     if (i == centerX)
    //     {
    //         currentGrid.SetLineWidth(firstWidth);
    //         currentGrid.SetLineAlpha(firstAlpha);
    //     }
    //     else if ((i - centerX) % 5 == 0) // 5の倍数ごとに太めの線（Second)を
    //     {
    //         currentGrid.SetLineWidth(secondWidth);
    //         currentGrid.SetLineAlpha(secondAlpha);
    //         GameObject currentAxisCount = Instantiate(AxisCountPrefab, new Vector3(i, centerY - 0.5f, 0), Quaternion.identity);
    //         currentAxisCount.transform.SetParent(this.gameObject.transform);
    //         currentAxisCount.transform.localScale = new Vector3(axisCountScale, axisCountScale, 1);
    //         currentAxisCount.GetComponent<Text>().text = (i - centerX).ToString();
    //     }
    //     else
    //     {
    //         currentGrid.SetLineWidth(thirdWidth);
    //         currentGrid.SetLineAlpha(thirdAlpha);
    //     }
    //     // currentGrid.AddPoint(new Vector3(-9, i, 0));
    //     // currentGrid.AddPoint(new Vector3(9, i, 0));
    //     currentGrid.AddPoint(new Vector3(i, yStart, 0));
    //     currentGrid.AddPoint(new Vector3(i, yEnd, 0));
    // }


    // x軸
    // DrawLines('x', xStart, xEnd, yStart, yEnd, centerX, centerY, 0.5f);
    // // y軸
    // DrawLines('y', yStart, yEnd, xStart, xEnd, centerY, centerX, 0.5f);
    // for(int i = -9; i < 10; i++)
    // for (int i = yStart; i < yEnd + 1; i++)
    // {
    //     currentGrid = Instantiate(GridPrefab, this.transform).GetComponent<GridSetting>(); ;
    //     if (i == centerY)
    //     {
    //         currentGrid.SetLineWidth(firstWidth);
    //         currentGrid.SetLineAlpha(firstAlpha);
    //     }
    //     else if ((i - centerY) % 5 == 0)
    //     {
    //         currentGrid.SetLineWidth(secondWidth);
    //         currentGrid.SetLineAlpha(secondAlpha);
    //         // AxisCountPrefabのInstance化　Vector3で座標指定
    //         GameObject currentAxisCount = Instantiate(AxisCountPrefab, new Vector3(0.5f + centerX, i, 0), Quaternion.identity);
    //         currentAxisCount.transform.SetParent(this.gameObject.transform);
    //         currentAxisCount.transform.localScale = new Vector3(axisCountScale, axisCountScale, 1);
    //         currentAxisCount.GetComponent<Text>().text = (i - centerY).ToString();
    //     }
    //     else
    //     {
    //         currentGrid.SetLineWidth(thirdWidth);
    //         currentGrid.SetLineAlpha(thirdAlpha);
    //     }
    //     // currentGrid.AddPoint(new Vector3(i, -5, 0));
    //     // currentGrid.AddPoint(new Vector3(i, 5, 0));
    //     currentGrid.AddPoint(new Vector3(xStart, i, 0));
    //     currentGrid.AddPoint(new Vector3(xEnd, i, 0));
    // }

    // }

    private void DrawLines(char axis, int start, int end, int verticalStart, int verticalEnd, float center, float verticalCenter)
    {
        for (int i = start; i < end + 1; i++)
        {
            //GridPrefabをインスタンス化して、GridSettingコンポーネントを取得
            currentGrid = Instantiate(GridPrefab, this.transform).GetComponent<GridSetting>();
            if (i == center)
            {
                currentGrid.SetLineWidth(firstWidth);
                currentGrid.SetLineAlpha(firstAlpha);
                InstantiateAxisCount(axis, i, center, verticalCenter);
            }
            else if ((i - center) % 5 == 0) // 5の倍数ごとに太めの線（Second)を
            {
                currentGrid.SetLineWidth(secondWidth);
                currentGrid.SetLineAlpha(secondAlpha);
                InstantiateAxisCount(axis, i, center, verticalCenter);
            }
            else
            {
                currentGrid.SetLineWidth(thirdWidth);
                currentGrid.SetLineAlpha(thirdAlpha);
            }
            if (axis == 'x')
            {
                currentGrid.AddPoint(new Vector3(i, verticalStart, 0));
                currentGrid.AddPoint(new Vector3(i, verticalEnd, 0));
            }
            else
            {
                currentGrid.AddPoint(new Vector3(verticalStart, i, 0));
                currentGrid.AddPoint(new Vector3(verticalEnd, i, 0));
            }

        }
    }

    // 軸の数字表示
    private void InstantiateAxisCount(char axis, int i, float center, float verticalCenter)
    {
        float gap = originalGap;
        float verticalGap = originalGap;
        GameObject currentAxisCount;

        if (i >= 0)
        {
            gap *= -1;
        }
        if (axis == 'x')
        {
            currentAxisCount = Instantiate(AxisCountPrefab, new Vector3(i + gap, verticalCenter - verticalGap, 0), Quaternion.identity);
        }
        else
        {
            currentAxisCount = Instantiate(AxisCountPrefab, new Vector3(verticalCenter - verticalGap, i + gap, 0), Quaternion.identity);
        }
        currentAxisCount.transform.SetParent(this.gameObject.transform);
        currentAxisCount.transform.localScale = new Vector3(axisCountScale, axisCountScale, 1);
        currentAxisCount.GetComponent<Text>().text = (i - center).ToString();
    }


}
