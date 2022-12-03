using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CaluculateExtention
{
    public class LinearFunctionValue : MonoBehaviour
    {
        static public float GetValue(float x1, float x2, float n1, float n2, float x)
        {
            var a = (1f / (x1 - x2)) * (n1 - n2);
            var b = (1f / (x1 - x2)) * (x1 * n2 - x2 * n1);
            var n = a * x + b;
            return n;
        }

        //x1:ëŒâûè„å¿ó  x2:ëŒâûâ∫å¿ó , n1:è„å¿íl n2:â∫å¿íl x:ïœêîë„ì¸ó 
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
    }
}

