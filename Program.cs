using System; 
using System.Device.Gpio;
using Avans.StatisticalRobot;




class program {


static void Main(String[] args)
{
   Led led5 = new Led(5);
Button button6 = new Button(6);
Ultrasonic ultrasonic22 = new Ultrasonic(22);

while (true)
{
    var afstand = ultrasonic22.GetUltrasoneDistance();

    while (afstand > 10)
    {
        Robot.Motors(300, 300);
        afstand = ultrasonic22.GetUltrasoneDistance(); // Update the distance measurement
    }

    Robot.Motors(0, 0); // Stop the motors if the distance is 10 cm or less
}
}
}
    
        
    