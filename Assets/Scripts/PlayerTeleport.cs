using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    private GameObject currentTeleporter;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentTeleporter != null) {
            transform.position = currentTeleporter.GetComponent<Teleporter>().GetDestination().position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Teleporter")) {
            currentTeleporter = collider.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.CompareTag("Teleporter")) {
            if (collider.gameObject == currentTeleporter) {
                currentTeleporter = null;
            }
        }
    }
}
