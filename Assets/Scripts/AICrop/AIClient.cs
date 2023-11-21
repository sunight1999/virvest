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
                Debug.Log("서버와의 통신이 불가능합니다. AI Client를 종료합니다.");
                return;
            }

            // 유니티 클라이언트 리스너 등록 요청
            Write("1");

            if (GetResultCode() < 0)
            {
                Debug.Log("AI Client를 종료합니다.");
                return;
            }

            Debug.Log("AI 서버와 성공적으로 연결되었습니다. 예측값 수신을 시작합니다.");

            // 서버에서 보내는 예측값 대기
            while (true)
            {
                string date = Read();
                int prediction = ReadInt();

                Debug.Log("날짜 : " + date + " 예측값 : " + prediction);
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

        Debug.Log("문자열(" + value + ") 길이 : " + bytes.Length);
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
