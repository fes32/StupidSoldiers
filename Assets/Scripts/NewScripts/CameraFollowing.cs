using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private float _speed;
    [SerializeField] private float _distanceXToTarget;
    
    private void Update()
    {

        float targetXPosition = Mathf.MoveTowards(transform.position.x, _target.transform.position.x- _distanceXToTarget, _speed) ;
        float targetYPosition = Mathf.MoveTowards(transform.position.y, _target.transform.position.y, _speed);

        Vector3 targetPosition = new Vector3(targetXPosition,targetYPosition ,transform.position.z);

        transform.position = targetPosition;
    }


}