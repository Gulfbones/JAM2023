using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraGrow : MonoBehaviour
{
    private GameObject player;
    private Transform playerTransform;

    private CinemachineVirtualCamera cam;

    [SerializeField] private float orthoBaseSize = 5.0f;
    [SerializeField] private float orthoIncrease = 0.1f; 

    private void Start()
    {
        player = FindObjectOfType<PlayerInput>().gameObject;
        playerTransform = player.GetComponent<Transform>();
        cam = gameObject.GetComponent<CinemachineVirtualCamera>();
        cam.m_Lens.OrthographicSize = orthoBaseSize;
    }

    private void Update()
    {
        //cam.m_Lens.OrthographicSize = playerTransform.localScale.x;
        //Debug.Log(playerTransform.localScale);
    }

    public void ChangeSize()
    {
        cam.m_Lens.OrthographicSize += orthoIncrease;
    }
}
