using System;
namespace COLSTRAT.Interfaces
{
    public interface IVersion
    {
        string GetName();
        string GetVersion();
        int GetBuild();
        DateTime GetLastCompileDate();
    }
}
