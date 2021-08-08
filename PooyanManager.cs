using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PooyanManager : MonoBehaviour
{
    [SerializeField] private Transform[] EnemyspawnPoints;
    [SerializeField] private Transform[] SpawnPoints;
    [SerializeField] private GameObject[] Frogs;
    
    
    private float timer = 0;
    private int health =6;


    public static PooyanManager i;

    private void Awake()
    {
        if (i == null)
        {
            i = this;
        }
        else
        {
            Destroy(this);
        }
    }


    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            SpawnPlayer();
        }
    }

    void Update()
    {
        if (!PhotonNetwork.IsMasterClient) return;
        if (timer <= 0) 
        {
            int random = (int)Random.Range(0, 6);
            GenerateBalloonWave(random);
            timer = 10;
        }
        else 
        {
            timer -= Time.deltaTime;
        }

    }

    void GenerateBalloonWave(int type)
    {
        switch (type)
        {
            case 0:
                StartCoroutine(Type0());
                break;
            case 1:
                Type1();
                break;
            case 2:
                StartCoroutine(Type2());
                break;
            case 3:
                StartCoroutine(Type3());
                break;
            case 4:
                StartCoroutine(Type4());
                break;
            case 5:
                StartCoroutine(Type5());
                break;
        }
    }


    IEnumerator Type0()
    {
        int type = Random.Range(0, 4);
        for (int i=0;i < EnemyspawnPoints.Length; i++)
        {

           GameObject balloon =  PhotonNetwork.Instantiate("Balloon", EnemyspawnPoints[i].position, Quaternion.identity);
            balloon.GetComponent<Ballon>().InitBalloon(type);
            yield return new WaitForSeconds(1);
        }
    }

    void Type1()
    {
        int type = Random.Range(0, 4);
        for (int i = 0; i < EnemyspawnPoints.Length; i++)
        {

            GameObject balloon = PhotonNetwork.Instantiate("Balloon", EnemyspawnPoints[i].position, Quaternion.identity);
            balloon.GetComponent<Ballon>().InitBalloon(type);

        }
    }

    IEnumerator Type2()
    {
        for (int i = 0; i < EnemyspawnPoints.Length; i++)
        {
            int type =(int) Random.Range(0, 3);
            GameObject balloon = PhotonNetwork.Instantiate("Balloon", EnemyspawnPoints[i].position, Quaternion.identity);
            balloon.GetComponent<Ballon>().InitBalloon(type);
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator Type3()
    {
        for (int i = 0; i < EnemyspawnPoints.Length; i++)
        {
            int type = (int)Random.Range(0, 4);
            float waitTime = Random.Range(0, 3);
            GameObject balloon = PhotonNetwork.Instantiate("Balloon", EnemyspawnPoints[i].position, Quaternion.identity);
            balloon.GetComponent<Ballon>().InitBalloon(type);
            yield return new WaitForSeconds(waitTime);
        }
    }
    IEnumerator Type4()
    {
        int type = Random.Range(0, 4);
        for (int i = EnemyspawnPoints.Length - 1; i >=0; i--)
        {

            GameObject balloon = PhotonNetwork.Instantiate("Balloon", EnemyspawnPoints[i].position, Quaternion.identity);
            balloon.GetComponent<Ballon>().InitBalloon(type);
            yield return new WaitForSeconds(1);
        }
    }
    IEnumerator Type5()
    {
        for (int i = EnemyspawnPoints.Length - 1; i >= 0; i--)
        {
            int type = (int)Random.Range(0, 3);
            GameObject balloon = PhotonNetwork.Instantiate("Balloon", EnemyspawnPoints[i].position, Quaternion.identity);
            balloon.GetComponent<Ballon>().InitBalloon(type);
            yield return new WaitForSeconds(1);
        }
    }

    void SpawnPlayer()
    {
        int player = 0;
        if (!PhotonNetwork.IsMasterClient)
        {
            player = 1;
        }

        GameObject Player = PhotonNetwork.Instantiate("PooyanPlayer", SpawnPoints[player].transform.position,Quaternion.identity);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(collision.gameObject);
            Frogs[health - 1].GetComponent<Rigidbody2D>().gravityScale = 1;
            Frogs[health - 1].GetComponent<Animator>().SetBool("Fall", true);
            health--;

            if (health == 0)
            {
                string winnerName = ScoreManager.i.Winnername;
                DataHolder.i.setWinnerName(winnerName);
                ScoreManager.i.ResetScoreManager();
                PhotonNetwork.LoadLevel(1);
            }
        }


    }


}
