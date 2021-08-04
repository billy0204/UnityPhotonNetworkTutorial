using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Rocks :  MonoBehaviourPunCallbacks
{
    [SerializeField] int Lenth;
    [SerializeField] int Width;
    public float speed;
    public int Chance = 5;
    public GameObject[] decoPrefab;
    private List<GameObject> rocks =  new List<GameObject>();
    private List<GameObject> decorates = new List<GameObject>();
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            GenerateRocks();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (transform.position.x <= -26)
        {
            transform.position = new Vector3(26, 0f, 0);
            GenerateRocks();
        }
        else
        {
            transform.position += Vector3.left * speed / 100;
        }
    }

 
    private void GenerateRocks()
    {
        foreach(GameObject rock in rocks)
        {
          PhotonNetwork.Destroy(rock);
        }
        foreach(GameObject deco in decorates)
        {
            PhotonNetwork.Destroy(deco);
        }
        rocks = new List<GameObject>();
        decorates = new List<GameObject>();
        for (int j = -Lenth+3; j <= Lenth; j += 3)
        {
            int counter = 0;
            int interval = (int)Random.Range(1, 3);
            for (int i = -Width; i <= Width; i += interval)
            {
                if (counter < 3 && Random.Range(0, 10) <= Chance)
                {
                    counter++;
                    GameObject rock = PhotonNetwork.Instantiate("Rock", new Vector2(transform.position.x + j, i), Quaternion.identity);
                    rock.transform.SetParent(transform);
                    rocks.Add(rock);
                }
                else
                {
                    if(Random.Range(1, 10) <= 5)
                    {
                        int random = Random.Range(1, 3);
                        GameObject decorate = PhotonNetwork.Instantiate($"WaterDeco{random}", new Vector2(transform.position.x + j, i), Quaternion.identity);
                        decorate.transform.SetParent(transform);
                        decorates.Add(decorate);
                    }
                }

            }
        }
    }

}
