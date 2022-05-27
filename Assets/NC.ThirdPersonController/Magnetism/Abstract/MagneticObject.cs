using System.Collections;
using NC.ThirdPersonController.Interfaces;
using UnityEngine;

namespace NC.ThirdPersonController.Magnetism.Abstract
{
    public abstract class MagneticObject : NeutralEntity, IMagnetic<MagnetWeapon>
    {
        protected override void InitializeObject()
        {
            currentObjectHealthPoints = maxObjectHealthPoints;
        }
        
        public abstract void OnInteract();
        public abstract IEnumerator OnInteractFeedback();
        public abstract void MagneticBehaviour(MagnetWeapon reference);

    }
}
