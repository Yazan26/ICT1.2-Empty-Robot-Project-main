using System;
using System.Device.Gpio;
using Avans.StatisticalRobot;
using SimpleMqtt;

string clientnum = "1234";	// clientnummer

var client = SimpleMqttClient.CreateSimpleMqttClientForHiveMQ(clientnum);
client.OnMessageReceived += (sender, args) =>
{
    Console.WriteLine($"Bericht ontvangen; topic={args.Topic}; message={args.Message};");
};

client.SubscribeToTopic("testMQtt");
client.PublishMessage("the quick brown fox jumps over the lazy dog 0123456789", "testMQtt");
