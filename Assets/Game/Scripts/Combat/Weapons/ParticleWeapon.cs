using UnityEngine;
public class ParticleWeapon : AbstractWeapon
{
    [SerializeField]
    private ParticleSystem _particles;
    public override void BeginAttack()
    {
        base.BeginAttack();
        _particles.Play(true);
    }
    public override void EndAttack()
    {
        base.EndAttack();
        _particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }
}