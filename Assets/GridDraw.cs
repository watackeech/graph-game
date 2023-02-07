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

    private float LineWidth;
    // public Gradient LineColor;

    GridSetting currentGrid;

    void Start()
    {
        xStart = xStartRelative + centerX;

        xEnd = xEndRelative + centerX;
        yStart = yStartRelative + centerY;
        yEnd = yEndRelative + centerY;
        DrawGrid();
    }

    void DrawGrid()
    {
        //! x軸の描画
        for (int i = xStart; i < xEnd + 1; i++)
        {
            //GridPrefabをインスタンス化して、GridSettingコンポーネントを取得
            currentGrid = Instantiate(GridPrefab, this.transform).GetComponent<GridSetting>();
            if (i == centerX)
            {
                currentGrid.SetLineWidth(firstWidth);
                currentGrid.SetLineAlpha(firstAlpha);
            }
            else if ((i - centerX) % 5 == 0) // 5の倍数ごとに太めの線（Second)を
            {
                currentGrid.SetLineWidth(secondWidth);
                currentGrid.SetLineAlpha(secondAlpha);
                GameObject currentAxisCount = Instantiate(AxisCountPrefab, new Vector3(i, centerY - 0.5f, 0), Quaternion.identity);
                currentAxisCount.transform.SetParent(this.gameObject.transform);
                currentAxisCount.transform.localScale = new Vector3(0.18f, 0.18f, 1);
                currentAxisCount.GetComponent<Text>().text = (i - centerX).ToString();
            }
            else
            {
                currentGrid.SetLineWidth(thirdWidth);
                currentGrid.SetLineAlpha(thirdAlpha);
            }
            // currentGrid.AddPoint(new Vector3(-9, i, 0));
            // currentGrid.AddPoint(new Vector3(9, i, 0));
            currentGrid.AddPoint(new Vector3(i, yStart, 0));
            currentGrid.AddPoint(new Vector3(i, yEnd, 0));
        }

        //y軸
        // for(int i = -9; i < 10; i++)
        for (int i = yStart; i < yEnd + 1; i++)
        {
            currentGrid = Instantiate(GridPrefab, this.transform).GetComponent<GridSetting>(); ;
            if (i == centerY)
            {
                currentGrid.SetLineWidth(firstWidth);
                currentGrid.SetLineAlpha(firstAlpha);
            }
            else if ((i - centerY) % 5 == 0)
            {
                currentGrid.SetLineWidth(secondWidth);
                currentGrid.SetLineAlpha(secondAlpha);
                GameObject currentAxisCount = Instantiate(AxisCountPrefab, new Vector3(0.5f + centerX, i, 0), Quaternion.identity);
                currentAxisCount.transform.SetParent(this.gameObject.transform);
                currentAxisCount.transform.localScale = new Vector3(0.18f, 0.18f, 1);
                currentAxisCount.GetComponent<Text>().text = (i - centerY).ToString();
            }
            else
            {
                currentGrid.SetLineWidth(thirdWidth);
                currentGrid.SetLineAlpha(thirdAlpha);
            }
            // currentGrid.AddPoint(new Vector3(i, -5, 0));
            // currentGrid.AddPoint(new Vector3(i, 5, 0));
            currentGrid.AddPoint(new Vector3(xStart, i, 0));
            currentGrid.AddPoint(new Vector3(xEnd, i, 0));
        }

    }
}
