using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;

namespace InnerWorkings.Job
{
    public class JobParser
    {
        //data 
        private List<string> jobElements;
        private List<JobComponents> components = new List<JobComponents>();

        public JobParser(string filename)
        {
            Debug.Assert(filename != null); //safety

            jobElements = File.ReadAllLines(filename).ToList();
            Debug.Assert(jobElements != null); //safety
        }

        public void LoadJobElements()
        {
            bool hasExtraMargin = false;
            bool isTaxExempt = false;

            foreach (var line in jobElements)
            {
                if (string.IsNullOrWhiteSpace(line) || string.IsNullOrEmpty(line))
                {
                    continue;
                }

                if (line.Equals("extra-margin", StringComparison.CurrentCultureIgnoreCase))
                {
                    hasExtraMargin = true;
                    continue;
                }

                var items = line.Split(' ');

                string description = items[0];
                decimal cost = Convert.ToDecimal(items[1]);

                if (items.Length > 2 && items[2].Equals("exempt", StringComparison.CurrentCultureIgnoreCase))
                {
                    isTaxExempt = true;
                }

                JobComponents newJob = new JobComponents(description, cost, isTaxExempt, hasExtraMargin);
                components.Add(newJob);
            }
        }

        //Accessors
        public JobComponents getJob(int index)
        {
            return components.ElementAt(index);
        }

        public IEnumerable<JobComponents> getJobAllElements()
        {
            var retList = new List<JobComponents>();
            
            foreach(var temp in components)
            {
                retList.Add(temp);
            }

            return retList;
        }
    }
}
