using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : SingleTon<CustomerManager>
{
    public GameObject customerObject;
    public Transform customersTable;

    public Texture[] customerImages;  

    Customer[] customers;
    int maxNoofCustomers;
	// Use this for initialization
	void Start ()
    {
        maxNoofCustomers = 4;
        customers = new Customer[maxNoofCustomers];

        Vector3 startPos = customersTable.TransformPoint(-0.5f, 0, -1.05f);
        for (int i = 0;i < maxNoofCustomers; i++)
        {
            GameObject obj = Instantiate(customerObject, transform);
            obj.SetActive(false);
            float partitionWidth = customersTable.localScale.x / maxNoofCustomers;

            if(i == 0)
            startPos.x += obj.transform.localScale.x ;
            obj.transform.position = startPos;

            startPos.x += partitionWidth;

            customers[i] = obj.GetComponent<Customer>();
        }

        SpawnCustomersWithDelay(1);
    }
	
    public void SpawnCustomersWithDelay(float delay)
    {
        Invoke("SpawnCustomers", delay);
    }

    void SpawnCustomers()
    {
        int custId = GetAvailableCustomerIndex();
        if (custId != -1)
        {
            int randomSalad = Random.Range(0, GameManager.Instance.gameData.saladDetails.Length);
            customers[custId].gameObject.SetActive(true);
            customers[custId].SetSalad(randomSalad);

            SpawnCustomersWithDelay(1);
        }
        else
            CancelInvoke();
    }


    int GetAvailableCustomerIndex()
    {
        for(int i = 0; i < maxNoofCustomers; i++)
        {
            if (customers[i].isAvailable)
                return i;
        }

        return -1;
    }
}
