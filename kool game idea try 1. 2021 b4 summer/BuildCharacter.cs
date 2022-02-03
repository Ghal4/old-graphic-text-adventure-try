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


namespace kool_game_idea_try_1._2021_b4_summer
{
    public partial class BuildCharacter : Form
    {

        //        new Image Hair1Blue =

        /*    States for CharacterCreationState
        CharacterCreationState == 1  =   Hair1Blue
        CharacterCreationState == 2  =   Hair1BlueHat1
        CharacterCreationState == 3  =   Hair2Blue
        CharacterCreationState == 4  =   Hair2BlueHat1
        CharacterCreationState == 5  =   Hair1Pink
        CharacterCreationState == 6  =   Hair1PinkHat1
        CharacterCreationState == 7  =   Hair2Pink
        CharacterCreationState == 8  =   Hair2PinkHat1

        */

        int HairState = 1;
        int SkinState = 1;
        int HatState = 1;

        bool MakeNewCharacterWasClicked;
        public int CharacterCreationState;

        public BuildCharacter()
        {
            InitializeComponent();

        }

        private void MakeNewCharacter_Click(object sender, EventArgs e)
        {
            MakeNewCharacterWasClicked = true;
            Character.Image = Properties.Resources.Hair1Blue;
            CharacterCreationState = 1;

            CharacterNameEnter.Text = "Name your character";

            CharacterNameEnter.Visible = true;
            MakeNewCharacter.Visible = false;
            CharacterName.Visible = false;
            ConfirmName.Visible = true;

        }


        private void ConfirmName_Click(object sender, EventArgs e)
        {

            if (CharacterNameEnter.Text.Length > 10)
            {
                WarningNameLengh.Visible = true;
            }

            else
            {

                WarningNameLengh.Visible = false;

                CharacterName.Text = CharacterNameEnter.Text;

                CharacterNameEnter.Visible = false;
                MakeNewCharacter.Visible = true;
                CharacterName.Visible = true;
                ConfirmName.Visible = false;

                // Properties.Resources.text = something save data
            }

        }


        private void HairLeft_Click(object sender, EventArgs e)
        {
            if (HairState > 1)
            {
                HairState = 1;

                switch (CharacterCreationState)
                {
                    case 3:
                        {

                            Character.Image = Properties.Resources.Hair1Blue;
                            CharacterCreationState = 1;
                            break;
                        }

                    case 4:
                        {

                            Character.Image = Properties.Resources.Hair1BlueHat1;
                            CharacterCreationState = 2;
                            break;
                        }

                    case 7:
                        {

                            Character.Image = Properties.Resources.Hair1Pink;
                            CharacterCreationState = 5;
                            break;
                        }

                    case 8:
                        {

                            Character.Image = Properties.Resources.Hair1PinkHat1;
                            CharacterCreationState = 6;
                            break;
                        }
                }
            }
        }
    

        private void HairRight_Click(object sender, EventArgs e)
        {
            if (HairState < 2)
            {
                HairState = 2;

                switch (CharacterCreationState)
                {
                    case 1:
                        {

                            Character.Image = Properties.Resources.Hair2Blue;
                            CharacterCreationState = 3;
                            break;
                        }

                    case 2:
                        {

                            Character.Image = Properties.Resources.Hair2BlueHat1;
                            CharacterCreationState = 4;
                            break;
                        }

                    case 5:
                        {

                            Character.Image = Properties.Resources.Hair2Pink;
                            CharacterCreationState = 7;
                            break;
                        }

                    case 6:
                        {

                            Character.Image = Properties.Resources.Hair2PinkHat1;
                            CharacterCreationState = 8;
                            break;
                        }
                }
            }
        }

        private void HatLeft_Click(object sender, EventArgs e)
        {
            if (CharacterCreationState == 2)
            {

                Character.Image = Properties.Resources.Hair1Blue;
                CharacterCreationState = 1;

            }

            else if (CharacterCreationState == 4)
            {

                Character.Image = Properties.Resources.Hair2Blue;
                CharacterCreationState = 3;

            }

            else if (CharacterCreationState == 6)
            {

                Character.Image = Properties.Resources.Hair1Pink;
                CharacterCreationState = 5;

            }

            else if (CharacterCreationState == 8)
            {

                Character.Image = Properties.Resources.Hair2Pink;
                CharacterCreationState = 7;

            }
        }

        private void HatRight_Click(object sender, EventArgs e)
        {
            if (CharacterCreationState == 1)
            {

                Character.Image = Properties.Resources.Hair1BlueHat1;
                CharacterCreationState = 2;

            }

            else if (CharacterCreationState == 3)
            {

                Character.Image = Properties.Resources.Hair2BlueHat1;
                CharacterCreationState = 4;

            }

            else if (CharacterCreationState == 5)
            {

                Character.Image = Properties.Resources.Hair1PinkHat1;
                CharacterCreationState = 6;

            }

            else if (CharacterCreationState == 7)
            {

                Character.Image = Properties.Resources.Hair2PinkHat1;
                CharacterCreationState = 8;

            }
        }

        private void ColorRight_Click(object sender, EventArgs e)
        {
            if (CharacterCreationState == 1)
            {

                Character.Image = Properties.Resources.Hair1Pink;
                CharacterCreationState = 5;

            }

            else if (CharacterCreationState == 2)
            {

                Character.Image = Properties.Resources.Hair1PinkHat1;
                CharacterCreationState = 6;

            }

            else if (CharacterCreationState == 3)
            {

                Character.Image = Properties.Resources.Hair2Pink;
                CharacterCreationState = 7;

            }

            else if (CharacterCreationState == 4)
            {

                Character.Image = Properties.Resources.Hair2PinkHat1;
                CharacterCreationState = 8;

            }


        }

        private void ColorLeft_Click(object sender, EventArgs e)
        {
            if (CharacterCreationState == 5)
            {

                Character.Image = Properties.Resources.Hair1Blue;
                CharacterCreationState = 1;

            }

            else if (CharacterCreationState == 6)
            {

                Character.Image = Properties.Resources.Hair1BlueHat1;
                CharacterCreationState = 2;

            }

            else if (CharacterCreationState == 7)
            {

                Character.Image = Properties.Resources.Hair2Blue;
                CharacterCreationState = 3;

            }

            else if (CharacterCreationState == 8)
            {

                Character.Image = Properties.Resources.Hair2BlueHat1;
                CharacterCreationState = 4;

            }
        }

        private void BuildCharacter_Load(object sender, EventArgs e)
        {

        }

        private void Confirm_Click(object sender, EventArgs e)
        {

            if (CharacterName.Text == "" && MakeNewCharacterWasClicked == false)
            {
                WarningNoName.Text = "You are required to make a character before continuing!";
                WarningNoName.Visible = true;
            }



            else if (ConfirmName.Visible == true || WarningNameLengh.Visible == true || CharacterName.Text == "" && MakeNewCharacterWasClicked == true)
            {
                WarningNoName.Text = "Give your character a proper name before confirming!";
                WarningNoName.Visible = true;
            }

            else
            {
                // change SaveData.exe   and   load next screen

                /*(temporary thing):  */
                WarningNoName.Text = "Character created successfully!";
                WarningNoName.Visible = true;



                FirstScene_TextBaseStart NextScreen = new FirstScene_TextBaseStart();
                NextScreen.Show();
                Close();
                
            }




        }
    }
}
