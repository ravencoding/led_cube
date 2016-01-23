using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class arduino_Serial_controller : MonoBehaviour {
	private SerialPort serialPort_;
	public string portName = "/dev/cu.usbmodem1421";
	public int baudRate = 9600;
	private bool isRunning_ = false;
	private Thread thread_;
	private bool isNewMessageReceived_ = false;
	private string message_;

	public delegate void SerialDataReceivedEventHandler(string message);
	public event SerialDataReceivedEventHandler OnDataReceived;

	void Awake(){
		Open ();
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (isNewMessageReceived_) {
			isNewMessageReceived_ = false;
			OnDataReceived(message_);// message_という引数を委譲先メソッドに渡す.
		}
	}
	
	void OnDestroy(){
		Close ();
	}

	private void Open(){
		serialPort_ = new SerialPort (portName, baudRate, Parity.None, 8, StopBits.One);
		serialPort_.Open ();
		isRunning_ = true;

		thread_ = new Thread (Read);
		thread_.Start ();
	}

	private void Read(){
		while (isRunning_ && serialPort_ != null && serialPort_.IsOpen) {
			try{
				if(serialPort_.BytesToRead > 0){
					message_ = serialPort_.ReadLine();
					isNewMessageReceived_ = true;
				}
			}catch(System.Exception e){
				Debug.LogWarning (e.Message);
			}
		}
	}

	private void Close(){
		isRunning_ = false;
		if (thread_ != null && thread_.IsAlive) {
			thread_.Join();
		}

		if (serialPort_ != null && serialPort_.IsOpen) {
			serialPort_.Close ();
			serialPort_.Dispose ();
		}
	}

	public void Write(string message){
		try{
			serialPort_.Write (message);
		}catch(System.Exception e){
			Debug.LogWarning (e.Message);
		}
	}
}
