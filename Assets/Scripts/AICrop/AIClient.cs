using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class AIClient : SingletonMono<AIClient>
{
    public string aiServerHost;
    public int aiServerPort;

    public delegate void DispatchPrediction(GreenHouseInfo greenHouseInfo);
    public DispatchPrediction dispatchPrediction;

    Thread predictionReceiver;

    private TcpClient socket;
    private NetworkStream stream;
    private byte[] buffer;
    private int bufferSize;

    protected override void Awake()
    {
        base.Awake();

        StartCoroutine(BeginAIClient());
    }

    private IEnumerator BeginAIClient()
    {
        // 30�ʸ��� AI ���� ���� �õ�
        while (!ConnectAIServer())
        {
            yield return new WaitForSeconds(30f);
        }

        // AI ������ ���ῡ �����ϸ� �������� �����ϴ� ������ ����
        predictionReceiver = new Thread(new ThreadStart(ReceivePrediction));
        predictionReceiver.Start();
    }

    private bool ConnectAIServer()
    {
        try
        {
            socket = new TcpClient(aiServerHost, aiServerPort);
            bufferSize = 1024;
            buffer = new byte[bufferSize];
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to connect AI Server " + e);
            return false;
        }
    }

    private void ReceivePrediction()
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
            Write64("1");

            if (GetResultCode() < 0)
            {
                Debug.Log("AI Client�� �����մϴ�.");
                return;
            }

            Debug.Log("AI ������ ���������� ����Ǿ����ϴ�. ������ ������ �����մϴ�.");

            // �������� ������ ������ ���
            while (true)
            {
                int datelen = ReadInt();
                string date = Read(datelen);
                int prediction = ReadInt();

                Debug.Log("������ : " + prediction);

                dispatchPrediction(new GreenHouseInfo(date, prediction));
            }
        }
        catch (SocketException e)
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
        Array.Fill<byte>(buffer, (byte)0x20);
    }

    private void Write(string value)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(value);

        stream.Write(bytes, 0, bytes.Length);
    }

    private void Write64(string value)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(value.PadLeft(64));

        stream.Write(bytes, 0, 64);
    }

    private string Read(int size = 0)
    {
        ClearBuffer();

        if (size == 0)
            stream.Read(buffer);
        else
            stream.Read(buffer, 0, size);

        return Encoding.UTF8.GetString(buffer);
    }

    private int ReadInt()
    {
        return int.Parse(Read(64));
    }

    private float ReadFloat()
    {
        return float.Parse(Read(64));
    }

    private int GetResultCode()
    {
        int result = ReadInt();

        if (result < 0)
        {
            stream.Read(buffer);
            string message = Encoding.UTF8.GetString(buffer);
            Debug.LogError("Server Error Occurred(" + result + ") : " + message);
        }

        return result;
    }
}