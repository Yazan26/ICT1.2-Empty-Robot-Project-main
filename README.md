# cleaning Robot project
robot that uses a raspberry Pi, 
arduino and sensors in order to drive around, 
clean and log data in a database.
uses **MQTT to send messages,
a DotNET Blazor website,
two ultrasonic sensors,
an LED+button, 
and a screen.**


the **blazor website** contains a control panel that sends **MQTT** messages to the robot in order for it to know which cleaningprograms to run.

the **ultrasonic sensors** are placed above each other
in order to avoid large objects and find small objects
when it finds a small object it stops and it only continues
if a person lets it continue by pressing the **button**
or using the **Blazor website control panel**.
the **screen** is used to display the battery percentage
