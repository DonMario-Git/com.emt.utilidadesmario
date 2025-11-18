using UnityEngine;

namespace EMT.UtilidadesMario
{
    /// <summary>
    /// marca con gizmos el rectángulo de un canvas en la escena
    /// </summary>
    public class CanvasMarker : MonoBehaviour
    {
        public RectTransform canvasRTr;
        public Color color = Color.yellow;

        private void OnDrawGizmos()
        {
            // Si no está asignado, intenta obtener el RectTransform del propio GameObject
            if (canvasRTr == null)
                canvasRTr = GetComponent<RectTransform>();
            if (canvasRTr == null)
                return;

            Gizmos.color = color;
    
            Vector3[] corners = new Vector3[4];
            canvasRTr.GetWorldCorners(corners);

            // Dibuja las líneas entre los vértices del rectángulo
            Gizmos.DrawLine(corners[0], corners[1]);
            Gizmos.DrawLine(corners[1], corners[2]);
            Gizmos.DrawLine(corners[2], corners[3]);
            Gizmos.DrawLine(corners[3], corners[0]);
        }
    }
}


