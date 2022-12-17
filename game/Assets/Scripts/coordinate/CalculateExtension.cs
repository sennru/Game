using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaluculateExtention
{
    public class LinearFunctionValue : MonoBehaviour
    {
        static public float GetValue(float x1, float x2, float y1, float y2, float x)
        {
            var a = (y1 - y2) / (x1 - x2);
            var b = y1 - a * x1;
            var y = a * x + b;
            return y;
        }
        //x1:xïœâªâ∫å¿ x2:xïœâªè„å¿, n1:yïœâªâ∫å¿ n2:yïœâªè„å¿ x:ïœêîë„ì¸ó 
    }

    public class PositionCalculate : MonoBehaviour
    {
        public static float RandomDegree(float min, float max)
        {
            float deg = Random.Range(min, max);
            return deg * Mathf.Deg2Rad;
        }

        public static Vector3 position(float radius)
        {
            var pos = new Vector3();
            float y_rot, z_rot, x, y, z;

            y_rot = RandomDegree(0f, 360f);
            z_rot = RandomDegree(60f, 90f);
            x = radius * Mathf.Sin(z_rot) * Mathf.Cos(y_rot);
            z = radius * Mathf.Sin(z_rot) * Mathf.Sin(y_rot);
            y = radius * Mathf.Cos(z_rot);

            pos.x = x; pos.y = y; pos.z = z;

            return pos;
        }

        public static float PositionToRadius(Vector3 pos)
        {
            var radius = Mathf.Sqrt(Mathf.Pow(pos.x, 2) + Mathf.Pow(pos.y, 2) + Mathf.Pow(pos.z, 2));
            return radius;
        }

        public static float Distance(Vector3 Start, Vector3 End)
        {
            var dis = Mathf.Sqrt(Mathf.Pow(Start.x - End.x, 2) + Mathf.Pow(Start.y - End.y, 2) + Mathf.Pow(Start.z - End.z, 2));
            return dis;
        }
    }
}

