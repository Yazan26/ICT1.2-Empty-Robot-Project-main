using System;
using System.Threading.Tasks;
using HiveMQtt.Client;
using SimpleMqtt;
using HiveMQtt.Client.Events;
using Avans.StatisticalRobot;

class Program2
{
    static async Task Main(string[] args)
    {
        var mqttClient = SimpleMqttClient.CreateSimpleMqttClientForHiveMQ("2aa2fd8d");

        mqttClient.OnMessageReceived += OnMessageReceived;

        await mqttClient.SubscribeToTopic("robot/control");

        // Keep the application running
        Console.ReadLine();
    }

    private static void OnMessageReceived(object? sender, SimpleMqttMessage e)
    {
        Console.WriteLine($"Received message: {e.Message} on topic: {e.Topic}");

        // Handle control commands
        if (e.Message == "forward")
        {
            Robot.Motors(200, 180);
        }
        else if (e.Message == "backward")
        {
            Robot.Motors(-200, -180);
        }
        else if (e.Message == "left")
        {
            Robot.Motors(-200, 200);
        }
        else if (e.Message == "right")
        {
            Robot.Motors(200, -200);
        }
        else if (e.Message == "stop")
        {
            Robot.Motors(0, 0);
        }
    }
}