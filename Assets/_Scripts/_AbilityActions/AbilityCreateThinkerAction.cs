public class AbilityCreateThinkerAction : AbilityActionBase
{
    public AbilityThinkerModifier Thinker { get; private set; }

    public AbilityCreateThinkerAction(string name, ICombatUnit caster, AbilityThinkerModifier thinker) 
        : base(name, caster)
    {
        Thinker = thinker;
    }

    protected override void Invoke()
    {
        Thinker.CreateModifier();

        Thinker.CheckAndInvoke();

        FireOnActionEnded();
    }
}
