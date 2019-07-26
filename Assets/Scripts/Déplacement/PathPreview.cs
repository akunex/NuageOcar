using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathPreview : MonoBehaviour
{
    void OnDrawGizmosSelected()
    {
        var nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (nav == null || nav.path == null)
            return;

        var line = this.GetComponent<LineRenderer>();
        if (line == null)
        {
            line = this.gameObject.AddComponent<LineRenderer>();
            line.material = new Material(Shader.Find("Sprites/Default")) { color = Color.white };
            line.SetWidth(0.2f, 0.2f);
            line.SetColors(Color.white, Color.white);
        }

        var path = nav.path;

        line.SetVertexCount(path.corners.Length);

        for (int i = 0; i < path.corners.Length; i++)
        {
            line.SetPosition(i, path.corners[i]);
        }

    }
}

