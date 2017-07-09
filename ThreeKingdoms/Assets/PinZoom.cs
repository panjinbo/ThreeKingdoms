using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System.Collections;
public class PinZoom : MonoBehaviour
{
	public float orthoZoomSpeed = 0.001f;        // aThe rate of change of the orthographic size in orthographic mode.
	private float panSpeed= 0.01f;
	private float MAXSCALE = 5.0f, MIN_SCALE = 2f; // zoom-in and zoom-out limits
	private float cameraWidth;
	private float cameraHeight;
	private float boarderX;
	private float boarderY;
	private bool isMousePressed;
	private Vector2 prevDist = new Vector2(0,0);
	private Vector2 curDist = new Vector2(0,0);
	private Vector2 midPoint = new Vector2(0,0);
	private Vector3 originalPos;
	private GameObject parentObject;
	void Start ()
	{
		// Game Object will be created and make current object as its child (only because we can set virtual anchor point of gameobject and can zoom in and zoom out from particular position)
		parentObject = new GameObject("ParentObject");
		parentObject.transform.parent = transform.parent;
		parentObject.transform.position = new Vector3(transform.position.x*-1, transform.position.y*-1, transform.position.z);
		transform.parent = parentObject.transform;
		originalPos = transform.position;
		isMousePressed = false;
		boarderY = 5f;
		boarderX = boarderY * Screen.width / Screen.height;
		cameraHeight = 5f;
		cameraWidth = cameraHeight * Screen.width / Screen.height;
	}

	void Update()
	{
		// If there are two touches on the device...

		if (Input.touchCount == 2)
		{
			// Store both touches.
			Touch touchZero = Input.GetTouch(0);
			Touch touchOne = Input.GetTouch(1);

			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			// Find the magnitude of the vector (the distance) between the touches in each frame.
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

			// Find the difference in the distances between each frame.
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

			Vector2 distance_between = touchZero.deltaPosition - touchOne.deltaPosition;

			// ... change the orthographic size based on the change in distance between the touches.
			Vector2 midPoint = (touchZero.position+touchOne.position)/2;
			if (distance_between.magnitude > 20f) {
				Camera.main.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;
				Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize,MIN_SCALE,MAXSCALE);
				transform.position = Camera.main.ScreenToWorldPoint(midPoint);
			} else {
				Vector2 move = (touchZero.deltaPosition+touchOne.deltaPosition) / 2;
				move = -move * panSpeed;
				transform.Translate (move, Space.World);
			}
			cameraHeight = Camera.main.orthographicSize;
			cameraWidth = cameraHeight * Screen.width / Screen.height;
			Vector3 tempPosition = transform.position;
			tempPosition.x = Mathf.Clamp (tempPosition.x, -boarderX + cameraWidth, boarderX - cameraWidth);
			tempPosition.y = Mathf.Clamp (tempPosition.y, -boarderY + cameraHeight, boarderY - cameraHeight);
			transform.position = tempPosition;
		}
	}
}