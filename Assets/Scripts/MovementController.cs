using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MovementController : MonoBehaviourPunCallbacks
{

    private Transform _transform;
    private Transform _cameraTransform;
    private CharacterController _character;
    private Vector3 _moveInput;
    public int velocity;

    // Start is called before the first frame update
    void Start()
    {
        if (!photonView.IsMine) return;
        this._transform = transform;
        this._cameraTransform = Camera.main.transform;
        this._character = this.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return;
        this._moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        transform.position = transform.position + new Vector3(_moveInput.x * Time.deltaTime * velocity, _moveInput.z * Time.deltaTime * velocity, 0);
        Camera.main.transform.position = Camera.main.transform.position + new Vector3(_moveInput.x * Time.deltaTime * velocity, _moveInput.z * Time.deltaTime * velocity, 0);
    }
}
