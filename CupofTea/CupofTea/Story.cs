using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CupofTea
{
    class Story
    {
        private const string Message1 = "You are really thirsty right now, need to make a Cup of tea!";
        private const string Success = "Success! You have a Cup of tea!";
        private const string Reset = "You have assembled and emptied all the parts again!";
        public string Boiled { get; set; }
        public bool AddBoiledMessage { get; set; }
        public string InHand { get; set; }
        public bool AddInHandMessage { get; set; }
        public string AddIngredient { get; set; }
        public bool AddAddIngredientMessage { get; set; }
        public string EmptyMessage { get; set; }
        public bool AddEmptyMessage { get; set; }
        public bool AddSuccessMessage { get; set; }
        public bool AddResetMessage { get; set; }
        public bool AddStirMessage { get; set; }
        public string StirMessage { get; set; }
        public string ResetStory(bool Reset)
        {
            StringBuilder BuildString = new StringBuilder();
            string Value;
            this.Boiled = "";
            this.AddBoiledMessage = false;
            this.InHand = "";
            this.AddInHandMessage = false;
            this.AddIngredient = "";
            this.AddAddIngredientMessage = false;
            this.EmptyMessage = "";
            this.AddEmptyMessage = false;
            this.AddSuccessMessage = false;
            this.AddResetMessage = Reset;
            this.AddStirMessage = false;
            this.StirMessage = "";


            BuildString.Append(this.UpdateStory());
            Value = BuildString.ToString();
            BuildString.Clear();
            return Value;

        }
    
        public string UpdateStory()
        {
            string Story;
            StringBuilder BuildString = new StringBuilder();

            if(this.AddSuccessMessage == false) { 
                BuildString.Append(Message1 + "\n");
                if (this.AddStirMessage == true)
                {
                    BuildString.Append(StirMessage);
                    this.AddStirMessage = false;
                }
                if (this.AddResetMessage == true)
                {
                    BuildString.Append(Reset + "\n");
                    this.AddResetMessage = false;
                }
                
                if(this.AddBoiledMessage == true)
                {
                    BuildString.Append(this.Boiled + "\n");
                    this.AddBoiledMessage = false;
                }
                if(this.AddInHandMessage == true)
                {
                    BuildString.Append(this.InHand + "\n");
                    this.AddInHandMessage = false;
                }
                if(this.AddAddIngredientMessage == true)
                {
                    BuildString.Append(this.AddIngredient + "\n");
                    this.AddAddIngredientMessage = false;
                }
                if(this.AddEmptyMessage == true)
                {
                    BuildString.Append(this.EmptyMessage + "\n");
                    this.AddEmptyMessage = false;
                }
            }
            else
            {
                BuildString.Append(Success);
                if (this.AddStirMessage == true)
                {
                    BuildString.Append(StirMessage);
                    this.AddStirMessage = false;
                }
            }
            Story = BuildString.ToString();
            BuildString.Clear();
            return Story;
        }

    }
}
