using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Camera _camera;

    Ray _ray;
    RaycastHit _hit;

    Touch _touch;

    public bool is2D;
    public Image image2D;
    public Material mat3D;
    public Color currentColor;

    [SerializeField]
    GraphicRaycasterResults _raycaster;
    [SerializeField]
    GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        image2D = null;
        mat3D = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchSupported)
        {
            if (Input.touchCount > 0)
            {
                _touch = Input.GetTouch(0);

                if (_touch.phase == TouchPhase.Began)
                {
                    _ray = _camera.ScreenPointToRay(_touch.position);

                    GetChangableObject();
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                _ray = _camera.ScreenPointToRay(Input.mousePosition);

                GetChangableObject();
            }
        }
    }

    void GetChangableObject()
    {
        Image buf = image2D;

        image2D = _raycaster.GetImage();

        if (image2D != null)
        {
            is2D = true;
            currentColor = image2D.color;
            SetChangableObject();
        }     
        else if (Physics.Raycast(_ray, out _hit))
        {
            if(_hit.transform.gameObject.tag == "Changable3D")
            {
                is2D = false;
                mat3D = _hit.transform.GetComponent<MeshRenderer>().material;
                currentColor = mat3D.color;
                SetChangableObject();
            }
        }
        else if(is2D == true && image2D == null) image2D = buf;

        UnityEngine.Debug.Log("Current Color: " + currentColor);
    }

    void SetChangableObject()
    {
        if (image2D != null || mat3D != null) _gameManager.SetInitialColor(currentColor);
    }
}
