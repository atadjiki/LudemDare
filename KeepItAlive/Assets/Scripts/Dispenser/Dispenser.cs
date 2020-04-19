using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : MonoBehaviour
{
    public GameObject Beer_Prefab;
    public Transform Initial_Transform;
    private Vector3 Initial_Pos;
    private Quaternion Initial_Rot;
    private bool ContainsObject;
    private Collider DispenserCollider;
    private Door door;
    private bool previousOpen;

    private void Awake()
    {
        DispenserCollider = GetComponent<Collider>();
        door = GetComponentInParent<Door>();
        previousOpen = door.IsOpen();
        Initial_Rot = Initial_Transform.rotation;
        Initial_Pos = Initial_Transform.position;
    }

    private void Update()
    {
        if(previousOpen != door.IsOpen() && door.IsOpen() == false)
        {
            DispenseBeer();
        }

        previousOpen = door.IsOpen();
    }

    public void DispenseBeer()
    {
        if(DoesContainBeer() == false)
        {
            SpawnBeer();
        }
    }

    public void SpawnBeer()
    {
        GameObject Beer = Instantiate<GameObject>(Beer_Prefab, Initial_Pos, Initial_Rot);
        Beer.name = "Beer";
        Debug.Log("Spawned beer");
    }

    public bool DoesContainBeer()
    {
        Collider[] caught = Physics.OverlapBox(DispenserCollider.bounds.center, DispenserCollider.bounds.extents, DispenserCollider.transform.rotation);

        foreach(Collider col in caught)
        {
            if(col.gameObject.GetComponent<Beer>())
            {
                return true;
            }
        }

        return false;
    }
}
