using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerRod : MonoBehaviour
{
    public string rodName;
    [SerializeField]
    List<Disc> discs = new List<Disc>();
    [SerializeField]
    List<Vector3> discPositions = new List<Vector3>();

    public Disc GetDisc()
    {
        Disc disc = discs[discs.Count - 1];
        discs.Remove(disc);
        return disc;
    }

    public Vector3 GetTopPosition()
    {
        Debug.Log("Got position: " + discs.Count);
        return discPositions[discs.Count];
    }

    public void AddDisc(Disc disc)
    {
        discs.Add(disc);
    }

}
