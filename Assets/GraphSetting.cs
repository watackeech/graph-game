using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphSetting : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public Rigidbody2D rigidBody;

    [HideInInspector] public List<Vector2> points = new List<Vector2>(); //EdgeCollider用のList
    [HideInInspector] public int pointsCount = 0; //頂点の数

    private float CircleColliderRadius;

    //!LineRenderer系の設定
    public void ChangeLineColor(Gradient LineColor)
    {
        lineRenderer.colorGradient = LineColor;
    }

    public void SetLineColor(Gradient LineColor)
    {
        lineRenderer.colorGradient = LineColor;
    }

    public void SetLineWidth(float width)
    {
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;

        edgeCollider.edgeRadius = width / 2f;
        CircleColliderRadius = width / 2f;
    }

    //!関数を描画するときに使う→GraphDraw.cs
    public void AddPoint(Vector2 newPoint)
    {
        points.Add(newPoint);
        pointsCount++;

        // Add CircleCollider to the point
        CircleCollider2D circleCollider = this.gameObject.AddComponent<CircleCollider2D>();
        circleCollider.offset = newPoint;
        circleCollider.radius = CircleColliderRadius;

        //LineRenderer
        lineRenderer.positionCount = pointsCount;
        lineRenderer.SetPosition(pointsCount - 1, newPoint);

        //EdgeCollider
        if (pointsCount > 1)
            edgeCollider.points = points.ToArray();
    }
    public void OnCollider()
    {
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll; //全動きを制限
        // rigidBody.constraints = RigidbodyConstraints2D.FreezePosition; //位置固定回転アリ
        rigidBody.simulated = true;
        // edgeCollider.enabled = true;
        // circleCollider.enabled = true;
    }

    public void OnDynamic()
    {
        rigidBody.constraints = RigidbodyConstraints2D.None; //制限解除
    }
    void OnTriggerEnter2D(Collider2D col)
    { //画面外にグラフが出たらオブジェクト削除
        if (col.gameObject.tag == "DethBlock")
        {
            Destroy(this.gameObject);
        }
    }
    public void DeleteThisGraph()
    {
        Destroy(this.gameObject);
    }
}
