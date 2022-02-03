using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Timers;


namespace kool_game_idea_try_1._2021_b4_summer
{
    public partial class FirstScene_TextBaseStart : Form
    {

        //////
        //// stuff strinigeis and inties
        ////

        int TimesOpt1;
        int TimesOpt2;
        int TimesOpt3;
        int TimesOpt4;

        ////
        //// end of stiff stringies and inties
        ////




        public FirstScene_TextBaseStart()
        {
            InitializeComponent();
            //Global();

        }


        private void FirstScene_TextBaseStartt(object sender, EventArgs e)
        {

        }

        // word = "you wake up at a bar feeling deezy" + Environment.NewLine + "you look around and see nobody expect the barman staring directly at you";







        //www.youtube.com/watch?v=BnExQ7JaY1c
        // problem "An object reference is required for the nonstatic field, method, or property 'InfoText'"
        //solution stackoverflow.com/questions/498400/cs0120-an-object-reference-is-required-for-the-nonstatic-field-method-or-prop
        // another problem - main thread is occupied with time consuming work and UI is not responsive while "Writing slow"
        // video explaining threads really well www.youtube.com/watch?v=8mjqXiggWNc
        // problem 2 with another problem - code was ~ Thread WriteSlowThread = new Thread(WriteSlow) ~ and there was an error bc of parameters I guess.
        // solution - stackoverflow.com/questions/14854878/creating-new-thread-with-method-with-parameter = took the one line to start a thread with parameter in a correct way.
        // this took like 3 hours btw.
        // but aparently I cannot call or change the object InfoText text box via a different thread
        // another solution stackoverflow.com/questions/519233/writing-to-a-textbox-from-another-thread/25282476   - which means we need to confirm invoke or something. [I made InfoTextThreadingChecker]
        //4h
        // yaaaa I added ~ this.Invoke( ( MethodInvoker )delegate(){   do smth }); ~ before the try to put text in the textbox and it workssssss

        System.Threading.EventWaitHandle WaitForConfirm_2; //for the Main External Thread... I need this to be outside the thread itself so figured id put it here....
        ManualResetEvent WaitForConfirm = new ManualResetEvent(false);

        public void TestButton_Click(object sender, EventArgs e)
        {
            MyGlobals.UserInputText = string.Copy(UserInputBox.Text);

            InfoTextBox.Visible = true;
            UserInputBox.Visible = true;
            InfoTextBox.ReadOnly = true;
            TestButton.Visible = false;
            InfoTextBox.Text = "";
            //Thread WriteSlowThread = new Thread(() => WriteSlow("You just finished eating your favorite meal at your usual dining place"));
            //WriteSlowThread.Start();
            //now the thread continues withought wating for the text to be finished. beware! .... nvm fixed XD

            Thread SecondaryMainThread = new Thread(SecondaryMain); //potentially should this be the "main" thread??
            SecondaryMainThread.Start();



            Thread DisplayGraphicThread = new Thread(DisplayGraphic);
            DisplayGraphicThread.Start();


            // WriteSlowThread = new Thread(() => WriteSlow("welcome to the zooca world"));
            // this makes writing two things at the same time l-asds-i-asda-k-asdas-e this.
            // WriteSlowThread.Start();
        }

        /////////   ^^^ start button ^^^ ... vvv slow text stuff




        public void WriteSlow(string text, int speed = 40, int TimesTimeBetweenWords = 5, int TimesTimeBetweenLines = 10)
        {
            FirstScene_TextBaseStart AnotherFormToDoTasksMeanwhile = new FirstScene_TextBaseStart();
            MyGlobals.SlowTextActive = true;

            //NewLineInfoText();

            foreach (char c in text)
            {
                
                if (c == ' ') // so it takes more time between each word.
                {
                    this.Invoke((MethodInvoker)delegate ()
                    {
                        InfoTextBox.Text += c;
                        System.Threading.Thread.Sleep(speed*TimesTimeBetweenWords);
                    });
                }
                else   // and regularly take the meant to be - speed, betwwen each character(letter).
                {
                    this.Invoke((MethodInvoker)delegate ()
                    {
                        InfoTextBox.Text += c;
                        System.Threading.Thread.Sleep(speed);
                    });
                }

            }

            NewLineInfoText();
            MyGlobals.SlowTextActive = false;
        }


