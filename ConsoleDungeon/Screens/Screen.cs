namespace ConsoleDungeon.Screens
{
    public abstract class Screen
    {
        public bool IsDisposed { get; protected set; }

        public abstract Screen Open();
    }
}
