using UnityEngine;
using System.Collections;

public class Util
{
    public static GameObject AttachGameObject(GameObject prefab, GameObject target ,Vector3 pos)
    {
        if (prefab == null)
        {
            return null;
        }

        GameObject inst = GameObject.Instantiate(prefab) as GameObject;

        if (target != null)
            inst.transform.parent = target.transform;
        inst.transform.localScale = prefab.transform.localScale;
        inst.transform.localPosition = pos;
        inst.transform.localEulerAngles = prefab.transform.localEulerAngles;
        return inst;
    }

    public static GameObject AttachGameObject(GameObject prefab, GameObject target)
    {
        if (prefab == null)
        {
            return null;
        }

        GameObject inst = GameObject.Instantiate(prefab) as GameObject;

        if(target != null)
            inst.transform.parent = target.transform;
        inst.transform.localScale = prefab.transform.localScale;
        inst.transform.localPosition = prefab.transform.localPosition;
        inst.transform.localEulerAngles = prefab.transform.localEulerAngles;
        return inst;
    }

    public static GameObject AttachGameObject(GameObject prefab, Vector3 pos)
    {
        if (prefab == null)
        {
            return null;
        }

        GameObject inst = GameObject.Instantiate(prefab , pos , Quaternion.identity) as GameObject;

        return inst;
    }

    //public static T AttachGameObject<T>(GameObject prefab, GameObject target)
    //{
    //    if (prefab == null || target == null)
    //    {
    //        return default(T);
    //    }

    //    GameObject inst = GameObject.Instantiate(prefab) as GameObject;

    //    inst.transform.parent = target.transform;
    //    inst.transform.localScale = prefab.transform.localScale;
    //    inst.transform.localPosition = prefab.transform.localPosition;

    //    T component = (T)inst.GetComponent<T>();
    //}

}
