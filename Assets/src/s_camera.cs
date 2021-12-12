using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_camera : s_singleton<s_camera>
{
    s_globals gl;
    o_player pl;
    public float angle;
    public float yPos;
    public Transform target;
    public float dstFromTarget = 2;
    public float distance;
    public Vector2 pitchMinMax = new Vector2(-40, 85);

    public float rotationSmoothTime = .12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;
    float yaw;
    public float pitch = 1;
    RaycastHit hit;

    float defaultPos = 0.2f;
    float targPos = 0.2f;

    float camRadius = 0.2f;
    float camColOffset = 0.2f;
    float minimumColOffset = 0.2f;
    Vector3 camTransformPos;
    public Cinemachine.CinemachineFreeLook camBr;

    float lockOnTimer = 0;
    const float LKTMR = 0.2f;

    private void Awake()
    {
           gl = s_globals.GetInstance();
        DontDestroyOnLoad(this);
    }

    public void CameraCollision() {

        Vector3 dir = transform.position - target.transform.position;
        if (Physics.SphereCast(transform.position, camRadius,dir, out hit, Mathf.Abs(defaultPos)))
        {

            Vector3 vc = target.position - transform.forward * dstFromTarget;
            float dist = Vector3.Distance(target.position, hit.point);
            targPos = -(dist - camColOffset);
        }

        if (Mathf.Abs(targPos) < minimumColOffset) {
            targPos = -minimumColOffset;
        }
        camTransformPos.z = targPos;
        transform.localPosition = camTransformPos;
    }

    public void GetCamToPlayer() {
        transform.position = target.transform.position;
    }

    public void LockOnPlayer()
    {
        camBr.m_BindingMode = Cinemachine.CinemachineTransposer.BindingMode.LockToTarget;
        lockOnTimer = Time.deltaTime * 3;
    }

    private void LateUpdate()
    {
        if (gl.currentPlayer != null)
        {
            if (pl == null)
            {
                pl = gl.currentPlayer;
                target = pl.transform;
                camBr.Follow = target;
                camBr.LookAt = target;
            }
            else
            {
                if (Input.GetKeyDown(s_globals.GetKeyPref("camera")))
                {
                    LockOnPlayer();
                }
                if (lockOnTimer > 0)
                {
                    lockOnTimer -= Time.deltaTime;
                    if (lockOnTimer <= 0)
                    {
                        camBr.m_BindingMode = Cinemachine.CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp;
                    }
                }
                currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw), ref rotationSmoothVelocity, 0.05f);
                transform.eulerAngles = currentRotation;
                transform.position = target.position - transform.forward * dstFromTarget;
                camTransformPos = transform.position;
            }
        }
    }
}
