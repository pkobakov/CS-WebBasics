namespace Git.Services
{

    using Git.ViewModels.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Text;


    public interface IRepositoriesService
    {
        void CreateRepository(string name,string selectType, string userId);
        IEnumerable<RepositoryViewModel> GetAll();
        string GetRepositoryName(string repoId);

    }
}
