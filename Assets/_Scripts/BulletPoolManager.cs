using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

// TODO: Bonus - make this class a Singleton!

[System.Serializable]
public class BulletPoolManager : MonoBehaviour
{
    public GameObject bullet;
    public BulletController bulletController;

    //TODO: create a structure to contain a collection of bullets
    private Queue<GameObject> BulletPool = new Queue<GameObject>();

    public int MaxBullets = 10; //Defaults to 10 since that's a good number

    // Start is called before the first frame update
    void Start()
    {
        bulletController.bulletPoolManager = this;

        // TODO: add a series of bullets to the Bullet Pool
        _BuildBulletPool();
    }

    void _BuildBulletPool()
    {
        for (int i = 0; i < MaxBullets; i++)
        {
            //Instantiates all bullets, sets them inactive, then queues them for use
            GameObject temp = Instantiate(bullet);
            temp.SetActive(false);

            BulletPool.Enqueue(temp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //TODO: modify this function to return a bullet from the Pool
    public GameObject GetBullet()
    {
        //If there's no bullets then it adds the maximum amount
        if (isEmpty())
            _BuildBulletPool();

        //Sets it active before dequeuing it
        BulletPool.Peek().SetActive(true);
        return BulletPool.Dequeue();
    }

    //TODO: modify this function to reset/return a bullet back to the Pool 
    public void ResetBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        BulletPool.Enqueue(bullet);

        //Checks if it's greater than the max, and if it is then it wipes it. Mainly used for debugging and testing
        isTooLarge();
    }

    int CheckSize()
    {
        return BulletPool.Count;
    }

    bool isEmpty()
    {
        if (CheckSize() == 0)
            return true;
        return false;
    }

    void isTooLarge()
    {
        if (CheckSize() > MaxBullets)
        {
            BulletPool.Clear();
            _BuildBulletPool();
        }
    }

}
