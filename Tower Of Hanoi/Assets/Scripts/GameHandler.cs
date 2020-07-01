using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameHandler : MonoBehaviour
{
    [SerializeField]
    GameObject discPrefab;

    public int numberOfDiscs = 7;
    [SerializeField]
    List<Color> discColors;
    public TowerRod[] towers;

    [System.Serializable]
    public struct Move
    {
        public TowerRod fromRod;
        public TowerRod toRod;
        public Move(TowerRod fromRod, TowerRod toRod)
        {
            this.fromRod = fromRod;
            this.toRod = toRod;
        }
    }
    public List<Move> moves = new List<Move>();

    [Range(0.1f, 10.0f), SerializeField]
    private float moveDuration = 1.0f;
    [SerializeField]
    Ease moveEase = Ease.Linear;

    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init(true, true);
        for (int i = 0; i < numberOfDiscs; i++)
        {
            Disc newDisk = Instantiate(discPrefab, FindObjectOfType<Canvas>().transform).GetComponent<Disc>();
            newDisk.transform.localPosition = towers[0].transform.localPosition + towers[0].GetTopPosition();
            newDisk.GetComponent<Image>().color = discColors[i];
            newDisk.GetComponent<RectTransform>().sizeDelta = new Vector2(50 + (numberOfDiscs - i) * 20f, 30f);
            newDisk.Size = numberOfDiscs - i;
            towers[0].AddDisc(newDisk);
        }
        Debug.Log(towers.Length);
        SolveTower(numberOfDiscs, towers[0], towers[2], towers[1]);
        StartCoroutine("MoveAllDiscs");
    }

    void SolveTower(int numOfDiscs, TowerRod fromRod, TowerRod toRod, TowerRod auxRod)
    {
        if (numOfDiscs == 1)
        {
            moves.Add(new Move(fromRod,toRod));
            return;
        }
        SolveTower(numOfDiscs - 1, fromRod, auxRod, toRod);        
        moves.Add(new Move(fromRod,toRod));
        SolveTower(numOfDiscs - 1, auxRod, toRod, fromRod);       
    }

    IEnumerator MoveDisc(TowerRod fromRod, TowerRod toRod)
    {
        Disc disc = fromRod.GetDisc();
        Vector3 targetPos = toRod.GetTopPosition();
        Debug.Log("Moving disc from " + fromRod.rodName + " to " + toRod.rodName);

        disc.transform.DOLocalMoveY(300f, moveDuration);
        yield return new WaitForSeconds(moveDuration);
        disc.transform.DOLocalMoveX(toRod.transform.localPosition.x + targetPos.x, moveDuration);
        yield return new WaitForSeconds(moveDuration);
        disc.transform.DOLocalMoveY(toRod.transform.localPosition.y + targetPos.y, moveDuration);
        toRod.AddDisc(disc);
        yield return new WaitForSeconds(moveDuration);
    }

    IEnumerator MoveAllDiscs()
    {
        foreach(Move move in moves)
        {
            yield return MoveDisc(move.fromRod, move.toRod);
        }
    }

}
