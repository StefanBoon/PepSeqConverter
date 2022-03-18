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
using System.Diagnostics;


namespace PepSeqConverter
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

        int butje = 0;
        private void threeButton_Checked(object sender, RoutedEventArgs e)
        {    butje = 1;  }
        private void oneButton_Checked(object sender, RoutedEventArgs e)
        { butje = 2;  }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string[] splitted = null;
            int NUM = 0;
            string input = InputSeq.Text;
            input = input.Replace(" ", string.Empty);
            input = input.Replace("\n", "").Replace("\r", "");
            string[] sequence = { "" };
            int numy = 0;
            int numo = 0;

            if (butje == 1)
            { /// /// /// THREE TO ONE
                input = input.ToLower();


                char[] seppers = new char[] { ' ', '-', '/', '_', '.', '|', '=', '/' };

                bool? booly = null;
                foreach (var charry in seppers)
                {
                    booly = input.Contains(charry);
                    if (booly == false) { NUM = 1; }
                    else
                    {
                        NUM = 0;
                        break;
                    }
                }

                string[] aminos = { "ala", "cys", "asp","glu","phe","gly","his","ile","lys"
              ,"leu","met","asn","pro","gln","arg","ser","thr","val","trp","tyr"};
                string[] amino = { "A", "C", "D", "E", "F", "G", "H", "I", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "Y" };

                if (NUM == 1)
                {
                    List<string> list = new List<string>();
                    double length = input.Length;
                    double ratio = length / 3;
                    if ((ratio % 1) == 0)  {
                    for (int i = 0; i < length; i += 3)
                        {
                            string threes = input.Substring(i, 3);
                            list.Add(threes);
                        }
                        splitted = list.ToArray();
                    } else
                    {
                        MessageBox.Show("Input Error!\n\n" +
                              "Possible causes:\n\n" +
                              "- One ore more amino acids contain less than three letters. Please check for typos.\n\n" +
                              "- Input amino acids do not exist or are non natural amino acids. Please check for typos.\n\n" +
                              "- Incorrect characters are used. Please only paste the sequence, without additive characters or text\n\n" +
                              " For more help contact Stefan Boon", "ERROR");
                        return;
                    }
                }
                else { splitted = input.Split(seppers); }


                List<string> seq = new List<string>(sequence.ToList());

             
                foreach (var b in splitted)
                {
                    if (aminos.Contains(b))
                    {
                        numo++;
                    }
                    else { numy++; }
                }

                if (numy != 0)
                {
                    MessageBox.Show("Input Error!\n\n" +
                        "Possible causes:\n\n" +
                        "- Input amino acids do not exist or are non natural amino acids. Please check for typos.\n\n" +
                        "- Seperators are not correct. Check if seperators are consistent throughout the sequence\n\n" +
                        "- Incorrect characters are used. Please only paste the sequence, without additive characters or text\n\n" +
                        " For more help contact Stefan Boon", "ERROR");
                }
                else
                {
                    foreach (var a in splitted)
                    {
                        int num = Array.FindIndex(aminos, m => m == a);
                        seq.Add(amino[num]);

                    }
                    string sequ = string.Join("", seq.ToArray());

                    TextOutput.Text = sequ;
                }
            }


            if (butje == 2) /// /// /// ONE TO THREE
            {
                string[] aminoos = { "Ala", "Cys", "Asp","Glu","Phe","Gly","His","Ile","Lys"
              ,"Leu","Met","Asn","Pro","Gln","Arg","Ser","Thr","Val","Trp","Tyr"};
                string[] aminoo = { "A", "C", "D", "E", "F", "G", "H", "I", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "Y" };


                List<string> listt = new List<string>();
                int length = input.Length;

                for (int i = 0; i < length; i++)
                {
                    string threes = input.Substring(i, 1);
                    listt.Add(threes);
                }
                splitted = listt.ToArray();


                List<string> seq = new List<string>(sequence.ToList());

               
                foreach (var b in splitted)
                {
                    if (aminoo.Contains(b))
                    {
                        numo++;
                    }
                    else { numy++; }
                }

                if (numy != 0)
                {
                    MessageBox.Show("Input Error!\n\n" +
                        "Possible causes:\n\n" +
                        "- Input amino acids do not exist or are non natural amino acids. Please check for typos.\n\n" +
                        "- Incorrect characters are used. Please only paste the sequence, without additive characters\n\n" +
                        " For more help contact Stefan Boon", "ERROR");
                }
                else
                {
                    foreach (var a in splitted)
                    {
                        int num = Array.FindIndex(aminoo, m => m == a);
                        seq.Add(aminoos[num]);

                    }
                    string sequ = string.Join("", seq.ToArray());

                    TextOutput.Text = sequ;

                }
            }
        }

        private void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e) 
        {  this.Close(); }
       private void hideButton_Click_1(object sender, RoutedEventArgs e)
        { this.WindowState = WindowState.Minimized; }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {  if (e.LeftButton == MouseButtonState.Pressed) { DragMove();  }  }

        
    }
}
