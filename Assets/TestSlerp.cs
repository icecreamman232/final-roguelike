using UnityEngine;

public class TestSlerp : MonoBehaviour
{
    public Transform PointA;
    public Transform PointB;
    public Transform MidPoint;
    public float curveRate;
    public float t;
    
    private void Start()
    {
        
    }

    private void Update()
    {
        if (t >= 1)
        {
            t = 0;
        }
        t += Time.deltaTime;
        MidPoint.position = Vector2.Lerp(PointA.position, PointB.position, 0.5f);
        MidPoint.position = GetPointAtDistanceFromLine(MidPoint.position,PointA.position,PointB.position, curveRate);
        transform.position = Vector2.Lerp(Lerp2(PointA.position, MidPoint.position, t), Lerp2(MidPoint.position, PointB.position, t), t);
    }

    private Vector2 Lerp2(Vector2 a, Vector2 b, float t)
    {
        return Vector2.Lerp(a, b, t);
    }
    
    private Vector2 GetPointAtDistanceFromLine(Vector2 midPoint, Vector2 pointA, Vector2 pointB, float distance)
    {
        Vector2 lineDirection = (pointB - pointA).normalized;
        Vector2 offsetDirection = new Vector2(-lineDirection.y, lineDirection.x); // Perpendicular to line

        return midPoint + offsetDirection * distance;
    }
}
