namespace Infrastructure.Bootstrapper
{
    public interface ITicker
    {
        public void AddTickable(ITickable tickable);
    }
}