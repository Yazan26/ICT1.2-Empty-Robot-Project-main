using System; 
using System.Device.Gpio;
using Avans.StatisticalRobot;


class Program {


static void Main(String[] args)
{
   Led led5 = new Led(5);
Button button6 = new Button(6);
Ultrasonic ultrasonic22 = new Ultrasonic(22);

while (true)
{
    try
    {
        var afstand = ultrasonic22.GetUltrasoneDistance();

        if (afstand <= 20) // Turn around if the distance is 20 cm or less
        {
            // Stop the motors
            Robot.Motors(0, 0);
            // Add a small delay before turning
            Robot.Wait(500);
            // Turn around (e.g., rotate in place)
            Robot.Motors(-200, 100); // One motor forward, one motor backward
            Robot.Wait(1000); // Adjust the duration to complete the turn
            // Stop the motors after turning
            Robot.Motors(0, 0);
        }
        else
        {
            Robot.Motors(200, 200); // Continue moving forward
        }
    }
    catch (System.IO.IOException ex)
    {
        Console.WriteLine($"I2C communication error: {ex.Message}");
        // Fallback behavior: stop the motors to prevent potential damage
        Robot.Motors(0, 0);
        // Optionally, wait for a short period before retrying
        System.Threading.Thread.Sleep(1000);
    }

    Robot.Wait(100); // Add a small delay to avoid excessive polling
}
}
}
