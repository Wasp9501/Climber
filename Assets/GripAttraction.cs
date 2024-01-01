using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GripAttraction : MonoBehaviour
{
    [SerializeField]
    private Transform attractableObject; // A vonzható objektum transzformjának beállítása Inspector-ban

    [SerializeField]
    private float attractionForce = 10f; // Az erõsség beállítása Inspector-ban

    [SerializeField]
    private float detectionRadius = 0.1f; // Az észlelési sugár beállítása Inspector-ban

    private bool isBeingHeld = false; // Az egérrel mozgatott objektumról jelzi, hogy éppen tartják-e

    private void FixedUpdate()
    {
        if (attractableObject != null)
        {
            // Ellenõrizze, hogy az objektum érintkezik-e valamelyik colliderrel a detectionRadius sugarú környezetében
            Collider[] colliders = Physics.OverlapSphere(attractableObject.position, detectionRadius);

            foreach (Collider collider in colliders)
            {
                // Ha talált collidert, alkalmazd a vonzó erõt
                Rigidbody otherRigidbody = collider.GetComponent<Rigidbody>();
                if (otherRigidbody != null)
                {
                    Vector3 directionToCenter = collider.bounds.center - attractableObject.position;
                    directionToCenter.Normalize();

                    if (!isBeingHeld)
                    {
                        // Ha az egérrel mozgatott objektumot nem tartják, akkor alkalmazzuk a vonzó erõt
                        otherRigidbody.AddForce(directionToCenter * attractionForce);
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Rajzold ki a detectionRadius-t, ha a FixedObject kijelölve van
        if (UnityEditor.Selection.activeGameObject == gameObject)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
    }

    public void SetBeingHeld(bool held)
    {
        isBeingHeld = held;
    }
}
