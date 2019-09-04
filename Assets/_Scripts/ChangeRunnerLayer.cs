using UnityEngine;

public class ChangeRunnerLayer : MonoBehaviour {

    public int LayerOnEnter; // InsideHole
    public int LayerOnExit;  // OutsideTable

    bool skipExit = false;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.layer = LayerOnEnter;
            skipExit = true;
            Debug.Log("enter");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(skipExit)
            {
                skipExit = false;
                return;
            }
            other.gameObject.layer = LayerOnExit;
            Debug.Log("exit");
        }
    }
}