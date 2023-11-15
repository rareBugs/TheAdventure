using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TheAdventure
{
    internal class Game
    {
        //Logik som kommer användas i metoder för dialog och outcome för dialog
        bool hasHorse = false;
        bool hasRuneLongSword = false;
        bool hasDragonFireShield = false;
        bool exit = false;
        bool visitedKing = false;

        //skapar items här så det blir lätt att ha koll på dessa, anropar och lägger till i inventory när de behövs
        Item Horse = new Item("Horse");
        Item dragonFireShield = new Item("Dragonfire Shield");
        Item runeLongSword = new Item("Rune Longsword");

        //lista där spelare lägger till items som sen används för att printa ut listans innehåll
        List<Item> inventory = new List<Item>();


        public Game() 
        {
            //Jag är osäker på hur jag ska använda konstruktorn i det här fallet
            //Det hade gett mer mening om konsollprogrammet handlade om kanske spotify eller något liknande
            Console.WriteLine("A dragon has somehow kidnapped the King Roald's Daughter, Princess Godzilla, and taken her to its castle.\n\nThis is the tale of Heisenberg who has for some reason decided to save the princess.\nRelive the events that happened from Heisenberg's point of view!\n\n");
            Console.Write("Press enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }


        public void runGame()//spelet startar här och hämtar metoder från längre ned.
        {            
            Console.Write("Where would you like to go first?\n"); //printas endast första gången.

            while (!exit) //så länge input inte är exit
            {
                PrintMenu(); //printar en meny med olika val

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1" or "king" or "roald" or "king roald": //användaren kan ge olika inputs, siffra eller keyword
                        Console.Clear();
                        KingRoald();
                        break;
                    case "2" or "blacksmith":
                        Console.Clear();
                        Blacksmith();
                        break;
                    case "3" or "armory":
                        Console.Clear();
                        Armory();
                        break;
                    case "4" or "stables":
                        Console.Clear();
                        Stables();
                        break;
                    case "5" or "castle":
                        Console.Clear();
                        Castle();
                        break;
                    case "6" or "check" or "inventory" or "check inventory":
                        Console.Clear();
                        CheckInventory();
                        break;
                    case "7" or "exit" or "quit":
                        Console.Clear();
                        exit = true;
                        break;
                    default:
                        NiceTry(); //ger man inte "korrekt" input så får man ett meddelande och skickas tillbaka till menyn
                        break;
                }
            }
        }

        // Nedan metoder för spelet samt ovan meny.
        // De flesta av dialogen har skrivits på en rad för att inte de inte ska ta för mycket plats, och att fokuset ska ligga på koden.

        

        //metod som används när användare inte har gett rätt input. Felmeddelande + skicka tillbaka till menyn för nytt försök
        private void NiceTry() 
        {
            Console.Clear();
            Console.WriteLine("Princess Godzilla's life is in danger, there's no time for this!\nPlease enter a valid input next time.\n\n");
            Console.Write("Press enter to go back to the menu...");
            Console.ReadLine();
            Console.Clear();
        }



        //logiken för att gå vidare efter olika val, besök av ställen och andra händelser
        private void Continue() 
        {
            Console.Write("\nPress enter to continue...");
            Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Where would you like to go next?\n");
        }



        // If else logik som kontrollerar antalet Items man har samlat på sig.
        // Har man 0 items får man ett meddelande, annars printas varje item ut genom en foreach loop.
        private void CheckInventory() 
        {
            Console.WriteLine("You take a moment to check what you have so far.");
            Console.WriteLine("You have: ");
            if (inventory.Count == 0)
            {
                Console.WriteLine("Oh dear, you have nothing.");
            }
            else
            {
                foreach (Item item in inventory)
                {
                    Console.WriteLine("- " + item.itemName);
                }
            }
            Continue();
        }



        // Nedan kollar om användaren har redan varit här och hanterar olika scenarion. 
        private void KingRoald() 
        {
            if (!visitedKing)
            {
                Console.WriteLine("You approach the king. He seem to be relieved to see you.\nHeisenberg! Thank Trafikverket you are here!\n\nI have sent out word to the blacksmith, armorer and stablemaster to have equipment ready for you.\nThe king gives you some directions to the castle where the dragon resides.\nMake sure to get equipped before you leave!\n\nPlease hurry and save my daughter!\n");
                visitedKing = true;
                Continue();
            }
            else
            {
                Console.WriteLine("The king is too worried and busy to talk to you.");
                Continue();
            }
        }



        // Nedan kollar om användaren har uppfyllt krav, behöver uppfylla krav, hämta objekt och lägga till lista samt olika scenarion.
        private void Blacksmith() 
        {
            if (!visitedKing) //om man inte pratat med kungen först
            {
                Console.WriteLine("You try to get in to the smith, but it's too crowded. The smith doesn't even look at you.\nYou decide to leave.\n");
                Continue();
            }
            else //om man pratat med kungen innan
            {
                if (!hasRuneLongSword) //om man inte redan har item som kan fås här
                {
                    Console.WriteLine("You arrive at the blacksmiths shop. There are a lot of people in line.\nYou check your smart scroll for fair maidens while you wait.\nIt's finally your turn.\nThe blacksmith hands you a newly sharpened Rune Longsword!");
                    inventory.Add(runeLongSword); //item läggs till i inventory
                    hasRuneLongSword = true; //man kan inte komma tillbaka hit igen
                    Continue();
                }
                else
                {
                    Console.WriteLine("The smith looks very busy and you already have your sword.\nPerhaps you should move on?");
                    Continue();
                }
            }
        }



        // Nedan kollar om användaren har uppfyllt krav, behöver uppfylla krav, hämta objekt och lägga till lista samt olika scenarion.
        private void Armory() 
        {
            if (!visitedKing) //om man inte pratat med kungen först
            {
                Console.WriteLine("I don't know what I'm looking for...");
                Continue();
            }
            else //om man pratat med kungen innan
            {
                if (!hasDragonFireShield) //om man inte redan har item som kan fås här
                {
                    Console.WriteLine("You finally find the entrance to the armory, there seem to be a knight waiting for you outside.\nHe hands you a shiny Dragonfire shield\n''It has the magical ability to absorb dragonfire!'' The knight says\n\nYou take the shield and leave.");
                    inventory.Add(dragonFireShield); //item läggs till i inventory
                    hasDragonFireShield = true; //man kan inte komma tillbaka hit igen
                    Continue();
                }
                else //om man redan har item som man kan få här
                {
                    Console.WriteLine("You try to find the entrance to the armory again. It's not easy.\nMaybe you should move on?");
                    Continue();
                }
            }         
        }


        // Nedan kollar om användaren har uppfyllt krav, behöver uppfylla krav, hämta objekt och lägga till lista samt olika scenarion.
        private void Stables() 
        {
            if (!visitedKing) //om man inte pratat med kungen först
            {
                Console.WriteLine("The stable master is too busy yodeling to even notice you.");
                Continue();
            }
            else //om man pratat med kungen innan
            {
                if (!hasHorse) //om man inte redan har item som kan fås här
                {
                    Console.WriteLine("You walk around looking for the stable master.\nSuddenly you hear someone shouting\nOy! You must be Sir Heisenberg! I've just the right horse for you!");                   
                    inventory.Add(Horse); //item läggs till i inventory
                    hasHorse = true; //man kan inte komma tillbaka hit igen
                    Continue();
                }
                else //om man redan har item som man kan få här
                {
                    Console.WriteLine("You ride your horse back to the stables.\nIt's empty here, the stable master must've given away all the horses and left...");
                    Continue();
                }
            }
        }



        //logiken för slutet av spelet, hanterar olika scenarion beroende på vilka objekt användaren har i sin inventory list
        //spelet ska avsluta utan förklaring om man saknar objekt och väljer att attackera draken
        private void Castle()
        {
            if (!visitedKing) //om man inte pratat med kungen först
            {
                Console.WriteLine("You don't even know what direction to go...\nPerhaps you should speak to King Roald?");
                Continue();
            }
            else // om man pratat med kungen innan
            {

                Console.WriteLine("You arrive at the castle after a long journey. The palce looks very dark and smoky.\nYou enter the castle gates and lock eyes with the dragon...\n");
                Console.Write("\n\nPress enter to continue...");
                Console.ReadLine();
                Console.Clear();

                if (hasDragonFireShield && hasRuneLongSword && hasHorse)
                {
                    // Om användaren har alla tre (ovan) objekt
                    Console.WriteLine("You are fully equipped, you feel confident! You charge the dragon, and slay him it the spot.\n\nYou run over to the tower and rescue the princess and return to King Roald for your reward.\nYour reward is a pizza with pineapple....\n\nWell done!\n\nTHE END\n");
                    exit = true;
                }

                else if (inventory.Count >= 1 && inventory.Count <= 3)
                {
                    // Om användaren har lägre än 3 objekt i inventory
                    Console.WriteLine("You tried your best and failed. The dragon cooks you for dinner.\nBetter luck next time!\n\n");
                    exit = true;
                }

                else if (inventory.Count == 0) //om inventory är tom får man val
                {
                    Console.WriteLine("You arrive at the castle, the dragon greets you at the courtyard.\nYou know you are ill equipped. What will you do?\n");

                    while (true) // Loopar tillbaka till val om användaren knappar in fel val vid valmöjlighet
                    {

                        Console.WriteLine("1. Attempt to attack anyway (not a very good idea)");
                        Console.WriteLine("2. Try to talk to it. Dragons are intelligent creatures, right?\n");
                        Console.Write("Enter number: ");

                        try
                        {
                            int choice = int.Parse(Console.ReadLine());

                            if (choice == 1)
                            {
                                // Spelet avslutas utan förklaring
                                exit = true;
                                break;
                            }
                            else if (choice == 2)
                            {
                                // Spelet får bra slut
                                Console.Clear();
                                Console.WriteLine("You have a long conversation with the dragon. Turns out her name is Elvarg and was feeling lonely.\nShe only grabbed the princess to have someone to talk to\nShe understands that the king is worried and let's the Princess leave with you!\n\nWell done!\n\nTHE END\n");
                                exit = true;
                                break;
                            }
                            else
                            {
                                // Om användaren knappar in annan siffra än 1 eller 2
                                Console.Clear();
                                Console.WriteLine("There are only two options, please enter 1 or 2.\n");
                            }
                        }
                        catch (FormatException)
                        {
                            // Om användaren knappar in något som inte är siffror
                            Console.Clear();
                            Console.WriteLine("Try that again? Your options are numbers 1 or 2.\n");
                        }
                    }
                }
            }
        }


        private void PrintMenu()
        {
            Console.WriteLine("----------------------------");
            Console.WriteLine("|  1. Go to King Roald     |");
            Console.WriteLine("|                          |");
            Console.WriteLine("|  2. Visit the Blacksmith |");
            Console.WriteLine("|                          |");
            Console.WriteLine("|  3. Visit the Armory     |");
            Console.WriteLine("|                          |");
            Console.WriteLine("|  4. Visit the Stables    |");
            Console.WriteLine("|                          |");
            Console.WriteLine("|  5. Go to the Castle     |");
            Console.WriteLine("|                          |");
            Console.WriteLine("|  6. Check inventory      |");
            Console.WriteLine("|                          |");
            Console.WriteLine("|  7. Exit Game            |");
            Console.WriteLine("----------------------------");
            Console.Write("Enter option: ");
        }
    }
}