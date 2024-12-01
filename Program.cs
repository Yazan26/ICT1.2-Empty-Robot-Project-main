using System;
using System.Threading;
using System.Device.Gpio;
using Avans.StatisticalRobot;
using SimpleMqtt;

class CleaningRobot
{
    private static GpioController _controller;
    private static Led _led5;
    
    private static Ultrasonic ultrasone;
    private static SimpleMqttClient _mqttClient;
    private const int Threshold = 500; // Drempelwaarde voor vervuilingsdetectie

    static void Main(string[] args)
    {
        _controller = new GpioController();
        _led5 = new Led(5);
        ultrasone = new Ultrasonic(6);
        _mqttClient = SimpleMqttClient.CreateSimpleMqttClientForHiveMQ("CleaningRobotClient");

        _mqttClient.OnMessageReceived += (sender, message) =>
        {
            Console.WriteLine($"Bericht ontvangen; topic={message.Topic}; message={message.Message};");
        };

        while (true)
        {
            int sensorValue = ultrasone.GetUltrasoneDistance();
            Console.WriteLine($"Sensor Value: {sensorValue}");

            if (sensorValue > Threshold)
            {
                Console.WriteLine("Vervuiling gedetecteerd!");
                _mqttClient.PublishMessage("Vervuiling gedetecteerd!", "cleaning/robot/status");
                StartCleaning();
            }
            else
            {
                Console.WriteLine("Geen vervuiling gedetecteerd.");
                _mqttClient.PublishMessage("Geen vervuiling gedetecteerd.", "cleaning/robot/status");
                StopCleaning();
            }

            Thread.Sleep(500);
        }
    }

    private static void StartCleaning()
    {
        Robot.Motors(1, 0); // Motoren aanzetten voor schoonmaakactie
        _led5.SetOn(); // LED aan om schoonmaak te starten
        _mqttClient.PublishMessage("Schoonmaak gestart.", "cleaning/robot/action");
        Thread.Sleep(1000); // Schrobben voor 1 seconde
        StopCleaning();
    }

    private static void StopCleaning()
    {
        Robot.Motors(0, 0); // Motoren uitzetten
        _led5.SetOff(); // LED uit om aan te geven dat schoonmaak klaar is
        _mqttClient.PublishMessage("Schoonmaak voltooid.", "cleaning/robot/action");
    }
}