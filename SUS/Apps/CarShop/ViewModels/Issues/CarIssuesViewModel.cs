using CarShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarShop.ViewModels.Issues
{
    public class CarIssuesViewModel
    {
       
        public string CarId { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public ICollection<IssueViewModel> Issues { get; set; } = new List<IssueViewModel>();
    }
}
