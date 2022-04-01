using UnityEngine;
using Enums;


public abstract class PuzzlePiece : MonoBehaviour, IPiece<PuzzleType>, IInteractable
{
    public PuzzleType GetPieceType() => puzzleType;

    [SerializeField] private PuzzleType puzzleType;
    public int ID { get { return _id; } }
    private int _id;
    public abstract void OnActivated();
    public abstract void OnDeactivated();
}