        public void WaitingForSlowTextToFinish()
        {
            int WasteTime = 0;
            System.Threading.Thread.Sleep(5000);
            while (true)
            {
                if (MyGlobals.SlowTextActive == true)
                {
                    WasteTime++;
                    System.Threading.Thread.Sleep(1000);
                }

                else if (MyGlobals.SlowTextActive == false)
                {
                    break;
                }
            }
            //this was necessary when there were two diffrent external threads running, so the "main thread" wouldnt start or go on withought the text written being finished.
            // basically so it won't be faster... but without the need to use two external threads this peace of gode is unnecessary. :)
        }



        //////////////////////////////    ^^^^^^^ wirte slow stuff ^^^^ ... vvvvvv text functionate and calling methodes vvvvvvv
        //


        private void btnEnterUIInput_Click(object sender, EventArgs e)   ///confirm button btnEnterUIInput_Click
        {
            MyGlobals.UserInputText = UserInputBox.Text; //make text variable match the current UI text

            //[this is done by the main UI thread btw]
            WaitForConfirm.Set(); // make second main external thread - continue, after making User input to be equal to the one in the UI box.
            WaitForConfirm.Reset(); // so its only temporary. because without this, whenever its supposed to stop itll just contie bc gate is open after one time program reads this :o

        }


        //short methods vv
        public void ResetInfoText()  ///for external threads.
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                InfoTextBox.Text = "";
            });
        }
