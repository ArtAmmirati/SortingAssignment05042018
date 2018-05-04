using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Array_File_Reader_Assignment
{
    public partial class Form1 : Form
    {

        const string pathFinder = "Names.csv";

        const int SIZE = 5000;
        string[] iArray = new string[SIZE];
        StreamReader source = File.OpenText("Names.csv");


        int counter;

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            DateTime start = DateTime.Now;
            DateTime Finish;
            TimeSpan Time;
            counter = 0;

            try
            {

                // Increment the i variable
                for (counter = 0; counter < 5000; counter++)
                {
                    // Adds each element into the array
                  
                    iArray[counter] = source.ReadLine();
                    // adds all elements to the listbox
                    listBox1.Items.Add(iArray[counter]);

                }
                Finish = DateTime.Now;
                Time = Finish - start;

                label1.Text = listBox1.Items.Count.ToString() + " " + "Results Loaded in" + " " + (Time.TotalSeconds.ToString()) + " " + "seconds";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // this sorts the selection
        private void SelectionSort(string[] iArray)
        {
            try
            {

            int minIndex;
            string minValue;

            for (int startScan = 0; startScan < iArray.Length - 1; startScan++)
            {
                minIndex = startScan;
                minValue = iArray[startScan];

                for (int index = startScan + 1; index < iArray.Length; index++)
                {
                    if (string.Compare(minValue, iArray[index], true) == 1)
                    {
                        minValue = iArray[index];
                        minIndex = index;
                    }
                }
                // this references the method "Swap"
                Swap(ref iArray[minIndex], ref iArray[startScan]);

            }
            }
            catch (Exception)
            {
                MessageBox.Show("Sorting process ERROR..");
            }
        }

        private void Swap(ref string a, ref string b)

        {
            string temp = a;
            a = b;
            b = temp;
        }

        private void sortToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                listBox1.Items.Clear();

                DateTime start = DateTime.Now;
                DateTime Finish;
                TimeSpan Time;

                while (counter < iArray.Length && !source.EndOfStream)
                {
                    iArray[counter] = source.ReadLine();
                    counter++;
                }

                listBox1.Items.Clear();
                SelectionSort(iArray);

                foreach (string value in iArray)
                {
                    listBox1.Items.Add(value);
                }

                Finish = DateTime.Now;
                Time = Finish - start;

                label2.Text = listBox1.Items.Count.ToString()
                    + " " + "Sorted in" + " " + (Time.TotalSeconds.ToString()) + " " + "seconds";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {// adds the sorted file to a text file
            try
            {

            
            string pathFinder2 = "sortedNames.csv";
            //  Writes the text files
            StreamWriter streamWriter = new StreamWriter(pathFinder2);
           // exports to the file path   
            foreach (string name in listBox1.Items)
            {
                streamWriter.WriteLine(name.ToString());
            }
                MessageBox.Show("File as been exported...");
            }
            catch (Exception)
            {
                MessageBox.Show("Error exporting file");      
            }

      
        }
        private int BinarySearch(string[] iArray, string value)
        {
            int first = 0;                      // first element
            int last = iArray.Length - 1;       // last element
            int middle;                         // Midpoint of search
            int position = -1;                  // position of search value
            bool found = false;                 // Found

            // Search for the value
            while (!found && first <= last)
            {
                // This calculates  the midpoint.
                middle = (first + last) / 2;

                // Midpoint found then true ...
                if (iArray[middle] == value)
                {
                    found = true;
                    position = middle;
                }

                //if value is in lower half...
                else if (string.Compare(iArray[middle], value, false) > 0)
                {
                    last = middle - 1;
                }

                // if value is in upper half...
                else
                {
                    first = middle + 1;
                }
            }
            // Return the position of the item.
            // If it was not found than -1
            return position;
            
        }
        
        private void searchButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Time variables
                DateTime start = DateTime.Now;
                DateTime Finish;
                TimeSpan Time;

                // name = text in textbox
                string Name = searchBox1.Text;
                // sets the position
                int position = BinarySearch(iArray, searchBox1.Text);
                // boolean determining if name is in the array
                bool Found = iArray.Contains(Name);
                // If it was not found, display this message

                if (position >= 0)
                {
                    Found = true;
                }

                if (Found == false)
                {
                    label5.Text = "This name was not found";
                }
                // If it is found
                
                else
                {
                    // Time stuff
                    Finish = DateTime.Now;
                    Time = Finish - start;
                    // Highlights the name

                    MessageBox.Show("This name was found at index # " + position);
                    listBox1.SetSelected(position, true);
                    string text = listBox1.GetItemText(listBox1.SelectedItem);
                    label5.Text = text + " " + "found in" + " " + (Time.TotalSeconds.ToString()) + " " + "seconds";
                }
            }
            catch
            {  
                MessageBox.Show("ERROR.....");
            }
        }
    }
}


        


  
    
