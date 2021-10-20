namespace Git.Services
{
    using Git.Data;
    using Git.ViewModels.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    public class RepositoriesService : IRepositoriesService
    {
        private readonly ApplicationDbContext db;

        public RepositoriesService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void CreateRepository(string name, string selectType, string userId)
        {
            var newRepository = new Repository 
            { 

             Name = name,
             CreatedOn = DateTime.UtcNow,
             IsPublic = selectType == "Public" ? true : false,
             OwnerId = userId,
             Owner = this.db.Users.FirstOrDefault(u=>u.Id == userId)
            
            };

            this.db.Repositories.Add(newRepository);
            this.db.SaveChanges();
        }

        public IEnumerable<RepositoryViewModel> GetAll()
        {
            var allRepos = this.db.Repositories
                                  .Where(r => r.IsPublic)
                                  .Select(r=> new RepositoryViewModel 
                                  { 
                                   Id = r.Id,
                                   Name = r.Name,
                                   CreatedOn = r.CreatedOn,
                                   Owner = r.Owner.Username,
                                   CommitsCount = r.Commits.Count()
                                  })
                                  .ToList();

            return allRepos;

        }

        public string GetRepositoryName(string repoId)
        {
            return this.db.Repositories.Where(r => r.Id == repoId).FirstOrDefault()?.Name;
        }
    }
}
