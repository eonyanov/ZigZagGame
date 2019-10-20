public abstract class AbstractInput
{
    public virtual bool Tap => CheckTap();
    protected abstract bool CheckTap();
}