using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InstantiateOnOppositePlane : MonoBehaviour
{
    public List<GameObject> InstantiateObs;
    public GameObject objectToInstantiate; // The object to be instantiated
    public Collider planeCollider; // The collider of the plane
    public float instantiateInterval = 3.0f; // Time interval for instantiation


    private float timer = 0.0f;
    private int randomObs;

    public List<Transform> obsPos;
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= instantiateInterval)
        {

            InstantiateObjectOnOppositePlane();
            timer = 0.0f;
        }
    }

    public void InstantiateObjectOnOppositePlane()
    {
        //  InstantiateObject.Add(objectToInstantiate);

        if (objectToInstantiate == null || planeCollider == null)
        {
            Debug.LogError("Object to instantiate or plane collider not assigned.");
            return;
        }

        // Generate random coordinates within the specified bounds



        // Calculate the opposite side position
        Vector3 planeCenter = planeCollider.bounds.center;


        // Instantiate the object at the calculated  position
        randomObs = Random.Range(0, InstantiateObs.Count);
        GameObject NewInstance = Instantiate(InstantiateObs[randomObs], obsPos[randomObs].position, objectToInstantiate.transform.localRotation);
    }


}

