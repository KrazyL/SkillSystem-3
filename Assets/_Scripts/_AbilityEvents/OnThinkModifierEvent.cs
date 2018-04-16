public class OnThinkModifierEvent : AbilityEventBase
{
    AbilityThinkerModifier _parentModifier;

    public OnThinkModifierEvent(AbilityThinkerModifier parentModifier)
    {
        _parentModifier = parentModifier;

        Bind();
    }

    protected override void Bind()
    {
        _parentModifier.OnThink += OnThink;

        _parentModifier.OnActionEnded += OnActionEnded;
    }

    protected override void Unbind()
    {
        _parentModifier.OnThink -= OnThink;
        _parentModifier.OnActionEnded -= OnActionEnded;

        TriggerActionList.Clear();
    }

    private void OnThink()
    {
        Trigger();
    }

    private void OnActionEnded(AbilityActionBase action)
    {
        Unbind();
    }
}
