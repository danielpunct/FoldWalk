using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Obstacle : MonoBehaviour
{
    public GameObject mesh;

    public void Reset()
    {
        mesh.SetActive(true);
        mesh.transform.localPosition = new Vector3(0, -0.5f, 0);
        mesh.transform.DOLocalMoveY(0, Random.Range(0.8f, 1.5f)).SetEase(Ease.InOutBack);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Turning Page")
        {
            mesh.transform.DOLocalMoveY(-1, Random.Range(0.8f, 1f));
        }
    }
}
