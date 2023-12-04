using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.Write("Enter your name: ");
        string name = Console.ReadLine();

        Thread receiveThread = new Thread(ReceiveMessages);
        receiveThread.Start();

        while (true)
        {
            string message = Console.ReadLine();
            SendMessage(name + ": " + message);
        }
    }

    private static void ReceiveMessages()
    {
       
        TcpClient client = new TcpClient("localhost", 12345);
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];
        int bytesRead;

        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
        {
            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Received: " + message);
        }

        Console.WriteLine("Server disconnected");
        Environment.Exit(0);
    }

    private static void SendMessage(string message)
    {
        // Lưu nội dung chat vào tệp tin
    SaveChatToFile(message);
        using (TcpClient client = new TcpClient("localhost", 12345))
        {
            NetworkStream stream = client.GetStream();
            byte[] data = Encoding.UTF8.GetBytes(message);
            stream.Write(data, 0, data.Length);
        }
    }
    private static void SaveChatToFile(string message)
    {
        string filePath = "chat_log.txt";

        // Ghi nội dung chat vào tệp tin
        using (StreamWriter writer = File.AppendText(filePath))
        {
            writer.WriteLine(message);
        }
    }
}
