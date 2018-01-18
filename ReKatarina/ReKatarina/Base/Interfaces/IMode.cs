namespace ReKatarina.Base.Interfaces
{
    internal interface IMode
    {
        void Initialize();
        void Execute();
        bool ShouldGetExecuted();
        void Draw();
    }
}
