using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler
{
    public class Pool
    {
        public GameObject prefab;
        public List<GameObject> lst;
        public int size;

        private int curIndex;

        public Pool(GameObject prefab, int size)
        {
            lst = new List<GameObject>();
            this.prefab = prefab;
            addGameObject(prefab, size);
            this.size = size;
                
        }

        public void addGameObject (GameObject prefab, int size ){
            for (int i = 0 ; i < size ; i ++){
                GameObject go = GameObject.Instantiate<GameObject>(prefab);
                go.SetActive(false);
                lst.Add(go);      
            }
        }

        public GameObject Instantiate(){
            GameObject res = null;
            for (int i = curIndex ; i < size + curIndex; i++){
                res = lst[i%size];
                if (res.activeSelf == false){
                    break;
                }
            }
            if (res.activeSelf == false){
                res.SetActive(true);
                return res;
            }
            else{
                this.addGameObject(this.prefab, size);
                this.size *= 2;
                return this.Instantiate();
            }
        }
    }
    const int DEFAULT_SIZE = 10;
    public static ObjectPooler SharedInstance;
    public Dictionary<GameObject,Pool> pools;

    public ObjectPooler(){
        pools = new Dictionary<GameObject, Pool>();
        SharedInstance = this;
    }

    public void AddPool(GameObject prefabs, int size = DEFAULT_SIZE){
        pools.Add(prefabs, new Pool(prefabs,size));
    }

    public GameObject Instantiate (GameObject prefabs)
    {
        if (pools.ContainsKey(prefabs))
            return pools[prefabs].Instantiate();
        else {
            this.AddPool(prefabs);
            return this.Instantiate(prefabs);
        }
    }
}