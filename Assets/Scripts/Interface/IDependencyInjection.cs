namespace Interface
{
    public interface IDependencyInjection
    {
        public void RegisterServices();

        public void RegisterInstance(object instance);
        
        
    }
}