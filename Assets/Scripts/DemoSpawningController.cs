using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoSpawningController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject[] waypoints;
    [SerializeField]
    Sprite sprite;
    [SerializeField]
    float speed;

    int waypoint_index = 0;
    GameObject mob;
    void Start()
    {
        CreateMob();
    }
    void CreateMob()
    {
        mob = new GameObject();
        mob.AddComponent<SpriteRenderer>().sprite = sprite;
        mob.GetComponent<SpriteRenderer>().sortingLayerName = "Monsters";
        mob.layer = LayerMask.NameToLayer("Monster");
        mob.transform.position = waypoints[waypoint_index].transform.position;
        mob.transform.localScale *= 0.5f;
    }
    // Update is called once per frame
    void Update()
    {
        //mob.transform.position = waypoints[waypoint_index].transform.position;
        if (waypoint_index < waypoints.Length)
        {
            mob.transform.position = Vector2.MoveTowards(mob.transform.position,
               waypoints[waypoint_index].transform.position,
               speed * Time.deltaTime);
        }
        if (mob.transform.position == waypoints[waypoint_index].transform.position)
        {
            waypoint_index += 1;
        }
        if (waypoint_index == waypoints.Length)
        {
            Destroy(mob);
            CreateMob();
        }
    }
}
