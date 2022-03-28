using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public abstract class Puzzle : MonoBehaviour, IPuzzle<PuzzleType>
{
    [SerializeField] protected List<PuzzlePiece> puzzlePieces;
    public PuzzleType GetPuzzleType() => _puzzleType;
    public int sceneID { get { return GetInstanceID(); } }
    protected int _piecesToBeSolved => puzzlePieces.Count;

    protected PuzzleType _puzzleType;
    protected int _piecesPlaced = 0;
    protected abstract IEnumerator OnPuzzleFailed();
    protected abstract void ResetPuzzleToDefault();
    protected abstract void OnPuzzleSolved();
}
