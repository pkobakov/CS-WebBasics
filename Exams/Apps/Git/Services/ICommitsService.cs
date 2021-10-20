using Git.ViewModels.Commits;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Services
{
    public interface ICommitsService
    {
        void CreateCommit(string description, string repoId, string userId);
        IEnumerable<CommitViewModel> GetAll(string userId);
        bool UserCanDeleteCommit(string userId, string repoId);
        void DeleteCommit(string id);

    }
}
