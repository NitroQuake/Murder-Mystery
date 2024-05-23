using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public GameObject bullet;
    public GameObject shoot;
    public PlayerObject playerObject;
    public Sound sound;
    // Start is called before the first frame update
    void Start()
    {

    }

    // follows mouse
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -35f;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + 90));

        // shoots a bullet if one has enough bullets
        if (Input.GetMouseButtonDown(0) && playerObject.getBullets() > 0)
        {
            sound.playGunshot();
            playerObject.shootBullet();
            Instantiate(bullet, shoot.transform.position + (Vector3.forward * 0.1f), shoot.transform.rotation);
            playerObject.dequipGun();
        }
    }

}
