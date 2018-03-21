using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace golf
{
    class Program
    {
        /// <summary>
        /// This is a function that prints out a message and the distance for every swing. 
        /// </summary>
        /// <param name="scoreCard is a array of type double."></param>
        /// <param name="numberOfSwings tells how many rows of distances to be printed."></param>
        /// <param name="endingMessage is a message from main program that is also to be printed"></param>
        static void printScoreCard(double[] scoreCard, int numberOfSwings, string endingMessage)
        {
            Console.WriteLine("\n{0}\nYour swings was as follow:", endingMessage);
            for (int i = 1; i <= numberOfSwings; i++)
            { Console.WriteLine("Swing nr: {0}\t{1:0.00}", i, scoreCard[i - 1]); }
        }


        /// <summary>
        /// This function checks the input to be numbers and not to be greater than
        /// maxValue or less than minValue
        /// </summary>
        /// <param name="minValue is the lowest value the recieved input should be"></param>
        /// <param name="maxValue is the highest value the recived input should be"></param>
        /// <returns></returns>
        static double checkValues(double minValue, double maxValue)
        {
            double inputValue = 0;
            try
            { inputValue = double.Parse(Console.ReadLine()); }
            catch (FormatException)
            { Console.WriteLine("Please enter a number instead of text!"); }
            if (inputValue <= minValue)
            { Console.WriteLine("You typed in a too low value. Please try a greater value than: {0}", minValue); }
            if (inputValue >= maxValue)
            { Console.WriteLine("You typed in a too high value. Please try a lower value than: {0}", maxValue); }
            return inputValue;
        }

        static void Main(string[] args)
        {
            //initializing the variables.
            double hittingAngle = 0, hittingVelocity = 0, hittingDistance, golfCourseDistance, cupHoleDiameter = 0.10, greenDistance;
            Random courseDistance = new Random();
            string acceptingInputValues = "n";
            double[] hittingDistanceTable = new double[10];
            bool reachedGreen = false;

            golfCourseDistance = Convert.ToDouble(courseDistance.Next(200, 900));//assign the length of the course and number of swings.
            int swing = 1, maxSwing = Convert.ToInt32(golfCourseDistance / 100) + 1; //assign the first swing and maxSwing
            greenDistance = golfCourseDistance / 10;//assign the length of the green and the size of the cup.
            Console.WriteLine("Welcome to this Golf Game!"); //Here is Welcome message and rules stated.
            Console.WriteLine("The aim is to land a golfball in the cup, this you do by enter a angle and the velocity of your swing. ");
            Console.WriteLine("You then get notified how long distance your swing went and how much more your need to get to the cup.");
            Console.WriteLine("Each course is randomly generated and depending on distance you have varying number of swings. ");
            Console.WriteLine("When you reach the green you have to hit the ball genty, or else the ball have went missing.");
            Console.WriteLine("The golf course is {0:0.00} meter long and you have max {1} swings.", golfCourseDistance, maxSwing);
            do  //For every swing, the following should be executed
            {
                do  //In this loop, the user is presented with the supplied values and is asked to confirm these.
                {
                    Console.WriteLine("\nThis is swingnumber: {0}", swing);
                    do // iteration statement that checks that a legal angle is given
                    {
                        Console.Write("What angle would you like to use when hitting the ball?(Only a value of 1-89 is acceptable.): ");
                        hittingAngle = checkValues(0, 90);
                    }
                    while (hittingAngle >= 90 || hittingAngle <= 0);
                    do  //iteration statement that checks a legal speed is given
                    {
                        Console.Write("What speed would you like to hit the ball with?(Minimum speed is 1m/s and maximun speed is 100m/s: ");
                        hittingVelocity = checkValues(0, 101);
                    }
                    while ((hittingVelocity >= 101) || (hittingVelocity <= 0));
                    do  //This loop is to present the values and get a confirmation that they are the intended.
                    {
                        Console.WriteLine("You will hit the ball at {0} degree angle and with the speed of {1} m/s.\nIs this correct (y/n): ", hittingAngle, hittingVelocity);
                        acceptingInputValues = Console.ReadLine();
                        if (acceptingInputValues != "y" && acceptingInputValues != "n")
                        { Console.WriteLine("Your answear was not expected. Please answear 'y' for yes or 'n' for no."); }
                    }
                    while ((acceptingInputValues != "y") && (acceptingInputValues != "n") && (acceptingInputValues != "Y") && (acceptingInputValues != "N"));
                }
                while (acceptingInputValues == "n");
                //Calculate angle in radius and how far the ball flies. Also notify the user of this distance.
                Console.WriteLine("Distance the ball is travelling is: {0:0.00}", hittingDistance = Math.Pow(hittingVelocity, 2) / 9.8 * (Math.Sin(2 * (Math.PI / 180) * hittingAngle)));
                //store the value of the distance that was hit in an array.
                hittingDistanceTable[swing - 1] = hittingDistance;
                // check if remaining distance is shorter that the hit distance
                //and that remaining distance is greater than diameter of the cup hole.
                //If hit distance is longer than remaining distance, handle the negative distance.
                if ((golfCourseDistance > cupHoleDiameter) && (golfCourseDistance < hittingDistance))
                { golfCourseDistance = (golfCourseDistance - hittingDistance) * -1; }
                else //if remaining distance is longer than the hit distance, make a subtraction.
                { golfCourseDistance = golfCourseDistance - hittingDistance; }
                Console.WriteLine("Remaining distance to go: {0:0.00}", golfCourseDistance);
                //If the remaining distance is less that 10%, then you have landed on the green.
                if (golfCourseDistance < greenDistance && golfCourseDistance > cupHoleDiameter && reachedGreen == false)
                { reachedGreen = true; }
                //If the remaining distance is greater than the greendistance and you have 
                //previously been within the greendistance then the user have hit the boll to 
                //hard and sent it missing. 
                else if ((golfCourseDistance + hittingDistance) > greenDistance && reachedGreen == true && swing <= maxSwing)
                {
                    printScoreCard(hittingDistanceTable, swing, "Oh no!! you have shot the ball of the course!! Better luck next time!");
                    swing = maxSwing+1; //This statement is to be able to exit the program
                    Console.ReadKey();
                }
                //if the ball lands within a certain distance from zero, it is considered to have
                //reached the cup. Number of swings have to be less or equal to max swings allowed.
                if (golfCourseDistance <= cupHoleDiameter && swing <= maxSwing)
                {
                    //go to the function that prints the scorecard.
                    printScoreCard(hittingDistanceTable, swing, "Congratulations!! You have reached the cup!");
                    swing = maxSwing + 1; //This statement is to be able to exit the program
                    Console.ReadKey();
                }
                // If user has not reached the cup and have made a number of swings equal 
                //to the max specified then no more swings is allowed. 
                else if (golfCourseDistance > cupHoleDiameter && swing == maxSwing)
                {
                    //go to the function that prints the scorecard
                    printScoreCard(hittingDistanceTable, swing, "Oh no!! This was your last swing and you did not reach the cup!! Better luck next time!");
                    swing = maxSwing + 1; //This statement is to be able to exit the program
                    Console.ReadKey();
                }
                swing++;
            }
            while (swing <= maxSwing);
        }
    }
}