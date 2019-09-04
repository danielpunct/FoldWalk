﻿using DG.Tweening;
using Gamelogic.Extensions;
using UnityEngine;

public class Runner : Singleton<Runner>
{
    // speed as how much time runner needs to walk over the distance of 1
    public float passTileTime = 0.3f;
    Rigidbody _rb;
    Transform _tr;

    Vector2Int currentPosition2D = new Vector2Int(0, 0);
    Vector2Int destinationPosition2D = new Vector2Int(0, 0);
    Vector3 currentDirection = Vector3.zero;

    public float Speed
    {
        get { return (1 / passTileTime * Time.fixedDeltaTime); }
    }

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _tr = transform;
    }

    private void Start()
    {
        _tr.localPosition = currentPosition2D.To3D();
    }

    void FixedUpdate()
    {
        // update current position in grid
        currentPosition2D = _tr.localPosition.To2D();

        // take the actual position as floats to compare with the destination and get there as close as is acceptable
        var currentPosition2DFloats = _tr.localPosition.To2DXZ();

        // compare with destination
        if ((destinationPosition2D - currentPosition2DFloats).sqrMagnitude >0.02f)
        {
            _rb.MovePosition(_tr.position + currentDirection * Speed);
        }
    }

    public void SetDirection(Vector3 direction)
    {
        FaceDireciton(direction);
        StartMove(direction);

        currentDirection = direction;
    }

    void FaceDireciton(Vector3 direction)
    {
        var eulerRotation = Quaternion.LookRotation(direction, Vector3.up).eulerAngles;
        _rb.DORotate(eulerRotation, 0.4f).SetEase(Ease.OutBack);
    }

    void StartMove(Vector3 direction)
    {
        destinationPosition2D = Grid.Instance.GetAvailablePosition(currentPosition2D, direction.To2D());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere (transform.parent.position + destinationPosition2D.To3D(), 0.1f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.parent.position + currentPosition2D.To3D(), 0.3f);

    }
}