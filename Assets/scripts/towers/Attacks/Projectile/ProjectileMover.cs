using UnityEngine;

public static class ProjectileMover
{
   
    public static void Move(Transform projectileTransform, Rigidbody rb, float speed)
    {
        //If IsSeeking is true make it so that the projectile locks onto a target using the SeekingArc variable


        Vector3 newPos = projectileTransform.position + projectileTransform.forward * speed * Time.deltaTime;
        rb.MovePosition(newPos);
    }
}