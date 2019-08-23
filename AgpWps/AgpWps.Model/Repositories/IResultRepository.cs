using AgpWps.Model.ViewModels;

namespace AgpWps.Model.Repositories
{
    public interface IResultRepository
    {

        void AddResult(ResultViewModel result);
        void RemoveResult(ResultViewModel result);
        ResultViewModel[] GetResults();

    }
}
