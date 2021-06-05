using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAim : MonoBehaviour
{
	[SerializeField] private Transform _originPoint;
    [SerializeField] private LineRenderer _lineRenderer;

    private void Update()
    {
        _lineRenderer.SetPosition(0, _originPoint.position);
        if (Physics.Raycast(new Ray(_originPoint.position, transform.up), out RaycastHit hit))
        {
            _lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            _lineRenderer.SetPosition(1, _originPoint.position + transform.up * 10);
        }
    }
}

