public class Unit2 : Unit
{
    public Interactable Damagable;

    protected override void InitInteractables()
    {
        Damagable.InitInteractable(this);

        base.InitInteractables();
    }
}
