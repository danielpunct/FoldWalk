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

    public HingeJoint _joint;
    public Collider _collider;

    Rigidbody _rb;
    Sequence seq;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void ResetForMenu()
    {
        _rb.velocity = Vector3.zero;
        _joint.useMotor = false;
        _collider.enabled = false;

        seq?.Kill();
        seq = DOTween.Sequence()
            .Insert(0.3f, _joint.transform.DOLocalRotate(new Vector3(-157, 0, 0), 0.5f))
            .InsertCallback(0.7f, () => { _collider.enabled = true; });
    }

    public void StartLevel(bool afterPass)
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        _collider.enabled = false;

        seq?.Kill();
        seq = DOTween.Sequence();
        if (afterPass)
        {
            _collider.enabled = true;
            transform.localEulerAngles = new Vector3(-157, 0, 0);
            _joint.useMotor = true;
        }
        else
        {
            seq.Insert(0, _rb.DORotate(new Vector3(-157, 0, 0), 0.5f))
               .InsertCallback(0.5f, () =>
               {
                   _joint.useMotor = true;
                   _collider.enabled = true;
               });

        }
    }
}
