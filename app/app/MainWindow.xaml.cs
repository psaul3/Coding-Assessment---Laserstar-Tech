using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IEnumerable<Country> countries;

        public MainWindow()
        {
            countries = getCountryList(ReadCSV("SoftwareCandidates.csv"));
            TreeViewItem treeItem = null;
            
            InitializeComponent();

            foreach (string continent in getContinentList(countries))
            {
                treeItem = new TreeViewItem
                {
                    Header = continent
                };
                tvContinents.Items.Add(treeItem);
            }

        }
        
        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            PopulateCountryList(((TreeViewItem)((TreeView)sender).SelectedItem).Header.ToString());

        }

        private void ListViewItem_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            // get the current selected item
            if (lvCountries.SelectedItem != null)
            {
                //Get country object from selected country
                Country country = (Country)lvCountries.SelectedItem;
                //Fill in Country details
                txtCountry.Text = country.Name;
                txtCapital.Text = country.Capital;
                txtPresident.Text = country.Name;
                txtArea.Text = String.Format("{0:n0}", country.Area);
                txtCurrency.Text = country.Currency;

                //Calculate population density
                double populationDensity = country.Population / country.Area;
                txtPopulationDensity.Text = String.Format("{0:n}(ppl/Sq-mi)", populationDensity);

                //Calculate current time in 'capital city' ?
                DateTime newTime = new DateTime(), 
                         now = DateTime.UtcNow;
                string countryTime = country.Timezone.Trim('"');
                if (countryTime.Contains("-"))
                {
                    countryTime = countryTime.Trim('-');
                    TimeSpan time;

                    try {
                        time = TimeSpan.Parse(countryTime);
                        newTime = now.Subtract(time);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("{0}: Bad Format", countryTime);
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("{0}: Out of Range", countryTime);
                    }


                }
                else
                {
                    TimeSpan time;
                    try
                    {
                        time = TimeSpan.Parse(countryTime);
                        newTime = now.Add(time);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("{0}: Bad Format", countryTime);
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("{0}: Out of Range", countryTime);
                    }
                    
                }
                txtCurrentTime.Text = newTime.ToString("h:mm tt");
            }
        }

        private void PopulateCountryList(string continent)
        {  
            List<Country> items = new List<Country>();

            foreach(Country country in getCountryListByContinent(countries, continent))
            {
                items.Add(country);
            }
            lvCountries.ItemsSource = items;
        }

        string[] ReadCSV(string fileName)
        {
            return File.ReadAllLines(System.IO.Path.ChangeExtension(fileName, ".csv"));
        }

        IEnumerable<Country> getCountryList(string[] lines)
        {
            return lines.Skip(1).Select(line =>
            {
                string[] data = line.Split(',');
                return new Country(data[0], data[1], data[2], data[3], Int32.Parse(data[4]), Int32.Parse(data[5]), data[6], data[7]);
            }).Where(x => x.Name != "Country");
        }

        IEnumerable<Country> getCountryListByContinent(IEnumerable<Country> countryList, string continent)
        {
            return countryList.Select(country =>
            {
                return country;
            }).Where(x => x.Continent == continent);
        }

        IEnumerable<String> getContinentList(IEnumerable<Country> countries)
        {
            return countries.Select(c => c.Continent).Distinct();
        }

        private static T GetFrameworkElementByName<T>(FrameworkElement referenceElement) where T : FrameworkElement
        {
            FrameworkElement child = null;
            for (Int32 i = 0; i < VisualTreeHelper.GetChildrenCount(referenceElement); i++)
            {
                child = VisualTreeHelper.GetChild(referenceElement, i) as FrameworkElement;
                System.Diagnostics.Debug.WriteLine(child);
                if (child != null && child.GetType() == typeof(T))
                { break; }
                else if (child != null)
                {
                    child = GetFrameworkElementByName<T>(child);
                    if (child != null && child.GetType() == typeof(T))
                    {
                        break;
                    }
                }
            }
            return child as T;
        }

        private void lvCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
