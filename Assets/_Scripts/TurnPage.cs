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
    public void ResetForMenu()
    {
        joint.useMotor = false;
        joint.GetComponent<Rigidbody>().DORotate(new Vector3(-157, 0, 0), 0.5f);
        //joint.transform.DOLocalRotate(new Vector3(-158, 0, 0), 0.5f);
    }

    public void StartLevel()
    {
        joint.useMotor = true;
    }
}
