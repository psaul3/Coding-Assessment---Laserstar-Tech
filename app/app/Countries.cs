using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app
{
    class Country
    {
        public string Name { get; set; }
        public string Continent { get; set; }
        public string Capital { get; set; }
        public string Leader { get; set; }
        public int Area { get; set; }
        public int Population { get; set; }
        public string Currency { get; set; }
        public string Timezone { get; set; }

        public Country(string name, string continent, string capital, string leader, int area, int population, string currency, string timezone)
        {
            Name = name;
            Continent = continent;
            Capital = capital;
            Leader = leader;
            Area = area;
            Population = population;
            Currency = currency;
            Timezone = timezone;
        }
    }
}
