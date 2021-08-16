using Assets.Scripts;

public class ExtraLife : Collectable
{
    protected override void ApplyEffect()
    {
        if (GameManager.Instance!=null)
        {
            GameManager.Instance.AddLife();
           
        }
    }
}
