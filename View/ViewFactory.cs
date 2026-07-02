using UnityEngine;
using System.Collections.Generic;

public class ViewFactory
{
    private BoardSettings stgs;
    private GameObject prefab;
    private GameObject origin;
    public ViewFactory(BoardSettings stgs, GameObject prefab, GameObject origin)
    {
        this.stgs = stgs;
        this.prefab = prefab;
        this.origin = origin;
    }
    public BoardView CreatBoardViewAndSphere()
    {
        float localRadius = stgs.radius;
        GameObject[] objs = new GameObject[stgs.layer * stgs.face * stgs.size * stgs.size];
        HashSet<Vector3> posHistory = new HashSet<Vector3>();
        for (int l = 0; l < stgs.layer; l++)
        {
            for (int f = 0; f < stgs.face; f++)
            {
                for (int s1 = 0; s1 < stgs.size; s1++)
                {
                    for (int s2 = 0; s2 < stgs.size; s2++)
                    {
                        int id = l * stgs.face * stgs.size * stgs.size +
                                 f * stgs.size * stgs.size +
                                 s1 * stgs.size + s2;
                        Vector3 cubePos = GetPositionOnCube((Face)f, s1, s2);
                        if (posHistory.Contains(cubePos))
                        {
                            objs[id] = null; // 数合わせのnullを入れる
                            continue;
                        }
                        posHistory.Add(cubePos);
                        Vector3 spherePos = CubeToSphere(cubePos);
                        spherePos *= localRadius;
                        spherePos += origin.transform.position;
                        Quaternion rotation = Quaternion.LookRotation(spherePos - origin.transform.position);
                        GameObject obj = MonoBehaviour.Instantiate(prefab, spherePos, rotation, origin.transform);
                        CellView view = obj.GetComponent<CellView>();
                        view.id = id;
                        objs[id] = obj;
                    }
                }
            }
            localRadius *= stgs.reductionRatioPerLayer;
        }

        foreach (GameObject target in objs)
        {
            foreach (GameObject obj in objs)
            {
                if (target == null || obj == null) continue;
                if (target == obj) continue;
                if (Vector3.Distance(target.transform.position, obj.transform.position) < stgs.minDistance)
                {
                    CellView targetView = target.GetComponent<CellView>();
                    targetView.SetNeighbor(obj);
                }
            }
        }

        return new BoardView(objs);
    }
    public Vector3 CubeToSphere(Vector3 p)
    {
        float x = p.x;
        float y = p.y;
        float z = p.z;

        float sx = x * Mathf.Sqrt(1.0f - (y * y / 2.0f) - (z * z / 2.0f) + (y * y * z * z / 3.0f));
        float sy = y * Mathf.Sqrt(1.0f - (z * z / 2.0f) - (x * x / 2.0f) + (z * z * x * x / 3.0f));
        float sz = z * Mathf.Sqrt(1.0f - (x * x / 2.0f) - (y * y / 2.0f) + (x * x * y * y / 3.0f));

        return new Vector3(sx, sy, sz);
    }
    private enum Face { Front, Back, Top, Bottom, Right, Left }
    private Vector3 GetPositionOnCube(Face face, float u, float v)
    {
        u = (float)((u / (stgs.size - 1) * 2.0f) - 1.0f);
        v = (float)((v / (stgs.size - 1) * 2.0f) - 1.0f);
        switch (face)
        {
            case Face.Front:  return new Vector3( u, v,  1);
            case Face.Back:   return new Vector3(-u, v, -1);
            case Face.Top:    return new Vector3(u,  1,  v);
            case Face.Bottom: return new Vector3(u, -1,  v);
            case Face.Right:  return new Vector3( 1, v, -u);
            case Face.Left:   return new Vector3(-1, v,  u);
            default: return Vector3.zero;
        }
    }
}