using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFieldOfView : MonoBehaviour
{
    private int layerMask;
    public GameObject enemy;
    private GameObject player;
    public float fov = 90f;
    public int rayCount = 50;
    public float veiwDistance = 5f;
    public float angle = 0f;
    private Mesh mesh;
    Vector3 origin = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        layerMask =~ LayerMask.GetMask("Enemy", "Player");
        player = GameObject.Find("Player");
    }

    void LateUpdate()
    {
        mesh.RecalculateBounds();
        //if enemy is alive
        if (enemy)
        {
            float _fov = fov;
            int _rayCount = rayCount;
            float _veiwDistance = veiwDistance;
            float angleIncrease;
            int vertexIndex = 1;
            int triangleIndex = 0;

            origin = enemy.transform.position;

            float _angle = angle + enemy.transform.rotation.eulerAngles.z;

            //Create veiw cone mesh
            angleIncrease = _fov / _rayCount;

            Vector3[] vertices = new Vector3[_rayCount + 2];
            Vector2[] uv = new Vector2[vertices.Length];
            int[] triangles = new int[_rayCount * 3];

            vertices[0] = origin;

            for (int i = 0; i <= _rayCount; i++)
            {
                Vector3 vertex;
                RaycastHit2D raycastHit = Physics2D.Raycast(origin, GetVectorFromAngle(_angle), _veiwDistance, layerMask);

                if (raycastHit.collider == null)
                {
                    vertex = origin + GetVectorFromAngle(_angle) * _veiwDistance;
                }
                else
                {
                    vertex = raycastHit.point;
                }
                vertices[vertexIndex] = vertex;

                if (i > 0)
                {
                    triangles[triangleIndex + 0] = 0;
                    triangles[triangleIndex + 1] = vertexIndex - 1;
                    triangles[triangleIndex + 2] = vertexIndex;

                    triangleIndex += 3;
                }

                _angle -= angleIncrease;
                vertexIndex++;
            }

            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;

            FindPlayer();
        }
        else
        {
            //if enemy is dead destroy fov cone
            Destroy(gameObject);
        }
    }

    public static Vector3 GetVectorFromAngle(float angle)
    {
        // returns vector from given angle
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    public void FindPlayer()
    {
        int _layerMask;
        _layerMask = ~LayerMask.GetMask("Enemy");

        if (Vector3.Distance(enemy.transform.position, player.transform.position) < veiwDistance)
        {
            //player is inside view distance
            Vector3 directionToPlayer = (player.transform.position - enemy.transform.position).normalized;
            Vector3 directionFacing = (GetVectorFromAngle(enemy.transform.rotation.eulerAngles.z));

            if (Vector3.Angle(directionFacing, directionToPlayer) < fov / 2f)
            {
                //player is inside feild of view
                RaycastHit2D raycastHit2D = Physics2D.Raycast(enemy.transform.position, directionToPlayer, veiwDistance, _layerMask);
                if (raycastHit2D.collider != null)
                {
                    if (raycastHit2D.collider.gameObject.GetComponent<PlayerController>() != null)
                    {
                        //player is not behind object
                        enemy.GetComponent<Enemy>().isActive = true;
                        return;
                    }
                }
            }
        }
        enemy.GetComponent<Enemy>().isActive = false;
    }
}



