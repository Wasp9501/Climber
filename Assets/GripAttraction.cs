using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GripAttraction : MonoBehaviour
{
    [SerializeField]
    private Transform attractableObject; // A vonzhat� objektum transzformj�nak be�ll�t�sa Inspector-ban

    [SerializeField]
    private float attractionForce = 10f; // Az er�ss�g be�ll�t�sa Inspector-ban

    [SerializeField]
    private float detectionRadius = 0.1f; // Az �szlel�si sug�r be�ll�t�sa Inspector-ban

    private bool isBeingHeld = false; // Az eg�rrel mozgatott objektumr�l jelzi, hogy �ppen tartj�k-e

    private void FixedUpdate()
    {
        if (attractableObject != null)
        {
            // Ellen�rizze, hogy az objektum �rintkezik-e valamelyik colliderrel a detectionRadius sugar� k�rnyezet�ben
            Collider[] colliders = Physics.OverlapSphere(attractableObject.position, detectionRadius);

            foreach (Collider collider in colliders)
            {
                // Ha tal�lt collidert, alkalmazd a vonz� er�t
                Rigidbody otherRigidbody = collider.GetComponent<Rigidbody>();
                if (otherRigidbody != null)
                {
                    Vector3 directionToCenter = collider.bounds.center - attractableObject.position;
                    directionToCenter.Normalize();

                    if (!isBeingHeld)
                    {
                        // Ha az eg�rrel mozgatott objektumot nem tartj�k, akkor alkalmazzuk a vonz� er�t
                        otherRigidbody.AddForce(directionToCenter * attractionForce);
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Rajzold ki a detectionRadius-t, ha a FixedObject kijel�lve van
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
