using System.Collections;
using System;
using UnityEngine;

public interface IDamageable<T> { void TakeDamage(T amount); IEnumerator DamageFeedback(); }

public interface IPuzzle<T> { T GetPuzzleType(); }
public interface IInteractable { void OnInteract(); }
public interface IPiece<T> { T GetPieceType(); }