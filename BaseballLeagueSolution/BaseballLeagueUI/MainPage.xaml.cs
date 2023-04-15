using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using BaseballLeagueLibrary;
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace BaseballLeagueUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 

    //Name : Banuka Kumara AMbegoda
    //Date : 2022-06-23
    //Lab 4/5
    public sealed partial class MainPage : Page
    {
        private static BaseballLeague db;
        public MainPage()
        {
            this.InitializeComponent();

            //Create the Database object
            db = new BaseballLeague(App.ConnectionString);

        }

        private void btnGetPlayers_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //get the playerName, ...from the textboxes
                string playerName = txbLastName.Text;
                string playerBattingAvgLow = txbMinBatAvg.Text;
                string playerBattingAvgHigh = txbMaxBatAvg.Text;
                //pass it to the database, and retrieve the player record
                List<Player> p = db.GetPlayerData(playerName, playerBattingAvgLow, playerBattingAvgHigh);

                //display the dept deptName in the textblock
                lvwPlayers.ItemsSource = p;
            }
            catch(Exception ex)
            {
                MessageDialog msg = new MessageDialog("Error Obtaining Player Data");
                msg.ShowAsync();
            }
            
                       
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            //Inserting Data
            try
            {
                Player p = new Player
                {
                    playerFirstName = txtFirstName.Text,
                    playerLastName = txtLastName.Text,
                    playerBattingAverage = Convert.ToDouble(txtBattingAvg.Text),
                    playerRunsScored = Int32.Parse(txtPlayerRuns.Text)
                };

                p = db.InsertDepartment(p);
                MessageDialog msg = new MessageDialog($"Insert Succesful");
                msg.ShowAsync();
            }
            catch (Exception ex)
            {
                //Error
                MessageDialog msg = new MessageDialog("Error inserting player data");
                msg.ShowAsync();
            }

        }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog msg = new MessageDialog("Are you sure you want to delete this?", "Confirm");
            msg.Commands.Add(new UICommand("Yes"));
            msg.Commands.Add(new UICommand("No"));

            IUICommand msgResult = await msg.ShowAsync();

            if (msgResult.Label == "Yes")
            {
                try
                {
                    int playerID = Int32.Parse(txtDelId.Text);
                    db.DeletePlayer(playerID);
                    List<Player> p = db.GetPlayerData("", "", "");
                    lvwPlayers.ItemsSource = p;
                    MessageDialog msg1 = new MessageDialog("Delete Succeded");
                    msg1.ShowAsync();
                }
                catch (Exception ex)
                {
                    MessageDialog msg1 = new MessageDialog("Delete Unsuccesful due to an error");
                    msg1.ShowAsync();
                }
            }
            

        }

        private async void btnDeleteOnList_Click(object sender, RoutedEventArgs e)
        {
            //delete button click event
            MessageDialog msg = new MessageDialog("Are you sure you want to delete this?", "Confirm");
            msg.Commands.Add(new UICommand("Yes"));
            msg.Commands.Add(new UICommand("No"));

            IUICommand msgResult = await msg.ShowAsync();

            if (msgResult.Label == "Yes")
            {
                try
                {
                    Button send = (Button)sender;

                    int playerID = Int32.Parse(send.Tag.ToString());

                    db.DeletePlayer(playerID);

                    List<Player> p = db.GetPlayerData("", "", "");
                    lvwPlayers.ItemsSource = p;
                    MessageDialog msg1 = new MessageDialog("Delete Succeded");
                    msg1.ShowAsync();
                }
                catch (Exception ex)
                {
                    MessageDialog err = new MessageDialog("Delete Failed", "Error");
                    err.ShowAsync();
                }
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Reset Button
            txtFirstName.Text = "";
            txtDelId.Text = "";
            txtLastName.Text = "";
            txtBattingAvg.Text = "";
            txtPlayerRuns.Text = "";
            txbLastName.Text = "";
            txbMaxBatAvg.Text = "";
            txbMinBatAvg.Text = "";
            List<Player> p = db.GetPlayerData("", "", "");
            lvwPlayers.ItemsSource = p;
        }
    }
}
