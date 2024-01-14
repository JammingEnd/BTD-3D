
using UnityEngine;

public class TripleDartScripts : MonoBehaviour, ISpecialTriggerAble
{
    public ISpecialTriggerAble Trigger(GameObject obj)
    {
       
        var OGRot = this.gameObject.transform.rotation.eulerAngles;
        var newRot = Quaternion.Euler(OGRot + new Vector3(0, 30, 0));
        var newProj = Instantiate(this.gameObject, this.transform.position, newRot);
        Debug.LogWarning(newProj.name);
        if (newProj.TryGetComponent(out TripleDartScripts scripts))
        {
            Destroy(scripts);
        }
        return null;
    }
}
