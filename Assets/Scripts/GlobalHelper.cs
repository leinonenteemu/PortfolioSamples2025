
using UnityEngine;

namespace Helpers { 

public static class GlobalHelper
{
    public static Transform PlayerTransform { get; private set; }

    public static void SetPlayerTransform(Transform p) => PlayerTransform = p;

    public static bool IsObjectInRange(Transform A, Transform B, float range)
    {
        return Vector3.Distance(A.position, B.position) <= range;
    }
    public static bool IsObjectInRangeVector2(Transform A, Transform B, float range)
    {
        return Vector2.Distance(A.position, B.position) <= range;
    }

    public static bool IsObjectInRangeVector2(Vector2 A, Vector2 B, float range)
        {
            return Vector2.Distance(A, B) <= range;
        }

    public static Vector3 DirectionTo(Vector3 from, Vector3 to)
    {
        Vector3 dir = to - from;
        return dir.normalized;
    }

    public static Vector2 DirectionTo(Vector2 from, Vector2 to, bool normalizeResult)
    {
        Vector2 dir = to - from;
        if (normalizeResult) return dir.normalized;
        return dir;
    }

    public static Vector2 DirectionFromAngle(float angleInDegrees)
    {
        return new Vector2(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad));
    }


    public static Vector2 DirectionFromAngle(float angleInDegrees, bool isLocalAngle, Transform t)
    {
        if (isLocalAngle)
        {
            angleInDegrees += t.eulerAngles.z;
        }
        return new Vector2(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), Mathf.Sin(angleInDegrees * Mathf.Deg2Rad));
    }

    public static float AngleFromDirection(Vector3 direction)
    {
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }

    public static float AngleFromDirection(Vector2 direction)
    {
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }

    public static Vector2 CalculateFuturePosition(Vector3 targetPosition, Vector3 targetVelocity, float time)
    {
        Vector2 predictedPosition = targetPosition + (targetVelocity * time);
        return predictedPosition;
    }

    public static Vector3 MouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public static Vector2 MouseWorldPositionVector2()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public static Vector2 MouseWorldPositionVector2(Camera cam)
    {
        return cam.ScreenToWorldPoint(Input.mousePosition);
    }

        public static int BuildLayerMask(LayerMask mask1, LayerMask mask2)
        {
            int mask = 1 << mask1 | 1 << mask2;
            return mask;
        }

    public static bool HasLineOfSight(Vector3 startPoint, Vector3 targetPoint)
    {
        Vector2 dir = targetPoint - startPoint;
        //layer A and B should be the comparable object's layers 
        //player
        int layerA = 6;

         //normal walls;
        int layerB = 10;

         //slideable walls
        int layerC = 15;

        var mask = 1 << layerA | 1 << layerB | 1 << layerC;
        RaycastHit2D hit = Physics2D.Raycast(startPoint, dir, Mathf.Infinity, mask);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.layer == layerB || hit.collider.gameObject.layer == layerC) return false;
            else return true;
        }
        return true;
    }

        public static int DamageableObjectsLayerMask()
        {
            //this should have all the layers that contain gameobjects the player can damage, such as enemies and barrels
            return LayerMask.GetMask("Enemies", "ThrowableBarrels");

            //int layerA = 1 << 7;
            //int layerB = 1 << 10;
            //return layerA + layerB;
        }

        public static int WallObjectsLayerMask()
        {
            return LayerMask.GetMask("Walls", "SlideableWalls");
        }

        public static bool HasLineOfSightToEnemy(Vector3 startPoint, Vector3 targetPoint)
        {
            Vector2 dir = targetPoint - startPoint;
            //layer A and B should be the comparable object's layers 
            //enemy
            int layerA = 7;

            //normal walls;
            int layerB = 10;

            //slideable walls
            int layerC = 15;

            var mask = 1 << layerA | 1 << layerB | 1 << layerC;
            RaycastHit2D hit = Physics2D.Raycast(startPoint, dir, Mathf.Infinity, mask);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.layer == layerB | hit.collider.gameObject.layer == layerC) return false;
                else return true;
            }
            return true;
        }

        public static bool HasLineOfSight(Vector3 startPoint, Vector3 targetPoint, LayerMask targetMask, LayerMask obstacleMask)
        {
            Vector2 dir = targetPoint - startPoint;
            var mask = targetMask + obstacleMask;
            //layer A and B should be the comparable object's layers 
            //int layerA = 6;
            //int layerB = 10;
            //var mask = 1 << layerA | 1 << layerB;
            
            RaycastHit2D hit = Physics2D.Raycast(startPoint, dir, Vector3.Distance(startPoint,targetPoint), mask);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.layer == targetMask) return true;
                else return false;
            }
            return true;
        }



        private static int SolveQuadratic(float a, float b, float c, out float root1, out float root2)
    {
        var discriminant = b * b - 4 * a * c;
        if (discriminant < 0)
        {
            root1 = Mathf.Infinity;
            root2 = -root1;
            return 0;
        }

        root1 = (-b + Mathf.Sqrt(discriminant))/ (2 * a);
        root2 = (-b - Mathf.Sqrt(discriminant)) / (2 * a);
        return discriminant > 1 ? 2 : 1;
    }

    public static bool InterceptionDirection(Vector2 a, Vector2 b, Vector2 vA, float sB, out Vector2 result)
    {
        var aToB = b - a;
        var dC = aToB.magnitude;
        var alpha = Vector2.Angle(aToB, vA) * Mathf.Deg2Rad;
        var sA = vA.magnitude;
        var r = sA / sB;

        int quadraticValue = SolveQuadratic(1 - r * r, 2 * r * dC * Mathf.Cos(alpha), -(dC * dC), out var root1, out var root2);

        if (quadraticValue == 0)
        {
            result = Vector2.zero;
            return false;
        }

        else
        {
            var dA = quadraticValue == 2 ? Mathf.Max(root1, root2) : Mathf.Min(root1, root2);
            var t = dA / sB;
            var c = a + vA * t;
            result = (c - b).normalized;
            return true;
        }
        
    }

    //public static Vector3 CalculateProjectileDirectionFromTargetDestinationPrediction(Vector3 targetPredictedPosition, float projectileSpeed, Vector3 startPosition)
    //{
    //    float distance = Vector3.Distance(startPosition, targetPredictedPosition);
    //}
}

}