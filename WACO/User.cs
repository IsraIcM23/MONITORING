using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WACO
{
    public class User
    {
        public int CI { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        List<Consumption> userConsumption = new List<Consumption>();

        public User(int ci, string name, string surname)
        {
            CI = ci;
            Name = name;
            SurName = surname;
        }
        public User(int ci, string name, string surname, List<Consumption> consumptions)
        {
            CI = ci;
            Name = name;
            SurName = surname;
            userConsumption = consumptions;
        }

        public void Add_Consumption(Consumption Myconsumption)
        {
            userConsumption.Add(Myconsumption);
        }

        public bool VerifyLecture(string period){
            return userConsumption.Any(x => x.Period == period);
        }

        public void ShowConsumptionRecord(int ci)
        {
            foreach (var item in userConsumption)
            {
                Console.WriteLine($"The consumption of the period { item.Period } is { item.Lecture }");
            }
        }
        public void TotalDebt()
        {
            double total = 0;
            int calc = 0;

            for (int i = 0; i < userConsumption.Count(); i++)
            {
                if (!userConsumption[i].IsPaid)
                {
                    calc = i == 0 ? Math.Abs((userConsumption[i].Lecture - userConsumption[i].initialLecture) * 2) : Math.Abs((userConsumption[i].Lecture - userConsumption[i - 1].Lecture) * 2);
                    total += calc;
                    Console.WriteLine($"Periodo : {userConsumption[i].Period}  Pago Consumo: {calc} Bs.");
                }
            }
            Console.WriteLine($"Deuda Final: {total} Bs.");
        }

        public void PaidTotalDebt()
        {
            foreach (var consumption in userConsumption)
            {
                if (!consumption.IsPaid)
                {
                    consumption.Pay();
                }
            }
        }

        public void PaidPartialDebt(int numberPeriods)
        {
            if(numberPeriods <= userConsumption.Count())
            {
                for (int i = 0; i < numberPeriods; i++)
                {
                    if (!userConsumption[i].IsPaid)
                    {
                        userConsumption[i].Pay();
                    }
                }
            }
            else
            {
                throw new ConsumptionException();
            }
            
        }

        public double TotalDebt(List<Consumption> consumptions)
        {
            double total = 0;
            int calc = 0;

            for (int i = 0; i < consumptions.Count(); i++)
            {
                if (!consumptions[i].IsPaid)
                {
                    calc = i == 0 ? Math.Abs((consumptions[i].Lecture - consumptions[i].initialLecture) * 2) : Math.Abs((consumptions[i].Lecture - consumptions[i - 1].Lecture) * 2);
                    total += calc;
                }
            }
            return total;
        }

        public int PaidTotalDebt(List<Consumption> consumptions)
        {
            foreach (var consumption in consumptions)
            {
                if (!consumption.IsPaid)
                {
                    consumption.Pay();
                }
            }
            return 0;
        }

        public void PaidPartialDebt(int numberPeriods, List<Consumption> consumptions)
        {
            if (numberPeriods <= consumptions.Count())
            {
                for (int i = 0; i < numberPeriods; i++)
                {
                    if (!consumptions[i].IsPaid)
                    {
                        consumptions[i].Pay();
                    }
                }
            }
            else
            {
                throw new ConsumptionException();
            }

        }
    }
}
