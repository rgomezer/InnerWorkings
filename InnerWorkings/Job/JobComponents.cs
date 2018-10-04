using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace InnerWorkings.Job
{
    public class JobComponents
    {
        //data
        private string jobType;
        private decimal cost;
        private bool isTaxExempt;
        private bool isExtraMargin;
        private const decimal salesTax = 0.07m;
        private const decimal baseMargin = 0.11m;
        private const decimal extraMargin = 0.05m;
        private decimal price;

        //Constructor
        public JobComponents(string _type, decimal _cost, bool _isTaxExempt = false, bool _isExtraMargin = false)
        {
            Debug.Assert(_type != null); //safety
            Debug.Assert(_cost >= 0.0m); //safety

            this.jobType = _type;
            this.cost = _cost;
            this.isTaxExempt = _isTaxExempt;
            this.isExtraMargin = _isExtraMargin;

            this.price = privSetPrice(this.isTaxExempt);
        }

        //Set price with added sales tax if item is not tax-exempt
        //else do not add sales tax
        private decimal privSetPrice(bool _isTaxExempt)
        {
            decimal temp = 0.0m;

            if(_isTaxExempt)
            {
                temp = this.cost;
            }
            else
            {
                temp = Math.Round(cost.CreatePercentage(salesTax), 2);
            }

            return temp;
        }

        //Accesors
        public string getJobType
        {
            get { return this.jobType; }
        }

        public decimal getCost
        {
            get { return this.cost; }
        }

        public bool getIsTaxExempt
        {
            get { return this.isTaxExempt; }
        }

        public bool getIsExtraMargin
        {
            get { return this.isExtraMargin; }
        }

        public decimal getPrice
        {
            get { return price; }
        }

        public decimal getMargin
        {
            get
            {
                if(isExtraMargin)
                {
                    return extraMargin + baseMargin;
                }

                return baseMargin;
            }
        }
    }

    public static class PercentExtension
    {
        public static decimal CreatePercentage(this decimal val, decimal rate)
        {
            return val * (1 + rate);
        }
    }
}
