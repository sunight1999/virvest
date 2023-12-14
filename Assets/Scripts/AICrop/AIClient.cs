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
        // 30초마다 AI 서버 연결 시도
        while (!ConnectAIServer())
        {
            yield return new WaitForSeconds(30f);
        }

        // AI 서버와 연결에 성공하면 예측값을 수신하는 쓰레드 생성
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
                Debug.Log("서버와의 통신이 불가능합니다. AI Client를 종료합니다.");
                return;
            }

            // 유니티 클라이언트 리스너 등록 요청
            Write64("1");

            if (GetResultCode() < 0)
            {
                Debug.Log("AI Client를 종료합니다.");
                return;
            }

            Debug.Log("AI 서버와 성공적으로 연결되었습니다. 예측값 수신을 시작합니다.");

            // 서버에서 보내는 예측값 대기
            while (true)
            {
                int datelen = ReadInt();
                string date = Read(datelen);
                int prediction = ReadInt();

                Debug.Log("예측값 : " + prediction);

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