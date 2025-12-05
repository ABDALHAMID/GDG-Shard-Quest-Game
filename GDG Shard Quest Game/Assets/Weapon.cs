using StarterAssets;
using UnityEngine;
using UnityEngine.Animations;

public class Weapon : MonoBehaviour
{
    private RotationConstraint RotationConstraint;
    private AimConstraint AimConstraint;
    public StarterAssetsInputs starterAssetsInputs;


    private void Awake()
    {
        RotationConstraint = GetComponent<RotationConstraint>();
        AimConstraint = GetComponent<AimConstraint>();
    }

    private void Update()
    {
        if (starterAssetsInputs.aim)
        {
            RotationConstraint.constraintActive = false;
            AimConstraint.constraintActive = true;
        }
        else
        {
            RotationConstraint.constraintActive = true;
            AimConstraint.constraintActive = false;
        }
    }
}
