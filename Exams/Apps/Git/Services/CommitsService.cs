namespace Git.Services
{
    using Git.Data;
    using Git.ViewModels.Commits;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;


    public class CommitsService : ICommitsService
    {
        private readonly ApplicationDbContext db;

        public CommitsService(ApplicationDbContext db)
        {
            this.db = db;
        }


        public void CreateCommit(string description, string repoId, string userId)
        {
            var newCommit = new Commit
            {

                Description = description,
                CreatedOn = DateTime.UtcNow,
                CreatorId = userId,
                RepositoryId = repoId
            };

            this.db.Commits.Add(newCommit);
            this.db.SaveChanges();
        }



        public IEnumerable<CommitViewModel> GetAll(string userId)
        {
            var repoAll = this.db.Commits.Where(c => c.CreatorId == userId)
                                         .Select(c=> new CommitViewModel 
                                         { 
                                             Id = c.Id,
                                          Repository = c.Repository.Name,
                                          CreatedOn = c.CreatedOn,
                                          Description = c.Description,

                                         }).ToList();

            return repoAll;
        }

        public bool UserCanDeleteCommit(string userId, string id)
        => this.db.Commits.Any(c => c.Id == id && c.CreatorId == userId);

        public void DeleteCommit(string commitId)
        {

            var commit = this.db.Commits.Find(commitId);
            this.db.Commits.Remove(commit);
            this.db.SaveChanges();
        }
    }
}
