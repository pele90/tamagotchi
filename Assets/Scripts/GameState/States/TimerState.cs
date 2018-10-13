public class TimerState : IGameState
{
    private const string TIMER_VIEW = "timer_view";

    public void Init()
    {
        ViewManager.Instance.ActivateView(TIMER_VIEW);
    }

    public void Update()
    {
        
    }

    public void AButton()
    {
        throw new System.NotImplementedException();
    }

    public void BButton()
    {
        ViewManager.Instance.DeactivateView(TIMER_VIEW);
    }

    public void CButton()
    {
        throw new System.NotImplementedException();
    }
}
