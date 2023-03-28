using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        TurnToFaceMouse();
    }


    //Function used to rotate tower to where mouse is pointing
    //Since this is a 3D game using a top down perspective, this is required to properly orient the Player Tower to the intended height/position
    //All it does is create a plane at the height of the tower cannon, and uses the point where the raycast from the camera to the floor (created when clicking a position on the floor with the mouse)
    //as the interpreted point for the click.
    //Basically - it calculates where a user "meant" to click instead of the ground level.
    void TurnToFaceMouse()
    {
        Vector3 transformHigh = transform.position;
        transformHigh.y = 6f;
        Plane playerPlane = new Plane(Vector3.up * transformHigh.y , transformHigh);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdist;
        if (playerPlane.Raycast(ray, out hitdist))
        {
            Vector3 targetPoint = ray.GetPoint(hitdist);
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transformHigh);
            transform.rotation = targetRotation;


        }
    }

}
