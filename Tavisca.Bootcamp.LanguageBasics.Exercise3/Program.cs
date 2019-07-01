using System;
using System.Linq;
using System.Collections.Generic;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 }, 
                new[] { 2, 8 }, 
                new[] { 5, 2 }, 
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" }, 
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 }, 
                new[] { 2, 8, 5, 1 }, 
                new[] { 5, 2, 4, 4 }, 
                new[] { "tFc", "tF", "Ftc" }, 
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 }, 
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 }, 
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 }, 
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" }, 
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }

        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            // Add your code here.
            //To keep track of the meals where the initial nutrients are trackIndexesOfSameNutrients
            List<int> trackIndexesOfSameNutrients = new List<int>();
            int foodItemsLength = protein.Length;
            //To keep track of the characters remaining to parse in each dietPlan[i]
            int letterLength;
            
            int [] calories = new int[foodItemsLength];
            int [] mealPlan = new int[dietPlans.Length];
            
            //Calculating Calories
            for(int i=0;i<foodItemsLength;i++)
            {
                calories[i] = 5*protein[i] + 5*carbs[i] + 9*fat[i];
            }

            for(int i=0;i<dietPlans.Length;i++)
            {
                trackIndexesOfSameNutrients.Clear();
                for(int j=0;j<foodItemsLength;j++)
                    trackIndexesOfSameNutrients.Add(j);
                letterLength = dietPlans[i].Length;
                foreach(var letter in dietPlans[i])
                {
                    
                    switch(letter)
                    {
                        case 'c':
                            mealPlan[i] = GetIndex(letter,carbs,ref trackIndexesOfSameNutrients,letterLength);
                            break;
                        case 'C':
                            mealPlan[i] = GetIndex(letter,carbs,ref trackIndexesOfSameNutrients,letterLength);
                            break;
                        case 'p':
                            mealPlan[i] = GetIndex(letter,protein,ref trackIndexesOfSameNutrients,letterLength);
                            break;
                        case 'P':
                            mealPlan[i] = GetIndex(letter,protein,ref trackIndexesOfSameNutrients,letterLength);
                            break;
                        case 't':
                            mealPlan[i] = GetIndex(letter,calories,ref trackIndexesOfSameNutrients,letterLength);
                            break;
                        case 'T':
                            mealPlan[i] = GetIndex(letter,calories,ref trackIndexesOfSameNutrients,letterLength);
                            break;
                        case 'f':
                            mealPlan[i] = GetIndex(letter,fat,ref trackIndexesOfSameNutrients,letterLength);
                            break;
                        case 'F':
                            mealPlan[i] = GetIndex(letter,fat,ref trackIndexesOfSameNutrients,letterLength);
                            break;
                        default:
                            mealPlan[i] = 0;
                            break;
                    }
                   
                    if(mealPlan[i] != -1)
                        break;
                    letterLength -= 1;
                }
            }

            return mealPlan;


        }

        private static int GetIndex(char letter,int[] nutrient,ref List<int> trackIndexesOfSameNutrients,int letterLength)
        {
            List<int> temp = new List<int>();
            int index=0,max,min;
            
            //Max nutrient value for uppercase nutrients
            if(char.IsUpper(letter))
            {
    
                max = int.MinValue;
                foreach(var i in trackIndexesOfSameNutrients)
                {
                    if(nutrient[i]>max)
                    {
                        max = nutrient[i];
                        index = i;
                    }
            
                }
                //trackIndexesOfSameNutrients.Clear();

                 foreach(var j in trackIndexesOfSameNutrients)
                {
                    if(nutrient[j] == max)
                    {
                        temp.Add(j);

                    }
                }
                trackIndexesOfSameNutrients = temp;

            }
            //Min nutrient value for lowercase nutrients
            else
            {
                min = int.MaxValue;
                foreach(var i in trackIndexesOfSameNutrients)
                {
                    if(nutrient[i]<min)
                    {
                        min = nutrient[i];
                        index = i;
                    }
                    //trackIndexesOfSameNutrients.Clear();
                }

                     foreach(var j in trackIndexesOfSameNutrients)
                    {
                        if(nutrient[j] == min)
                            temp.Add(j);
                    }

                   trackIndexesOfSameNutrients = temp;     
            }

        
              
            if(trackIndexesOfSameNutrients.Count > 1 && letterLength != 1)
                return -1;
            return index;
    }
  }

}
