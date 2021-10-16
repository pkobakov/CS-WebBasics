namespace CarShop.Services
{
    using CarShop.Data;
    using CarShop.Data.Models;
    using CarShop.ViewModels.Issues;
    using System.Linq;

    public class IssuesService : IIssuesService
    {
        private readonly ApplicationDbContext db;

        public IssuesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public bool CarIdIsValid(string carId)
       => this.db.Cars.Any(c => c.Id == carId);

        public void AddIssue(string description, string carId) 
        {
            var issue = new Issue 
            { 
            Description = description,
            IsFixed = false,
            CarId = carId,
            Car = this.db.Cars.FirstOrDefault(c=>c.Id == carId)
            };

            this.db.Issues.Add(issue);
            this.db.SaveChanges();

        
        }

        public CarIssuesViewModel GetAll(string carId)
        {
            var issues = db.Cars.Where(c => c.Id == carId)
                                  .Select(c => new CarIssuesViewModel
                                  {
                                      CarId = c.Id,
                                      Model = c.Model,
                                      Year = c.Year,
                                      Issues = c.Issues.Select(i=> new IssueViewModel 
                                      { 
                                        Id = i.Id,
                                        Description = i.Description,
                                        IsItFixed = i.IsFixed?"Yes":"Not yet"
                                      
                                      }).ToList()

                                  })
                                  
                                  .FirstOrDefault();

            return issues;
        }

        public void FixIssue(string issueId, string carId)
        {
            var issue = this.db.Issues.Where(i=>i.Id == issueId && i.CarId == carId).FirstOrDefault();
            issue.IsFixed = true;
            this.db.SaveChanges();
        }

        public void DeleteIssue(string issueId, string cardId) 
        {
            var issue = this.db.Issues.Where(i=>i.Id == issueId && i.CarId == cardId).FirstOrDefault();
            this.db.Issues.Remove(issue);
            this.db.SaveChanges();
        
        
        }
    }
}
