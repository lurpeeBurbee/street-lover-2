using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataFetcher : MonoBehaviour
{
    [SerializeField] GameObject dataObject;
    string fetchedData;
    bool fetchedBoolean;
    void Start()
    {
        fetchedData = dataObject.GetComponent<Data>().DataName;
        fetchedBoolean = dataObject.GetComponent<Data>().DataBoolean;
        Debug.Log(fetchedData + " " + fetchedBoolean);

    }

}
