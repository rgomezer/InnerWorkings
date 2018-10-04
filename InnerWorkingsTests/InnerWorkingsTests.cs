using System;
using System.Collections;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InnerWorkings.Job;

namespace InnerWorkingsTests
{
    [TestClass]
    public class InnerWorkingsUnitTest
    {
        [TestMethod]
        public void JobComponentCreationTestMethod()
        {
            string description = "envelope";
            decimal cost = 500.00m;
            bool taxExempt = false;
            bool extraMargin = true;

            JobComponents job1 = new JobComponents(description, cost, taxExempt, extraMargin);

            Assert.AreEqual(description, job1.getJobType);
            Assert.AreEqual(cost, job1.getCost);
            Assert.AreEqual(taxExempt, job1.getIsTaxExempt);
            Assert.AreEqual(extraMargin, job1.getIsExtraMargin);
            Assert.AreEqual(535.00m, job1.getPrice);

            bool taxExempt2 = true;
            string description2 = "letterhead";

            JobComponents job2 = new JobComponents(description2, cost, taxExempt2);

            Assert.AreEqual(description2, job2.getJobType);
            Assert.AreEqual(cost, job2.getCost);
            Assert.AreEqual(taxExempt2, job2.getIsTaxExempt);
            Assert.AreEqual(false, job2.getIsExtraMargin);
            Assert.AreEqual(500.00m, job2.getPrice);
        }

        [TestMethod]
        public void JobParserTestMethod()
        {
            bool extraMargin = true;
            bool taxExempt = false;
            string description = "envelopes";
            decimal price = 556.40m;

            JobParser job1 = new JobParser("../../JobFiles/Job1.txt");
            job1.LoadJobElements();

            JobComponents temp = job1.getJob(0);
            Assert.AreEqual(description, temp.getJobType);
            Assert.AreEqual(extraMargin, temp.getIsExtraMargin);
            Assert.AreEqual(price, temp.getPrice);
            Assert.AreEqual(taxExempt, temp.getIsTaxExempt);

            extraMargin = true;
            taxExempt = true;
            description = "letterhead";
            price = 1983.37m;

            temp = job1.getJob(1);

            Assert.AreEqual(description, temp.getJobType);
            Assert.AreEqual(extraMargin, temp.getIsExtraMargin);
            Assert.AreEqual(price, temp.getPrice);
            Assert.AreEqual(taxExempt, temp.getIsTaxExempt);
        }

        [TestMethod]
        public void Job1JobCalculatorTestMethod()
        {
            JobParser job1 = new JobParser("../../JobFiles/Job1.txt");
            job1.LoadJobElements();

            JobComponents temp = job1.getJob(0);
            List<JobComponents> test = job1.getJobAllElements().ToList();
            JobCalculator pCalc = new JobCalculator(temp, test);

            pCalc.DumpOutput(1);

            decimal total = 2940.30m;

            Assert.AreEqual(total, pCalc.CalculateTotal());
        }

        [TestMethod]
        public void Job2JobCalculatorTestMethod()
        {
            JobParser job2 = new JobParser("../../JobFiles/Job2.txt");
            job2.LoadJobElements();

            JobComponents temp = job2.getJob(0);
            List<JobComponents> test = job2.getJobAllElements().ToList();

            JobCalculator pCalc = new JobCalculator(temp, test);
            pCalc.DumpOutput(2);

            decimal total = 346.96m;

            Assert.AreEqual(total, pCalc.CalculateTotal());
        }

        [TestMethod]
        public void Job3JobCalculatorTestMethod()
        {
            JobParser job3 = new JobParser("../../JobFiles/Job3.txt");
            job3.LoadJobElements();

            JobComponents temp = job3.getJob(0);
            List<JobComponents> test = job3.getJobAllElements().ToList();

            JobCalculator pCalc = new JobCalculator(temp, test);
            pCalc.DumpOutput(3);

            decimal total = 24608.68m;

            Assert.AreEqual(total, pCalc.CalculateTotal());
        }

        [TestMethod]
        public void ReadJob1OutputTestMethod()
        {
            List<string> descriptions;
            List<decimal> costs;

            string type1 = "envelopes:";
            string type2 = "letterhead:";
            decimal cost1 = 556.40m;
            decimal cost2 = 1983.37m;
            decimal total = 2940.30m;

            privReadOutputFile("../../JobFilesOutput/Job1Output.txt", out descriptions, out costs);

            Assert.AreEqual(type1, descriptions.ElementAt(0));
            Assert.AreEqual(type2, descriptions.ElementAt(1));
            Assert.AreEqual(cost1, costs.ElementAt(0));
            Assert.AreEqual(cost2, costs.ElementAt(1));
            Assert.AreEqual(total, costs.ElementAt(2));
        }

        [TestMethod]
        public void ReadJob2OutputTestMethod()
        {
            List<string> descriptions;
            List<decimal> costs;

            string type1 = "t-shirts:";
            decimal cost1 = 314.62m;
            decimal total = 346.96m;

            privReadOutputFile("../../JobFilesOutput/Job2Output.txt", out descriptions, out costs);

            Assert.AreEqual(type1, descriptions.ElementAt(0));
            Assert.AreEqual(cost1, costs.ElementAt(0));
            Assert.AreEqual(total, costs.ElementAt(1));
        }

        [TestMethod]
        public void ReadJob3OutputTestMethod()
        {
            List<string> descriptions;
            List<decimal> costs;

            string type1 = "frisbees:";
            string type2 = "yo-yos:";
            decimal cost1 = 19385.38m;
            decimal cost2 = 1829.00m;
            decimal total = 24608.68m;

            privReadOutputFile("../../JobFilesOutput/Job3Output.txt", out descriptions, out costs);

            Assert.AreEqual(type1, descriptions.ElementAt(0));
            Assert.AreEqual(type2, descriptions.ElementAt(1));
            Assert.AreEqual(cost1, costs.ElementAt(0));
            Assert.AreEqual(cost2, costs.ElementAt(1));
            Assert.AreEqual(total, costs.ElementAt(2));
        }

        //Helper function
        private void privReadOutputFile(string filename, out List<string> descriptions, out List<decimal> costs)
        {
            List<string> jobElements = File.ReadAllLines(filename).ToList();

            descriptions = new List<string>();
            costs = new List<decimal>();

            foreach (var line in jobElements)
            {
                if (string.IsNullOrWhiteSpace(line) || string.IsNullOrEmpty(line))
                {
                    continue;
                }

                var items = line.Split(' ');

                string description = items[0];
                decimal cost = Decimal.Parse(items[1], NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint | NumberStyles.AllowCurrencySymbol);

                descriptions.Add(description);
                costs.Add(cost);
            }
        }
    }
}
