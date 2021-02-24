using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shane {
  public class PlayerController2 : MonoBehaviour
  {

    // Outlet
    Rigidbody2D _rigidbody2D;
    public Transform aimPivot;
    public GameObject projectilePrefab;

    void Start(){
      _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update(){
      //Move up
      if(Input.GetKey(KeyCode.UpArrow)){
        transform.position += new Vector3(0, 5f,0) * Time.deltaTime;
      }
      //Move down
      if(Input.GetKey(KeyCode.DownArrow)){
        transform.position += new Vector3(0, -5f,0) * Time.deltaTime;
      }
      //Move left
      if(Input.GetKey(KeyCode.LeftArrow)){
        transform.position += new Vector3(-5f, 0 ,0) * Time.deltaTime;
      }
      //Move right
      if(Input.GetKey(KeyCode.RightArrow)){
        transform.position += new Vector3(5f, 0, 0) * Time.deltaTime;
      }

      //Aim Toward Mouse
      Vector3 mousePosition = Input.mousePosition;
      Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
      Vector3 directionFromPlayerToMouse = mousePositionInWorld - transform.position;

      float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x);
      float angleToMouse = radiansToMouse * Mathf.Rad2Deg;

      aimPivot.rotation = Quaternion.Euler(0,0, angleToMouse);

      //Shoot
      if(Input.GetMouseButtonDown(0)){
          GameObject newProjectile = Instantiate(projectilePrefab);
          newProjectile.transform.position = transform.position;
          newProjectile.transform.rotation = aimPivot.rotation;
          }
        }

  }
}
