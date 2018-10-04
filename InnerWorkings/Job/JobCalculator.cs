using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace InnerWorkings.Job
{
    public class JobCalculator
    {
        //data
        private IEnumerable<JobComponents> componentsList;
        private JobComponents pComponents;

        //Constructor
        public JobCalculator(JobComponents _components, List<JobComponents> _componentsList)
        {
            Debug.Assert(_components != null); //safety
            Debug.Assert(_componentsList != null); //safety

            pComponents = _components;
            componentsList = _componentsList;
        }

        //This function calculates the total price of the job
        //based on certain variables
        public decimal CalculateTotal()
        {
            decimal temp = 0.0m;

            decimal totalCost = componentsList.Sum(x => x.getCost);
            decimal totalItemPrice = componentsList.Sum(x => x.getPrice);

            decimal margin = totalCost * pComponents.getMargin;

            temp = Math.Round((totalItemPrice + margin) / 2m, 2) * 2m;

            return temp;
        }

        //Print method
        public string DumpOutput(int index)
        {
            string temp = string.Concat(componentsList.Select(x => $"{x.getJobType}: ${x.getPrice:F2}\n")) + $"total: ${CalculateTotal():F2}";

            Console.WriteLine(temp);
            File.WriteAllLines($"../../JobFilesOutput/Job{index}Output.txt", temp.Split('\n'));

            return temp;
        }
    }
}
