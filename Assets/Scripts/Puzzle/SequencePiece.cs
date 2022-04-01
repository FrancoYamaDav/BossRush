using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequencePiece : PuzzlePiece
{
    private SequencePuzzle _reference;

    [SerializeField] Transform activatedPos;
    [SerializeField] Transform deactivatedPos;
    public SequencePiece SetPuzzleControllerReference(SequencePuzzle reference)
    {
        _reference = reference;
        
        return this;
    }

    public override void OnActivated()
    {
        _reference.AddElementToPlayerSequence(this);

        gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        OnActivated();
    }

    public override void OnDeactivated()
    {
        throw new System.NotImplementedException();
    }
}
