using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraGrow : MonoBehaviour
{
    private GameObject player;
    private Transform playerTransform;

    private CinemachineVirtualCamera cam;
    private float desiredScale;

    [SerializeField] private float orthoBaseSize = 3.0f;
    [SerializeField] private float orthoIncrease = 0.1f; 

    private void Start()
    {
        player = FindObjectOfType<PlayerInput>().gameObject;
        playerTransform = player.GetComponent<Transform>();
        cam = gameObject.GetComponent<CinemachineVirtualCamera>();
        cam.m_Lens.OrthographicSize = orthoBaseSize;
        desiredScale = cam.m_Lens.OrthographicSize;
    }

    private void Update()
    {
        //cam.m_Lens.OrthographicSize = playerTransform.localScale.x;
        //Debug.Log(playerTransform.localScale);
        var currentScale = new Vector3(cam.m_Lens.OrthographicSize, cam.m_Lens.OrthographicSize, cam.m_Lens.OrthographicSize);
        cam.m_Lens.OrthographicSize = Mathf.MoveTowards(cam.m_Lens.OrthographicSize,desiredScale, 1.0f * Time.deltaTime);
    }

    public void ChangeSize(float orthoChange)
    {
        //orthoChange;
        //cam.m_Lens.OrthographicSize = 3 * orthoChange - (2.0f*(orthoChange - 1.0f));
        desiredScale = 3 * orthoChange - (2.0f*(orthoChange - 1.0f));
    }
}
