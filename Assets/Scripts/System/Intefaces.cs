using System.Collections;
using System;
using UnityEngine;

public interface IDamageable<T> { void TakeDamage(T amount); IEnumerator DamageFeedback(); }

public interface IPuzzle<T> { T GetPuzzleType(); }
public interface IInteractable { void OnActivated(); void OnDeactivated(); }
public interface IPiece<T> { T GetPieceType(); }