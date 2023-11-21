using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class AIClient : MonoBehaviour
{
    public string aiServerHost;
    public int aiServerPort;

    Thread predictionReceiver;

    private TcpClient socket;
    NetworkStream stream;
    private byte[] buffer;
    private int bufferSize;

    void Start()
    {
        ConnectAIServer();
        predictionReceiver = new Thread(new ThreadStart(HandlePrediction));
        predictionReceiver.Start();
    }

    private void ConnectAIServer()
    {
        try
        {
            socket = new TcpClient(aiServerHost, aiServerPort);
            bufferSize = 1024;
            buffer = new byte[bufferSize];
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to connect AI Server " + e);
        }
    }

    private void HandlePrediction()
    {
        if (socket == null)
            return;

        try
        {
            stream = socket.GetStream();
            if (!stream.CanWrite || !stream.CanRead)
            {
                Debug.Log("�������� ����� �Ұ����մϴ�. AI Client�� �����մϴ�.");
                return;
            }

            // ����Ƽ Ŭ���̾�Ʈ ������ ��� ��û
            Write("1");

            if (GetResultCode() < 0)
            {
                Debug.Log("AI Client�� �����մϴ�.");
                return;
            }

            Debug.Log("AI ������ ���������� ����Ǿ����ϴ�. ������ ������ �����մϴ�.");

            // �������� ������ ������ ���
            while (true)
            {
                string date = Read();
                int prediction = ReadInt();

                Debug.Log("��¥ : " + date + " ������ : " + prediction);
            }
        }
        catch(SocketException e)
        {
            Debug.LogError("Failed to communicate " + e);
        }
        finally
        {
            stream.Close();
            socket.Close();
        }
    }

    private void ClearBuffer()
    {
        Array.Fill<byte>(buffer, 0);
    }

    private void Write(string value)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(value);

        Debug.Log("���ڿ�(" + value + ") ���� : " + bytes.Length);
        stream.Write(bytes, 0, bytes.Length);
    }

    private string Read()
    {
        ClearBuffer();
        
        stream.Read(buffer);
        return Encoding.UTF8.GetString(buffer);
    }

    private int ReadInt()
    {
        return int.Parse(Read());
    }

    private int GetResultCode()
    {
        stream.Read(buffer);
        int result = int.Parse(Encoding.UTF8.GetString(buffer));

        if (result < 0)
        {
            stream.Read(buffer);
            string message = Encoding.UTF8.GetString(buffer);
            Debug.LogError("Server Error Occurred(" + result + ") : " + message);
        }

        return result;
    }
}
