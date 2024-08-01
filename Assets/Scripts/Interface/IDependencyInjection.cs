namespace Interface
{
    public interface IDependencyInjection
    {
        public void RegisterServices();
        
        public void InjectDependenciesInAllClasses();

        public void RegisterInstance(object instance);
        
        
    }
}