//////////////////////////////////////////////////////////
        public void NewLineInfoText()  ///for external threads.
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                string TextToCheck = InfoTextBox.Text;
                int NumberOfLines = InfoTextBox.Text.Split('\n').Length; // back slash is a really tricky character but I guess this is how you do it ;-;... btw thanks for google "How to count lines in a string?"

                if (NumberOfLines >= 8)
                {
                    System.Threading.Thread.Sleep(2000);
                    ResetInfoText();
                    InfoTextBox.Text += System.Environment.NewLine;
                    
                }
                else
                {
                    InfoTextBox.Text += System.Environment.NewLine;
                }
            });
        }
        //short methods ^^


        //whats good about this vv is that I dont need to make a loop to confirm the button "confirm" was pressed, because it accures only when pressed... XD
        public void AskUser(string opt1, string opt2, string opt3, string opt4)  ///should accure when user needs to respond. (contains variables of components for questions.)
        {
                


            TimesOpt1 = 0;
            TimesOpt2 = 0;
            TimesOpt3 = 0;
            TimesOpt4 = 0;
            bool WriteOptions = true;
            //reset the number of times done each answer.

            while (true)
            {
                NewLineInfoText();



                if (WriteOptions == true) { WriteSlow("[" + opt1 + ", " + opt2 + ", " + opt3 + ", " + opt4 + "]"); }

                /// I should make a thing that break the asnwer if it has several words, and split it to check if its there so yk, user doesnt have to be too specific (@!!!!!!!!!!)!!!!
                ///

                WaitForConfirm.WaitOne(); ///after asking the user a question, wait for the user to press the confirm button.

                string UserCommand;           ///make new string so next step will be doable. and also make all the below look nicer.

                UserCommand = MyGlobals.UserInputText.Trim();    //make sure to remove exeses " " space bars/.
                

                if (UserCommand == "")  ///in case UserInput is actually blank
                {
                    MyGlobals.UserInputSerialNumberAnswer = 0; // value 0 means its not a valid ansewr OR there is not answer!!!!
                    MyGlobals.UserInputText = "Null";
                    WriteSlow("make sure to write a response in the box below");
                    WriteOptions = false;
                }
                else if (UserCommand.StartsWith(opt1) && opt1 != "") //added =! ""    -> so if to begin with it was blank, user info being blank wont trigger it.
                {
                    ResetInfoText();
                    TimesOpt1 += 1;
                    MyGlobals.UserInputSerialNumberAnswer = 1;
                    break;
                }
                else if (UserCommand.StartsWith(opt2) && opt2 != "")
                {
                    ResetInfoText();
                    TimesOpt2 += 1;
                    MyGlobals.UserInputSerialNumberAnswer = 2;
                    break;
                }
                else if (UserCommand.StartsWith(opt3) &&opt3 != "")
                {
                    ResetInfoText();
                    TimesOpt3 += 1;
                    MyGlobals.UserInputSerialNumberAnswer = 3;
                    break;
                }
                else if (UserCommand.StartsWith(opt4) && opt4 != "")
                {
                    ResetInfoText();
                    TimesOpt4 += 1;
                    MyGlobals.UserInputSerialNumberAnswer = 4;
                    break; 
                }

                /// GeneralCommands

                else if (UserCommand.StartsWith("examine")) /// examine X
                {
                    ResetInfoText();
                    //
                    break;
                }

                else if (UserCommand.StartsWith("search")) // search in X. not search for X.
                {
                    if (UserCommand.StartsWith("search for"))
                    {

                    }
                    ResetInfoText();
                    //
                    break;
                }

                else if (UserCommand == "wait") /// wait.
                {
                    ResetInfoText();
                    //
                    break;
                }

                else if (UserCommand == "inventory") /// inventory.
                {
                    ResetInfoText();
                    //
                    break;
                }

                else if (UserCommand.StartsWith("talk")) // talk to X
                {

                }


                //// GeneralCommands end



                else
                {
                    WriteSlow("sorry that is not a valid option");       //this will be good only if theres a button to push to confirm user info, like... pressing ENTER or pressing a button.
                    MyGlobals.UserInputSerialNumberAnswer = 0;// value 0 means its not a valid ansewr OR there is not answer!!!!
                    WriteOptions = true;
                }
            }
        }

        //////////////////////////////end of AskUser///////////////////////////////////////////




        ///                                                                                                     ///
        /// ///actual main thread vv [secondary, for process and basically make the game move on, forward.      ///
        ///                                                                                                     ///
        public void SecondaryMain()
        {


            // Thread WriteSlowThread = new Thread(() => WriteSlow("You just finished eating your favorite meal at your usual dining place"));
            // WriteSlowThread.Start();
            // .... wait.. im aalready in a diffrent thread other than the main one that uses UI. do I really need to make ANOTHER THREAD?? lemme try 0~0
            //WriteSlow("You just finished eating your favorite meal at your usual dining place");
            //WaitingForSlowTextToFinish();
            // .... I also dont need to wait for the other thread to end becasue there is no other thread if I do this, since there cannot be problems with
            // one of the threads being too fast or finishing too early :D

            //    WriteSlowThread = new Thread(() => WriteSlow("After a long day it was all you wished for, and now you feel satisfied."));
            //    WriteSlowThread.Start();

            //WriteSlow("After a long day it was all you wished for, and now you feel satisfied.");
            //WaitingForSlowTextToFinish();

            //holy shit... it looks so clean without all the messy unnecessary stuff... I love efficiating :,>

            //
            //but I need to do this in order to call objects and do stuff outside the external method
            //
            //  this.Invoke((MethodInvoker)delegate ()
            //  {
            //    code
            //    code
            //  });
            //
            //  ... or on the other hand I can just create another shortcut pre-made function and call it inside the methode with one line of text. lol.
            //

            WriteSlow("You just finished eating your favorite meal at your usual dining place");
            System.Threading.Thread.Sleep(2000);

            WriteSlow("After a long day it was all you wished for, and now you feel satisfied.");
            System.Threading.Thread.Sleep(2000);

            this.Invoke((MethodInvoker)delegate ()
                {
                    GraphicDisplay.Visible = true;
                    GraphicDisplay.Image = Properties.Resources.disp1_FinishedEating;
                    //image must be 444, 277.
                    NewLineInfoText();
                }); ///can make easier methode to make it without the weaird claunksdy code xx
            System.Threading.Thread.Sleep(2000);


            string[] OptionsToAsk = { "sip", "stand", "sing", "" };

            // idk how to make ask user function use string array and not 4 strings....................................

            string ask1 = "sip";
            string ask2 = "stand";
            string ask3 = "sing";
            string ask4 = "";

            bool AskUserLoop = true;
            bool BreakAskUserLoop = false;

            while (AskUserLoop)
            {

                WriteSlow("what do u do?");



                AskUser(ask1, ask2, ask3, ask4);

                switch (MyGlobals.UserInputSerialNumberAnswer)

                {
                    case 1:
                        {
                            if (TimesOpt1 >= 2)   //because not the first time, the second time and above.
                            {
                                ResetInfoText();
                                WriteSlow("you are not thirsty anymore.");
                                System.Threading.Thread.Sleep(2000);
                                // AskUser("sip", "stand", "sing", ""); //not out of dialoge loop
                                break;
                            }
                            else
                            {
                                ResetInfoText();
                                WriteSlow("even though you have just finished eating, you still feel thirsty");
                                WriteSlow("you sip the last remaining drink in your water glass");
                                //WaterBar.Value = 100;    bug cross thread other than the one it was created on
                                System.Threading.Thread.Sleep(2000);
                                // AskUser("sip", "stand", "sing", ""); //not out of dialoge loop     --------------------current_problem---> why doesnt it jump to AskUser function?
                                                                     //, also - check if in the WriteSlow function its better to go down a line before the writing begins or after the writing begins (after eg. for the next writing to take place)
                                break;
                            }
                        }

                    case 2:
                        {
                            ResetInfoText();
                            WriteSlow("you feel as it has been enough");
                            WriteSlow("time to head home.");
                            System.Threading.Thread.Sleep(2000);
                            //out of dialoge loop

                            BreakAskUserLoop = true;
                            break;
                            

                        }
                    case 3:
                        {
                            ResetInfoText();
                            WriteSlow("♫♪ lalalala ♫ ♪ ♫♪");
                            System.Threading.Thread.Sleep(2000);
                            // AskUser("sip", "stand", "sing", ""); //not out of dialoge loop
                            break;
                        }
                    default:
                        {

                            WriteSlow("ERROR ASDHJAHSKFJHA");       //this should not accure, since there is a loop and it should not get to here before having a valid answer
                            break;
                        }
                }

                if (BreakAskUserLoop)
                {
                    break;
                }
            }

            /// after finishing eating dialog.

            ResetInfoText();
            WriteSlow("you ask for the bill, thank the waiter and get out through the main door.");
            System.Threading.Thread.Sleep(2000);



            while (AskUserLoop)
            {



                if (BreakAskUserLoop)
                {
                    break;
                }
            }




                WriteSlow("code ends here.");



            // current problem - text does not match the actual text in the UserInputBox... as if it doesnt sync. :/
            // yaaa I did it but its kinda clunky.. by making the confirm button also change the variable to whatever the text currently is
            //
            // well it works fine now, but I dont want to make this never ending loop to check every 5 milli secnonds wether the button is pressed or not...
            //maybe I can make a bool and make it True when the dialog should take place, and then after clicking the button do something only if its true :>
            // but then I kind of want to suspend this thread,, I mean.. so it should freeze and wait for the dialog (which is done by the other main UI thread) to end and not move forwards meanwhile.





        }
                ///////////////   ^^^^ MAIN THREAD / ...   vvvvvv GraphicDisplay stuff vvvvvvv


                public void DisplayGraphic()
        {
            // GraphicDisplay.Visible = true;

        }

        private void HealthBar_Click(object sender, EventArgs e)
        {

        }

        private void WaterBar_Click(object sender, EventArgs e)
        {

        }









        /*   static class Global
           {
               private static string _globalVar = "";

               public static string GlobalVar
               {
                   get { return _globalVar; }
                   set { _globalVar = value; }
               }
           }



           GlobalClass.GlobalVar = "any string value"
           */

    }


}







public static class MyGlobals
{
    public static bool SlowTextActive = false;
    public static int UserInputSerialNumberAnswer;
    public static bool ConfirmButtonPress;
    public static string UserInputText;
    public static int MapLocationSerialNumber; // 1 = restaurant. 2 = outside restaurant. 3 = ...?
    //private static AutoResetEvent WaitForConfirm = new AutoResetEvent(false);
    
}
