using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts.towers.visuals
{
    [RequireComponent(typeof(LineRenderer))]
    public class RangeVisualizer : MonoBehaviour
    {
        public Color circleColor;
        
        public int numSegments = 60;

       

        public void RenderCircle(float radius, Vector3 center)
        {
            LineRenderer renderer = this.gameObject.GetComponent<LineRenderer>();
            renderer.SetPosition(0, center);
            renderer.loop = true; 
            renderer.positionCount = numSegments;
            renderer.startColor = circleColor;
            renderer.endColor = circleColor;
            renderer.startWidth = 0.3f;
            renderer.endWidth = 0.3f;
            float angle = 0f;

            for (int i = 0; i < numSegments; i++)
            {
                float x = radius * Mathf.Cos(angle);
                float y = radius * Mathf.Sin(angle);
                Vector3 pos = center + new Vector3(x, 0.5f, y);
                renderer.SetPosition(i, pos);

                angle += 2f * Mathf.PI / numSegments;
            }
        }
    }
}
