using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencePiece : PuzzlePiece
{
    private SequencePuzzle _reference;
    public SequencePiece SetPuzzleControllerReference(SequencePuzzle reference)
    {
        _reference = reference;
        
        return this;
    }

    public override void OnInteract()
    {
        _reference.AddElementToPlayerSequence(this);

        gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        OnInteract();
    }
}
