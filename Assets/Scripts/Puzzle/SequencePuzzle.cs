using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SequencePuzzle : Puzzle
{
    private List<PuzzlePiece> playerSequence = new List<PuzzlePiece>();

    private void Start()
    {
        foreach (var item in puzzlePieces)
        {
            item.GetComponent<SequencePiece>().SetPuzzleControllerReference(this);
        }
    }

    public void AddElementToPlayerSequence(PuzzlePiece piece)
    {
        playerSequence.Add(piece);

        if (playerSequence.Count == puzzlePieces.Count)
        {
            if (playerSequence.SequenceEqual(puzzlePieces))
            {
                OnPuzzleSolved();
            }
            else
            {
                StartCoroutine(OnPuzzleFailed());
            }
        }

    }
    protected override IEnumerator OnPuzzleFailed()
    {
        playerSequence.Clear();

        gameObject.GetComponent<Renderer>().material.color = Color.red;

        yield return new WaitForSeconds(2f);

        ResetPuzzleToDefault();
    }

    protected override void OnPuzzleSolved()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.green;
        //EventManager.TriggerEvent(EventTypes.PuzzleCompleted);
    }

    protected override void ResetPuzzleToDefault()
    {
        foreach (var item in puzzlePieces)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.white;
            item.gameObject.SetActive(true);
        }
    }
}
