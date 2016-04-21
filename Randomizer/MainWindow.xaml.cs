using System;
using System.Collections.Generic;
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

using System.Text.RegularExpressions;

namespace Randomizer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Randomize_Button_Click(object sender, RoutedEventArgs e)
        {
            var rnd = new Random();
            var regex = @"^\d+$";

            var userInput = EventsCount_TextBox.Text + TestsCount_TextBox.Text;

            if (!Regex.IsMatch(userInput, regex))
                return;

            var eventsCount = int.Parse(EventsCount_TextBox.Text);
            var testsCount = uint.Parse(TestsCount_TextBox.Text);

            if(eventsCount == 0 || testsCount == 0)
            {
                Results_TextBox.Text = "Нинад так.";
                return;
            }

            Event[] eventsArray = new Event[eventsCount];

            for(int i = 0; i < eventsCount; i++)
            {
                //eventsArray[i].Name = new string(i.ToString().ToCharArray());
                //eventsArray[i].Count = new uint();
                //eventsArray[i].Count = 0;
                eventsArray[i] = new Event { Name = (i + 1).ToString(), Count = 0 };
            }

            for(uint testN = 0; testN < testsCount; testN++)
            {
                var randEventIndex = rnd.Next(0, eventsCount);

                eventsArray[randEventIndex].Count++;
            }

            var sortedEvents = eventsArray.OrderByDescending(x => x.Count).ToArray();

            var longestNumLen = sortedEvents[0].Count.ToString().Length;

            var stringFormatHeader = "{0}{1," + (longestNumLen + (int)(10 / 1.2)).ToString() + "}";
            

            Results_TextBox.Text = String.Format(stringFormatHeader, "Исход №", "Число") + "\n\n";

            foreach (var currEvent in sortedEvents)
            {
                var stringFormatNum = "{0}{1," + (longestNumLen + 14 - currEvent.Name.Length).ToString() + "}";

                Results_TextBox.Text += String.Format(stringFormatNum, currEvent.Name, currEvent.Count) + "\n";
            }
        }

        public class Event
        {
            public string Name = "";
            public uint Count = 0;
        }
    }
}
