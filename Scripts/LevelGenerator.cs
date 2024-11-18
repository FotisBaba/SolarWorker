using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject obstacle, light, parent;
    
    public List<GameObject> objectPositions, objects;
    // Start is called before the first frame update
    void Start()
    {
        GenerateObstacles();
        PlaceObjects();
        RandomizeLight();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateObstacles()
    {
        float r, ra;
        float j = 6.7f;
        while (j <= 6.7 * 2)
        {
            for (int i = 1; i < 20; i++)
            {
                r = Random.Range(0f, 1f);
                if (r < .6f)
                {
                    Instantiate(obstacle, new Vector3(i, 0.5f, j), new Quaternion(0f, 0f, 0f, 0f));
                }
                ra = Random.Range(0f, 1f);
                if (ra < .6f)
                {
                    Instantiate(obstacle, new Vector3(j, 0.5f, i), new Quaternion(0f, 0f, 0f, 0f));
                }
            }

            j *= 2;
        }
        transform.GetChild(0).GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    private void PlaceObjects()
    {
        List<GameObject> obj = new List<GameObject>(objects);
        List<GameObject> objPos = new List<GameObject>(objectPositions);
        float r, ra;
        for (int i = 0; i < objects.Count; i++)
        {
            r = Random.Range(0f, objPos.Count-1);
            ra = Random.Range(0f, obj.Count-1);
            Instantiate(obj[(int)ra], objPos[(int)r].transform.position, new Quaternion(0, 0, 0, 0));
            obj.Remove(obj[(int)ra]);
            objPos.Remove(objPos[(int)r]);
        }
    }

    private void RandomizeLight()
    {
        int r;
        r = Random.Range(0, 4);
        switch (r)
        {
            case 0:
                light.transform.rotation = Quaternion.Euler(20f, 0f, 0f);
                break;
            case 1:
                light.transform.rotation = Quaternion.Euler(20f, 90f, 0f);
                break;
            case 2:
                light.transform.rotation = Quaternion.Euler(20f, -90f, 0f);
                break;
            case 3:
                light.transform.rotation = Quaternion.Euler(20f, 180f, 0f);
                break;
        }
    }
}
