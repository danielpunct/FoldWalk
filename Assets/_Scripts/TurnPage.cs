using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TurnPage : MonoBehaviour
{
    public float gameMotorForce = 20;
    public float resetMotorForce = -150;
    public float gameVelocity = 20;
    public float resetVelocity = -99999;

    public HingeJoint joint;

    Rigidbody _rb;
    Sequence seq;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void ResetForMenu()
    {
        joint.useMotor = false;
        joint.transform.DOLocalRotate(new Vector3(-157, 0, 0), 0.5f);
    }

    public void StartLevel(bool afterPass)
    {
        seq?.Kill();
        seq = DOTween.Sequence();
        if (afterPass)
        {
            transform.localEulerAngles = new Vector3(-157, 0, 0);
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            joint.useMotor = true;
        }
        else
        {
            seq.Insert(0, _rb.DORotate(new Vector3(-157, 0, 0), 0.5f))
               .InsertCallback(0.5f, () => { joint.useMotor = true; });
        }
    }
}
