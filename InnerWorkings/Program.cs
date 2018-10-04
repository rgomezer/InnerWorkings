using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using InnerWorkings.Job;

namespace InnerWorkings
{
    class Program
    {
        static void ProcessJobs(int index)
        {
            Console.WriteLine("\nJob {0} Output\n", index);
            JobParser job1 = new JobParser($"../../JobFiles/Job{index}.txt");
            job1.LoadJobElements();

            JobComponents temp = job1.getJob(0);
            List<JobComponents> test = job1.getJobAllElements().ToList();

            JobCalculator pCalc = new JobCalculator(temp, test);

            pCalc.DumpOutput(index);     
        }

        static void Main(string[] args)
        {
            for(int i = 1; i < 4; i++)
            {
                ProcessJobs(i);
            }
        } 
    }
}
