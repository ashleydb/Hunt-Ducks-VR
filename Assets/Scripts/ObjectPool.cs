// Copyright 2016 Ashley Bennett. All rights reserved.

using UnityEngine;
using System.Collections.Generic;

// An object pooling class to save time and memory churning when creating and destroying reusable instances of objects.
// This is a base class that you would derive from for a pool of some given object, like enemies, particle effects, etc.
// Add the derived class's script as a component to a game object in the scene, then set the prefab and poolSize in the editor.
public class ObjectPool : MonoBehaviour
{
    // Which object this pool will store instances of
    public GameObject prefab_;
    // How many instances this pool will create
    public int poolSize_ = 5;
    // The inactive and active list of objects in the pool
    protected List<GameObject> pool_ = new List<GameObject>();
    protected List<GameObject> active_pool_ = new List<GameObject>();

    // Upon creation, fill our pool with inactive objects
    public void Awake()
    {
        if (pool_.Count == 0 && active_pool_.Count == 0)
        {
            for (int i = 0; i < poolSize_; ++i)
            {
                GameObject obj = GameObject.Instantiate(prefab_);
                obj.SetActive(false);
                pool_.Add(obj);
            }
        }
    }

    // Return an object from our inactive list, if there is one available. Leaves it to the caller to activate the object.
    public GameObject CreateInactive()
    {
        if (pool_.Count > 0)
        {
            GameObject obj = pool_[0];
            active_pool_.Add(obj);
            pool_.RemoveAt(0);

            return obj;
        }
        Debug.Log("No available objects in pool.");
        return null;
    }

    // Return an activated object from our inactive list, if there is one available
    public GameObject Create()
    {
        if (pool_.Count > 0)
        {
            GameObject obj = pool_[0];
            active_pool_.Add(obj);
            pool_.RemoveAt(0);

            // Unique to this Create() method
            obj.SetActive(true);

            return obj;
        }
        Debug.Log("No available objects in pool.");
        return null;
    }

    // Get an object from the pool, then rotate & position it in the world and activate
    public GameObject Create(Transform at)
    {
        if (pool_.Count > 0)
        {
            GameObject obj = pool_[0];
            active_pool_.Add(obj);
            pool_.RemoveAt(0);

            // Unique to this Create() method
            obj.transform.position = at.position;
            obj.transform.rotation = at.rotation;
            obj.SetActive(true);

            return obj;
        }
        Debug.Log("No available objects in pool.");
        return null;
    }

    // Get an object from the pool, then rotate & position it in the world and activate
    public GameObject Create(Vector3 position)
    {
        if (pool_.Count > 0)
        {
            GameObject obj = pool_[0];
            active_pool_.Add(obj);
            pool_.RemoveAt(0);

            // Unique to this Create() method
            obj.transform.position = position;
            obj.SetActive(true);

            return obj;
        }
        Debug.Log("No available objects in pool.");
        return null;
    }

    // Get an object from the pool, then rotate & position it in the world and activate
    public GameObject Create(Quaternion rotation)
    {
        if (pool_.Count > 0)
        {
            GameObject obj = pool_[0];
            active_pool_.Add(obj);
            pool_.RemoveAt(0);

            // Unique to this Create() method
            obj.transform.rotation = rotation;
            obj.SetActive(true);

            return obj;
        }
        Debug.Log("No available objects in pool.");
        return null;
    }

    // Get an object from the pool, then rotate & position it in the world and activate
    public GameObject Create(Vector3 position, Quaternion rotation)
    {
        if (pool_.Count > 0)
        {
            GameObject obj = pool_[0];
            active_pool_.Add(obj);
            pool_.RemoveAt(0);

            // Unique to this Create() method
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.SetActive(true);

            return obj;
        }
        Debug.Log("No available objects in pool.");
        return null;
    }

    // Deactivate an object in the active list and put it back in our inactive list for reuse
    public void Destroy(GameObject obj)
    {
        if (active_pool_.Contains(obj))
        {
            obj.SetActive(false);
            pool_.Add(obj);
            active_pool_.Remove(obj);
        }
        else
            Debug.Log("Attempting to destroy object not in pool.");
	}

	// Deactivate all objects in the active list and put them back in our inactive list for reuse
	public void DestroyAllActive()
	{
		foreach (GameObject obj in active_pool_)
		{
			obj.SetActive(false);
			pool_.Add(obj);
			active_pool_.Remove(obj);
		}
	}
}
