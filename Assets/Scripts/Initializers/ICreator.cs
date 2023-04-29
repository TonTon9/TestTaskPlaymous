namespace Game
{
    public interface ICreator
    {
        public void Create();

        public bool IsInitialize { get; }
    }
}
