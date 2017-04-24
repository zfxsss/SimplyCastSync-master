
namespace SimplyCastSync.Config
{
    public interface IConfigReader<T>
    {
        T GetConfiguration(string configname);
    }
}
